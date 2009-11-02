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
using log4net;
using YukiYume.Json;

#endregion

namespace YukiYume.GitHub.Json
{
    /// <summary>
    /// JSON implementation of INetworkService
    /// </summary>
    public class JsonNetworkService : BaseService, INetworkService
    {
        public JsonNetworkService()
            : base(FormatType.Json)
        {
        }

        public JsonNetworkService(string gitHubUserName, string gitHubApiToken)
            : base(FormatType.Json, gitHubUserName, gitHubApiToken)
        {
        }

        public virtual NetworkMeta GetNetworkMeta(string userName, string repository)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repository);
            var data = Client.DownloadNetwork(string.Format("{0}/{1}/network_meta", userName, repository));

            return !string.IsNullOrEmpty(data) ?
                           JsonDeserializer.Deserialize<NetworkMeta>(data) :
                           null;
        }

        public virtual NetworkMeta GetNetworkMeta(User user, Repository repository)
        {
            Validation.ValidateUserRepository(user, repository);
            return GetNetworkMeta(user.Login, repository.Name);
        }

        public virtual IEnumerable<NetworkCommit> GetNetworkData(string userName, string repository, string netHash)
        {
            ValidateNetworkData(userName, repository, netHash);
            var data = Client.DownloadNetwork(string.Format("{0}/{1}/network_data_chunk?nethash={2}", userName, repository, netHash));

            return !string.IsNullOrEmpty(data) ?
                           JsonDeserializer.Deserialize<CommitsContainer>(data).Commits :
                           new List<NetworkCommit>();
        }

        public virtual IEnumerable<NetworkCommit> GetNetworkData(User user, Repository repository, string netHash)
        {
            Validation.ValidateUserRepository(user, repository);
            return GetNetworkData(user.Login, repository.Name, netHash);
        }

        public virtual IEnumerable<NetworkCommit> GetNetworkData(string userName, string repository, string netHash, int start, int end)
        {
            ValidateNetworkData(userName, repository, netHash);
            Validation.ValidateArgument(start, arg => arg < 0, "start cannot be less than 0", "start");
            Validation.ValidateArgument(start, arg => end < start, "end cannot be less than start", "end");

            var data = Client.DownloadNetwork(string.Format("{0}/{1}/network_data_chunk?nethash={2}&start={3}&end={4}", userName, repository, netHash, start, end));

            return !string.IsNullOrEmpty(data) ?
                           JsonDeserializer.Deserialize<CommitsContainer>(data).Commits :
                           new List<NetworkCommit>();
        }

        public virtual IEnumerable<NetworkCommit> GetNetworkData(User user, Repository repository, string netHash, int start, int end)
        {
            Validation.ValidateUserRepository(user, repository);
            return GetNetworkData(user.Login, repository.Name, netHash, start, end);
        }

        #region Validation

        protected static void ValidateNetworkData(string userName, string repository, string netHash)
        {
            Validation.ValidateUserNameRepositoryNameArguments(userName, repository);
            Validation.ValidateStringArgument(netHash, "netHash");
        }

        #endregion

        #region protected classes

        protected class CommitsContainer
        {
            [JsonName("commits")]
            public List<NetworkCommit> Commits { get; set; }
        }

        #endregion
    }
}
