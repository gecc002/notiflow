using Microsoft.Extensions.Hosting;
using Puzzle.Lib.Documentation.Settings;
using System;

namespace Puzzle.Lib.Documentation.Tests.Middlewares
{
    public class ApplicationBuilderExtensionsTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly Mock<IOptions<SwaggerSetting>> _optionsMock;
        private readonly Mock<IApplicationBuilder> _appBuilderMock;
        private readonly Mock<IApplicationBuilder> _reDocBuilderMock;

        public ApplicationBuilderExtensionsTests()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _optionsMock = new Mock<IOptions<SwaggerSetting>>();
            _appBuilderMock = new Mock<IApplicationBuilder>();
            _reDocBuilderMock = new Mock<IApplicationBuilder>();
        }

        [Fact]
        public void UseSwaggerDoc_Should_Not_Use_Swagger_In_Production_Environment()
        {
            //_webHostEnvironmentMock.Setup(x => x.IsProduction()).Returns(true);
            _webHostEnvironmentMock.SetupGet(env => env.EnvironmentName).Returns("Production");

            IApplicationBuilder appBuilder = _appBuilderMock.Object;
            appBuilder.ApplicationServices = _serviceProviderMock.Object;
            //_serviceProviderMock.Setup(sp => sp.GetRequiredService<IWebHostEnvironment>()).Returns(_webHostEnvironmentMock.Object);
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(IWebHostEnvironment))).Returns(_webHostEnvironmentMock.Object);

            //appBuilder.UseSwaggerDoc();

            //_appBuilderMock.Verify(ab => ab.UseSwagger(It.IsAny<Action<SwaggerOptions>>()), Times.Never);
            //_appBuilderMock.Verify(ab => ab.UseSwaggerUI(It.IsAny<Action<SwaggerUIOptions>>()), Times.Never);

        }

        [Fact]
        public void UseSwaggerDoc_Should_Use_Swagger_In_Development_Environment()
        {
            //_webHostEnvironmentMock.Setup(x => x.IsProduction()).Returns(false);
            _webHostEnvironmentMock.SetupGet(env => env.EnvironmentName).Returns("Development");
            IApplicationBuilder appBuilder = _appBuilderMock.Object;
            appBuilder.ApplicationServices = _serviceProviderMock.Object;
            //_serviceProviderMock.Setup(sp => sp.GetRequiredService<IWebHostEnvironment>()).Returns(_webHostEnvironmentMock.Object);
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(IWebHostEnvironment))).Returns(_webHostEnvironmentMock.Object);

            SwaggerSetting swaggerSetting = new()
            {
                Description = "",
                Title = "",
                Version = ""
            };
            var _optionsMock = new Mock<IOptions<SwaggerSetting>>();
            _optionsMock.SetupGet(o => o.Value).Returns(swaggerSetting);
            //_optionsMock.Setup(op => op.Value).Returns(swaggerSetting);
            //_serviceProviderMock.Setup(sp => sp.GetRequiredService<IOptions<SwaggerSetting>>()).Returns(_optionsMock.Object);
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(IOptions<SwaggerSetting>))).Returns(_optionsMock.Object);

            //appBuilder.UseSwaggerDoc();

            //_appBuilderMock.Verify(ab => ab.UseSwagger(It.IsAny<Action<SwaggerOptions>>()), Times.Once);
            //_appBuilderMock.Verify(ab => ab.UseSwaggerUI(It.IsAny<Action<SwaggerUIOptions>>()), Times.Once);
        }
    }
}
