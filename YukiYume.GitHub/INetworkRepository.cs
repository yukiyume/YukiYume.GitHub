using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YukiYume.GitHub
{
    public interface INetworkRepository
    {
        NetworkMeta GetNetworkMeta(string userName, string repository);
        IEnumerable<NetworkCommit> GetNetworkData(string userName, string repository, string netHash);
        IEnumerable<NetworkCommit> GetNetworkData(string userName, string repository, string netHash, int start, int end);
    }
}
