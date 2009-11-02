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
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        #region updateable info

        [JsonName("name")]
        public string Name { get; set; }

        [JsonName("company")]
        public string Company { get; set; }

        [JsonName("email")]
        public string Email { get; set; }

        [JsonName("blog")]
        public string Blog { get; set; }

        [JsonName("location")]
        public string Location { get; set; }

        #endregion

        [JsonName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonName("public_gist_count")]
        public int PublicGistCount { get; set; }

        [JsonName("public_repo_count")]
        public int PublicRepositoryCount { get; set; }

        [JsonName("following_count")]
        public int FollowingCount { get; set; }

        [JsonName("id")]
        public int Id { get; set; }

        [JsonName("followers_count")]
        public int FollowersCount { get; set; }

        [JsonName("login")]
        public string Login { get; set; }

        #region authenticated only info

        [JsonName("total_private_repo_count")]
        public int TotalPrivateRepositoryCount { get; set; }

        [JsonName("collaborators")]
        public int Collaborators { get; set; }

        [JsonName("disk_usage")]
        public int DiskUsage { get; set; }

        [JsonName("owned_private_repo_count")]
        public int OwnedPrivateRepositoryCount { get; set; }

        [JsonName("private_gist_count")]
        public int PrivateGistCount { get; set; }

        [JsonName("plan")]
        public UserPlan Plan { get; set; }

        #endregion

        public override string ToString()
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendFormat("Name: {0}\n", Name ?? string.Empty);
            infoBuilder.AppendFormat("Company: {0}\n", Company ?? string.Empty);
            infoBuilder.AppendFormat("Email: {0}\n", Email ?? string.Empty);
            infoBuilder.AppendFormat("Blog: {0}\n", Blog ?? string.Empty);
            infoBuilder.AppendFormat("Location: {0}\n", Location ?? string.Empty);
            infoBuilder.AppendFormat("CreatedAt: {0}\n", CreatedAt);
            infoBuilder.AppendFormat("PublicGistCount: {0}\n", PublicGistCount);
            infoBuilder.AppendFormat("PublicRepositoryCount: {0}\n", PublicRepositoryCount);
            infoBuilder.AppendFormat("FollowingCount: {0}\n", FollowingCount);
            infoBuilder.AppendFormat("Id: {0}\n", Id);
            infoBuilder.AppendFormat("FollowersCount: {0}\n", FollowersCount);
            infoBuilder.AppendFormat("Login: {0}\n", Login ?? string.Empty);
            infoBuilder.AppendFormat("TotalPrivateRepositoryCount: {0}\n", TotalPrivateRepositoryCount);
            infoBuilder.AppendFormat("Collaborators: {0}\n", Collaborators);
            infoBuilder.AppendFormat("DiskUsage: {0}\n", DiskUsage);
            infoBuilder.AppendFormat("OwnedPrivateRepositoryCount: {0}\n", OwnedPrivateRepositoryCount);
            infoBuilder.AppendFormat("PrivateGistCount: {0}\n", PrivateGistCount);
            infoBuilder.AppendFormat("Plan: {0}\n", Plan);

            return infoBuilder.ToString();
        }
    }
}
