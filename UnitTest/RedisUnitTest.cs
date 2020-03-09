using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ServiceLB;
using System;
using System.Threading.Tasks;

namespace UnitTest
{
    public class Tests
    {
        private ICacheService cacheService;

        [SetUp]
        public void Setup()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            cacheService = new RedisService(configuration["Redis:Host"],
                                configuration["Redis:Password"]);
        }

        [Test, Order(1)]
        public async Task TestSet()
        {
            bool result = await cacheService.Set("test", "test").ConfigureAwait(false);

            Assert.AreEqual(true, result);
        }

        [Test, Order(2)]
        public async Task TestGet()
        {
            string result = await cacheService.Get("test").ConfigureAwait(false);

            Assert.AreEqual("test", result);
        }

        [Test, Order(3)]
        public async Task TestDelete()
        {
            bool result = await cacheService.Delete("test").ConfigureAwait(false);

            Assert.AreEqual(true, result);
        }
    }
}