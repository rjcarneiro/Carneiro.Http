using Bogus;
using Microsoft.Extensions.Options;
using NUnit.Framework;

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
            var options = Options.Create<HttpOrchestratorOptions>(new HttpOrchestratorOptions
            {
                Url = _faker.Internet.Url()
            });

            var orchestrator = new HttpOrchestrator(options);

            Assert.That(orchestrator, Is.Not.Null);
        }

        [Test]
        public void CreateOptions_Success()
        {
            string url = _faker.Internet.Url();
            var options = Options.Create<HttpOrchestratorOptions>(new HttpOrchestratorOptions
            {
                Url = url
            });
            Assert.That(options, Is.Not.Null);
            Assert.That(options.Value, Is.InstanceOf<HttpOrchestratorOptions>());
            Assert.That(options.Value.Url, Is.EqualTo(url));
        }
    }
}