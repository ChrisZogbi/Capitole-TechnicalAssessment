using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.CreateVehicle
{
    public sealed class CreateVehicleUseCaseTests
    {
        [Fact]
        public async Task ExecuteWhenInputIsNullThrowsArgumentNullException()
        {
            var vehicleRepository = new Mock<IVehicleRepository>();
            var sut = new CreateVehicleUseCase(vehicleRepository.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Execute(null!));

            vehicleRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Vehicle>()), Times.Never);
        }

        [Theory]
        [AutoMoqData]
        public async Task ExecuteWhenManufacturingDateIsValidAddsVehicleAndReturnsSuccess(
            [Frozen] Mock<IVehicleRepository> vehicleRepository,
            CreateVehicleUseCase sut)
        {
            var validDate = DateTime.UtcNow.AddYears(-2).Date;
            var input = new CreateVehicleInput(validDate);

            var result = await sut.Execute(input);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(validDate, result.Data.ManufacturingDate);
            Assert.True(result.Data.IsAvailable);
            Assert.Null(result.ErrorCode);
            vehicleRepository.Verify(
                r => r.Add(It.Is<Domain.Entities.Vehicle>(v =>
                    v.ManufacturingDate.Value == validDate && v.IsAvailable)),
                Times.Once);
        }

        [Fact]
        public async Task ExecuteWhenManufacturingDateIsTooOldReturnsFailureWithVehicleTooOldForFleet()
        {
            var vehicleRepository = new Mock<IVehicleRepository>();
            var sut = new CreateVehicleUseCase(vehicleRepository.Object);
            var tooOldDate = DateTime.UtcNow.AddYears(-6).Date;
            var input = new CreateVehicleInput(tooOldDate);

            var result = await sut.Execute(input);

            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Equal(UseCaseErrorCode.VehicleTooOldForFleet, result.ErrorCode);
            Assert.NotNull(result.ErrorMessage);
            vehicleRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Vehicle>()), Times.Never);
        }
    }
}
