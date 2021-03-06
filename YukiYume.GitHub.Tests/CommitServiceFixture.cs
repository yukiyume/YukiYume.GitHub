﻿#region MIT License

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
    /// <summary>
    /// Unit Tests for ICommitService
    /// </summary>
    [TestFixture]
    public class CommitServiceFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CommitServiceFixture));
        private ICommitService CommitRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            CommitRepository = GithubServiceLocator.Get<ICommitService>();
        }

        #region ListBranch

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListBranchNullUserString()
        {
            CommitRepository.List(null, "repos", "branch");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListBranchNullUser()
        {
            CommitRepository.List(null, new Repository(), "branch");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListBranchEmptyUser()
        {
            CommitRepository.List(string.Empty, "repos", "branch");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListBranchNullRepositoryString()
        {
            CommitRepository.List("user", null, "branch");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListBranchNullRepository()
        {
            CommitRepository.List(new User(), null, "branch");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListBranchEmptyRepository()
        {
            CommitRepository.List("user", string.Empty, "branch");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListBranchNullBranch()
        {
            CommitRepository.List("user", "repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListBranchEmptyBranch()
        {
            CommitRepository.List("user", "repos", string.Empty);
        }

        [Test]
        public void ListBranch()
        {
            var commitList = CommitRepository.List(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "master");
            Assert.That(commitList != null);
            Assert.That(commitList.Count() > 0);

            commitList.Each(commit =>
            {
                Assert.That(commit != null);

                Log.Info(commit);
            });
        }

        [Test]
        public void ListBranch2()
        {
            var commitList = CommitRepository.List(new User { Login = Config.GitHub.Authentication.UserName }, new Repository { Name = "YukiYume.GitHub" }, "master");
            Assert.That(commitList != null);
            Assert.That(commitList.Count() > 0);

            commitList.Each(commit =>
            {
                Assert.That(commit != null);

                Log.Info(commit);
            });
        }

        [Test]
        public void ListUnknownBranch()
        {
            var commitList = CommitRepository.List(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "qqqqqqqqqq");
            Assert.That(commitList != null);
            Assert.That(commitList.Count() == 0);
        }

        #endregion

        #region ListFile

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListFileNullUserString()
        {
            CommitRepository.List(null, "repos", "branch", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListFileNullUser()
        {
            CommitRepository.List(null, new Repository(), "branch", "path");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListFileEmptyUser()
        {
            CommitRepository.List(string.Empty, "repos", "branch", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListFileNullRepositoryString()
        {
            CommitRepository.List("user", null, "branch", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListFileNullRepository()
        {
            CommitRepository.List(new User(), null, "branch", "path");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListFileEmptyRepository()
        {
            CommitRepository.List("user", string.Empty, "branch", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListFileNullBranch()
        {
            CommitRepository.List("user", "repos", null, "path");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListFileEmptyBranch()
        {
            CommitRepository.List("user", "repos", string.Empty, "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ListFileNullPath()
        {
            CommitRepository.List("user", "repos", "branch", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ListFileEmptyPath()
        {
            CommitRepository.List("user", "repos", "branch", string.Empty);
        }

        [Test]
        public void ListFile()
        {
            var commitList = CommitRepository.List(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "master", "YukiYume.GitHub/User.cs");
            Assert.That(commitList != null);
            Assert.That(commitList.Count() > 0);

            commitList.Each(commit =>
            {
                Assert.That(commit != null);
                Log.Info(commit);
            });
        }

        [Test]
        public void ListFile2()
        {
            var commitList = CommitRepository.List(new User { Login = Config.GitHub.Authentication.UserName }, new Repository { Name = "YukiYume.GitHub" }, "master", "YukiYume.GitHub/User.cs");
            Assert.That(commitList != null);
            Assert.That(commitList.Count() > 0);

            commitList.Each(commit =>
            {
                Assert.That(commit != null);
                Log.Info(commit);
            });
        }

        [Test]
        public void ListUnknownFile()
        {
            var commitList = CommitRepository.List(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "master", "qqqqqqqqqqq");
            Assert.That(commitList != null);
            Assert.That(commitList.Count() == 0);
        }

        #endregion

        #region Get

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullUserString()
        {
            CommitRepository.Get(null, "repos", "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullUser()
        {
            CommitRepository.Get(null, new Repository(), "sha");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptyUser()
        {
            CommitRepository.Get(string.Empty, "repos", "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullRepositoryString()
        {
            CommitRepository.Get("user", null, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullRepository()
        {
            CommitRepository.Get(new User(), null, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptyRepository()
        {
            CommitRepository.Get("user", string.Empty, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullSha()
        {
            CommitRepository.Get("user", "repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptySha()
        {
            CommitRepository.Get("user", "repos", string.Empty);
        }

        [Test]
        public void GetExistingSha()
        {
            var commit = CommitRepository.Get(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "cfb534dad9cdf552c90c41a88d276c03e6f98f24");
            Assert.That(commit != null);
            Assert.That(commit.Id == "cfb534dad9cdf552c90c41a88d276c03e6f98f24");

            Log.Info(commit);
        }

        [Test]
        public void GetExistingSha2()
        {
            var commit = CommitRepository.Get(new User { Login = Config.GitHub.Authentication.UserName }, new Repository { Name = "YukiYume.GitHub" }, "cfb534dad9cdf552c90c41a88d276c03e6f98f24");
            Assert.That(commit != null);
            Assert.That(commit.Id == "cfb534dad9cdf552c90c41a88d276c03e6f98f24");

            Log.Info(commit);
        }

        [Test]
        public void GetNonExistingSha()
        {
            var commit = CommitRepository.Get(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            Assert.That(commit == null);

            Log.Info(commit);
        }

        #endregion
    }
}
