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
    /// interface for services that work with the GitHub Commit API
    /// See http://develop.github.com/p/commits.html for more information.
    /// </summary>
    public interface ICommitService : IGithubService
    {
        /// <summary>
        /// Gets a list of commits for a GitHub repository branch
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repositoryName">GitHub Repository name</param>
        /// <param name="branchName">repository branch</param>
        /// <returns>IEnumerable&lt;Commit&gt; for commits for the branch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Commit> List(string userName, string repositoryName, string branchName);

        /// <summary>
        /// Gets a list of commits for a GitHub repository branch
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="branchName">repository branch</param>
        /// <returns>IEnumerable&lt;Commit&gt; for commits for the branch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Commit> List(User user, Repository repository, string branchName);

        /// <summary>
        /// Gets a list of commits for a file in a GitHub repository branch
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repositoryName">GitHub Repository name</param>
        /// <param name="branchName">repository branch</param>
        /// <param name="path">path of the file</param>
        /// <returns>IEnumerable&lt;Commit&gt; for commits for the file</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Commit> List(string userName, string repositoryName, string branchName, string path);

        /// <summary>
        /// Gets a list of commits for a file in a GitHub repository branch
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="branchName">repository branch</param>
        /// <param name="path">path of the file</param>
        /// <returns>IEnumerable&lt;Commit&gt; for commits for the file</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Commit> List(User user, Repository repository, string branchName, string path);

        /// <summary>
        /// Gets a specific commit in a GitHub repository by commit SHA
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repositoryName">GitHub Repository name</param>
        /// <param name="sha">SHA for the commit</param>
        /// <returns>Commit for the commit if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Commit Get(string userName, string repositoryName, string sha);

        /// <summary>
        /// Gets a specific commit in a GitHub repository by commit SHA
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="sha">SHA for the commit</param>
        /// <returns>Commit for the commit if found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Commit Get(User user, Repository repository, string sha);
    }
}
