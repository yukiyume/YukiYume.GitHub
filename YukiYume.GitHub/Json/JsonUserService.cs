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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using YukiYume.GitHub.Configuration;
using YukiYume.Json;
using Ninject;
using System.Web;
#endregion

namespace YukiYume.GitHub.Json
{
    /// <summary>
    /// JSON implementation of IUserService
    /// </summary>
    public class JsonUserService : BaseService, IUserService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonUserService));
        protected static readonly Regex ValidEmail = new Regex(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled);

        public JsonUserService()
            : base(FormatType.Json)
        {
        }

        public JsonUserService(string gitHubUserName, string gitHubApiToken)
            : base(FormatType.Json, gitHubUserName, gitHubApiToken)
        {
        }

        public virtual User Get(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");
            var data = Client.Download(string.Format("user/show/{0}", userName), string.Compare(userName, Config.GitHub.Authentication.UserName, true) == 0);

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<UserContainer>(data).User : null;
        }

        public virtual IEnumerable<SearchUser> Search(string searchName)
        {
            Validation.ValidateStringArgument(searchName, "searchName");
            var data = Client.Download(string.Format("user/search/{0}", HttpUtility.UrlEncode(searchName)));

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<SearchContainer>(data).Users : new List<SearchUser>();
        }

        public virtual IEnumerable<string> Followers(User user)
        {
            Validation.ValidateArgument(user, "user");
            return Followers(user.Login);
        }

        public virtual IEnumerable<string> Followers(SearchUser user)
        {
            Validation.ValidateArgument(user, "user");
            return Followers(user.UserName);
        }

        public virtual IEnumerable<string> Followers(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");
            var data = Client.Download(string.Format("user/show/{0}/followers", userName));

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<FollowContainer>(data).Users : new List<string>();
        }

        public virtual IEnumerable<string> Following(User user)
        {
            Validation.ValidateArgument(user, "user");
            return Following(user.Login);
        }

        public virtual IEnumerable<string> Following(SearchUser user)
        {
            Validation.ValidateArgument(user, "user");
            return Following(user.UserName);
        }

        public virtual IEnumerable<string> Following(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");
            var data = Client.Download(string.Format("user/show/{0}/following", userName));

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<FollowContainer>(data).Users : new List<string>();
        }

        public virtual IEnumerable<string> Follow(User user)
        {
            Validation.ValidateArgument(user, "user");
            return Follow(user.Login);
        }

        public virtual IEnumerable<string> Follow(SearchUser user)
        {
            Validation.ValidateArgument(user, "user");
            return Follow(user.UserName);
        }

        public virtual IEnumerable<string> Follow(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");
            var data = Client.Download(string.Format("user/follow/{0}", userName), true);

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<FollowContainer>(data).Users : new List<string>();
        }

        public virtual IEnumerable<string> Unfollow(User user)
        {
            Validation.ValidateArgument(user, "user");
            return Unfollow(user.Login);
        }

        public virtual IEnumerable<string> Unfollow(SearchUser user)
        {
            Validation.ValidateArgument(user, "user");
            return Unfollow(user.UserName);
        }

        public virtual IEnumerable<string> Unfollow(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");
            var data = Client.Download(string.Format("user/unfollow/{0}", userName), true);

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<FollowContainer>(data).Users : new List<string>();
        }

        public virtual IEnumerable<PublicKey> GetKeys()
        {
            var data = Client.Download("user/keys", true);
            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<KeyContainer>(data).Keys : new List<PublicKey>();
        }

        public virtual IEnumerable<PublicKey> AddKey(PublicKey publicKey)
        {
            Validation.ValidateArgument(publicKey, "publicKey");
            return AddKey(publicKey.Title, publicKey.Key);
        }

        public virtual IEnumerable<PublicKey> AddKey(string title, string key)
        {
            Validation.ValidateStringArgument(title, "title");
            Validation.ValidateStringArgument(key, "key");
            var data = Client.Download("user/key/add", true, new NameValueCollection { { "title", title }, { "key", key } });

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<KeyContainer>(data).Keys : new List<PublicKey>();
        }

        public virtual IEnumerable<PublicKey> RemoveKey(PublicKey publicKey)
        {
            Validation.ValidateArgument(publicKey, "publicKey");
            return RemoveKey(publicKey.Id);
        }

        public virtual IEnumerable<PublicKey> RemoveKey(int id)
        {
            Validation.ValidateArgument(id, arg => arg <= 0, "id cannot be 0 or negative", "id");
            var data = Client.Download("user/key/remove", true, new NameValueCollection { { "id", id.ToString() } });

            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<KeyContainer>(data).Keys : new List<PublicKey>();
        }

        public virtual IEnumerable<string> GetEmails()
        {
            var data = Client.Download("user/emails", true);
            return !string.IsNullOrEmpty(data) ? JsonDeserializer.Deserialize<EmailContainer>(data).Emails : new List<string>();
        }

        public virtual IEnumerable<string> AddEmail(string email)
        {
            ValidateEmail(email);
            var data = Client.Download("user/email/add", true, new NameValueCollection { { "email", email } });

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<EmailContainer>(data).Emails :
                new List<string>();
        }

        public virtual IEnumerable<string> RemoveEmail(string email)
        {
            ValidateEmail(email);
            var data = Client.Download("user/email/remove", true, new NameValueCollection { { "email", email } });

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<EmailContainer>(data).Emails :
                new List<string>();
        }

        public virtual User Update(string name, string email, string blog, string company, string location)
        {
            var parameters = new NameValueCollection();
            AddUpdateParameter(name, "values[name]", parameters);
            AddUpdateParameter(email, "values[email]", parameters);
            AddUpdateParameter(blog, "values[blog]", parameters);
            AddUpdateParameter(company, "values[company]", parameters);
            AddUpdateParameter(location, "values[location]", parameters);

            if (parameters.Count == 0)
                throw new ArgumentException("at least one Update parameter must not be null or empty");

            var data = Client.Download(string.Format("user/show/{0}", Client.LoginInfo["login"]), true, parameters);

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<UserContainer>(data).User :
                null;
        }

        protected static void AddUpdateParameter(string name, string formName, NameValueCollection parameters)
        {
            if (name != null)
                parameters.Add(formName, name);
        }

        public virtual User Update(User user)
        {
            Validation.ValidateArgument(user, "user");
            return Update(user.Name, user.Email, user.Blog, user.Company, user.Location);
        }

        #region Validation

        protected static void ValidateEmail(string email)
        {
            Validation.ValidateStringArgument(email, "email");
            Validation.ValidateArgument(email, arg => !ValidEmail.IsMatch(arg), "invalid email", "email");
        }

        #endregion

        #region protected classes

        protected class SearchContainer
        {
            [JsonName("users")]
            public List<SearchUser> Users { get; set; }
        }

        protected class FollowContainer
        {
            [JsonName("users")]
            public List<string> Users { get; set; }
        }

        protected class KeyContainer
        {
            [JsonName("public_keys")]
            public List<PublicKey> Keys { get; set; }
        }

        protected class EmailContainer
        {
            [JsonName("emails")]
            public List<string> Emails { get; set; }
        }

        protected class UserContainer
        {
            [JsonName("user")]
            public User User { get; set; }
        }

        #endregion
    }
}
