using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace YukiYume.GitHub.Configuration
{
    public class AuthenticationElement : ConfigurationElement
    {
        [ConfigurationProperty("userName", IsRequired = false)]
        public string UserName
        {
            get { return this["userName"] as string; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("apiToken", IsRequired = false)]
        public string ApiToken
        {
            get { return this["apiToken"] as string; }
            set { this["apiToken"] = value; }
        }
    }
}
