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
using Ninject.Parameters;
using NUnit.Framework;
using log4net;
using Ninject;
using YukiYume.GitHub.Configuration;
using YukiYume.GitHub.Json;

#endregion

namespace YukiYume.GitHub.Tests
{
    /// <summary>
    /// Unit Tests for GitHub class
    /// </summary>
    [TestFixture]
    public class GitHubFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GitHubFixture));
        private GitHub GitHub { get; set; }

        [SetUp]
        public void Setup()
        {
            var kernel = new StandardKernel(new JsonModule());
            GitHub = kernel.Get<GitHub>();
        }

        [Test]
        public void PropertiesNotNull()
        {
            PropertiesNotNull(GitHub);
        }

        private static void PropertiesNotNull(GitHub gitHub)
        {
            Assert.That(gitHub != null);
            Assert.That(gitHub.CommitService != null);
            Assert.That(gitHub.IssueService != null);
            Assert.That(gitHub.NetworkService != null);
            Assert.That(gitHub.ObjectService != null);
            Assert.That(gitHub.RepositoryService != null);
            Assert.That(gitHub.UserService != null);
        }

        [Test]
        public void ServicesAreJsonServices()
        {
            ServicesAreJsonServices(GitHub);
        }

        [Test]
        public void UsesDefaultLogin()
        {
            IsCorrectLoginToken(GitHub.CommitService, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
            IsCorrectLoginToken(GitHub.IssueService, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
            IsCorrectLoginToken(GitHub.NetworkService, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
            IsCorrectLoginToken(GitHub.ObjectService, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
            IsCorrectLoginToken(GitHub.RepositoryService, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
            IsCorrectLoginToken(GitHub.UserService, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
        }

        private static void ServicesAreJsonServices(GitHub gitHub)
        {
            Assert.That(gitHub.CommitService is JsonCommitService);
            Assert.That(gitHub.IssueService is JsonIssueService);
            Assert.That(gitHub.NetworkService is JsonNetworkService);
            Assert.That(gitHub.ObjectService is JsonObjectService);
            Assert.That(gitHub.RepositoryService is JsonRepositoryService);
            Assert.That(gitHub.UserService is JsonUserService);
        }

        private static void IsCorrectLoginToken(IService service, string login, string token)
        {
            Assert.That(service.Client.LoginInfo["login"] == login);
            Assert.That(service.Client.LoginInfo["token"] == token);
        }

        [Test]
        public void SpecifyUserNameAndToken()
        {
            var kernel = new StandardKernel(new JsonModule("user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"));
            var gitHub = kernel.Get<GitHub>();
            Assert.That(gitHub != null);
            PropertiesNotNull(gitHub);
            ServicesAreJsonServices(gitHub);
            IsCorrectLoginToken(gitHub.CommitService, "user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            IsCorrectLoginToken(gitHub.IssueService, "user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            IsCorrectLoginToken(gitHub.NetworkService, "user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            IsCorrectLoginToken(gitHub.ObjectService, "user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            IsCorrectLoginToken(gitHub.RepositoryService, "user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            IsCorrectLoginToken(gitHub.UserService, "user2", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        }
    }
}
