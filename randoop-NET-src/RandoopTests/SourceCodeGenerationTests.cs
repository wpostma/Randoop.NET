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
using Randoop;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Collections;
using System.Security.Policy;
using System.Security.Permissions;
using System.Runtime.Remoting;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandoopTests
{
    [TestClass]
    public class SourceCodeGenerationTests
    {
        private static readonly string DATA = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Data.dll";
        private static readonly string XML = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.XML.dll";
        private static readonly string SECURITY = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Security.dll";
        private static readonly string DRAWING = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Drawing.dll";
        private static readonly string MSCORLIB = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\mscorlib.dll";
        private static readonly string WEB = "C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Web.dll";

        private static readonly string hello = "C:\\dev\\Randoop.NET\\ClassLibrary1\\ClassLibrary1\\bin\\Debug\\ClassLibrary1.dll";


        [TestMethod]
        public void TestAgainstSomeMicrosoftDotNetFrameworkAssemblies()
        {
            // Load some MS .NET 2.0 assemblies and fuzz them.
            //Test(DATA);
            //Test(XML);
            //Test(SECURITY);
            //Test(DRAWING);
            //Test(MSCORLIB);
            //Test(WEB);

            // Test a small sample .net 4 assembly
            Test(hello);

        }

        public void Test(string dll)
        {
            string workingDirectory = Common.TempDir.CreateTempDir();
            StringBuilder args = new StringBuilder();
            //args.Append("/dontexecute /noexplorer /outputnormal /noexplorer /timelimit:100");
            args.Append(" /timelimit:40");
            args.Append(" " + dll);
            Process p = Util.RunRandoop(args.ToString(), workingDirectory);
            if (p.ExitCode != 0)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("*** Randoop execution failed.");
                msg.Append("DLL: " + dll + ", WORKING DIRECTORY: " + workingDirectory);
                throw new TestFailedException(msg.ToString());
            }

            /*

            p = Util.CompileTests(new DirectoryInfo(workingDirectory));
            if (p.ExitCode != 0)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("*** Compilation of randoop-generated tests failed with exit code "+p.ExitCode.ToString()+" \n");
                msg.Append("Using .net assembly " + dll + ", WORKING DIRECTORY: " + workingDirectory);
                throw new TestFailedException(msg.ToString());
            }
            */

            Console.WriteLine("Deleting temporary directory " + workingDirectory);
            new DirectoryInfo(workingDirectory).Delete(true);
        }

        /// <summary>
        /// Used for testing type generation code.
        /// </summary>
        public class A<T>
        {
            public class B<X, U>
            {
                public class D<Z>
                {
                    public string foo(int i, object o)
                    {
                        return null;
                    }
                }
            }
        }

        [TestMethod]
        public void TestTypeGeneration()
        {
            Type t = typeof(A<int>.B<string, Dictionary<int, List<int>>>.D<float>);
            string expected = "RandoopTests.SourceCodeGenerationTests.A<System.Int32>.B<System.String, System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.Int32>>>.D<System.Single>";
            string actual = Common.SourceCodePrinting.ToCodeString(t);
            if (expected != actual)
                throw new TestFailedException("\nEXPECTED:" + expected + "\nACTUAL:" + actual + "\n");
        }

    }
}
