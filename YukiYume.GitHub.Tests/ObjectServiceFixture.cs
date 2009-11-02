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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using log4net;
using YukiYume.GitHub.Configuration;

#endregion

namespace YukiYume.GitHub.Tests
{
    /// <summary>
    /// Unit Tests for IObjectService
    /// </summary>
    [TestFixture]
    public class ObjectServiceFixture
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ObjectServiceFixture));
        private IObjectService ObjectRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            ObjectRepository = GitHubServiceLocator.Get<IObjectService>();
        }

        #region TreeList

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TreeListNullUserString()
        {
            ObjectRepository.TreeList(null, "repos", "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TreeListNullUser()
        {
            ObjectRepository.TreeList(null, new Repository(), "sha");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TreeListEmptyUser()
        {
            ObjectRepository.TreeList(string.Empty, "repos", "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TreeListNullRepositoryString()
        {
            ObjectRepository.TreeList("user", null, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TreeListNullRepository()
        {
            ObjectRepository.TreeList(new User(), null, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TreeListEmptyRepository()
        {
            ObjectRepository.TreeList("user", string.Empty, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TreeListNullTreeSha()
        {
            ObjectRepository.TreeList("user", "repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TreeListEmptyTreeSha()
        {
            ObjectRepository.TreeList("user", "repos", string.Empty);
        }

        [Test]
        public void TreeList()
        {
            var treeList = ObjectRepository.TreeList(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "cfb534dad9cdf552c90c41a88d276c03e6f98f24");
            Assert.That(treeList != null);
            Assert.That(treeList.Count() > 0);

            treeList.Each(tree =>
            {
                Assert.That(!string.IsNullOrEmpty(tree.Name));
                Assert.That(!string.IsNullOrEmpty(tree.Type));
                Assert.That(!string.IsNullOrEmpty(tree.Mode));
                Assert.That(!string.IsNullOrEmpty(tree.Sha));

                Log.Info(tree);
            });
        }

        [Test]
        public void TreeListEmpty()
        {
            var treeList = ObjectRepository.TreeList(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "qqqqqqqqqqqqqqqqqqqqqqqqq");
            Assert.That(treeList != null);
            Assert.That(treeList.Count() == 0);
        }

        #endregion

        #region GetBlobMeta

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobMetaNullUserString()
        {
            ObjectRepository.GetBlobMeta(null, "repos", "sha", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobMetaNullUser()
        {
            ObjectRepository.GetBlobMeta(null, new Repository(), "sha", "path");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobMetaEmptyUser()
        {
            ObjectRepository.GetBlobMeta(string.Empty, "repos", "sha", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobMetaNullRepositoryString()
        {
            ObjectRepository.GetBlobMeta("user", null, "sha", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobMetaNullRepository()
        {
            ObjectRepository.GetBlobMeta(new User(), null, "sha", "path");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobMetaEmptyRepository()
        {
            ObjectRepository.GetBlobMeta("user", string.Empty, "sha", "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobMetaNullTreeSha()
        {
            ObjectRepository.GetBlobMeta("user", "repos", null, "path");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobMetaEmptyTreeSha()
        {
            ObjectRepository.GetBlobMeta("user", "repos", string.Empty, "path");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobMetaNullPath()
        {
            ObjectRepository.GetBlobMeta("user", "repos", "sha", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobMetaEmptyPath()
        {
            ObjectRepository.GetBlobMeta("user", "repos", "sha", string.Empty);
        }

        [Test]
        public void GetBlobMeta()
        {
            var blobMeta = ObjectRepository.GetBlobMeta(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "cfb534dad9cdf552c90c41a88d276c03e6f98f24", "README");
            Assert.That(blobMeta != null);
            Assert.That(!string.IsNullOrEmpty(blobMeta.Name));
            Assert.That(!string.IsNullOrEmpty(blobMeta.Mode));
            Assert.That(!string.IsNullOrEmpty(blobMeta.MimeType));
            Assert.That(!string.IsNullOrEmpty(blobMeta.Sha));
            Assert.That(blobMeta.Size > 0);
            Assert.That(!string.IsNullOrEmpty(blobMeta.Data));

            Log.Info(blobMeta);
        }

        [Test]
        public void GetBlobMetaPng()
        {
            var blobMeta = ObjectRepository.GetBlobMeta(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "cfb534dad9cdf552c90c41a88d276c03e6f98f24", "testblob.png");
            Assert.That(blobMeta != null);
            Assert.That(!string.IsNullOrEmpty(blobMeta.Name));
            Assert.That(!string.IsNullOrEmpty(blobMeta.Mode));
            Assert.That(!string.IsNullOrEmpty(blobMeta.MimeType));
            Assert.That(!string.IsNullOrEmpty(blobMeta.Sha));
            Assert.That(blobMeta.Size > 0);
            Assert.That(!string.IsNullOrEmpty(blobMeta.Data));

            Log.Info(blobMeta);
        }

        [Test]
        public void GetBlobMetaNonExisting()
        {
            var blobMeta = ObjectRepository.GetBlobMeta(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "cfb534dad9cdf552c90c41a88d276c03e6f98f24", "READMEXXXXXXXXX");
            Assert.That(blobMeta == null);
        }

        #endregion

        #region GetBlob

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobNullUserString()
        {
            ObjectRepository.GetBlob(null, "repos", "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobNullUser()
        {
            ObjectRepository.GetBlob(null, new Repository(), "sha");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobEmptyUser()
        {
            ObjectRepository.GetBlob(string.Empty, "repos", "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobNullRepositoryString()
        {
            ObjectRepository.GetBlob("user", null, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobNullRepository()
        {
            ObjectRepository.GetBlob(new User(), null, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobEmptyRepository()
        {
            ObjectRepository.GetBlob("user", string.Empty, "sha");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetBlobNullBlobSha()
        {
            ObjectRepository.GetBlob("user", "repos", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetBlobEmptyBlobSha()
        {
            ObjectRepository.GetBlob("user", "repos", string.Empty);
        }

        [Test]
        public void GetBlobPlainText()
        {
            var blob = ObjectRepository.GetBlob(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "c29bf9752c50963cc397baa0efb0f4a2087eb250");
            Assert.That(blob != null);

            var text = Encoding.UTF8.GetString(blob);
            Assert.That(!string.IsNullOrEmpty(text));

            Log.Info(text);
        }

        [Test]
        public void GetBlobPng()
        {
            var blob = ObjectRepository.GetBlob(Config.GitHub.Authentication.UserName, "YukiYume.GitHub", "3b7c261398cb37d8f0e6487ebfb317fb578522cb");
            Assert.That(blob != null);

            using (var memoryStream = new MemoryStream(blob))
            using (var image = new Bitmap(memoryStream))
            {
                Assert.That(image != null);
                Assert.That(image.Width == 100);
                Assert.That(image.Height == 100);
            }
        }

        #endregion
    }
}
