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
    public class UserPlan
    {
        [JsonName("name")]
        public string Name { get; set; }

        [JsonName("collaborators")]
        public int Collaborators { get; set; }

        [JsonName("space")]
        public int Space { get; set; }

        [JsonName("private_repos")]
        public int PrivateRepositories { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("\tName: {0}\n", Name ?? string.Empty);
            infoBuilder.AppendFormat("\tCollaborators: {0}\n", Collaborators);
            infoBuilder.AppendFormat("\tSpace: {0}\n", Space);
            infoBuilder.AppendFormat("\tPrivateRepositories: {0}\n", PrivateRepositories);

            return infoBuilder.ToString();
        }
    }
}
