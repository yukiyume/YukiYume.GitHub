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
using NUnit.Framework;
using log4net;
using YukiYume.GitHub.Configuration;

#endregion

namespace YukiYume.GitHub.Tests
{
    [TestFixture]
    public class IssueRepositoryFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(IssueRepositoryFixture));
        private IIssueRepository IssueRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            IssueRepository = Kernel.Get<IIssueRepository>();
        }

        #region Search

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchNullUser()
        {
            IssueRepository.Search(null, "repos", IssueStateType.Open, "test");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchEmptyUser()
        {
            IssueRepository.Search(string.Empty, "repos", IssueStateType.Open, "test");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchNullRepository()
        {
            IssueRepository.Search("user", null, IssueStateType.Open, "test");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchEmptyRepository()
        {
            IssueRepository.Search("user", string.Empty, IssueStateType.Open, "test");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchNullSearchTerm()
        {
            IssueRepository.Search("user", "repos", IssueStateType.Open, null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchEmptySearchTerm()
        {
            IssueRepository.Search("user", "repos", IssueStateType.Open, string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchUnknownIssueType()
        {
            IssueRepository.Search("user", "repos", IssueStateType.Unknown, "test");
        }

        [Test]
        public void SearchNonExistingUser()
        {
            var issues = IssueRepository.Search("qqqqqqqqqqqqqqqq", "qqqqqqqqqqqqqqqq", IssueStateType.Open, "test");
            Assert.That(issues != null);
            Assert.That(issues.Count() == 0);   
        }

        [Test]
        public void SearchExistingOpen()
        {
            var issues = IssueRepository.Search(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", IssueStateType.Open, "test");
            Assert.That(issues != null);
            Assert.That(issues.Count() > 0);

            issues.Each(issue =>
            {
                Assert.That(issue != null);
                Assert.That(issue.Number > 0);
                Assert.That(issue.IssueState == IssueStateType.Open);
                Log.Info(issue);
            });
        }

        [Test]
        public void SearchNonExisting()
        {
            var issues = IssueRepository.Search("defunkt", "github-issues", IssueStateType.Open, "qqqqqqqqqqqqqqqq");
            Assert.That(issues != null);
            Assert.That(issues.Count() == 0);
        }

        [Test]
        public void SearchExistingClosed()
        {
            var issues = IssueRepository.Search(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", IssueStateType.Closed, "test");
            Assert.That(issues != null);
            Assert.That(issues.Count() > 0);

            issues.Each(issue =>
            {
                Assert.That(issue != null);
                Assert.That(issue.Number > 0);
                Assert.That(issue.IssueState == IssueStateType.Closed);
                Log.Info(issue);
            });
        }

        #endregion

        #region List

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListNullUser()
        {
            IssueRepository.List(null, "repos", IssueStateType.Open);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListEmptyUser()
        {
            IssueRepository.List(string.Empty, "repos", IssueStateType.Open);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListNullRepository()
        {
            IssueRepository.List("user", null, IssueStateType.Open);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListEmptyRepository()
        {
            IssueRepository.List("user", string.Empty, IssueStateType.Open);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListUnknownIssueType()
        {
            IssueRepository.List("user", "repos", IssueStateType.Unknown);
        }

        [Test]
        public void ListOpen()
        {
            List(IssueStateType.Open);
        }

        [Test]
        public void ListClosed()
        {
            List(IssueStateType.Closed);
        }

        private void List(IssueStateType issueState)
        {
            var issues = IssueRepository.List(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", issueState);
            Assert.That(issues != null);
            Assert.That(issues.Count() > 0);

            issues.Each(issue =>
            {
                Assert.That(issue != null);
                Assert.That(issue.Number > 0);
                Assert.That(issue.IssueState == issueState);
                Log.Info(issue);
            });
        }

        #endregion

        #region Get

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullUser()
        {
            IssueRepository.Get(null, "repos", 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptyUser()
        {
            IssueRepository.Get(string.Empty, "repos", 1);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullRepository()
        {
            IssueRepository.Get("user", null, 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptyRepository()
        {
            IssueRepository.Get("user", string.Empty, 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBadNumber()
        {
            IssueRepository.Get("user", "repos", -5);
        }

        [Test]
        public void GetExistingOpen()
        {
            Get(1, IssueStateType.Open);
        }

        private void Get(int number, IssueStateType issueState)
        {
            var issue = IssueRepository.Get(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", number);
            Assert.That(issue != null);
            Assert.That(issue.Number == number);
            Assert.That(issue.IssueState == issueState);
            Log.Info(issue);
        }

        [Test]
        public void GetExistingClosed()
        {
            Get(2, IssueStateType.Closed);
        }

        #endregion

        #region Open

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void OpenNullUser()
        {
            IssueRepository.Open(null, "repos", "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void OpenEmptyUser()
        {
            IssueRepository.Open(string.Empty, "repos", "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void OpenNullRepository()
        {
            IssueRepository.Open("user", null, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void OpenEmptyRepository()
        {
            IssueRepository.Open("user", string.Empty, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void OpenNullTitle()
        {
            IssueRepository.Open("user", "repos", null, "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void OpenEmptyTitle()
        {
            IssueRepository.Open("user", "repos", string.Empty, "body");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void OpenNullBody()
        {
            IssueRepository.Open("user", "repos", "title", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void OpenEmptyBody()
        {
            IssueRepository.Open("user", "repos", "title", string.Empty);
        }

        [Test]
        public void Open()
        {
            var issue = IssueRepository.Open(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "title from open unit test", "body from open unit test");
            Assert.That(issue != null);
            Assert.That(issue.Number > 0);
            Assert.That(issue.Title == "title from open unit test");
            Assert.That(issue.Body == "body from open unit test");
            Assert.That(issue.IssueState == IssueStateType.Open);

            Log.Info(issue);
        }

        #endregion

        #region Close

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void CloseNullUser()
        {
            IssueRepository.Close(null, "repos", 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CloseEmptyUser()
        {
            IssueRepository.Close(string.Empty, "repos", 1);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void CloseNullRepository()
        {
            IssueRepository.Close("user", null, 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CloseEmptyRepository()
        {
            IssueRepository.Close("user", string.Empty, 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CloseBadNumber()
        {
            IssueRepository.Close("user", "repos", -5);
        }

        [Test]
        public void CloseExisting()
        {
            var issue = IssueRepository.Close(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", 4);
            Assert.That(issue != null);
            Assert.That(issue.Number == 4);
            Assert.That(issue.IssueState == IssueStateType.Closed);
            Log.Info(issue);
        }

        #endregion

        #region ReOpen

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ReOpenNullUser()
        {
            IssueRepository.ReOpen(null, "repos", 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ReOpenEmptyUser()
        {
            IssueRepository.ReOpen(string.Empty, "repos", 1);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ReOpenNullRepository()
        {
            IssueRepository.ReOpen("user", null, 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ReOpenEmptyRepository()
        {
            IssueRepository.ReOpen("user", string.Empty, 1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ReOpenBadNumber()
        {
            IssueRepository.ReOpen("user", "repos", -5);
        }

        [Test]
        public void ReOpenExisting()
        {
            var issue = IssueRepository.ReOpen(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", 4);
            Assert.That(issue != null);
            Assert.That(issue.Number == 4);
            Assert.That(issue.IssueState == IssueStateType.Open);
            Log.Info(issue);
        }

        #endregion

        #region Edit

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void EditNullUser()
        {
            IssueRepository.Edit(null, "repos", 1, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void EditEmptyUser()
        {
            IssueRepository.Edit(string.Empty, "repos", 1, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void EditNullRepository()
        {
            IssueRepository.Edit("user", null, 1, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void EditEmptyRepository()
        {
            IssueRepository.Edit("user", string.Empty, 1, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void EditBadNumber()
        {
            IssueRepository.Edit("user", "repos", -5, "title", "body");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void EditNullTitle()
        {
            IssueRepository.Edit("user", "repos", 1, null, "body");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void EditEmptyTitle()
        {
            IssueRepository.Edit("user", "repos", 1, string.Empty, "body");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void EditNullBody()
        {
            IssueRepository.Edit("user", "repos", 1, "title", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void EditEmptyBody()
        {
            IssueRepository.Edit("user", "repos", 1, "title", string.Empty);
        }

        [Test]
        public void Edit()
        {
            var issue = IssueRepository.Edit(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", 4, "editing title", "editing body");
            Assert.That(issue != null);
            Assert.That(issue.Number == 4);
            Log.Info(issue);
        }

        #endregion

        #region GetLabels

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetLabelsNullUser()
        {
            IssueRepository.GetLabels(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetLabelsEmptyUser()
        {
            IssueRepository.GetLabels(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetLabelsNullRepository()
        {
            IssueRepository.GetLabels("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetLabelsEmptyRepository()
        {
            IssueRepository.GetLabels("user", string.Empty);
        }

        [Test]
        public void GetLabels()
        {
            var labels = IssueRepository.GetLabels(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(labels != null);
            Assert.That(labels.Count() > 0);

            labels.Each(label =>
            {
                Assert.That(!string.IsNullOrEmpty(label));
                Log.Info(label);
            });
        }

        #endregion

        #region AddLabel

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddLabelNullUser()
        {
            IssueRepository.AddLabel(null, "repos", "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddLabelEmptyUser()
        {
            IssueRepository.AddLabel(string.Empty, "repos", "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddLabelNullRepository()
        {
            IssueRepository.AddLabel("user", null, "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddLabelEmptyRepository()
        {
            IssueRepository.AddLabel("user", string.Empty, "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddLabelNullLabel()
        {
            IssueRepository.AddLabel("user", "repos", null, 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddLabelEmptyLabel()
        {
            IssueRepository.AddLabel("user", "repos", string.Empty, 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddLabelBadNumber()
        {
            IssueRepository.AddLabel("user", "repos", "newlabel", -4);
        }

        [Test]
        public void AddLabel()
        {
            var labels = IssueRepository.AddLabel(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "newlabel", 4);
            Assert.That(labels != null);
            Assert.That(labels.Count() > 0);

            var newLabel = (from label in labels
                            where string.Compare(label, "newlabel") == 0
                            select label).FirstOrDefault();

            Assert.That(!string.IsNullOrEmpty(newLabel));

            labels.Each(label =>
            {
                Assert.That(!string.IsNullOrEmpty(label));
                Log.Info(label);
            });
        }

        #endregion

        #region RemoveLabel

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveLabelNullUser()
        {
            IssueRepository.RemoveLabel(null, "repos", "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveLabelEmptyUser()
        {
            IssueRepository.RemoveLabel(string.Empty, "repos", "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveLabelNullRepository()
        {
            IssueRepository.RemoveLabel("user", null, "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveLabelEmptyRepository()
        {
            IssueRepository.RemoveLabel("user", string.Empty, "newlabel", 4);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveLabelNullLabel()
        {
            IssueRepository.RemoveLabel("user", "repos", null, 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveLabelEmptyLabel()
        {
            IssueRepository.RemoveLabel("user", "repos", string.Empty, 4);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveLabelBadNumber()
        {
            IssueRepository.RemoveLabel("user", "repos", "newlabel", -4);
        }

        [Test]
        public void RemoveLabel()
        {
            var labels = IssueRepository.RemoveLabel(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "newlabel", 4);
            Assert.That(labels != null);

            var newLabel = (from label in labels
                            where string.Compare(label, "newlabel") == 0
                            select label).FirstOrDefault();

            Assert.That(newLabel == null);

            labels.Each(label =>
            {
                Assert.That(!string.IsNullOrEmpty(label));
                Log.Info(label);
            });
        }

        #endregion

        #region Comment

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddCommentNullUser()
        {
            IssueRepository.AddComment(null, "repos", 1, "title");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddCommentEmptyUser()
        {
            IssueRepository.AddComment(string.Empty, "repos", 1, "title");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddCommentNullRepository()
        {
            IssueRepository.AddComment("user", null, 1, "title");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddCommentEmptyRepository()
        {
            IssueRepository.AddComment("user", string.Empty, 1, "title");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddCommentBadNumber()
        {
            IssueRepository.AddComment("user", "repos", -5, "title");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddCommentNullComment()
        {
            IssueRepository.AddComment("user", "repos", 1, null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddCommentEmptyComment()
        {
            IssueRepository.AddComment("user", "repos", 1, string.Empty);
        }

        [Test]
        public void AddComment()
        {
            var comment = IssueRepository.AddComment(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", 4, "test comment");
            Assert.That(comment != null);
            Assert.That(comment.Status == "saved");
            Assert.That(comment.CommentText == "test comment");

            Log.Info(comment);
        }

        #endregion
    }
}
