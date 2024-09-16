using Autofac.Extras.Moq;
using desafio_dev.API.Core.Services;
using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Domain;
using Moq;

namespace desafio_dev_Tests
{
    public class ServiceTest
    {
        [Fact]
        public async Task Test1()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IHttpService>()
                .Setup(m => m.GetPrevisaoAtualAsync(It.IsAny<string>()))
                .ReturnsAsync(new WeatherResponse())
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.GetPrevisaoAtualAsync(It.IsAny<string>());

            //result.Should().BeNull();            
            Assert.Equal("", result?.Location.Region);
        }
    }
}