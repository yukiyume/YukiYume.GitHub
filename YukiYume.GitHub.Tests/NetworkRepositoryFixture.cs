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
using NUnit.Framework;
using log4net;
using YukiYume.GitHub.Configuration;

#endregion

namespace YukiYume.GitHub.Tests
{
    [TestFixture]
    public class NetworkRepositoryFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NetworkRepositoryFixture));

        private INetworkRepository NetworkRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            NetworkRepository = Kernel.Get<INetworkRepository>();
        }

        #region GetNetworkMeta

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkMetaNullUser()
        {
            NetworkRepository.GetNetworkMeta(null, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkMetaEmptyUser()
        {
            NetworkRepository.GetNetworkMeta(string.Empty, "repos");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkMetaNullRepository()
        {
            NetworkRepository.GetNetworkMeta("user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkMetaEmptyRepository()
        {
            NetworkRepository.GetNetworkMeta("user", string.Empty);
        }

        [Test]
        public void GetNetworkMeta()
        {
            var networkMeta = NetworkRepository.GetNetworkMeta(Config.GitHub.Authentication.UserName, "YukiYume.GitHub");
            Assert.That(networkMeta != null);
            
            Log.Info(networkMeta);
        }

        #endregion

        #region GetNetworkData

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkDataNullUser()
        {
            NetworkRepository.GetNetworkData(null, "repos", "nethash");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataEmptyUser()
        {
            NetworkRepository.GetNetworkData(string.Empty, "repos", "nethash");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkDataNullRepository()
        {
            NetworkRepository.GetNetworkData("user", null, "nethash");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataEmptyRepository()
        {
            NetworkRepository.GetNetworkData("user", string.Empty, "nethash");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkDataNullNetHash()
        {
            NetworkRepository.GetNetworkData("user", "repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataEmptyNetHash()
        {
            NetworkRepository.GetNetworkData("user", "repos", string.Empty);
        }

        [Test]
        public void GetNetworkData()
        {
            var networkData = NetworkRepository.GetNetworkData(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "ce0f9b4332760c9f85d7b181f1a459428abe4052");
            Assert.That(networkData != null);
            Assert.That(networkData.Count() > 0);

            networkData.Each(commit =>
            {
                Assert.That(commit.Time >= 0);
                Log.Info(commit);
            });
        }

        #endregion

        #region GetNetworkDataStartEnd

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkDataStartEndNullUser()
        {
            NetworkRepository.GetNetworkData(null, "repos", "nethash", 0, 100);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataStartEndEmptyUser()
        {
            NetworkRepository.GetNetworkData(string.Empty, "repos", "nethash", 0, 100);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkDataStartEndNullRepository()
        {
            NetworkRepository.GetNetworkData("user", null, "nethash", 0, 100);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataStartEndEmptyRepository()
        {
            NetworkRepository.GetNetworkData("user", string.Empty, "nethash", 0, 100);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetNetworkDataStartEndNullNetHash()
        {
            NetworkRepository.GetNetworkData("user", "repos", null, 0, 100);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataStartEndEmptyNetHash()
        {
            NetworkRepository.GetNetworkData("user", "repos", string.Empty, 0, 100);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataStartEndBadStart()
        {
            NetworkRepository.GetNetworkData("user", "repos", "nethash", -5, 100);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNetworkDataStartEndBadEnd()
        {
            NetworkRepository.GetNetworkData("user", "repos", "nethash", 5, 2);
        }

        [Test]
        public void GetNetworkDataStartEnd()
        {
            var networkData = NetworkRepository.GetNetworkData(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "ce0f9b4332760c9f85d7b181f1a459428abe4052", 1, 1);
            Assert.That(networkData != null);
            Assert.That(networkData.Count() > 0);

            networkData.Each(commit =>
            {
                Assert.That(commit.Time >= 0);
                Log.Info(commit);
            });
        }

        #endregion
    }
}
