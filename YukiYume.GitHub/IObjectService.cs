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
    /// interface for services that work with the GitHub Object API
    /// See http://develop.github.com/p/object.html for more information.
    /// </summary>
    public interface IObjectService : IService
    {
        /// <summary>
        /// Gets the contents of a tree by tree SHA
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repositoryName">GitHub Repository name</param>
        /// <param name="treeSha">SHA for the tree</param>
        /// <returns>IEnumerable&lt;TreeEntry&gt; containing the entries in the tree</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<TreeEntry> TreeList(string userName, string repositoryName, string treeSha);

        /// <summary>
        /// Gets the contents of a tree by tree SHA
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="treeSha">SHA for the tree</param>
        /// <returns>IEnumerable&lt;TreeEntry&gt; containing the entries in the tree</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<TreeEntry> TreeList(User user, Repository repository, string treeSha);

        /// <summary>
        /// Gets the meta data about a blob by tree SHA and path
        /// </summary>
        /// <param name="userName">User associated with the GitHub repository</param>
        /// <param name="repositoryName">GitHub Repository name</param>
        /// <param name="treeSha">SHA for the tree</param>
        /// <param name="path">path for the blob</param>
        /// <returns>Blob containg the meta data for the blob if it is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Blob GetBlobMeta(string userName, string repositoryName, string treeSha, string path);

        /// <summary>
        /// Gets the meta data about a blob by tree SHA and path
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="treeSha">SHA for the tree</param>
        /// <param name="path">path for the blob</param>
        /// <returns>Blob containg the meta data for the blob if it is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Blob GetBlobMeta(User user, Repository repository, string treeSha, string path);

        /// <summary>
        /// Gets the raw contents of a blob by the blob's SHA
        /// </summary>
        /// <param name="userName">User associated with the GitHub Repository</param>
        /// <param name="repositoryName">GitHub Repository name</param>
        /// <param name="blobSha">SHA for the blob</param>
        /// <returns>byte array containing the raw data of the blob if it is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        byte[] GetBlob(string userName, string repositoryName, string blobSha);

        /// <summary>
        /// Gets the raw contents of a blob by the blob's SHA
        /// </summary>
        /// <param name="user">User associated with the GitHub Repository, user.Login should be set</param>
        /// <param name="repository">GitHub Repository, repository.Name should be set</param>
        /// <param name="blobSha">SHA for the blob</param>
        /// <returns>byte array containing the raw data of the blob if it is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        byte[] GetBlob(User user, Repository repository, string blobSha);
    }
}
