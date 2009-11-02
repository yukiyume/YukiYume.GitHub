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
    /// interface for services that work with the GitHub Issues API
    /// See http://develop.github.com/p/issues.html for more information.
    /// </summary>
    public interface IIssueService : IService
    {
        /// <summary>
        /// Searches the specified project for issues matching the issueState (Open or Closed) and the searchTerm
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="issueState">IssueState to search, Open or Closed</param>
        /// <param name="searchTerm">term to search for issues on</param>
        /// <returns>IEnumerable&lt;Issue&gt; containing matching Issues</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Issue> Search(string userName, string repositoryName, IssueStateType issueState, string searchTerm);

        /// <summary>
        /// Searches the specified project for issues matching the issueState (Open or Closed) and the searchTerm
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issueState">IssueState to search, Open or Closed</param>
        /// <param name="searchTerm">term to search for issues on</param>
        /// <returns>IEnumerable&lt;Issue&gt; containing matching Issues</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Issue> Search(User user, Repository repository, IssueStateType issueState, string searchTerm);

        /// <summary>
        /// Lists a projects issues matching the issueState (Open or Closed)
        /// </summary>
        /// <param name="userName">user to list for</param>
        /// <param name="repositoryName">repository to list for</param>
        /// <param name="issueState">IssueState to list, Open or Closed</param>
        /// <returns>IEnumerable&lt;Issue&gt; containing matching Issues</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Issue> List(string userName, string repositoryName, IssueStateType issueState);

        /// <summary>
        /// Lists a projects issues matching the issueState (Open or Closed)
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issueState">IssueState to list, Open or Closed</param>
        /// <returns>IEnumerable&lt;Issue&gt; containing matching Issues</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Issue> List(User user, Repository repository, IssueStateType issueState);

        /// <summary>
        /// Gets the data on the specified issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="number">number of the issue to get data on</param>
        /// <returns>Issue containing the specified issue if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Get(string userName, string repositoryName, int number);

        /// <summary>
        /// Gets the data on the specified issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="number">number of the issue to get data on</param>
        /// <returns>Issue containing the specified issue if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Get(User user, Repository repository, int number);

        /// <summary>
        /// Opens a new issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="title">title of the new issue</param>
        /// <param name="body">body of the new issue</param>
        /// <returns>Issue containing the new issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Open(string userName, string repositoryName, string title, string body);

        /// <summary>
        /// Opens a new issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issue">Issue containing the title and body for the new issue</param>
        /// <returns>Issue containing the new issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Open(User user, Repository repository, Issue issue);

        /// <summary>
        /// Reopens an existing issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="number">number of the issue to reopen</param>
        /// <returns>Issue containing the reopened issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue ReOpen(string userName, string repositoryName, int number);

        /// <summary>
        /// Reopens an existing issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issue">Issue containing the number to reopen</param>
        /// <returns>Issue containing the reopened issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue ReOpen(User user, Repository repository, Issue issue);

        /// <summary>
        /// Closes an issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="number">number of the issue to close</param>
        /// <returns>Issue containing the closed issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Close(string userName, string repositoryName, int number);

        /// <summary>
        /// Closes an issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issue">Issue containing the number to close</param>
        /// <returns>Issue containing the closed issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Close(User user, Repository repository, Issue issue);

        /// <summary>
        /// Edits an issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="number">number of the issue to edit</param>
        /// <param name="title">edited title of the issue</param>
        /// <param name="body">edited body of the issue</param>
        /// <returns>Issue containing the edited issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Edit(string userName, string repositoryName, int number, string title, string body);

        /// <summary>
        /// Edits an issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issue">Issue containing the number of the issue to edit along with the edited title and body</param>
        /// <returns>Issue containing the edited issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Issue Edit(User user, Repository repository, Issue issue);

        /// <summary>
        /// Gets a list of labels for the projects issues
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <returns>IEnumerable&lt;string&gt; containing the labels for the projects issues</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> GetLabels(string userName, string repositoryName);

        /// <summary>
        /// Gets a list of labels for the projects issues
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing the labels for the projects issues</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> GetLabels(User user, Repository repository);

        /// <summary>
        /// Adds a new label for the specified issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="label">label for the issue</param>
        /// <param name="number">number of the issue</param>
        /// <returns>IEnumerable&lt;string&gt; containing the labels for the issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> AddLabel(string userName, string repositoryName, string label, int number);

        /// <summary>
        /// Adds a new label for the specified issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="label">label for the issue</param>
        /// <param name="issue">Issue containing the number of the issue</param>
        /// <returns>IEnumerable&lt;string&gt; containing the labels for the issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> AddLabel(User user, Repository repository, string label, Issue issue);

        /// <summary>
        /// Removes a label from the specified issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="label">label for the issue</param>
        /// <param name="number">number of the issue</param>
        /// <returns>IEnumerable&lt;string&gt; containing the labels for the issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> RemoveLabel(string userName, string repositoryName, string label, int number);

        /// <summary>
        /// Removes a label from the specified issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="label">label for the issue</param>
        /// <param name="issue">Issue containing the number of the issue</param>
        /// <returns>IEnumerable&lt;string&gt; containing the labels for the issue</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> RemoveLabel(User user, Repository repository, string label, Issue issue);

        /// <summary>
        /// Adds a comment to the specified issue
        /// </summary>
        /// <param name="userName">user specifying the project</param>
        /// <param name="repositoryName">repository specifying the project</param>
        /// <param name="number">number of the issue</param>
        /// <param name="comment">comment to add to the issue</param>
        /// <returns>new Comment containing data on the added comment</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Comment AddComment(string userName, string repositoryName, int number, string comment);

        /// <summary>
        /// Adds a comment to the specified issue
        /// </summary>
        /// <param name="user">user specifying the project, user.Login should be set</param>
        /// <param name="repository">repository specifying the project, repository.Name should be set</param>
        /// <param name="issue">Issue containing the number of the issue</param>
        /// <param name="comment">comment to add to the issue</param>
        /// <returns>new Comment containing data on the added comment</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Comment AddComment(User user, Repository repository, Issue issue, Comment comment);
    }
}