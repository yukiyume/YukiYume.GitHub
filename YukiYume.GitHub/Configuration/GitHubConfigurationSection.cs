using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace YukiYume.GitHub.Configuration
{
    public class GitHubConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("authentication", IsRequired = false)]
        public AuthenticationElement Authentication 
        {
            get { return this["authentication"] as AuthenticationElement; }
            set { this["authentication"] = value; }
        }

        [ConfigurationProperty("client", IsRequired = true)]
        public ClientElement Client
        {
            get { return this["client"] as ClientElement; }
            set { this["client"] = value; }
        }
    }
}
