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

#endregion

namespace YukiYume.GitHub
{
    /// <summary>
    /// The GitHub class provides an easy way of working with the various GitHub services
    /// The easiest way of working with it is to use Ninject, such as in the following:
    /// var kernel = new StandardKernel(new JsonModule());
    /// var gitHub = kernel.Get&lt;GitHub&gt;();
    /// or if you want to use a GitHub API username / token different from the default
    /// var kernel = new StandardKernel(new JsonModule("username", "apitoken"));
    /// var gitHub = kernel.Get&lt;GitHub&gt;();
    /// then with your gitHub object, you can use the various services:
    /// var user = gitHub.UserService.Get("someusername");
    /// etc.
    /// </summary>
    public sealed class GitHub
    {
        public IUserService UserService { get; private set; }
        public IRepositoryService RepositoryService { get; private set; }
        public INetworkService NetworkService { get; private set; }
        public IIssueService IssueService { get; private set; }
        public IObjectService ObjectService { get; private set; }
        public ICommitService CommitService { get; private set; }

        [Inject]
        public GitHub(IUserService userService, IRepositoryService repositoryService, INetworkService networkService,
                      IIssueService issueService, IObjectService objectService, ICommitService commitService)
        {
            UserService = userService;
            RepositoryService = repositoryService;
            NetworkService = networkService;
            IssueService = issueService;
            ObjectService = objectService;
            CommitService = commitService;
        }
    }
}
