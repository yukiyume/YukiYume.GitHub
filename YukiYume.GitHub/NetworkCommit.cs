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
using System.Collections;
using YukiYume.Json;

#endregion

namespace YukiYume.GitHub
{
    public class NetworkCommit
    {
        [JsonName("parents")]
        public IEnumerable<IEnumerable<object>> Parents { get; set; }

        [JsonName("author")]
        public virtual string Author { get; set; }

        [JsonName("time")]
        public virtual int Time { get; set; }

        [JsonName("id")]
        public virtual string Id { get; set; }

        [JsonName("date")]
        public virtual DateTime Date { get; set; }

        [JsonName("gravatar")]
        public virtual string Gravatar { get; set; }

        [JsonName("space")]
        public virtual int Space { get; set; }

        [JsonName("message")]
        public virtual string Message { get; set; }

        [JsonName("login")]
        public virtual string Login { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.Append("\nParents:\n");
            if (Parents != null)
                Parents.Each(parent => parent.Each(item => infoBuilder.AppendFormat("\t{0}\n", item)));

            infoBuilder.AppendFormat("Author: {0}\n", Author ?? string.Empty);
            infoBuilder.AppendFormat("Time: {0}\n", Time);
            infoBuilder.AppendFormat("Id: {0}\n", Id ?? string.Empty);
            infoBuilder.AppendFormat("Date: {0}\n", Date);
            infoBuilder.AppendFormat("Gravatar: {0}\n", Gravatar ?? string.Empty);
            infoBuilder.AppendFormat("Space: {0}\n", Space);
            infoBuilder.AppendFormat("Message: {0}\n", Message ?? string.Empty);
            infoBuilder.AppendFormat("Login: {0}\n", Login ?? string.Empty);

            return infoBuilder.ToString();
        }
    }
}
