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
using System.Web;
using YukiYume.Json;
using log4net;
using System.Collections.Specialized;

#endregion

namespace YukiYume.GitHub.Json
{
    public class JsonGitRepository : BaseRepository, IGitRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonGitRepository));

        public JsonGitRepository()
            : base(FormatType.Json)
        {
        }

        public JsonGitRepository(string gitHubUserName, string gitHubApiToken)
            : base(FormatType.Json, gitHubUserName, gitHubApiToken)
        {
        }

        public virtual IEnumerable<SearchRepository> Search(string searchTerm)
        {
            ValidateSearch(searchTerm);

            var data = Client.Download(string.Format("repos/search/{0}", HttpUtility.UrlEncode(searchTerm)));

            return data != null ?
                JsonDeserializer.Deserialize<SearchRepositoriesContainer>(data).Repositories :
                new List<SearchRepository>();
        }

        public virtual Repository Get(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/show/{0}/{1}", userName, repositoryName));

            return data != null ?
                JsonDeserializer.Deserialize<RepositoryContainer>(data).Repository :
                null;
        }

        public virtual Repository Get(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return Get(user.Login, repository.Name);
        }

        public virtual IEnumerable<Repository> GetAll(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");

            var data = Client.Download(string.Format("repos/show/{0}", userName));

            return data != null ?
                JsonDeserializer.Deserialize<RepositoriesContainer>(data).Repositories :
                new List<Repository>();
        }

        public virtual IEnumerable<Repository> GetAll(User user)
        {
            ValidateUser(user);
            return GetAll(user.Login);
        }

        public virtual Repository Watch(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/watch/{0}/{1}", userName, repositoryName), true);

            return data != null ?
                JsonDeserializer.Deserialize<RepositoryContainer>(data).Repository :
                null;
        }

        public virtual Repository Watch(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return Watch(user.Login, repository.Name);
        }

        public virtual Repository Unwatch(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/unwatch/{0}/{1}", userName, repositoryName), true);

            return data != null ?
                JsonDeserializer.Deserialize<RepositoryContainer>(data).Repository :
                null;
        }

        public virtual Repository Unwatch(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return Unwatch(user.Login, repository.Name);
        }

        public virtual Repository Fork(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/fork/{0}/{1}", userName, repositoryName), true);

            return data != null ?
                JsonDeserializer.Deserialize<RepositoryContainer>(data).Repository :
                null;
        }

        public virtual Repository Fork(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return Fork(user.Login, repository.Name);
        }

        public virtual Repository Create(string name, string description, string homePage, bool isPublic)
        {
            Validation.ValidateStringArgument(name, "name");

            var parameters = new NameValueCollection { { "name", name }, { "public", isPublic ? "1" : "0" } };

            if (!string.IsNullOrEmpty(description))
                parameters.Add("description", description);

            if (!string.IsNullOrEmpty(homePage))
                parameters.Add("homepage", homePage);

            var data = Client.Download("repos/create", true, parameters);

            return data != null ?
                JsonDeserializer.Deserialize<RepositoryContainer>(data).Repository :
                null;
        }

        public virtual Repository Create(Repository repository)
        {
            Validation.ValidateArgument(repository, "repository");
            return Create(repository.Name, repository.Description, repository.HomePage, !repository.IsPrivate);
        }

        public virtual string Delete(string repositoryName)
        {
            Validation.ValidateStringArgument(repositoryName, "repositoryName");

            var data = Client.Download(string.Format("repos/delete/{0}", repositoryName), true);

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<DeleteTokenContainer>(data).DeleteToken :
                null;
        }

        public virtual string Delete(Repository repository)
        {
            Validation.ValidateArgument(repository, "repository");
            return Delete(repository.Name);
        }

        public virtual string ConfirmDelete(string repositoryName, string confirmationToken)
        {
            Validation.ValidateStringArgument(repositoryName, "repositoryName");
            Validation.ValidateStringArgument(confirmationToken, "confirmationToken");

            var data = Client.Download(string.Format("repos/delete/{0}", repositoryName), true, new NameValueCollection { { "delete_token", confirmationToken } });

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<DeleteStatusContainer>(data).Status :
                null;
        }

        public virtual string ConfirmDelete(Repository repository, string confirmationToken)
        {
            Validation.ValidateArgument(repository, "repository");
            return ConfirmDelete(repository.Name, confirmationToken);
        }

        // TODO: implement SetPrivate and SetPublic
        // not implemented since I don't have access to a paid account (and thus private repositories)
        public virtual Repository SetPrivate(string repositoryName)
        {
            throw new NotImplementedException();
        }

        public virtual Repository SetPrivate(Repository repository)
        {
            Validation.ValidateArgument(repository, "repository");
            return SetPrivate(repository.Name);
        }

        // TODO: implement SetPrivate and SetPublic
        // not implemented since I don't have access to a paid account (and thus private repositories)
        public virtual Repository SetPublic(string repositoryName)
        {
            throw new NotImplementedException();
        }

        public virtual Repository SetPublic(Repository repository)
        {
            Validation.ValidateArgument(repository, "repository");
            return SetPublic(repository.Name);
        }

        public virtual IEnumerable<PublicKey> GetKeys(string repositoryName)
        {
            Validation.ValidateStringArgument(repositoryName, "repositoryName");

            var data = Client.Download(string.Format("repos/keys/{0}", repositoryName), true);

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<DeployKeysContainer>(data).PublicKeys :
                new List<PublicKey>();
        }

        public virtual IEnumerable<PublicKey> GetKeys(Repository repository)
        {
            Validation.ValidateArgument(repository, "repository");
            return GetKeys(repository.Name);
        }

        public virtual IEnumerable<PublicKey> AddKey(string repositoryName, string title, string key)
        {
            Validation.ValidateStringArgument(repositoryName, "repositoryName");
            Validation.ValidateStringArgument(title, "title");
            Validation.ValidateStringArgument(key, "key");

            var data = Client.Download(string.Format("repos/key/{0}/add", repositoryName), true, new NameValueCollection { { "title", title }, { "key", key } });
            Log.Info(data);

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<DeployKeysContainer>(data).PublicKeys :
                new List<PublicKey>();
        }

        public virtual IEnumerable<PublicKey> AddKey(Repository repository, PublicKey publicKey)
        {
            Validation.ValidateArgument(repository, "repository");
            Validation.ValidateArgument(publicKey, "publicKey");
            return AddKey(repository.Name, publicKey.Title, publicKey.Key);
        }

        public virtual IEnumerable<PublicKey> RemoveKey(string repositoryName, int id)
        {
            Validation.ValidateStringArgument(repositoryName, "repositoryName");
            Validation.ValidateArgument(id, arg => arg <= 0, "id cannot be less than or equal to 0", "id");

            var data = Client.Download(string.Format("repos/key/{0}/remove", repositoryName), true, new NameValueCollection { { "id", id.ToString() } });

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<DeployKeysContainer>(data).PublicKeys :
                new List<PublicKey>();
        }

        public virtual IEnumerable<PublicKey> RemoveKey(Repository repository, PublicKey publicKey)
        {
            Validation.ValidateArgument(repository, "repository");
            Validation.ValidateArgument(publicKey, "publicKey");
            return RemoveKey(repository.Name, publicKey.Id);
        }

        public virtual IEnumerable<string> GetCollaborators(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/show/{0}/{1}/collaborators", userName, repositoryName));

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<CollaboratorsContainer>(data).Collaborators :
                new List<string>();
        }

        public virtual IEnumerable<string> GetCollaborators(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return GetCollaborators(user.Login, repository.Name);
        }

        public virtual IEnumerable<string> AddCollaborator(string repositoryName, string userName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/collaborators/{0}/add/{1}", repositoryName, userName), true);

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<CollaboratorsContainer>(data).Collaborators :
                new List<string>();
        }

        public virtual IEnumerable<string> AddCollaborator(Repository repository, User user)
        {
            ValidateUserRepository(user, repository);
            return AddCollaborator(repository.Name, user.Login);
        }

        public virtual IEnumerable<string> RemoveCollaborator(string repositoryName, string userName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            var data = Client.Download(string.Format("repos/collaborators/{0}/remove/{1}", repositoryName, userName), true);

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<CollaboratorsContainer>(data).Collaborators :
                new List<string>();
        }

        public virtual IEnumerable<string> RemoveCollaborator(Repository repository, User user)
        {
            ValidateUserRepository(user, repository);
            return RemoveCollaborator(repository.Name, user.Login);
        }

        public virtual IEnumerable<Repository> GetNetwork(string userName, string repositoryName)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            var data = Client.Download(string.Format("repos/show/{0}/{1}/network", userName, repositoryName));

            return data != null ?
                JsonDeserializer.Deserialize<NetworkContainer>(data).Repositories :
                new List<Repository>();
        }

        public virtual IEnumerable<Repository> GetNetwork(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return GetNetwork(user.Login, repository.Name);
        }

        public virtual IDictionary<string, long> GetLanguages(string userName, string repositoryName)
        {
            return GetDictionary<long>(userName, repositoryName, "languages", 
                (pair, dictionary) =>
                {
                    if (!string.IsNullOrEmpty(pair.Key) && pair.Value is JsonNumber)
                        dictionary.Add(pair.Key, (JsonNumber)pair.Value);
                });
        }

        public virtual IDictionary<string, long> GetLanguages(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return GetLanguages(user.Login, repository.Name);
        }

        public virtual IDictionary<string, string> GetTags(string userName, string repositoryName)
        {
            return GetDictionary<string>(userName, repositoryName, "tags", 
                (pair, dictionary) =>
                {
                    if (!string.IsNullOrEmpty(pair.Key) && pair.Value is JsonString)
                        dictionary.Add(pair.Key, (JsonString)pair.Value);
                });
        }

        public virtual IDictionary<string, string> GetTags(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return GetTags(user.Login, repository.Name);
        }

        protected virtual IDictionary<string, T> GetDictionary<T>(string userName, string repositoryName, string action, Action<KeyValuePair<string, IJsonValue>, Dictionary<string, T>> addMemberAction)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);

            var data = Client.Download(string.Format("repos/show/{0}/{1}/{2}", userName, repositoryName, action));
            var json = JsonParser.Parse(data) as JsonObject;

            if (json == null || !json.Members.ContainsKey(action))
                return new Dictionary<string, T>();

            var list = json.Members[action] as JsonObject;

            if (list == null)
                return new Dictionary<string, T>();

            var dictionary = new Dictionary<string, T>();
            list.Members.Each(pair => addMemberAction(pair, dictionary));

            return dictionary;
        }

        public virtual IDictionary<string, string> GetBranches(string userName, string repositoryName)
        {
            return GetDictionary<string>(userName, repositoryName, "branches", 
                (pair, dictionary) =>
                {
                    if (!string.IsNullOrEmpty(pair.Key) && pair.Value is JsonString)
                        dictionary.Add(pair.Key, (JsonString)pair.Value);
                });
        }

        public virtual IDictionary<string, string> GetBranches(User user, Repository repository)
        {
            ValidateUserRepository(user, repository);
            return GetBranches(user.Login, repository.Name);
        }

        public virtual IEnumerable<Repository> Watched(string userName)
        {
            Validation.ValidateStringArgument(userName, "userName");

            var data = Client.Download(string.Format("repos/watched/{0}", userName));

            return data != null ?
                JsonDeserializer.Deserialize<RepositoriesContainer>(data).Repositories :
                new List<Repository>();
        }

        public virtual IEnumerable<Repository> Watched(User user)
        {
            Validation.ValidateArgument(user, "user");
            Validation.ValidateStringArgument(user.Login, "user.Login");

            var data = Client.Download(string.Format("repos/watched/{0}", user.Login));

            return data != null ?
                JsonDeserializer.Deserialize<RepositoriesContainer>(data).Repositories :
                new List<Repository>();
        }

        #region Validation

        protected static void ValidateUserRepository(User user, Repository repository)
        {
            ValidateUser(user);
            ValidateRepository(repository);
        }

        protected static void ValidateRepository(Repository repository)
        {
            Validation.ValidateArgument(repository, "repository");
            Validation.ValidateStringArgument(repository.Name, "repository.Name");
        }

        protected static void ValidateUser(User user)
        {
            Validation.ValidateArgument(user, "user");
            Validation.ValidateStringArgument(user.Login, "user.Login");
        }

        protected static void ValidateSearch(string searchTerm)
        {
            Validation.ValidateStringArgument(searchTerm, "searchTerm");
        }

        #endregion

        #region Protected Classes

        protected class CollaboratorsContainer
        {
            [JsonName("collaborators")]
            public List<string> Collaborators { get; set; }
        }

        protected class DeployKeysContainer
        {
            [JsonName("public_keys")]
            public List<PublicKey> PublicKeys { get; set; }
        }

        protected class SearchRepositoriesContainer
        {
            [JsonName("repositories")]
            public List<SearchRepository> Repositories { get; set; }
        }

        protected class RepositoriesContainer
        {
            [JsonName("repositories")]
            public List<Repository> Repositories { get; set; }
        }

        protected class NetworkContainer
        {
            [JsonName("network")]
            public List<Repository> Repositories { get; set; }
        }

        protected class RepositoryContainer
        {
            [JsonName("repository")]
            public Repository Repository { get; set; }
        }

        protected class DeleteTokenContainer
        {
            [JsonName("delete_token")]
            public string DeleteToken { get; set; }
        }

        protected class DeleteStatusContainer
        {
            [JsonName("status")]
            public string Status { get; set; }
        }

        #endregion
    }
}
