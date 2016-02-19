//*********************************************************
//
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Apache License, Version 2.0.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandoopTests
{
    [TestClass]
    public class SimpleMatcherTests
    {
        [TestMethod]

        public void TestWildcardMatcher()
        {
            string matcher;
            
            matcher = "foo";
            
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "foo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "f oo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " foo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " foo "));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "foo "));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "fooa"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "afoo"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "a foo"));

            matcher = "*foo";
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "foo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fo o"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " foo "));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "fooa"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "afoo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "af oo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " afoo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " af oo"));

            matcher = "foo*";
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "foo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " foo "));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fooa"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fooa "));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fo oa "));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "afoo"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "afo o"));

            matcher = "*foo*";
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "foo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, " foo "));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fooa"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "afoo"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "afoob"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "a f o o b"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "afocob"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "a f o c o b"));

            matcher = "fo|*|o*";
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "fo*"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fo*o"));
            Assert.IsTrue(true == WildcardMatcher.Matches(matcher, "fo*oa"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "foao"));
            Assert.IsTrue(false == WildcardMatcher.Matches(matcher, "foaoa"));
        }
    }
}

