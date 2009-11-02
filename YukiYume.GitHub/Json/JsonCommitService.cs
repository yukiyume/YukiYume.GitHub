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
using YukiYume.Json;

#endregion

namespace YukiYume.GitHub.Json
{
    /// <summary>
    /// JSON implementation of ICommitService
    /// </summary>
    public class JsonCommitService : BaseService, ICommitService
    {
        public JsonCommitService()
            : base(FormatType.Json)
        {
        }

        public JsonCommitService(string gitHubUserName, string gitHubApiToken)
            : base(FormatType.Json, gitHubUserName, gitHubApiToken)
        {
        }

        public virtual IEnumerable<Commit> List(string userName, string repositoryName, string branchName)
        {
            ValidateList(userName, repositoryName, branchName);

            var data = Client.Download(string.Format("commits/list/{0}/{1}/{2}", userName, repositoryName, branchName));

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<CommitsContainer>(data).Commits :
                new List<Commit>();
        }

        public virtual IEnumerable<Commit> List(User user, Repository repository, string branchName)
        {
            Validation.ValidateArgument(user, "user");
            Validation.ValidateArgument(repository, "repository");
            return List(user.Login, repository.Name, branchName);
        }

        public virtual IEnumerable<Commit> List(string userName, string repositoryName, string branchName, string path)
        {
            ValidateList(userName, repositoryName, branchName);
            Validation.ValidateStringArgument(path, "path");

            var data = Client.Download(string.Format("commits/list/{0}/{1}/{2}/{3}", userName, repositoryName, branchName, path));

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<CommitsContainer>(data).Commits :
                new List<Commit>();
        }

        public virtual IEnumerable<Commit> List(User user, Repository repository, string branchName, string path)
        {
            Validation.ValidateArgument(user, "user");
            Validation.ValidateArgument(repository, "repository");
            return List(user.Login, repository.Name, branchName, path);
        }

        public virtual Commit Get(string userName, string repositoryName, string sha)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            Validation.ValidateStringArgument(sha, "sha");

            var data = Client.Download(string.Format("commits/show/{0}/{1}/{2}", userName, repositoryName, sha));

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<CommitContainer>(data).Commit :
                null;
        }

        public virtual Commit Get(User user, Repository repository, string sha)
        {
            Validation.ValidateArgument(user, "user");
            Validation.ValidateArgument(repository, "repository");
            return Get(user.Login, repository.Name, sha);
        }

        #region Validation

        protected static void ValidateList(string userName, string repositoryName, string branchName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            Validation.ValidateStringArgument(branchName, "branchName");
        }

        #endregion

        #region protected classes

        protected class CommitsContainer
        {
            [JsonName("commits")]
            public List<Commit> Commits { get; set; }
        }

        protected class CommitContainer
        {
            [JsonName("commit")]
            public Commit Commit { get; set; }
        }

        #endregion
    }
}
