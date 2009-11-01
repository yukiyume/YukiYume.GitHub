using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YukiYume.Json;

namespace YukiYume.GitHub
{
    public class PublicKey
    {
        [JsonName("title")]
        public string Title { get; set; }

        [JsonName("id")]
        public int Id { get; set; }

        [JsonName("key")]
        public string Key { get; set; }

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("Title: {0}\n", Title ?? string.Empty);
            infoBuilder.AppendFormat("Id: {0}\n", Id);
            infoBuilder.AppendFormat("Key: {0}\n", Key ?? string.Empty);

            return infoBuilder.ToString();
        }
    }
}
