#region MIT License

/*
 * Copyright (c) 2009 Kristopher Baker (ao@yukiyume.net)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

#endregion

#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using YukiYume.GitHub.Configuration;
using YukiYume.Json;
using System.Collections.Specialized;

#endregion

namespace YukiYume.GitHub.Json
{
    public class JsonIssueRepository : BaseRepository, IIssueRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonUserRepository));

        public JsonIssueRepository()
            : base(FormatType.Json)
        {
        }

        public JsonIssueRepository(string gitHubUserName, string gitHubApiToken)
            : base(FormatType.Json, gitHubUserName, gitHubApiToken)
        {
        }

        public virtual IEnumerable<Issue> Search(string userName, string repositoryName, IssueStateType issueState, string searchTerm)
        {
            ValidateSearchArguments(userName, repositoryName, issueState, searchTerm);

            var action = string.Format("issues/search/{0}/{1}/{2}/{3}", userName, repositoryName, issueState == IssueStateType.Open ? "open" : "closed", searchTerm);
            var data = Client.Download(action);

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<IssuesContainer>(data).Issues
                       : new List<Issue>();
        }

        public virtual IEnumerable<Issue> List(string userName, string repositoryName, IssueStateType issueState)
        {
            ValidateListArguments(userName, repositoryName, issueState);

            var action = string.Format("issues/list/{0}/{1}/{2}", userName, repositoryName, issueState == IssueStateType.Open ? "open" : "closed");
            var data = Client.Download(action);

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<IssuesContainer>(data).Issues
                       : new List<Issue>();
        }

        public virtual Issue Get(string userName, string repositoryName, int number)
        {
            ValidateGetArguments(userName, repositoryName, number);

            var action = string.Format("issues/show/{0}/{1}/{2}", userName, repositoryName, number);
            var data = Client.Download(action);

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<IssueContainer>(data).Issue
                       : null;
        }

        public virtual Issue Open(string userName, string repositoryName, string title, string body)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            ValidateTitleBody(title, body);

            var action = string.Format("issues/open/{0}/{1}", userName, repositoryName);
            var data = Client.Download(action, true, new NameValueCollection { { "title", title }, { "body", body } });

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<IssueContainer>(data).Issue
                       : null;
        }

        public virtual Issue ReOpen(string userName, string repositoryName, int number)
        {
            return CloseOrReOpen(userName, repositoryName, number, "reopen");
        }

        public virtual Issue Close(string userName, string repositoryName, int number)
        {
            return CloseOrReOpen(userName, repositoryName, number, "close");
        }

        protected Issue CloseOrReOpen(string userName, string repositoryName, int number, string method)
        {
            ValidateGetArguments(userName, repositoryName, number);

            var action = string.Format("issues/{3}/{0}/{1}/{2}", userName, repositoryName, number, method);
            var data = Client.Download(action, true);

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<IssueContainer>(data).Issue
                       : null;
        }

        public virtual Issue Edit(string userName, string repositoryName, int number, string title, string body)
        {
            ValidateGetArguments(userName, repositoryName, number);
            ValidateTitleBody(title, body);

            var action = string.Format("issues/edit/{0}/{1}/{2}", userName, repositoryName, number);
            var data = Client.Download(action, true, new NameValueCollection { { "title", title }, { "body", body } });

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<IssueContainer>(data).Issue
                       : null;
        }

        public virtual IEnumerable<string> GetLabels(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var action = string.Format("issues/labels/{0}/{1}", userName, repositoryName);
            var data = Client.Download(action, true);

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<LabelsContainer>(data).Labels
                       : new List<string>();
        }

        public virtual IEnumerable<string> AddLabel(string userName, string repositoryName, string label, int number)
        {
            return AddRemoveLabel(userName, repositoryName, label, number, "add");
        }

        private IEnumerable<string> AddRemoveLabel(string userName, string repositoryName, string label, int number, string method)
        {
            ValidateLabel(userName, repositoryName, label, number);

            var action = string.Format("issues/label/{4}/{0}/{1}/{2}/{3}", userName, repositoryName, label, number, method);
            var data = Client.Download(action, true);

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<LabelsContainer>(data).Labels
                       : new List<string>();
        }

        public virtual IEnumerable<string> RemoveLabel(string userName, string repositoryName, string label, int number)
        {
            return AddRemoveLabel(userName, repositoryName, label, number, "remove");
        }

        public virtual Comment AddComment(string userName, string repositoryName, int number, string comment)
        {
            ValidateGetArguments(userName, repositoryName, number);

            if (comment == null)
                throw new ArgumentNullException("comment");

            if (comment.Length == 0)
                throw new ArgumentException("comment cannot be empty", "comment");

            var action = string.Format("issues/comment/{0}/{1}/{2}", userName, repositoryName, number);
            var data = Client.Download(action, true, new NameValueCollection { { "comment", comment } });

            if (Log.IsDebugEnabled)
                Log.Debug(data);

            return !string.IsNullOrEmpty(data)
                       ? JsonDeserializer.Deserialize<CommentContainer>(data).Comment
                       : null;
        }

        #region Validation

        protected static void ValidateLabel(string userName, string repositoryName, string label, int number)
        {
            ValidateGetArguments(userName, repositoryName, number);
            Validation.ValidateStringArgument(label, "label");
        }

        protected static void ValidateSearchArguments(string userName, string repositoryName, IssueStateType issueState, string searchTerm)
        {
            ValidateListArguments(userName, repositoryName, issueState);
            Validation.ValidateStringArgument(searchTerm, "searchTerm");
        }

        protected static void ValidateListArguments(string userName, string repositoryName, IssueStateType issueState)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            Validation.ValidateArgument(issueState, arg => arg == IssueStateType.Unknown, "issueState cannot be Unknown", "issueState");
        }

        protected static void ValidateGetArguments(string userName, string repositoryName, int number)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            Validation.ValidateArgument(number, arg => arg <= 0, "number must be greater than 0", "number");
        }

        protected static void ValidateTitleBody(string title, string body)
        {
            Validation.ValidateStringArgument(title, "title");
            Validation.ValidateStringArgument(body, "body");
        }

        #endregion

        #region protected classes

        protected class CommentContainer
        {
            [JsonName("comment")]
            public Comment Comment { get; set; }
        }

        protected class LabelsContainer
        {
            [JsonName("labels")]
            public List<string> Labels { get; set; }
        }

        protected class IssuesContainer
        {
            [JsonName("issues")]
            public List<Issue> Issues { get; set; }
        }

        protected class IssueContainer
        {
            [JsonName("issue")]
            public Issue Issue { get; set; }
        }

        #endregion
    }
}
