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
    /// Encapsulates GitHub commit information
    /// See http://develop.github.com/p/commits.html for more information
    /// </summary>
    public class Commit
    {
        /// <summary>
        /// list of files added in the commit
        /// </summary>
        [JsonName("added")]
        public virtual IEnumerable<CommitFile> Added { get; set; }

        /// <summary>
        /// list of files modified in the commit
        /// </summary>
        [JsonName("modified")]
        public virtual IEnumerable<CommitFile> Modified { get; set; }

        /// <summary>
        /// list of files removed in the commit
        /// </summary>
        [JsonName("removed")]
        public virtual IEnumerable<CommitFile> Removed { get; set; }

        /// <summary>
        /// parents of the commit
        /// </summary>
        [JsonName("parents")]
        public virtual IEnumerable<CommitParent> Parents { get; set; }

        /// <summary>
        /// commit author
        /// </summary>
        [JsonName("author")]
        public virtual CommitAuthor Author { get; set; }

        /// <summary>
        /// GitHub url for the commit
        /// </summary>
        [JsonName("url")]
        public virtual string Url { get; set; }

        /// <summary>
        /// SHA for the commit
        /// </summary>
        [JsonName("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// date of the commit
        /// </summary>
        [JsonName("committed_date")]
        public virtual DateTime CommittedDate { get; set; }

        /// <summary>
        /// authored date
        /// </summary>
        [JsonName("authored_date")]
        public virtual DateTime AuthoredDate { get; set; }

        /// <summary>
        /// commit message
        /// </summary>
        [JsonName("message")]
        public virtual string Message { get; set; }

        /// <summary>
        /// tree SHA for the commit
        /// </summary>
        [JsonName("tree")]
        public virtual string Tree { get; set; }

        /// <summary>
        /// committer
        /// </summary>
        [JsonName("committer")]
        public virtual CommitAuthor Committer { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.Append("\nAdded:\n");
            if (Added != null)
                Added.Each(commitFile => infoBuilder.AppendFormat("{0}", commitFile));

            infoBuilder.Append("Modified:\n");
            if (Modified != null)
                Modified.Each(commitFile => infoBuilder.AppendFormat("{0}", commitFile));

            infoBuilder.Append("Removed:\n");
            if (Removed != null)
                Removed.Each(commitFile => infoBuilder.AppendFormat("{0}", commitFile));

            infoBuilder.Append("Parents:\n");
            if (Parents != null)
                Parents.Each(parent => infoBuilder.AppendFormat("{0}", parent));

            infoBuilder.AppendFormat("Author: \n{0}", Author != null ? Author.ToString() : string.Empty);
            infoBuilder.AppendFormat("Url: {0}\n", Url ?? string.Empty);
            infoBuilder.AppendFormat("Id: {0}\n", Id ?? string.Empty);
            infoBuilder.AppendFormat("CommittedDate: {0}\n", CommittedDate);
            infoBuilder.AppendFormat("AuthoredDate: {0}\n", AuthoredDate);
            infoBuilder.AppendFormat("Message: {0}\n", Message ?? string.Empty);
            infoBuilder.AppendFormat("Tree: {0}\n", Tree ?? string.Empty);
            infoBuilder.AppendFormat("Committer: \n{0}", Committer != null ? Committer.ToString() : string.Empty);

            return infoBuilder.ToString();
        }
    }
}
