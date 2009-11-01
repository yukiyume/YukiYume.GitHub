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
    public interface IGitRepository
    {
        IEnumerable<SearchRepository> Search(string searchTerm);

        Repository Get(string userName, string repositoryName);
        Repository Get(User user, Repository repository);

        IEnumerable<Repository> GetAll(string userName);
        IEnumerable<Repository> GetAll(User user);

        Repository Watch(string userName, string repositoryName);
        Repository Watch(User user, Repository repository);

        Repository Unwatch(string userName, string repositoryName);
        Repository Unwatch(User user, Repository repository);

        Repository Fork(string userName, string repositoryName);
        Repository Fork(User user, Repository repository);

        Repository Create(string name, string description, string homePage, bool isPublic);
        Repository Create(Repository repository);

        string Delete(string repositoryName);
        string Delete(Repository repository);

        string ConfirmDelete(string repositoryName, string confirmationToken);
        string ConfirmDelete(Repository repository, string confirmationToken);

        Repository SetPrivate(string repositoryName);
        Repository SetPrivate(Repository repository);

        Repository SetPublic(string repositoryName);
        Repository SetPublic(Repository repository);

        IEnumerable<PublicKey> GetKeys(string repositoryName);
        IEnumerable<PublicKey> GetKeys(Repository repository);

        IEnumerable<PublicKey> AddKey(string repositoryName, string title, string key);
        IEnumerable<PublicKey> AddKey(Repository repository, PublicKey publicKey);

        IEnumerable<PublicKey> RemoveKey(string repositoryName, int id);
        IEnumerable<PublicKey> RemoveKey(Repository repository, PublicKey publicKey);

        IEnumerable<string> GetCollaborators(string userName, string repositoryName);
        IEnumerable<string> GetCollaborators(User user, Repository repository);

        IEnumerable<string> AddCollaborator(string repositoryName, string userName);
        IEnumerable<string> AddCollaborator(Repository repository, User user);

        IEnumerable<string> RemoveCollaborator(string repositoryName, string userName);
        IEnumerable<string> RemoveCollaborator(Repository repository, User user);

        IEnumerable<Repository> GetNetwork(string userName, string repositoryName);
        IEnumerable<Repository> GetNetwork(User user, Repository repository);

        IDictionary<string, long> GetLanguages(string userName, string repositoryName);
        IDictionary<string, long> GetLanguages(User user, Repository repository);

        IDictionary<string, string> GetTags(string userName, string repositoryName);
        IDictionary<string, string> GetTags(User user, Repository repository);

        IDictionary<string, string> GetBranches(string userName, string repositoryName);
        IDictionary<string, string> GetBranches(User user, Repository repository);

        IEnumerable<Repository> Watched(string userName);
        IEnumerable<Repository> Watched(User user);
    }
}
