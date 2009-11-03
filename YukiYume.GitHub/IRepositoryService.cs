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

#endregion

namespace YukiYume.GitHub
{
    /// <summary>
    /// interface for services that work with the GitHub Repositories API
    /// See http://develop.github.com/p/repo.html for more information.
    /// </summary>
    public interface IRepositoryService : IGithubService
    {
        /// <summary>
        /// Searches for all repositories matching the searchTerm
        /// </summary>
        /// <param name="searchTerm">term to search on</param>
        /// <returns>IEnumerable&lt;SearchRepository&gt; containing all matching repositories</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<SearchRepository> Search(string searchTerm);

        /// <summary>
        /// Gets repository information for the specified project
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>new Repository containing data on the repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Get(string userName, string repositoryName);

        /// <summary>
        /// Gets repository information for the specified project
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>new Repository containing data on the repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Get(User user, Repository repository);

        /// <summary>
        /// Gets a list of repositories for the specified user
        /// </summary>
        /// <param name="userName">user to get repositories of</param>
        /// <returns>IEnumerable&lt;Repository&gt; containing data on all of the specified users repositories</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Repository> GetAll(string userName);

        /// <summary>
        /// Gets a list of repositories for the specified user
        /// </summary>
        /// <param name="user">user to get repositories of, user.Login should be set</param>
        /// <returns>IEnumerable&lt;Repository&gt; containing data on all of the specified users repositories</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Repository> GetAll(User user);

        /// <summary>
        /// watches the specified project for the authenticated user
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>new Repository containing data on the watched repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Watch(string userName, string repositoryName);

        /// <summary>
        /// watches the specified project for the authenticated user
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>new Repository containing data on the watched repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Watch(User user, Repository repository);

        /// <summary>
        /// unwatches the specified project for the authenticated user
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>new Repository containing data on the unwatched repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Unwatch(string userName, string repositoryName);

        /// <summary>
        /// unwatches the specified project for the authenticated user
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>new Repository containing data on the unwatched repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Unwatch(User user, Repository repository);

        /// <summary>
        /// Forks the specified repository for the authenticated user
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>new Repository containing data on the forked repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Fork(string userName, string repositoryName);

        /// <summary>
        /// Forks the specified repository for the authenticated user
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>new Repository containing data on the forked repository if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Fork(User user, Repository repository);

        /// <summary>
        /// Creates a new repository for the authenticated user
        /// </summary>
        /// <param name="name">name of the new repository</param>
        /// <param name="description">description of the new repository, can be null</param>
        /// <param name="homePage">homepage for the new repository, can be null</param>
        /// <param name="isPublic">true iff the new repository is a public repository</param>
        /// <returns>new Repository containing data on the created repository if successful, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Create(string name, string description, string homePage, bool isPublic);

        /// <summary>
        /// Creates a new repository for the authenticated user
        /// </summary>
        /// <param name="repository">Repository containing the name, description, homepage, and public or private data on the new repository</param>
        /// <returns>new Repository containing data on the created repository if successful, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Repository Create(Repository repository);

        /// <summary>
        /// Gets a token to confirm deletion of the specified repository for the authenticated user
        /// </summary>
        /// <param name="repositoryName">repository to delete</param>
        /// <returns>deletion confirmation token</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        string Delete(string repositoryName);

        /// <summary>
        /// Gets a token to confirm deletion of the specified repository for the authenticated user
        /// </summary>
        /// <param name="repository">Repository containing the repository name to delete</param>
        /// <returns>deletion confirmation token</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        string Delete(Repository repository);

        /// <summary>
        /// Deletes the specified repository for the authenticated user using the confirmation token
        /// </summary>
        /// <param name="repositoryName">repository to delete</param>
        /// <param name="confirmationToken">deletion confirmation token, obtained from Delete</param>
        /// <returns>states of the delete, "deleted" if the repository was deleted</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        string ConfirmDelete(string repositoryName, string confirmationToken);

        /// <summary>
        /// Deletes the specified repository for the authenticated user using the confirmation token
        /// </summary>
        /// <param name="repository">Repository containing the repository name to delete</param>
        /// <param name="confirmationToken">deletion confirmation token, obtained from Delete</param>
        /// <returns>states of the delete, "deleted" if the repository was deleted</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        string ConfirmDelete(Repository repository, string confirmationToken);

        /// <summary>
        /// Sets the specified repository to private for the authenticated user
        /// current not implemented
        /// </summary>
        /// <param name="repositoryName">repository to set to private</param>
        /// <returns>Repository data for the specified repository</returns>
        /// <exception cref="NotImplementedException"></exception>
        Repository SetPrivate(string repositoryName);

        /// <summary>
        /// Sets the specified repository to private for the authenticated user
        /// current not implemented
        /// </summary>
        /// <param name="repository">Repository containing the repository name to set to private</param>
        /// <returns>Repository data for the specified repository</returns>
        /// <exception cref="NotImplementedException"></exception>
        Repository SetPrivate(Repository repository);

        /// <summary>
        /// Sets the specified repository to public for the authenticated user
        /// current not implemented
        /// </summary>
        /// <param name="repositoryName">repository to set to public</param>
        /// <returns>Repository data for the specified repository</returns>
        /// <exception cref="NotImplementedException"></exception>
        Repository SetPublic(string repositoryName);

