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
    /// Encapsulates network meta information for a GitHub repository
    /// See http://develop.github.com/p/network.html for more information
    /// </summary>
    public class NetworkMeta
    {
        /// <summary>
        /// focus
        /// </summary>
        [JsonName("focus")]
        public virtual int Focus { get; set; }

        /// <summary>
        /// nethash for the network
        /// </summary>
        [JsonName("nethash")]
        public virtual string NetHash { get; set; }

        /// <summary>
        /// dates
        /// </summary>
        [JsonName("dates")]
        public virtual IEnumerable<DateTime> Dates { get; set; }

        /// <summary>
        /// users
        /// </summary>
        [JsonName("users")]
        public virtual IEnumerable<NetworkMetaUser> Users { get; set; }

        /// <summary>
        /// blocks
        /// </summary>
        [JsonName("blocks")]
        public virtual IEnumerable<NetworkMetaBlock> Blocks { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("\nFocus: {0}\n", Focus);
            infoBuilder.AppendFormat("NetHash: {0}\n", NetHash ?? string.Empty);
            infoBuilder.Append("Dates: \n");
            if (Dates != null)
                Dates.Each(date => infoBuilder.AppendFormat("\t{0}\n", date));
            infoBuilder.Append("Users: \n");
            if (Users != null)
                Users.Each(user => infoBuilder.AppendFormat("{0}\n", user));
            infoBuilder.Append("Blocks: \n");
            if (Blocks != null)
                Blocks.Each(block => infoBuilder.AppendFormat("{0}\n", block));

            return infoBuilder.ToString();
        }
    }
}
