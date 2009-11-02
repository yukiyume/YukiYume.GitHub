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
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using log4net;
using NUnit.Framework;
using YukiYume;
using YukiYume.GitHub.Configuration;
using YukiYume.Json;

#endregion

namespace YukiYume.GitHub.Tests
{
    /// <summary>
    /// Unit Tests for IUserRepository
    /// </summary>
    [TestFixture]
    public class UserServiceFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserServiceFixture));
        private IUserService UserService { get; set; }

        [SetUp]
        public void Setup()
        {
            UserService = GitHubServiceLocator.Get<IUserService>();
        }

        #region Search

        [Test]
        public void SearchMultipleResults()
        {
            var users = UserService.Search("chacon");

            Assert.That(users != null);
            Assert.That(users.Count() > 0);

            users.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user.Id));
                Assert.That(user.Id != "0");
                Assert.That(!string.IsNullOrEmpty(user.Name));
                Log.Info(user);
            });
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchNull()
        {
            UserService.Search(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchEmpty()
        {
            UserService.Search(string.Empty);
        }

        [Test]
        public void SearchSingleResult()
        {
            var users = UserService.Search("kristophergbaker");

            Assert.That(users != null);
            Assert.That(users.Count() == 1);

            users.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user.Id));
                Assert.That(user.Id != "0");
                Assert.That(!string.IsNullOrEmpty(user.Name));
                Log.Info(user);
            });
        }

        [Test]
        public void SearchNoResults()
        {
            var users = UserService.Search("qqqqqqqqqqqqqqqqqqq");

            Assert.That(users != null);
            Assert.That(users.Count() == 0);
        }

        #endregion

        #region Get

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNull()
        {
            UserService.Get(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmpty()
        {
            UserService.Get(string.Empty);
        }

        [Test]
        public void GetExistingNonAuthenticated()
        {
            var user = UserService.Get("schacon");
            Assert.That(user != null);
            Assert.That(user.Id == 70);
            Assert.That(string.Compare(user.Login, "schacon", true) == 0);
            Log.Info(user);
        }

        [Test]
        public void GetExistingAuthenticated()
        {
            var user = UserService.Get(Config.GitHub.Authentication.UserName);
            Assert.That(user != null);
            Assert.That(user.Id > 0);
            Assert.That(string.Compare(user.Login, Config.GitHub.Authentication.UserName, true) == 0);
            Log.Info(user);
        }

        [Test]
        public void GetNonExisting()
        {
            var user = UserService.Get("qqqqqqqqqqqqqqqq");
            Assert.That(user == null);
        }

        #endregion

        #region Followers

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowersNull()
        {
            const string nullString = null;
            UserService.Followers(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowersNullUser()
        {
            const User nullUser = null;
            UserService.Followers(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowersNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserService.Followers(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FollowersEmpty()
        {
            UserService.Followers(string.Empty);
        }

        [Test]
        public void FollowersMany()
        {
            var followers = UserService.Followers("schacon");

            Assert.That(followers != null);
            Assert.That(followers.Count() > 0);

            followers.Each(follower =>
            {
                Assert.That(!string.IsNullOrEmpty(follower));
                Log.Info(follower);
            });
        }

        [Test]
        public void FollowersManyUser()
        {
            var followers = UserService.Followers(new User { Login = "schacon" });

            Assert.That(followers != null);
            Assert.That(followers.Count() > 0);

            followers.Each(follower =>
            {
                Assert.That(!string.IsNullOrEmpty(follower));
                Log.Info(follower);
            });
        }

        [Test]
        public void FollowersManySearchUser()
        {
            var followers = UserService.Followers(new SearchUser { UserName = "schacon" });

            Assert.That(followers != null);
            Assert.That(followers.Count() > 0);

            followers.Each(follower =>
            {
                Assert.That(!string.IsNullOrEmpty(follower));
                Log.Info(follower);
            });
        }

        [Test]
        public void FollowersNonExisting()
        {
            var followers = UserService.Followers("qqqqqqqqqqqqqqqq");

            Assert.That(followers != null);
            Assert.That(followers.Count() == 0);
        }

        #endregion

        #region Following

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowingNull()
        {
            const string nullString = null;
            UserService.Following(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowingNullUser()
        {
            const User nullUser = null;
            UserService.Following(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowingNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserService.Following(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FollowingEmpty()
        {
            UserService.Following(string.Empty);
        }

        [Test]
        public void FollowingMany()
        {
            var following = UserService.Following("schacon");

            Assert.That(following != null);
            Assert.That(following.Count() > 0);

            following.Each(follower =>
            {
                Assert.That(!string.IsNullOrEmpty(follower));
                Log.Info(follower);
            });
        }

        [Test]
        public void FollowingManyUser()
        {
            var following = UserService.Following(new User { Login = "schacon" });

            Assert.That(following != null);
            Assert.That(following.Count() > 0);

            following.Each(follower =>
            {
                Assert.That(!string.IsNullOrEmpty(follower));
                Log.Info(follower);
            });
        }

        [Test]
        public void FollowingManySearchUser()
        {
            var following = UserService.Following(new SearchUser { UserName = "schacon" });

            Assert.That(following != null);
            Assert.That(following.Count() > 0);

            following.Each(follower =>
            {
                Assert.That(!string.IsNullOrEmpty(follower));
                Log.Info(follower);
            });
        }

        [Test]
        public void FollowingNonExisting()
        {
            var followers = UserService.Following("qqqqqqqqqqqqqqqq");

            Assert.That(followers != null);
            Assert.That(followers.Count() == 0);
        }

        #endregion

        #region Follow

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowNull()
        {
            const string nullString = null;
            UserService.Follow(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowNullUser()
        {
            const User nullUser = null;
            UserService.Follow(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserService.Follow(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FollowEmpty()
        {
            UserService.Follow(string.Empty);
        }

        [Test]
        public void FollowExisting()
        {
            var result = UserService.Follow("schacon");
            Assert.That(result != null);
            Assert.That(result.Count() > 0);

            result.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user));
                Log.Info(user);
            });
        }

        [Test]
        public void FollowExistingUser()
        {
            var result = UserService.Follow(new User { Login = "schacon" });
            Assert.That(result != null);
            Assert.That(result.Count() > 0);

            result.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user));
                Log.Info(user);
            });
        }

        [Test]
        public void FollowExistingSearchUser()
        {
            var result = UserService.Follow(new SearchUser { UserName = "schacon" });
            Assert.That(result != null);
            Assert.That(result.Count() > 0);

            result.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user));
                Log.Info(user);
            });
        }

        #endregion

        #region Unfollow

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnfollowNull()
        {
            const string nullString = null;
            UserService.Unfollow(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnfollowNullUser()
        {
            const User nullUser = null;
            UserService.Unfollow(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnfollowNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserService.Unfollow(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void UnfollowEmpty()
        {
            UserService.Unfollow(string.Empty);
        }

        [Test]
        public void UnfollowExisting()
        {
            var result = UserService.Unfollow("schacon");
            Assert.That(result != null);
            Assert.That(result.Count() > 0);

            result.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user));
                Log.Info(user);
            });
        }

        [Test]
        public void UnfollowExistingUser()
        {
            var result = UserService.Unfollow(new User { Login = "schacon" });
            Assert.That(result != null);
            Assert.That(result.Count() > 0);

            result.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user));
                Log.Info(user);
            });
        }

        [Test]
        public void UnfollowExistingSearchUser()
        {
            var result = UserService.Unfollow(new SearchUser { UserName = "schacon" });
            Assert.That(result != null);
            Assert.That(result.Count() > 0);

            result.Each(user =>
            {
                Assert.That(!string.IsNullOrEmpty(user));
                Log.Info(user);
            });
        }

        #endregion

        #region Keys

        [Test]
        public void GetKeys()
        {
            var keys = UserService.GetKeys();
            Assert.That(keys != null);
            Assert.That(keys.Count() > 0);

            keys.Each(key =>
            {
                Assert.That(key != null);
                Assert.That(!string.IsNullOrEmpty(key.Title));
                Assert.That(key.Id > 0);
                Assert.That(!string.IsNullOrEmpty(key.Key));
                Log.Info(key);
            });
        }

        #endregion

        #region AddKey

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyNullTitle()
        {
            UserService.AddKey(null, "asdf");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyNullKey()
        {
            UserService.AddKey("asdf", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyTitle()
        {
            UserService.AddKey(string.Empty, "asdf");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyKey()
        {
            UserService.AddKey("asdf", string.Empty);
        }

        [Test]
        public void AddKey()
        {
            var keys = UserService.AddKey("testkey", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=");

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);
            Log.Info(testkey);
        }

        [Test]
        public void AddKeyPublicKey()
        {
            var keys = UserService.AddKey(new PublicKey { Title = "testkey", Key = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=" });

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);
            Log.Info(testkey);
        }

        #endregion

        #region RemoveKey

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveKeyBadId()
        {
            UserService.RemoveKey(-4);
        }

        [Test]
        public void RemoveKey()
        {
            var keys = UserService.AddKey("testkey", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=");

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);

            keys = UserService.RemoveKey(testkey.Id);

            testkey = (from key in keys
                       where key.Title == "testkey"
                       select key).FirstOrDefault();

            Assert.That(testkey == null);
        }

        [Test]
        public void RemoveKeyPublicKey()
        {
            var keys = UserService.AddKey("testkey", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=");

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);

            keys = UserService.RemoveKey(testkey);

            testkey = (from key in keys
                       where key.Title == "testkey"
                       select key).FirstOrDefault();

            Assert.That(testkey == null);
        }

        #endregion

        #region Emails

        [Test]
        public void GetEmails()
        {
            var emails = UserService.GetEmails();
            Assert.That(emails != null);
            Assert.That(emails.Count() > 0);

            emails.Each(email =>
            {
                Assert.That(!string.IsNullOrEmpty(email));
                Log.Info(email);
            });
        }

        #endregion

        #region AddEmail

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddEmailNull()
        {
            UserService.AddEmail(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddEmailEmpty()
        {
            UserService.AddEmail(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddEmailBadEmail()
        {
            UserService.AddEmail("thisisnotanemailaddress");
        }

        [Test]
        public void AddEmail()
        {
            var emails = UserService.AddEmail("test@example.net");
            Assert.That(emails != null);
            Assert.That(emails.Count() > 0);

            var newEmail = (from email in emails
                            where string.Compare(email, "test@example.net", true) == 0
                            select email).FirstOrDefault();

            Assert.That(newEmail != null);

            emails.Each(email =>
            {
                Assert.That(!string.IsNullOrEmpty(email));
                Log.Info(email);
            });
        }

        #endregion

        #region RemoveEmail

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveEmailNull()
        {
            UserService.RemoveEmail(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveEmailEmpty()
        {
            UserService.RemoveEmail(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveEmailBadEmail()
        {
            UserService.RemoveEmail("thisisnotanemailaddress");
        }

        [Test]
        public void RemoveEmail()
        {
            var emails = UserService.RemoveEmail("test@example.net");
            Assert.That(emails != null);
            Assert.That(emails.Count() > 0);

            emails.Each(email =>
            {
                Assert.That(!string.IsNullOrEmpty(email));
                Assert.That(string.Compare(email, "test@example.net", true) != 0);
                Log.Info(email);
            });
        }

        #endregion

        #region Update

        [Test, ExpectedException(typeof(ArgumentException))]
        public void UpdateAllNull()
        {
            UserService.Update(null, null, null, null, null);
        }

        [Test]
        public void UpdateName()
        {
            var user = UserService.Update(new User { Name = "yukiyume test" });
            Assert.That(user != null);
            Assert.That(user.Name == "yukiyume test");
            Log.Info(user);

            user = UserService.Update(new User { Name = "yukiyume" });
            Assert.That(user != null);
            Assert.That(user.Name == "yukiyume");
            Log.Info(user);
        }

        [Test]
        public void UpdateEmail()
        {
            var user = UserService.Update(new User { Email = "yuki@yukiyume.net" });
            Assert.That(user != null);
            Assert.That(user.Email == "yuki@yukiyume.net");
            Log.Info(user);

            user = UserService.Update(new User { Email = string.Empty });
            Assert.That(user != null);
            Assert.That(user.Email.Length == 0);
            Log.Info(user);
        }

        [Test]
        public void UpdateBlog()
        {
            var user = UserService.Update(new User { Blog = "http://dev.yukiyume.net/" });
            Assert.That(user != null);
            Assert.That(user.Blog == "http://dev.yukiyume.net/");
            Log.Info(user);

            user = UserService.Update(new User { Blog = string.Empty });
            Assert.That(user != null);
            Assert.That(user.Blog.Length == 0);
            Log.Info(user);
        }

        [Test]
        public void UpdateCompany()
        {
            var user = UserService.Update(new User { Company = "yukiyume", Blog = string.Empty });
            Assert.That(user != null);
            Assert.That(user.Company == "yukiyume");
            Log.Info(user);

            user = UserService.Update(new User { Company = string.Empty });
            Assert.That(user != null);
            Assert.That(user.Company.Length == 0);
            Log.Info(user);
        }

        [Test]
        public void UpdateLocation()
        {
            var user = UserService.Update(new User { Location = "Boise, ID" });
            Assert.That(user != null);
            Assert.That(user.Location == "Boise, ID");
            Log.Info(user);

            user = UserService.Update(new User { Location = string.Empty, Blog = string.Empty });
            Assert.That(user != null);
            Assert.That(user.Location.Length == 0);
            Log.Info(user);
        }

        #endregion
    }
}
