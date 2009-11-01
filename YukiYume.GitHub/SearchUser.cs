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
    public class SearchUser
    {
        [JsonName("name")]
        public virtual string Name { get; set; }

        [JsonName("location")]
        public virtual string Location { get; set; }

        [JsonName("followers")]
        public virtual int Followers { get; set; }

        [JsonName("username")]
        public virtual string UserName { get; set; }

        [JsonName("language")]
        public virtual string Language { get; set; }

        [JsonName("fullname")]
        public virtual string FullName { get; set; }

        [JsonName("repos")]
        public virtual int Repositories { get; set; }

        [JsonName("id")]
        public virtual string Id { get; set; }

        [JsonName("type")]
        public virtual string Type { get; set; }

        [JsonName("pushed")]
        public virtual DateTime Pushed { get; set; }

        [JsonName("score")]
        public virtual double Score { get; set; }

        [JsonName("created")]
        public virtual DateTime Created { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("Name: {0}\n", Name ?? string.Empty);
            infoBuilder.AppendFormat("Location: {0}\n", Location ?? string.Empty);
            infoBuilder.AppendFormat("Followers: {0}\n", Followers);
            infoBuilder.AppendFormat("UserName: {0}\n", UserName ?? string.Empty);
            infoBuilder.AppendFormat("Language: {0}\n", Language ?? string.Empty);
            infoBuilder.AppendFormat("FullName: {0}\n", FullName ?? string.Empty);
            infoBuilder.AppendFormat("Repositories: {0}\n", Repositories);
            infoBuilder.AppendFormat("Id: {0}\n", Id ?? string.Empty);
            infoBuilder.AppendFormat("Type: {0}\n", Type ?? string.Empty);
            infoBuilder.AppendFormat("Pushed: {0}\n", Pushed);
            infoBuilder.AppendFormat("Score: {0}\n", Score);
            infoBuilder.AppendFormat("Created: {0}\n", Created);

            return infoBuilder.ToString();
        }
    }
}
