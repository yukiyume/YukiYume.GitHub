﻿#region MIT License

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
using Ninject.Core;
using NUnit.Framework;

#endregion

namespace YukiYume.GitHub.Tests
{
    /// <summary>
    /// Setup for all fixtures
    /// </summary>
    [SetUpFixture]
    public class SetupFixture
    {
        public static StandardKernel Kernel { get; set; }

        [SetUp]
        public void Setup()
        {
            // setup NInject
            Kernel = new StandardKernel(new TestModule());
        }

        [TearDown]
        public void Teardown()
        {
            Kernel.Dispose();
        }
    }
}
