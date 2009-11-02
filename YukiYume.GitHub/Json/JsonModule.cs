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
using Ninject;
using Ninject.Modules;

#endregion

namespace YukiYume.GitHub.Json
{
    /// <summary>
    /// JsonModule is a NinjectModule that binds the GitHub services to the Json implementations of them
    /// </summary>
    public class JsonModule : NinjectModule
    {
        private string GitHubUserName { get; set; }
        private string GitHubApiToken { get; set; }

        /// <summary>
        /// The default constructor will setup the GitHub services using the 
        /// GitHub username and api token as specified in the config file
        /// </summary>
        public JsonModule()
        {
        }

        /// <summary>
        /// This constructor will setup the GitHub services using the
        /// specified GitHub username and api token
        /// </summary>
        /// <param name="gitHubUserName">GitHub API username</param>
        /// <param name="gitHubApiToken">GitHub API token</param>
        public JsonModule(string gitHubUserName, string gitHubApiToken)
        {
            GitHubUserName = gitHubUserName;
            GitHubApiToken = gitHubApiToken;
        }

        public override void Load()
        {
            if (!string.IsNullOrEmpty(GitHubUserName) && !string.IsNullOrEmpty(GitHubApiToken))
            {
                Bind<IUserService>().To<JsonUserService>()
                    .WithConstructorArgument("gitHubUserName", GitHubUserName)
                    .WithConstructorArgument("gitHubApiToken", GitHubApiToken);

                Bind<IIssueService>().To<JsonIssueService>()
                    .WithConstructorArgument("gitHubUserName", GitHubUserName)
                    .WithConstructorArgument("gitHubApiToken", GitHubApiToken);

                Bind<INetworkService>().To<JsonNetworkService>()
                    .WithConstructorArgument("gitHubUserName", GitHubUserName)
                    .WithConstructorArgument("gitHubApiToken", GitHubApiToken);

                Bind<ICommitService>().To<JsonCommitService>()
                    .WithConstructorArgument("gitHubUserName", GitHubUserName)
                    .WithConstructorArgument("gitHubApiToken", GitHubApiToken);

                Bind<IObjectService>().To<JsonObjectService>()
                    .WithConstructorArgument("gitHubUserName", GitHubUserName)
                    .WithConstructorArgument("gitHubApiToken", GitHubApiToken);

                Bind<IRepositoryService>().To<JsonRepositoryService>()
                    .WithConstructorArgument("gitHubUserName", GitHubUserName)
                    .WithConstructorArgument("gitHubApiToken", GitHubApiToken);
            }
            else
            {
                Bind<IUserService>().To<JsonUserService>();
                Bind<IIssueService>().To<JsonIssueService>();
                Bind<INetworkService>().To<JsonNetworkService>();
                Bind<ICommitService>().To<JsonCommitService>();
                Bind<IObjectService>().To<JsonObjectService>();
                Bind<IRepositoryService>().To<JsonRepositoryService>();
            }
        }
    }
}
