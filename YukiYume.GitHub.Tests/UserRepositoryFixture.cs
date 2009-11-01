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
    [TestFixture]
    public class UserRepositoryFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserRepositoryFixture));

        private IUserRepository UserRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            UserRepository = Kernel.Get<IUserRepository>();
        }

        #region Search

        [Test]
        public void SearchMultipleResults()
        {
            var users = UserRepository.Search("chacon");

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
            UserRepository.Search(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SearchEmpty()
        {
            UserRepository.Search(string.Empty);
        }

        [Test]
        public void SearchSingleResult()
        {
            var users = UserRepository.Search("kristophergbaker");

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
            var users = UserRepository.Search("qqqqqqqqqqqqqqqqqqq");

            Assert.That(users != null);
            Assert.That(users.Count() == 0);
        }

        #endregion

        #region Get

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNull()
        {
            UserRepository.Get(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetEmpty()
        {
            UserRepository.Get(string.Empty);
        }

        [Test]
        public void GetExistingNonAuthenticated()
        {
            var user = UserRepository.Get("schacon");
            Assert.That(user != null);
            Assert.That(user.Id == 70);
            Assert.That(string.Compare(user.Login, "schacon", true) == 0);
            Log.Info(user);
        }

        [Test]
        public void GetExistingAuthenticated()
        {
            var user = UserRepository.Get(Config.GitHub.Authentication.UserName);
            Assert.That(user != null);
            Assert.That(user.Id > 0);
            Assert.That(string.Compare(user.Login, Config.GitHub.Authentication.UserName, true) == 0);
            Log.Info(user);
        }

        [Test]
        public void GetNonExisting()
        {
            var user = UserRepository.Get("qqqqqqqqqqqqqqqq");
            Assert.That(user == null);
        }

        #endregion

        #region Followers

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowersNull()
        {
            const string nullString = null;
            UserRepository.Followers(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowersNullUser()
        {
            const User nullUser = null;
            UserRepository.Followers(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowersNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserRepository.Followers(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FollowersEmpty()
        {
            UserRepository.Followers(string.Empty);
        }

        [Test]
        public void FollowersMany()
        {
            var followers = UserRepository.Followers("schacon");

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
            var followers = UserRepository.Followers(new User { Login = "schacon" });

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
            var followers = UserRepository.Followers(new SearchUser { UserName = "schacon" });

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
            var followers = UserRepository.Followers("qqqqqqqqqqqqqqqq");

            Assert.That(followers != null);
            Assert.That(followers.Count() == 0);
        }

        #endregion

        #region Following

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowingNull()
        {
            const string nullString = null;
            UserRepository.Following(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowingNullUser()
        {
            const User nullUser = null;
            UserRepository.Following(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowingNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserRepository.Following(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FollowingEmpty()
        {
            UserRepository.Following(string.Empty);
        }

        [Test]
        public void FollowingMany()
        {
            var following = UserRepository.Following("schacon");

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
            var following = UserRepository.Following(new User { Login = "schacon" });

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
            var following = UserRepository.Following(new SearchUser { UserName = "schacon" });

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
            var followers = UserRepository.Following("qqqqqqqqqqqqqqqq");

            Assert.That(followers != null);
            Assert.That(followers.Count() == 0);
        }

        #endregion

        #region Follow

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowNull()
        {
            const string nullString = null;
            UserRepository.Follow(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowNullUser()
        {
            const User nullUser = null;
            UserRepository.Follow(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FollowNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserRepository.Follow(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FollowEmpty()
        {
            UserRepository.Follow(string.Empty);
        }

        [Test]
        public void FollowExisting()
        {
            var result = UserRepository.Follow("schacon");
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
            var result = UserRepository.Follow(new User { Login = "schacon" });
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
            var result = UserRepository.Follow(new SearchUser { UserName = "schacon" });
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
            UserRepository.Unfollow(nullString);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnfollowNullUser()
        {
            const User nullUser = null;
            UserRepository.Unfollow(nullUser);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void UnfollowNullSearchUser()
        {
            const SearchUser nullSearchUser = null;
            UserRepository.Unfollow(nullSearchUser);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void UnfollowEmpty()
        {
            UserRepository.Unfollow(string.Empty);
        }

        [Test]
        public void UnfollowExisting()
        {
            var result = UserRepository.Unfollow("schacon");
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
            var result = UserRepository.Unfollow(new User { Login = "schacon" });
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
            var result = UserRepository.Unfollow(new SearchUser { UserName = "schacon" });
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
            var keys = UserRepository.GetKeys();
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
            UserRepository.AddKey(null, "asdf");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyNullKey()
        {
            UserRepository.AddKey("asdf", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyTitle()
        {
            UserRepository.AddKey(string.Empty, "asdf");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddKeyEmptyKey()
        {
            UserRepository.AddKey("asdf", string.Empty);
        }

        [Test]
        public void AddKey()
        {
            var keys = UserRepository.AddKey("testkey", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=");

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);
            Log.Info(testkey);
        }

        [Test]
        public void AddKeyPublicKey()
        {
            var keys = UserRepository.AddKey(new PublicKey { Title = "testkey", Key = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=" });

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
            UserRepository.RemoveKey(-4);
        }

        [Test]
        public void RemoveKey()
        {
            var keys = UserRepository.AddKey("testkey", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=");

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);

            keys = UserRepository.RemoveKey(testkey.Id);

            testkey = (from key in keys
                       where key.Title == "testkey"
                       select key).FirstOrDefault();

            Assert.That(testkey == null);
        }

        [Test]
        public void RemoveKeyPublicKey()
        {
            var keys = UserRepository.AddKey("testkey", "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAIEAqhPcajyWiuO+LTR4yCms4RyxeWWqRYL+LxApm5NIJBOhAI+jkwhYea7acQZjl23a2V8QkQ907KeO4AnMjo3Aq+OU1KOw9WU845h1HZ5CFUX/nu0Qg8FwptiUAngXhlImVZ/JOZYqGTLAL0fmU6gPiwxrWsbEtR0kVXoxABFIYks=");

            var testkey = (from key in keys
                           where key.Title == "testkey"
                           select key).FirstOrDefault();

            Assert.That(testkey != null);

            keys = UserRepository.RemoveKey(testkey);

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
            var emails = UserRepository.GetEmails();
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
            UserRepository.AddEmail(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddEmailEmpty()
        {
            UserRepository.AddEmail(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddEmailBadEmail()
        {
            UserRepository.AddEmail("thisisnotanemailaddress");
        }

        [Test]
        public void AddEmail()
        {
            var emails = UserRepository.AddEmail("test@example.net");
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
            UserRepository.RemoveEmail(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveEmailEmpty()
        {
            UserRepository.RemoveEmail(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void RemoveEmailBadEmail()
        {
            UserRepository.RemoveEmail("thisisnotanemailaddress");
        }

        [Test]
        public void RemoveEmail()
        {
            var emails = UserRepository.RemoveEmail("test@example.net");
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
    }
}
