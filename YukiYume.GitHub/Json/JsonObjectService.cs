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
using YukiYume.Json;

#endregion

namespace YukiYume.GitHub.Json
{
    /// <summary>
    /// JSON implementation of IObjectService
    /// </summary>
    public class JsonObjectService : BaseService, IObjectService
    {
        public JsonObjectService()
            : base(FormatType.Json)
        {
        }

        public JsonObjectService(string gitHubUserName, string gitHubApiToken)
            : base(FormatType.Json, gitHubUserName, gitHubApiToken)
        {
        }

        public virtual IEnumerable<TreeEntry> TreeList(string userName, string repositoryName, string treeSha)
        {
            ValidateTreeListArguments(userName, repositoryName, treeSha);
            var data = Client.Download(string.Format("tree/show/{0}/{1}/{2}", userName, repositoryName, treeSha));

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<TreeContainer>(data).Tree :
                new List<TreeEntry>();
        }

        public virtual IEnumerable<TreeEntry> TreeList(User user, Repository repository, string treeSha)
        {
            Validation.ValidateUserRepository(user, repository);
            return TreeList(user.Login, repository.Name, treeSha);
        }

        public virtual Blob GetBlobMeta(string userName, string repositoryName, string treeSha, string path)
        {
            ValidateGetBlobMetaArguments(userName, repositoryName, treeSha, path);
            var data = Client.Download(string.Format("blob/show/{0}/{1}/{2}/{3}", userName, repositoryName, treeSha, path));

            return !string.IsNullOrEmpty(data) ?
                JsonDeserializer.Deserialize<BlobContainer>(data).Blob :
                null;
        }

        public virtual Blob GetBlobMeta(User user, Repository repository, string treeSha, string path)
        {
            Validation.ValidateUserRepository(user, repository);
            return GetBlobMeta(user.Login, repository.Name, treeSha, path);
        }

        public virtual byte[] GetBlob(string userName, string repositoryName, string blobSha)
        {
            ValidateGetBlobArguments(userName, repositoryName, blobSha);
            return Client.DownloadData(string.Format("blob/show/{0}/{1}/{2}", userName, repositoryName, blobSha));
        }

        public virtual byte[] GetBlob(User user, Repository repository, string blobSha)
        {
            Validation.ValidateUserRepository(user, repository);
            return GetBlob(user.Login, repository.Name, blobSha);
        }

        #region Validation

        protected static void ValidateGetBlobArguments(string userName, string repositoryName, string blobSha)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            Validation.ValidateStringArgument(blobSha, "blobSha");
        }

        protected static void ValidateGetBlobMetaArguments(string userName, string repositoryName, string treeSha, string path)
        {
            ValidateTreeListArguments(userName, repositoryName, treeSha);
            Validation.ValidateStringArgument(path, "path");
        }

        protected static void ValidateTreeListArguments(string userName, string repositoryName, string treeSha)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repositoryName);
            Validation.ValidateStringArgument(treeSha, "treeSha");
        }

        #endregion

        #region Protected Classes

        protected class TreeContainer
        {
            [JsonName("tree")]
            public List<TreeEntry> Tree { get; set; }
        }

        protected class BlobContainer
        {
            [JsonName("blob")]
            public Blob Blob { get; set; }
        }

        #endregion
    }
}
