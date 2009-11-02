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
    internal static class Validation
    {
        internal static void ValidateUserNameRepositoryNameArguments(string userName, string repositoryName)
        {
            ValidateStringArgument(userName, "userName");
            ValidateStringArgument(repositoryName, "repositoryName");
        }

        internal static void ValidateStringArgument(string argument, string argumentName)
        {
            ValidateArgument(argument, argumentName);

            if (argument.Length == 0)
                throw new ArgumentException(string.Format("{0} cannot be empty", argumentName), argumentName);
        }

        internal static void ValidateArgument<T>(T argument, string parameterName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(parameterName);
        }

        internal static void ValidateArgument<T>(T argument, Predicate<T> predicate, string message, string parameterName)
        {
            if (predicate(argument))
                throw new ArgumentException(message, parameterName);
        }

        internal static void ValidateUserRepository(User user, Repository repository)
        {
            ValidateArgument(user, "user");
            ValidateArgument(repository, "repository");
        }
    }
}
