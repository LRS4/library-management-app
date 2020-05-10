using Google.Protobuf.WellKnownTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ILS.Library.Web.Tests.IntegrationTests.Core
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        /// <summary>
        /// Returns the value of the given parameter from the runsettings file
        /// unless it's null or empty, if so returns value of an environment 
        /// variable with the same name as the runsettings key
        /// </summary>
        /// <param name="testParameters">The parameters property of the test execution context</param>
        /// <param name="name">The key identifying the value to be returned</param>
        /// <returns>The string value of the parameter from either the TestParameters section
        /// of the runsettings file or the environment variable</returns>
        public static string GetAndCheckEnvironment(
            this TestParameters testParameters,
            string name)
        {
            var value = testParameters[name];
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable(name);

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = Environment.GetEnvironmentVariable(
                        name, EnvironmentVariableTarget.User);
                }
            }

            return value;
        }

        /// <summary>
        /// Suffixes the path segments indicated by <paramref name="args" /> to the URI
        /// indicated by <paramref name="uri" /> and removes the trailing slash
        /// </summary>
        /// <param name="uri">The Uri object to which the path segments are being added</param>
        /// <param name="args">The path segments to be added to <paramref name="uri"/></param>
        /// <returns>A valid Uri object</returns>
        public static string Combine(
            this Uri uri,
            params string[] args)
        {
            string newUri = uri.ToString();

            if (newUri.EndsWith("/"))
            {
                newUri = newUri.Remove(newUri.Length - 1);
            }

            foreach (var arg in args)
            {
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    newUri = string.Format("{0}/{1}", newUri, arg.Replace("/", ""));
                }
            }

            return newUri;
        }
    }
}
