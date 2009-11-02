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
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using log4net;
using YukiYume.Logging;
using YukiYume.GitHub.Configuration;

#endregion

namespace YukiYume.GitHub
{
    public sealed class GitHubClient
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GitHubClient));

        public FormatType FormatType { get; private set; }
        public NameValueCollection LoginInfo { get; private set; }

        private static readonly Semaphore WebClientPool = new Semaphore(Config.GitHub.Client.PoolSize, Config.GitHub.Client.PoolSize);
        private static readonly Queue<WebClient> WebClientQueue = new Queue<WebClient>(Config.GitHub.Client.PoolSize);
        private static readonly object WebClientQueuePadlock = new object();

        static GitHubClient()
        {
            Config.GitHub.Client.PoolSize.Times(() => WebClientQueue.Enqueue(new WebClient()));
        }

        public GitHubClient(FormatType formatType)
        {
            FormatType = formatType;

            if (Config.GitHub.Authentication != null && !string.IsNullOrEmpty(Config.GitHub.Authentication.UserName) && !string.IsNullOrEmpty(Config.GitHub.Authentication.ApiToken))
                LoginInfo = new NameValueCollection { { "login", Config.GitHub.Authentication.UserName }, { "token", Config.GitHub.Authentication.ApiToken } };
        }

        public GitHubClient(FormatType formatType, string gitHubUserName, string gitHubApiToken)
        {
            LoginInfo = new NameValueCollection { { "login", gitHubUserName }, { "token", gitHubApiToken } };
            FormatType = formatType;
        }

        public string Download(string action)
        {
            return Download(action, false);
        }

        public string Download(string action, bool isAuthenticating)
        {
            return Download(action, isAuthenticating, null);
        }

        public string Download(string action, bool isAuthenticating, NameValueCollection values)
        {
            var url = string.Format(Config.GitHub.Client.ApiUrlFormat, isAuthenticating ? "s" : string.Empty, Format.TypeToString(FormatType), action);

            return Download(action, isAuthenticating, values, url);
        }

        public string DownloadNetwork(string action)
        {
            var url = string.Format(Config.GitHub.Client.NetworkUrlFormat, action);
            return Download(action, false, null, url);
        }

        public string Download(string action, bool isAuthenticating, NameValueCollection values, string url)
        {
            var data = DownloadData(action, isAuthenticating, values, url);

            return data != null ? Encoding.UTF8.GetString(data) : null;
        }

        public byte[] DownloadData(string action)
        {
            return DownloadData(action, false);
        }

        public byte[] DownloadData(string action, bool isAuthenticating)
        {
            return DownloadData(action, false, null);
        }

        public byte[] DownloadData(string action, bool isAuthenticating, NameValueCollection values)
        {
            var url = string.Format(Config.GitHub.Client.ApiUrlFormat, isAuthenticating ? "s" : string.Empty, Format.TypeToString(FormatType), action);

            return DownloadData(action, isAuthenticating, values, url);
        }

        public byte[] DownloadData(string action, bool isAuthenticating, NameValueCollection values, string url)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var parameters = GetParameters(isAuthenticating, values);

            if (Log.IsDebugEnabled)
                Log.Debug("Downloading from Url: {0}...", url);

            var client = GetClient();
            byte[] data = null;

            try
            {
                data = parameters != null ? client.UploadValues(url, parameters) : client.DownloadData(url);
            }
            catch (WebException webException)
            {
                if (Log.IsDebugEnabled)
                    Log.Debug(webException.Message, webException);
            }

            ReleaseClient(client);

            return data;
        }

        private static WebClient GetClient()
        {
            WebClientPool.WaitOne();
            WebClient client;

            lock (WebClientQueuePadlock)
            {
                client = WebClientQueue.Dequeue();
            }

            return client;
        }

        private static void ReleaseClient(WebClient client)
        {
            lock (WebClientQueuePadlock)
            {
                WebClientQueue.Enqueue(client);
            }

            WebClientPool.Release();
        }

        private NameValueCollection GetParameters(bool isAuthenticating, NameValueCollection values)
        {
            NameValueCollection parameters = null;

            if (values != null)
            {
                if (isAuthenticating)
                    values.Add(LoginInfo);

                parameters = values;
            }
            else if (isAuthenticating)
                parameters = LoginInfo;

            return parameters;
        }
    }
}
