using System.Collections.Generic;

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