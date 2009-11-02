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
using YukiYume.Logging;

#endregion

namespace YukiYume.GitHub.Tests
{
    /// <summary>
    /// Unit Tests for IRepositoryService
    /// </summary>
    [TestFixture]
    public class RepositoryServiceFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RepositoryServiceFixture));
        private IRepositoryService GitRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            GitRepository = GitHubServiceLocator.Get<IRepositoryService>();
        }

        #region Search

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchNull()
        {
            GitRepository.Search(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchEmpty()
        {
            GitRepository.Search(string.Empty);
        }

        [Test]
        public void Search()
        {
            var searchResults = GitRepository.Search("yuki");
            Assert.That(searchResults != null);
            Assert.That(searchResults.Count() > 0);

            searchResults.Each(result =>
            {
                Assert.That(result != null);
                Assert.That(!string.IsNullOrEmpty(result.Id));
                Log.Info(result);
            });
        }

        #endregion

        #region Get

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullUser()
        {
            GitRepository.Get(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptyUser()
        {
            GitRepository.Get(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNullRepository()
        {
            GitRepository.Get("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmptyRepository()
        {
            GitRepository.Get("user", string.Empty);
        }

        [Test]
        public void GetExisting()
        {
            var repository = GitRepository.Get(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(repository != null);
            Assert.That(!string.IsNullOrEmpty(repository.Name));

            Log.Info(repository);
        }

        #endregion

        #region GetAll

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetAllUserNull()
        {
            const string nullString = null;
            GitRepository.GetAll(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetAllUserEmpty()
        {
            GitRepository.GetAll(string.Empty);
        }

        [Test]
        public void GetAll()
        {
            var results = GitRepository.GetAll(Config.GitHub.Authentication.UserName);
            Assert.That(results != null);
            Assert.That(results.Count() > 0);

            results.Each(result =>
            {
                Assert.That(result != null);
                Assert.That(!string.IsNullOrEmpty(result.Name));
                Log.Info(result);
            });
        }

        #endregion

        #region Watch

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WatchNullUser()
        {
            GitRepository.Watch(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WatchEmptyUser()
        {
            GitRepository.Watch(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WatchNullRepository()
        {
            GitRepository.Watch("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WatchEmptyRepository()
        {
            GitRepository.Watch("user", string.Empty);
        }

        [Test]
        public void Watch()
        {
            var repository = GitRepository.Watch(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(repository != null);
            Assert.That(repository.Name == "YukiYume.GitHub");

            Log.Info(repository);
        }

        #endregion

        #region Unwatch

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnwatchNullUser()
        {
            GitRepository.Unwatch(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void UnwatchEmptyUser()
        {
            GitRepository.Unwatch(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnwatchNullRepository()
        {
            GitRepository.Unwatch("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void UnwatchEmptyRepository()
        {
            GitRepository.Unwatch("user", string.Empty);
        }

        [Test]
        public void Unwatch()
        {
            var repository = GitRepository.Unwatch(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(repository != null);
            Assert.That(repository.Name == "YukiYume.GitHub");

            Log.Info(repository);
        }

        #endregion

        #region Fork

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ForkNullUser()
        {
            GitRepository.Fork(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ForkEmptyUser()
        {
            GitRepository.Fork(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ForkNullRepository()
        {
            GitRepository.Fork("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ForkEmptyRepository()
        {
            GitRepository.Fork("user", string.Empty);
        }

        [Test]
        public void Fork()
        {
            var repository = GitRepository.Fork("KristopherGBaker", "YukiYume.GitHub");
            Assert.That(repository != null);
            Assert.That(repository.Name == "YukiYume.GitHub");
            Assert.That(repository.Owner == Config.GitHub.Authentication.UserName);

            Log.Info(repository);
        }

        #endregion

        #region Create

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void CreateNullName()
        {
            GitRepository.Create(null, "description", "homepage", true);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CreateEmptyName()
        {
            GitRepository.Create(string.Empty, "description", "homepage", true);
        }

        [Test]
        public void Create()
        {
            var repository = GitRepository.Create("Test", "Test Repository", "http://dev.yukiyume.net", true);
            Assert.That(repository != null);
            Assert.That(repository.Name == "Test");
            Assert.That(repository.Description == "Test Repository");
            Assert.That(repository.HomePage == "http://dev.yukiyume.net");
            Assert.That(!repository.IsPrivate);

            Log.Info(repository);
        }

        #endregion

        #region Delete

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNullName()
        {
            const string nullString = null;
            GitRepository.Delete(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void DeleteEmptyName()
        {
            GitRepository.Delete(string.Empty);
        }

        [Test]
        public void Delete()
        {
            var token = GitRepository.Delete("Test");
            Assert.That(!string.IsNullOrEmpty(token));
            Log.Info(token);
        }

        #endregion

        #region ConfirmDelete

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConfirmDeleteNullName()
        {
            const string nullString = null;
            GitRepository.ConfirmDelete(nullString, "token");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ConfirmDeleteEmptyName()
        {
            GitRepository.ConfirmDelete(string.Empty, "token");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConfirmDeleteNullConfirmationToken()
        {
            GitRepository.ConfirmDelete("name", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ConfirmDeleteEmptyConfirmationToken()
        {
            GitRepository.ConfirmDelete("name", string.Empty);
        }

        [Test]
        public void ConfirmDelete()
        {
            var token = GitRepository.Delete("Test");
            Assert.That(!string.IsNullOrEmpty(token));
            Log.Info(token);

            var status = GitRepository.ConfirmDelete("Test", token);
            Assert.That(!string.IsNullOrEmpty(status));
            Assert.That(status == "deleted");
            Log.Info(status);
        }

        #endregion

        #region GetKeys

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetKeysNullName()
        {
            const string nullString = null;
            GitRepository.GetKeys(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetKeysEmptyName()
        {
            GitRepository.GetKeys(string.Empty);
        }

        [Test]
        public void GetKeys()
        {
            var keys = GitRepository.GetKeys("YukiYume.GitHub");
            Assert.That(keys != null);
            Assert.That(keys.Count() > 0);

            keys.Each(key =>
            {
                Assert.That(key != null);
                Assert.That(!string.IsNullOrEmpty(key.Title));
                Assert.That(!string.IsNullOrEmpty(key.Key));
                Assert.That(key.Id > 0);

                Log.Info(key);
            });
        }

        #endregion

        #region AddKey

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyNullRepository()
        {
            GitRepository.AddKey(null, "title", "key");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyRepository()
        {
            GitRepository.AddKey(string.Empty, "title", "key");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyNullTitle()
        {
            GitRepository.AddKey("repos", null, "key");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyTitle()
        {
            GitRepository.AddKey("repos", string.Empty, "key");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyNullKey()
        {
            GitRepository.AddKey("repos", "title", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyKey()
        {
            GitRepository.AddKey("repos", "title", string.Empty);
        }

        [Test]
        public void AddKey()
        {
            var keys = GitRepository.AddKey("YukiYume.GitHub", "test2", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEA5jZesKNZXRNSbw1znzryqbUqJQ+GMJdk5n0dhGo3Vy4vOfsLy2dimyh770KDgSmuS3yFNE9cUMINWHE7uPPCwWyqc6YyV3EE3x8gRXu+ZNtOWpZBx5jFWCwovMWttxV8Jsmu0K0udQZ0YSUv74npEDltXNtmAdDZSOIt5zlOJys= rsa-key-20091031");
            Assert.That(keys != null);
            Assert.That(keys.Count() > 0);

            var newKey = (from key in keys
                          where key.Title == "test2"
                          select key).FirstOrDefault();
            Assert.That(newKey != null);

            keys.Each(key =>
            {
                Assert.That(key != null);
                Assert.That(!string.IsNullOrEmpty(key.Title));
                Assert.That(!string.IsNullOrEmpty(key.Key));
                Assert.That(key.Id > 0);

                Log.Info(key);
            });
        }

        #endregion

        #region RemoveKey

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveKeyNullRepository()
        {
            GitRepository.RemoveKey(null, 3);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveKeyEmptyRepository()
        {
            GitRepository.RemoveKey(string.Empty, 3);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveKeyBadId()
        {
            GitRepository.RemoveKey("repos", -3);
        }

        [Test]
        public void RemoveKey()
        {
            var keys = GitRepository.AddKey("YukiYume.GitHub", "test2", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEA5jZesKNZXRNSbw1znzryqbUqJQ+GMJdk5n0dhGo3Vy4vOfsLy2dimyh770KDgSmuS3yFNE9cUMINWHE7uPPCwWyqc6YyV3EE3x8gRXu+ZNtOWpZBx5jFWCwovMWttxV8Jsmu0K0udQZ0YSUv74npEDltXNtmAdDZSOIt5zlOJys= rsa-key-20091031");
            Assert.That(keys != null);
            Assert.That(keys.Count() > 0);

            var newKey = (from key in keys
                          where key.Title == "test2"
                          select key).FirstOrDefault();
            Assert.That(newKey != null);

            keys.Each(key =>
            {
                Assert.That(key != null);
                Assert.That(!string.IsNullOrEmpty(key.Title));
                Assert.That(!string.IsNullOrEmpty(key.Key));
                Assert.That(key.Id > 0);

                Log.Info(key);
            });

            var id = newKey.Id;
            keys = GitRepository.RemoveKey("YukiYume.GitHub", id);
            Assert.That(keys != null);
            Assert.That(keys.Count() > 0);

            newKey = (from key in keys
                      where key.Id == id
                      select key).FirstOrDefault();
            Assert.That(newKey == null);

            keys.Each(key =>
            {
                Assert.That(key != null);
                Assert.That(!string.IsNullOrEmpty(key.Title));
                Assert.That(!string.IsNullOrEmpty(key.Key));
                Assert.That(key.Id > 0);

                Log.Info(key);
            });
        }

        #endregion

        #region GetCollaborators

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollaboratorsNullUser()
        {
            GitRepository.GetCollaborators(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetCollaboratorsEmptyUser()
        {
            GitRepository.GetCollaborators(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollaboratorsNullRepository()
        {
            GitRepository.GetCollaborators("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetCollaboratorsEmptyRepository()
        {
            GitRepository.GetCollaborators("user", string.Empty);
        }

        [Test]
        public void GetCollaborators()
        {
            var collaborators = GitRepository.GetCollaborators(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(collaborators != null);
            Assert.That(collaborators.Count() > 0);

            collaborators.Each(collaborator =>
            {
                Assert.That(!string.IsNullOrEmpty(collaborator));
                Log.Info(collaborator);
            });
        }

        #endregion

        #region AddCollaborator

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddCollaboratorNullRepository()
        {
            GitRepository.AddCollaborator(null, "user");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddCollaboratorEmptyRepository()
        {
            GitRepository.AddCollaborator(string.Empty, "user");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddCollaboratorNullUser()
        {
            GitRepository.AddCollaborator("repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddCollaboratorEmptyUser()
        {
            GitRepository.AddCollaborator("repos", string.Empty);
        }

        [Test]
        public void AddCollaborator()
        {
            var collaborators = GitRepository.AddCollaborator("YukiYume.GitHub", "Aoreem");
            Assert.That(collaborators != null);
            Assert.That(collaborators.Count() > 0);

            var user = (from collaborator in collaborators
                        where collaborator == "Aoreem"
                        select collaborator).FirstOrDefault();
            Assert.That(!string.IsNullOrEmpty(user));

            collaborators.Each(collaborator =>
            {
                Assert.That(!string.IsNullOrEmpty(collaborator));
                Log.Info(collaborator);
            });
        }

        #endregion

        #region RemoveCollaborator

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveCollaboratorNullRepository()
        {
            GitRepository.RemoveCollaborator(null, "user");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveCollaboratorEmptyRepository()
        {
            GitRepository.RemoveCollaborator(string.Empty, "user");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveCollaboratorNullUser()
        {
            GitRepository.RemoveCollaborator("repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveCollaboratorEmptyUser()
        {
            GitRepository.RemoveCollaborator("repos", string.Empty);
        }

        [Test]
        public void RemoveCollaborator()
        {
            var collaborators = GitRepository.RemoveCollaborator("YukiYume.GitHub", "Aoreem");
            Assert.That(collaborators != null);
            Assert.That(collaborators.Count() > 0);

            var user = (from collaborator in collaborators
                        where collaborator == "Aoreem"
                        select collaborator).FirstOrDefault();
            Assert.That(user == null);

            collaborators.Each(collaborator =>
            {
                Assert.That(!string.IsNullOrEmpty(collaborator));
                Log.Info(collaborator);
            });
        }

        #endregion

        #region Network

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkNullUser()
        {
            GitRepository.GetNetwork(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkEmptyUser()
        {
            GitRepository.GetNetwork(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkNullRepository()
        {
            GitRepository.GetNetwork("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkEmptyRepository()
        {
            GitRepository.GetNetwork("user", string.Empty);
        }

        [Test]
        public void GetNetwork()
        {
            var results = GitRepository.GetNetwork(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(results != null);
            Assert.That(results.Count() > 0);

            results.Each(result =>
            {
                Assert.That(result != null);
                Assert.That(!string.IsNullOrEmpty(result.Name));
                Log.Info(result);
            });
        }

        #endregion

        #region Languages

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetLanguagesNullUser()
        {
            GitRepository.GetLanguages(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetLanguagesEmptyUser()
        {
            GitRepository.GetLanguages(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetLanguagesNullRepository()
        {
            GitRepository.GetLanguages("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetLanguagesEmptyRepository()
        {
            GitRepository.GetLanguages("user", string.Empty);
        }

        [Test]
        public void GetLanguages()
        {
            var languages = GitRepository.GetLanguages(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(languages != null);
            Assert.That(languages.Count > 0);

            languages.Each(pair =>
            {
                Assert.That(!string.IsNullOrEmpty(pair.Key));
                Assert.That(pair.Value > 0);

                Log.Info("{0}: {1}", pair.Key, pair.Value);
            });
        }

        #endregion

        #region Tags

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetTagsNullUser()
        {
            GitRepository.GetTags(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetTagsEmptyUser()
        {
            GitRepository.GetTags(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetTagsNullRepository()
        {
            GitRepository.GetTags("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetTagsEmptyRepository()
        {
            GitRepository.GetTags("user", string.Empty);
        }

        [Test]
        public void GetTags()
        {
            var tags = GitRepository.GetTags(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(tags != null);
            Assert.That(tags.Count > 0);

            tags.Each(pair =>
            {
                Assert.That(!string.IsNullOrEmpty(pair.Key));
                Assert.That(!string.IsNullOrEmpty(pair.Value));

                Log.Info("{0}: {1}", pair.Key, pair.Value);
            });
        }

        #endregion

        #region Branches

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBranchesNullUser()
        {
            GitRepository.GetBranches(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBranchesEmptyUser()
        {
            GitRepository.GetBranches(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBranchesNullRepository()
        {
            GitRepository.GetBranches("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBranchesEmptyRepository()
        {
            GitRepository.GetBranches("user", string.Empty);
        }

        [Test]
        public void GetBranches()
        {
            var branches = GitRepository.GetBranches(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(branches != null);
            Assert.That(branches.Count > 0);

            branches.Each(pair =>
            {
                Assert.That(!string.IsNullOrEmpty(pair.Key));
                Assert.That(!string.IsNullOrEmpty(pair.Value));

                Log.Info("{0}: {1}", pair.Key, pair.Value);
            });
        }

        #endregion

        #region Watched

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WatchedNullStringName()
        {
            const string nullString = null;
            GitRepository.Watched(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WatchedEmptyStringName()
        {
            GitRepository.Watched(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WatchedNullName()
        {
            const User nullUser = null;
            GitRepository.Watched(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WatchedNullName2()
        {
            GitRepository.Watched(new User { Login = null });
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WatchedEmptyName()
        {
            GitRepository.Watched(new User { Login = string.Empty });
        }

        [Test]
        public void WatchedString()
        {
            var repositories = GitRepository.Watched(Config.GitHub.Authentication.UserName);
            Assert.That(repositories != null);
            Assert.That(repositories.Count() > 0);

            repositories.Each(repository =>
            {
                Assert.That(repository != null);
                Assert.That(!string.IsNullOrEmpty(repository.Name));

                Log.Info(repository);
            });
        }

        [Test]
        public void WatchedUser()
        {
            var repositories = GitRepository.Watched(new User { Login = Config.GitHub.Authentication.UserName });
            Assert.That(repositories != null);
            Assert.That(repositories.Count() > 0);

            repositories.Each(repository =>
            {
                Assert.That(repository != null);
                Assert.That(!string.IsNullOrEmpty(repository.Name));

                Log.Info(repository);
            });
        }

        #endregion
    }
}
