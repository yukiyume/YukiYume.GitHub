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
    public enum IssueStateType
    {
        Open,
        Closed,
        Unknown
    }

    public class Issue
    {
        [JsonName("number")]
        public int Number { get; set; }

        [JsonName("votes")]
        public int Votes { get; set; }

        [JsonName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonName("body")]
        public string Body { get; set; }

        [JsonName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonName("title")]
        public string Title { get; set; }

        [JsonName("closed_at")]
        public DateTime? ClosedAt { get; set; }

        [JsonName("user")]
        public string User { get; set; }

        [JsonName("labels")]
        public IEnumerable<string> Labels { get; set; }

        [JsonName("state")]
        public string State { get; set; }

        public IssueStateType IssueState
        {
            get
            {
                if (string.Compare(State, "Open", true) == 0)
                    return IssueStateType.Open;
                else if (string.Compare(State, "Closed", true) == 0)
                    return IssueStateType.Closed;
                else
                    return IssueStateType.Unknown;
            }

            set
            {
                switch (value)
                {
                    case IssueStateType.Open:
                        State = "open";
                        break;

                    case IssueStateType.Closed:
                        State = "closed";
                        break;

                    default:
                        break;
                }
            }
        }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("Number: {0}\n", Number);
            infoBuilder.AppendFormat("Votes: {0}\n", Votes);
            infoBuilder.AppendFormat("CreatedAt: {0}\n", CreatedAt);
            infoBuilder.AppendFormat("Body: {0}\n", Body ?? string.Empty);
            infoBuilder.AppendFormat("UpdatedAt: {0}\n", UpdatedAt);
            infoBuilder.AppendFormat("Title: {0}\n", Title ?? string.Empty);
            infoBuilder.AppendFormat("ClosedAt: {0}\n", ClosedAt.HasValue ? ClosedAt.Value.ToString() : string.Empty);
            infoBuilder.AppendFormat("User: {0}\n", User);
            infoBuilder.Append("Labels: \n");
            if (Labels != null)
                Labels.Each(label => infoBuilder.AppendFormat("\t{0}\n", label));
            infoBuilder.AppendFormat("State: {0}\n", State);

            return infoBuilder.ToString();
        }
    }
}
