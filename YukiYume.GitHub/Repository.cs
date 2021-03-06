﻿#region MIT License

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

namespace YukiYume.GitHub
{
    /// <summary>
    /// Encapsulates information about a GitHub Git repository
    /// See http://develop.github.com/p/repo.html for more information
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// description
        /// </summary>
        [JsonName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// number of open issues
        /// </summary>
        [JsonName("open_issues")]
        public virtual int OpenIssues { get; set; }

        /// <summary>
        /// number of watchers
        /// </summary>
        [JsonName("watchers")]
        public virtual int Watchers { get; set; }

        /// <summary>
        /// GitHub url for the repository
        /// </summary>
        [JsonName("url")]
        public virtual string Url { get; set; }

        /// <summary>
        /// homepage
        /// </summary>
        [JsonName("homepage")]
        public virtual string HomePage { get; set; }

        /// <summary>
        /// fork
        /// </summary>
        [JsonName("fork")]
        public virtual bool IsFork { get; set; }

        /// <summary>
        /// private
        /// </summary>
        [JsonName("private")]
        public virtual bool IsPrivate { get; set; }

        /// <summary>
        /// repository name
        /// </summary>
        [JsonName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// author login
        /// </summary>
        [JsonName("owner")]
        public virtual string Owner { get; set; }

        /// <summary>
        /// number of pledgies
        /// </summary>
        [JsonName("pledgie")]
        public virtual int Pledgie { get; set; }

        /// <summary>
        /// number of forks
        /// </summary>
        [JsonName("forks")]
        public virtual int Forks { get; set; }

        public override string ToString()
        {
            var infoBuider = new StringBuilder();

            infoBuider.AppendFormat("\nDescription: {0}\n", Description ?? string.Empty);
            infoBuider.AppendFormat("OpenIssues: {0}\n", OpenIssues);
            infoBuider.AppendFormat("Watchers: {0}\n", Watchers);
            infoBuider.AppendFormat("Url: {0}\n", Url ?? string.Empty);
            infoBuider.AppendFormat("HomePage: {0}\n", HomePage ?? string.Empty);
            infoBuider.AppendFormat("IsFork: {0}\n", IsFork);
            infoBuider.AppendFormat("IsPrivate: {0}\n", IsPrivate);
            infoBuider.AppendFormat("Name: {0}\n", Name ?? string.Empty);
            infoBuider.AppendFormat("Owner: {0}\n", Owner ?? string.Empty);
            infoBuider.AppendFormat("Pledgie: {0}\n", Pledgie);
            infoBuider.AppendFormat("Forks: {0}\n", Forks);

            return infoBuider.ToString();
        }
    }
}
