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
using YukiYume.Json;

#endregion

namespace YukiYume.GitHub
{
    /// <summary>
    /// Encapsulates information about a comment on a GitHub issue
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the comments text
        /// </summary>
        [JsonName("comment")]
        public virtual string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the status of the comment, "saved" if successfully added
        /// </summary>
        [JsonName("status")]
        public virtual string Status { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("Comment: {0}\n", CommentText ?? string.Empty);
            infoBuilder.AppendFormat("Status: {0}\n", Status ?? string.Empty);

            return infoBuilder.ToString();
        }
    }
}
