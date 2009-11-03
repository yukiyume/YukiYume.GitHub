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
    /// <summary>
    /// interface for services that work with the GitHub User API
    /// See http://develop.github.com/p/users.html for more information.
    /// </summary>
    public interface IUserService : IGithubService
    {
        /// <summary>
        /// Adds an e-mail address for the authenticated user
        /// </summary>
        /// <param name="email">e-mail address</param>
        /// <returns>IEnumerable&lt;string&gt; containing all e-mails for the authenticated user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> AddEmail(string email);

        /// <summary>
        /// Adds a public key for the authenticated user
        /// </summary>
        /// <param name="title">public key title</param>
        /// <param name="key">public key</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing all PublicKeys for the authenicated user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> AddKey(string title, string key);

        /// <summary>
        /// Adds a public key for the authenticated user
        /// </summary>
        /// <param name="publicKey">public key to add, publicKey.Title and publicKey.Key should be set</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing all PublicKeys for the authenicated user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> AddKey(PublicKey publicKey);

        /// <summary>
        /// Adds a user to follow for the authenticated user
        /// </summary>
        /// <param name="userName">user to follow</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the authenticated is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Follow(string userName);

        /// <summary>
        /// Adds a user to follow for the authenticated user
        /// </summary>
        /// <param name="user">user to follow, user.UserName should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the authenticated is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Follow(SearchUser user);

        /// <summary>
        /// Adds a user to follow for the authenticated user
        /// </summary>
        /// <param name="user">user to follow, user.Login should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the authenticated is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Follow(User user);

        /// <summary>
        /// Gets a list of users the specified user is following
        /// </summary>
        /// <param name="userName">user to get the following of</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the specified user is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Followers(string userName);

        /// <summary>
        /// Gets a list of users the specified user is following
        /// </summary>
        /// <param name="user">user to get the following of, user.UserName should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the specified user is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Followers(SearchUser user);

        /// <summary>
        /// Gets a list of users the specified user is following
        /// </summary>
        /// <param name="user">user to get the following of, user.Login should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the specified user is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Followers(User user);

        /// <summary>
        /// Gets a list of followers for the specified user
        /// </summary>
        /// <param name="userName">user to get the followers of</param>
        /// <returns>IEnumerable&lt;string&gt; containing all followers of the specified user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Following(string userName);

        /// <summary>
        /// Gets a list of followers for the specified user
        /// </summary>
        /// <param name="user">user to get the followers of, user.UserName should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all followers of the specified user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Following(SearchUser user);

        /// <summary>
        /// Gets a list of followers for the specified user
        /// </summary>
        /// <param name="user">user to get the followers of, user.Login should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all followers of the specified user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Following(User user);

        /// <summary>
        /// gets User information for the specified user
        /// </summary>
        /// <param name="userName">user to show</param>
        /// <returns>User if the username is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        User Get(string userName);

        /// <summary>
        /// Gets the list of e-mails for the authenticated user
        /// </summary>
        /// <returns>IEnumerable&lt;string&gt; containing the e-mails for the authenticated user</returns>
        IEnumerable<string> GetEmails();

        /// <summary>
        /// Gets the list of PublicKeys for the authenticated user
        /// </summary>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing the PublicKeys for the authenticated user</returns>
        IEnumerable<PublicKey> GetKeys();

        /// <summary>
        /// Removes the specified e-mail address for the authenticated user
        /// </summary>
        /// <param name="email">e-mail address to remove</param>
        /// <returns>IEnumerable&lt;string&gt; containing all e-mails for the authenticated user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> RemoveEmail(string email);

        /// <summary>
        /// Removes the specified public key for the authenicated user
        /// </summary>
        /// <param name="id">id of the public key to remove</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing all PublicKeys for the authenicated user</returns>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> RemoveKey(int id);

        /// <summary>
        /// Removes the specified public key for the authenicated user
        /// </summary>
        /// <param name="publicKey">PublicKey to remove, publicKey.Id should be set</param>
        /// <returns>IEnumerable&lt;PublicKey&gt; containing all PublicKeys for the authenicated user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<PublicKey> RemoveKey(PublicKey publicKey);

        /// <summary>
        /// searches for all users that match the specified name
        /// </summary>
        /// <param name="searchName">term to search for users on</param>
        /// <returns>IEnumerable&lt;Search&gt; containing all matching users</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<SearchUser> Search(string searchName);

        /// <summary>
        /// Unfollows the specified user for the authenticated user
        /// </summary>
        /// <param name="userName">user to unfollow</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the authenticated is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Unfollow(string userName);

        /// <summary>
        /// Unfollows the specified user for the authenticated user
        /// </summary>
        /// <param name="user">user to unfollow, user.UserName should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the authenticated is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Unfollow(SearchUser user);

        /// <summary>
        /// Unfollows the specified user for the authenticated user
        /// </summary>
        /// <param name="user">user to unfollow, user.Login should be set</param>
        /// <returns>IEnumerable&lt;string&gt; containing all users the authenticated is following</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<string> Unfollow(User user);

        /// <summary>
        /// Updates the authenticated users information
        /// </summary>
        /// <param name="name">full name</param>
        /// <param name="email">e-mail address</param>
        /// <param name="blog">blog url</param>
        /// <param name="company">company name</param>
        /// <param name="location">location</param>
        /// <returns>new User containing the updated information</returns>
        /// <exception cref="ArgumentException"></exception>
        User Update(string name, string email, string blog, string company, string location);

        /// <summary>
        /// Updates the authenticated users information
        /// </summary>
        /// <param name="user">User containing the full name, e-mail address, blog url, company name, or location to update</param>
        /// <returns>new User containing the updated information</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        User Update(User user);
    }
}
