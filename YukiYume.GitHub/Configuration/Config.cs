using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace YukiYume.GitHub.Configuration
{
    public static class Config
    {
        private static GitHubConfigurationSection _gitHub;

        public static GitHubConfigurationSection GitHub
        {
            get
            {
                if (_gitHub == null)
                    _gitHub = ConfigurationManager.GetSection("YukiYume.GitHub") as GitHubConfigurationSection;

                return _gitHub;
            }
        }
    }
}
