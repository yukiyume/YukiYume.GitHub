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
using System.Configuration;
using System.Linq;
using System.Text;

#endregion

namespace YukiYume.GitHub.Configuration
{
    public class ClientElement : ConfigurationElement
    {
        [ConfigurationProperty("apiUrlFormat", IsRequired = true)]
        public string ApiUrlFormat
        {
            get { return this["apiUrlFormat"] as string; }
            set { this["apiUrlFormat"] = value; }
        }

        [ConfigurationProperty("networkUrlFormat", IsRequired = true)]
        public string NetworkUrlFormat
        {
            get { return this["networkUrlFormat"] as string; }
            set { this["networkUrlFormat"] = value; }
        }

        [ConfigurationProperty("poolSize", IsRequired = true)]
        public int PoolSize
        {
            get { return ConvertWrapper.ToInt32(this["poolSize"]); }
            set { this["poolSize"] = value; }
        }
    }
}
