using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YukiYume.Json;

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
