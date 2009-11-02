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
    /// Encapsulates meta information about a blob object in a GitHub repository
    /// See http://develop.github.com/p/object.html for more information
    /// </summary>
    public class Blob
    {
        /// <summary>
        /// filename of the blob
        /// </summary>
        [JsonName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// size in bytes of the blob
        /// </summary>
        [JsonName("size")]
        public virtual int Size { get; set; }

        /// <summary>
        /// SHA hash of the blob
        /// </summary>
        [JsonName("sha")]
        public virtual string Sha { get; set; }

        /// <summary>
        /// blob file mode
        /// </summary>
        [JsonName("mode")]
        public virtual string Mode { get; set; }

        /// <summary>
        /// text contents of the blob
        /// </summary>
        [JsonName("data")]
        public virtual string Data { get; set; }

        /// <summary>
        /// mime type of the blob
        /// </summary>
        [JsonName("mime_type")]
        public virtual string MimeType { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("Name: {0}\n", Name ?? string.Empty);
            infoBuilder.AppendFormat("Size: {0}\n", Size);
            infoBuilder.AppendFormat("Sha: {0}\n", Sha ?? string.Empty);
            infoBuilder.AppendFormat("Mode: {0}\n", Mode ?? string.Empty);
            infoBuilder.AppendFormat("MimeType: {0}\n", MimeType ?? string.Empty);
            infoBuilder.AppendFormat("Data: {0}\n", Data ?? string.Empty);

            return infoBuilder.ToString();
        }
    }
}
