﻿//
// AzureFunctionForSplunkVS
//
// Copyright (c) Microsoft Corporation
//
// All rights reserved. 
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the ""Software""), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is furnished 
// to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using AzureFunctionForSplunk.Common;
using System;
using System.Collections.Generic;

namespace AzureFunctionForSplunk.WindowsVmTelemetry
{
    public class WadAzMonLog : AzMonMessage
    {
        public WadAzMonLog(dynamic message)
        {
            Message = message;

            ResourceType = "MICROSOFT.COMPUTE/VIRTUALMACHINES";

            SplunkSourceType = "azwm:compute:vm";

            if (((IDictionary<String, Object>)message).ContainsKey("properties"))
            {
                var properties = message.properties;
                if (((IDictionary<String, Object>)properties).ContainsKey("RoleInstance"))
                {
                    string theName = message.properties.RoleInstance;

                    // if it's there at all, RoleInstance starts with _
                    if (theName.Length > 1) ResourceName = theName.Substring(1);
                }
            }

            AddStandardProperties("azwl");
        }
    }
}