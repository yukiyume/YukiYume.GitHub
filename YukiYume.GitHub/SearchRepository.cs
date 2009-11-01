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
    public class SearchRepository
    {
        [JsonName("name")]
        public virtual string Name { get; set; }

        [JsonName("size")]
        public virtual int Size { get; set; }

        [JsonName("followers")]
        public virtual int Followers { get; set; }

        [JsonName("username")]
        public virtual string UserName { get; set; }

        [JsonName("language")]
        public virtual string Language { get; set; }

        [JsonName("fork")]
        public virtual bool IsFork { get; set; }

        [JsonName("id")]
        public virtual string Id { get; set; }

        [JsonName("type")]
        public virtual string Type { get; set; }

        [JsonName("pushed")]
        public virtual DateTime Pushed { get; set; }

        [JsonName("forks")]
        public virtual int Forks { get; set; }

        [JsonName("description")]
        public virtual string Description { get; set; }

        [JsonName("score")]
        public virtual double Score { get; set; }

        [JsonName("created")]
        public virtual DateTime Created { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("\nName: {0}\n", Name ?? string.Empty);
            infoBuilder.AppendFormat("Size: {0}\n", Size);
            infoBuilder.AppendFormat("Followers: {0}\n", Followers);
            infoBuilder.AppendFormat("UserName: {0}\n", UserName ?? string.Empty);
            infoBuilder.AppendFormat("Language: {0}\n", Language ?? string.Empty);
            infoBuilder.AppendFormat("IsFork: {0}\n", IsFork);
            infoBuilder.AppendFormat("Id: {0}\n", Id ?? string.Empty);
            infoBuilder.AppendFormat("Type: {0}\n", Type ?? string.Empty);
            infoBuilder.AppendFormat("Pushed: {0}\n", Pushed);
            infoBuilder.AppendFormat("Forks: {0}\n", Forks);
            infoBuilder.AppendFormat("Description: {0}\n", Description ?? string.Empty);
            infoBuilder.AppendFormat("Score: {0}\n", Score);
            infoBuilder.AppendFormat("Created: {0}\n", Created);

            return infoBuilder.ToString();
        }
    }
}
