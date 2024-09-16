using Autofac.Extras.Moq;
using AutoFixture;
using desafio_dev.API.Core.Services;
using desafio_dev.API.Core.Services.Interface;
using desafio_dev.API.Model;
using desafio_dev.API.Repository;
using FluentAssertions;
using Moq;

namespace desafio_dev_Tests
{
    public class ServiceTest
    {
        private readonly Fixture _fixture;
        public ServiceTest()
        {
            _fixture = new Fixture();            
        }
        [Fact]
        public async Task When_GetPrevisaoAtualAsync_Received_Correct_Request_Should_Be_Return_Ok()
        {
            var request = _fixture.Create<string>();
            var weatherModel = _fixture.Create<WeatherModel>();

            using var mock = AutoMock.GetLoose();
            
            
            mock.Mock<IHttpService>()
                .Setup(m => m.GetPrevisaoAtualAsync(It.Is<string>(x => x.Equals(request))))
                .ReturnsAsync(weatherModel)
                .Verifiable();

            mock.Mock<IWeatherRepository>()
                .Setup(m => m.InsertWeatherAsync(It.Is<WeatherModel>(x => x.Equals(weatherModel))))
                .Returns(Task.CompletedTask)
                .Verifiable();
            
            var service = mock.Create<Service>();

            var result = await service.GetPrevisaoAtualAsync(request);
            
            result.Should().NotBeNull();
            result?.Location.Should().BeEquivalentTo(weatherModel.Location);
            mock.Mock<IHttpService>().Verify(m => m.GetPrevisaoAtualAsync(It.Is<string>(x => x.Equals(request))), Times.Once);
            mock.Mock<IWeatherRepository>().Verify(m => m.InsertWeatherAsync(It.Is<WeatherModel>(x => x.Equals(weatherModel))), Times.Once);
        }

        [Fact]
        public async Task When_GetPrevisaoAtualAsync_Received_Incorrect_Request_Should_Be_Return_Null()
        {
            var request = _fixture.Create<string>();
            var weatherModel = _fixture.Create<WeatherModel>();



            using var mock = AutoMock.GetLoose();


            mock.Mock<IHttpService>()
                .Setup(m => m.GetPrevisaoAtualAsync(It.Is<string>(x => x.Equals(request))))
                .ReturnsAsync(default(WeatherModel))
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.GetPrevisaoAtualAsync(request);

            result.Should().BeNull();            
            mock.Mock<IHttpService>().Verify(m => m.GetPrevisaoAtualAsync(It.Is<string>(x => x.Equals(request))), Times.Once);
            mock.Mock<IWeatherRepository>().Verify(m => m.InsertWeatherAsync(It.IsAny<WeatherModel>()), Times.Never);
        }

        [Fact]
        public async Task When_GetPrevisaoEstendidaAsync_Received_Correct_Request_Should_Be_Return_Ok()
        {
            var cidade = _fixture.Create<string>();
            var dias = _fixture.Create<int>();
            var weatherForecastModel = _fixture.Create<WeatherForecastModel>();

            using var mock = AutoMock.GetLoose();

            mock.Mock<IHttpService>()
                .Setup(m => m.GetPrevisaoEstendidaAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(weatherForecastModel)
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.GetPrevisaoEstendidaAsync(cidade, dias);

            result.Should().NotBeNull();
            result?.Location.Should().BeEquivalentTo(weatherForecastModel.Location);
            mock.Mock<IHttpService>().Verify(m => m.GetPrevisaoEstendidaAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task When_GetPrevisaoEstendidaAsync_Received_Incorrect_Request_Should_Be_Return_Null()
        {
            var cidade = _fixture.Create<string>();
            var dias = _fixture.Create<int>();
            

            using var mock = AutoMock.GetLoose();


            mock.Mock<IHttpService>()
                .Setup(m => m.GetPrevisaoEstendidaAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(default(WeatherForecastModel))
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.GetPrevisaoEstendidaAsync(cidade, dias);

            result.Should().BeNull();
            mock.Mock<IHttpService>().Verify(m => m.GetPrevisaoEstendidaAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);            
        }

        [Fact]
        public async Task When_GetCacheAsync_Received_Correct_Request_And_Found_Registry_Should_Be_Return_Ok()
        {            
            var weatherForecastModel = _fixture.CreateMany<WeatherModel>().ToList();

            using var mock = AutoMock.GetLoose();

            mock.Mock<IWeatherRepository>()
                .Setup(m => m.GetCacheAsync())
                .ReturnsAsync(weatherForecastModel)
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.GetCacheAsync();

            result.Should().NotBeNull();
            result?.Count().Should().Be(weatherForecastModel.Count);
            mock.Mock<IWeatherRepository>().Verify(m => m.GetCacheAsync(), Times.Once);

        }

        [Fact]
        public async Task When_GetCacheAsync_Not_Found_Nothing_On_Repository_Should_Be_Return_Null()
        {
            
            using var mock = AutoMock.GetLoose();            

            mock.Mock<IWeatherRepository>()
                .Setup(m => m.GetCacheAsync())
                .ReturnsAsync(new List<WeatherModel>())
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.GetCacheAsync();

            result.Should().BeEmpty();            
            mock.Mock<IWeatherRepository>().Verify(m => m.GetCacheAsync(), Times.Once);
        }

        [Fact]
        public async Task When_DeleteCacheAsync_Received_Correct_Request_And_Found_Registry_Should_Be_Return_Ok()
        {
            var weatherModel = _fixture.CreateMany<WeatherModel>().ToList();

            using var mock = AutoMock.GetLoose();

            mock.Mock<IWeatherRepository>()
                .Setup(m => m.DeleteCacheAsync())
                .ReturnsAsync(weatherModel.Count)
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.DeleteCacheAsync();

            result.Should().NotBe(0);
            result.Should().Be(weatherModel.Count);
            mock.Mock<IWeatherRepository>().Verify(m => m.DeleteCacheAsync(), Times.Once);

        }

        [Fact]
        public async Task When_DeleteCacheAsync_Not_Found_Nothing_On_Repository_Should_Be_Return_Null()
        {

            using var mock = AutoMock.GetLoose();

            mock.Mock<IWeatherRepository>()
                .Setup(m => m.DeleteCacheAsync())
                .ReturnsAsync(0)
                .Verifiable();

            var service = mock.Create<Service>();

            var result = await service.DeleteCacheAsync();

            result.Should().Be(0);            
            mock.Mock<IWeatherRepository>().Verify(m => m.DeleteCacheAsync(), Times.Once);
        }


    }
}