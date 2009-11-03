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
    /// interface for services that work with the GitHub Network API
    /// See http://develop.github.com/p/network.html for more information.
    /// </summary>
    public interface INetworkService : IGithubService
    {
        /// <summary>
        /// Gets meta information about a GitHub repository's network
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repository">GitHub Repository name</param>
        /// <returns>NetworkMeta containing information about the repository's network if the repository is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        NetworkMeta GetNetworkMeta(string userName, string repository);

        /// <summary>
        /// Gets meta information about a GitHub repository's network
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <returns>NetworkMeta containing information about the repository's network if the repository is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        NetworkMeta GetNetworkMeta(User user, Repository repository);

        /// <summary>
        /// Gets network commit data for a GitHub repository
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repository">GitHub Repository name</param>
        /// <param name="netHash">nethash of the network, which can be obtained from NetworkMeta</param>
        /// <returns>IEnumerable&lt;NetworkCommit&gt; containing the first 100 commits by branch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<NetworkCommit> GetNetworkData(string userName, string repository, string netHash);

        /// <summary>
        /// Gets network commit data for a GitHub repository
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="netHash">nethash of the network, which can be obtained from NetworkMeta</param>
        /// <returns>IEnumerable&lt;NetworkCommit&gt; containing the first 100 commits by branch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<NetworkCommit> GetNetworkData(User user, Repository repository, string netHash);

        /// <summary>
        /// Gets network commit data for a GitHub repository
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repository">GitHub Repository name</param>
        /// <param name="netHash">nethash of the network, which can be obtained from NetworkMeta</param>
        /// <param name="start">starting position in the NetworkMeta to get commits for</param>
        /// <param name="end">ending position in the NetworkMeta to get commits for</param>
        /// <returns>IEnumerable&lt;NetworkCommit&gt; containing the commits in the start-end range by branch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<NetworkCommit> GetNetworkData(string userName, string repository, string netHash, int start, int end);

        /// <summary>
        /// Gets network commit data for a GitHub repository
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="netHash">nethash of the network, which can be obtained from NetworkMeta</param>
        /// <param name="start">starting position in the NetworkMeta to get commits for</param>
        /// <param name="end">ending position in the NetworkMeta to get commits for</param>
        /// <returns>IEnumerable&lt;NetworkCommit&gt; containing the commits in the start-end range by branch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<NetworkCommit> GetNetworkData(User user, Repository repository, string netHash, int start, int end);
    }
}
