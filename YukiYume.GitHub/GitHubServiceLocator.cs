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
using YukiYume.GitHub.Json;
using Ninject.Modules;

#endregion

namespace YukiYume.GitHub
{
    /// <summary>
    /// GitHubServiceLocator is an alternative to using the GitHub class
    /// Use GitHubService to get instances of the various GitHub services
    /// Use of the Init method is optional
    /// Usage:
    /// var userService = GitHubServiceLocator.Get&lgt;IUserService&gt;();
    /// var user = userService.Get("username");
    /// If you want to use a different NinjectModule than the default, 
    /// before getting any services, call the Init method as in the following:
    /// GitHubServiceLocator.Init(new JsonModule("username", "apitoken"));
    /// </summary>
    public static class GitHubServiceLocator
    {
        private static IKernel Kernel { get; set; }

        /// <summary>
        /// Gets an instance of a GitHub service
        /// </summary>
        /// <typeparam name="T">the type of GitHub service to get, such as IUserService</typeparam>
        /// <returns>a new instance of the specified GitHub service</returns>
        public static T Get<T>() where T : IService
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Initializes the GitHubServiceLocator with the specified NinjectModule
        /// </summary>
        /// <param name="module">the module to initialize the Ninject kernel with</param>
        public static void Init(INinjectModule module)
        {
            Kernel = new StandardKernel(module);
        }

        static GitHubServiceLocator()
        {
            Kernel = new StandardKernel(new JsonModule());
        }
    }
}
