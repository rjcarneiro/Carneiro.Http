using Bogus;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace Carneiro.Http.Tests.Core
{
    public class DotnetCoreTest
    {
        private Faker _faker;

        [SetUp]
        public void Setup()
        {
            _faker = new Faker();
        }

        [Test]
        public void CreateObject_Success()
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .RegisterHttpOrchestrator(_faker.Internet.Url()).BuildServiceProvider();

            HttpOrchestrator httpOrchestrator = serviceProvider.GetService<HttpOrchestrator>();

            Assert.That(httpOrchestrator, Is.Not.Null);
        }
    }
}