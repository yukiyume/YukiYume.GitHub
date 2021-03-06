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
    /// Encapsulates information about a file added/removed/modified in a commit
    /// See http://develop.github.com/p/commits.html for more information
    /// </summary>
    public class CommitFile
    {
        /// <summary>
        /// filename
        /// </summary>
        [JsonName("filename")]
        public virtual string FileName { get; set; }

        /// <summary>
        /// diff (if a modification)
        /// </summary>
        [JsonName("diff")]
        public virtual string Diff { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();
            
            infoBuilder.AppendFormat("\tFileName: {0}\n", FileName ?? string.Empty);
            infoBuilder.AppendFormat("\tDiff: {0}\n", Diff ?? string.Empty);

            return infoBuilder.ToString();
        }
    }
}
