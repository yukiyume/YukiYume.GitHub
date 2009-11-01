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

#endregion

namespace YukiYume.GitHub
{
    public interface IIssueRepository
    {
        IEnumerable<Issue> Search(string userName, string repositoryName, IssueStateType issueState, string searchTerm);
        IEnumerable<Issue> List(string userName, string repositoryName, IssueStateType issueState);
        Issue Get(string userName, string repositoryName, int number);
        Issue Open(string userName, string repositoryName, string title, string body);
        Issue ReOpen(string userName, string repositoryName, int number);
        Issue Close(string userName, string repositoryName, int number);
        Issue Edit(string userName, string repositoryName, int number, string title, string body);
        IEnumerable<string> GetLabels(string userName, string repositoryName);
        IEnumerable<string> AddLabel(string userName, string repositoryName, string label, int number);
        IEnumerable<string> RemoveLabel(string userName, string repositoryName, string label, int number);
        Comment AddComment(string userName, string repositoryName, int number, string comment);
    }
}