using NUnit.Framework;
using System;
using System.IO;
using XMindAPI.Configuration;
using XMindAPI.Writers;
using FluentAssertions;
using XMindAPI;

namespace Tests
{
    [TestFixture]
    public class XMindConfigurationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateDefaultMetaFile_DefaultCreate_Success()
        {
            var config = new XMindConfiguration()
                .WriteTo
                .Writer(new LoggerWriter()
                        .SetOutputName(new LoggerWriterOutput("root")));
           
        }
    }
}