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
using YukiYume.GitHub.Configuration;

#endregion

namespace YukiYume.GitHub
{
    /// <summary>
    /// All GitHub services should inherit from BaseService
    /// BaseService sets up a GitHubClient for inheriting classes to use
    /// </summary>
    public abstract class BaseService : IService
    {
        /// <summary>
        /// Gets or sets the GitHubClient
        /// </summary>
        public GitHubClient Client { get; set; }

        protected BaseService(FormatType format)
        {
            if (Config.GitHub.Authentication != null && !string.IsNullOrEmpty(Config.GitHub.Authentication.UserName) && !string.IsNullOrEmpty(Config.GitHub.Authentication.ApiToken))
                Client = new GitHubClient(format, Config.GitHub.Authentication.UserName, Config.GitHub.Authentication.ApiToken);
            else
                Client = new GitHubClient(format);
        }

        protected BaseService(FormatType format, string gitHubUserName, string gitHubApiToken) 
        {
            Client = new GitHubClient(format, gitHubUserName, gitHubApiToken);
        }
    }
}
