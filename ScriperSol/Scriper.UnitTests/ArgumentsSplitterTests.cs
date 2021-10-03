﻿using NUnit.Framework;
using Scriper.Models;
using Scriper.UnitTests.Models;
using System.Collections.Generic;

namespace Scriper.UnitTests
{
    public class ArgumentsSplitterTests : TestsBase
    {
        private readonly ScriperContainer _scriperContainer;

        public ArgumentsSplitterTests()
        {
            _scriperContainer = new TestScriperContainer(filePath, uiFilePath);
        }

        [TestCaseSource(typeof(ArgumentsSourceData))]
        public void SplittingTest(string arguments, List<string> expectedResult)
        {
            var argumentsSplitter = _scriperContainer.GetInstance<IArgumentsSplitter>();
            var result = argumentsSplitter.SplitArguments(arguments);
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestCaseSource(typeof(ArgumentsSourceData))]
        public void JoiningTest(string expectedResult, List<string> arguments)
        {
            var argumentsSplitter = _scriperContainer.GetInstance<IArgumentsSplitter>();
            var result = argumentsSplitter.JoinArguments(arguments);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