        /// <summary>
        /// Sets the specified repository to public for the authenticated user
        /// current not implemented
        /// </summary>
        /// <param name="repository">Repository containing the repository name to set to public</param>
        /// <returns>Repository data for the specified repository</returns>
        /// <exception cref="NotImplementedException"></exception>
        Repository SetPublic(Repository repository);

        /// <summary>
        /// Gets a list of the PublicKeys of the specified repository for the authenticated user
        /// </summary>
        /// <param name="repositoryName">repository name</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys of the repository</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> GetKeys(string repositoryName);

        /// <summary>
        /// Gets a list of the PublicKeys of the specified repository for the authenticated user
        /// </summary>
        /// <param name="repository">Repository to list keys for, repository.Name should be set</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys of the repository</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> GetKeys(Repository repository);

        /// <summary>
        /// Adds a new PublicKey to the specified repository for the authenticated user
        /// </summary>
        /// <param name="repositoryName">repository name</param>
        /// <param name="title">public key title</param>
        /// <param name="key">public key name</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys of the repository</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> AddKey(string repositoryName, string title, string key);

        /// <summary>
        /// Adds a new PublicKey to the specified repository for the authenticated user
        /// </summary>
        /// <param name="repository">Repository to add the key to, repository.Name should be set</param>
        /// <param name="publicKey">PublicKey containing the title and key of the new public key</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys of the repository</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> AddKey(Repository repository, PublicKey publicKey);

        /// <summary>
        /// Removes a PublicKey from the specified repository for the authenticated user
        /// </summary>
        /// <param name="repositoryName">repository name</param>
        /// <param name="id">id of the key to remove</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys of the repository</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> RemoveKey(string repositoryName, int id);

        /// <summary>
        /// Removes a PublicKey from the specified repository for the authenticated user
        /// </summary>
        /// <param name="repository">Repository to remove the key from, repository.Name should be set</param>
        /// <param name="publicKey">PublicKey containing the id of the key to remove</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys of the repository</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> RemoveKey(Repository repository, PublicKey publicKey);

        /// <summary>
        /// Gets a list of all collaborators for the specified project
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>IEnumerable&lt;string&gt; containing the collaborators of the project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> GetCollaborators(string userName, string repositoryName);

        /// <summary>
        /// Gets a list of all collaborators for the specified project
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing the collaborators of the project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> GetCollaborators(User user, Repository repository);

        /// <summary>
        /// Adds a collaborator to the specified project for the authenticated user
        /// </summary>
        /// <param name="repositoryName">repository name</param>
        /// <param name="userName">username to add as a collaborator</param>
        /// <returns>IEnumerable&lt;string&gt; containing the collaborators of the project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> AddCollaborator(string repositoryName, string userName);

        /// <summary>
        /// Adds a collaborator to the specified project for the authenticated user
        /// </summary>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="user">user to add as a collaborator, user.Login should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing the collaborators of the project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> AddCollaborator(Repository repository, User user);

        /// <summary>
        /// Removes a collaborator from the specified project for the authenticated user
        /// </summary>
        /// <param name="repositoryName">repository name</param>
        /// <param name="userName">username to remove as a collaborator</param>
        /// <returns>IEnumerable&lt;string&gt; containing the collaborators of the project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> RemoveCollaborator(string repositoryName, string userName);

        /// <summary>
        /// Removes a collaborator from the specified project for the authenticated user
        /// </summary>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="user">user to remove as a collaborator, user.Login should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing the collaborators of the project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> RemoveCollaborator(Repository repository, User user);

        /// <summary>
        /// Gets a list of repositories in the network for the specified project
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>IEnumerable&lt;Repository&gt; containing data on repositories in the specified project's network</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Repository> GetNetwork(string userName, string repositoryName);

        /// <summary>
        /// Gets a list of repositories in the network for the specified project
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>IEnumerable&lt;Repository&gt; containing data on repositories in the specified project's network</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Repository> GetNetwork(User user, Repository repository);

        /// <summary>
        /// Gets a list of languages used in the specified project along with the number of bytes used for each language
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>IDictionary&lt;string, long&gt; containing the specified project's language data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, long> GetLanguages(string userName, string repositoryName);

        /// <summary>
        /// Gets a list of languages used in the specified project along with the number of bytes used for each language
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>IDictionary&lt;string, long&gt; containing the specified project's language data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, long> GetLanguages(User user, Repository repository);

        /// <summary>
        /// Gets a list of tags for the specified project
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>IDictionary&lt;string, string&gt; containing the specified project's tags</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, string> GetTags(string userName, string repositoryName);

        /// <summary>
        /// Gets a list of tags for the specified project
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>IDictionary&lt;string, string&gt; containing the specified project's tags</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, string> GetTags(User user, Repository repository);

        /// <summary>
        /// Gets a list of branches for the specified project
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>IDictionary&lt;string, string&gt; containing the specified project's branches</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, string> GetBranches(string userName, string repositoryName);

        /// <summary>
        /// Gets a list of branches for the specified project
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>IDictionary&lt;string, string&gt; containing the specified project's branches</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, string> GetBranches(User user, Repository repository);

        /// <summary>
        /// Gets a list of repositories being watched by the specified user
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns>IEnumerable&lt;Repository&gt; containing the repositories being watched by the user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Repository> Watched(string userName);

        /// <summary>
        /// Gets a list of repositories being watched by the specified user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>IEnumerable&lt;Repository&gt; containing the repositories being watched by the user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Repository> Watched(User user);
    }
}
