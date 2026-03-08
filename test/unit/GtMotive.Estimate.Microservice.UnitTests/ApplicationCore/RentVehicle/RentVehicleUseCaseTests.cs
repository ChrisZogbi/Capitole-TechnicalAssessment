#nullable enable

using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.RentVehicle
{
    public sealed class RentVehicleUseCaseTests
    {
        [Fact]
        public async Task ExecuteWhenInputIsNullThrowsArgumentNullException()
        {
            var vehicleRepo = new Mock<IVehicleRepository>();
            var rentalRepo = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RentVehicleUseCase(vehicleRepo.Object, rentalRepo.Object, unitOfWork.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Execute(null!));

            vehicleRepo.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteWhenVehicleNotFoundReturnsNotFound()
        {
            var fixture = new Fixture();
            var vehicleId = fixture.Create<Guid>();
            var renterId = fixture.Create<Guid>();
            var vehicleRepo = new Mock<IVehicleRepository>();
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync((Vehicle?)null);
            var rentalRepo = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RentVehicleUseCase(vehicleRepo.Object, rentalRepo.Object, unitOfWork.Object);
            var input = new RentVehicleInput(vehicleId, renterId);

            var result = await sut.Execute(input);

            Assert.False(result.IsSuccess);
            Assert.Equal(UseCaseErrorCode.NotFound, result.ErrorCode);
            Assert.Null(result.Data);
            rentalRepo.Verify(r => r.Add(It.IsAny<Rental>()), Times.Never);
            vehicleRepo.Verify(r => r.Update(It.IsAny<Vehicle>()), Times.Never);
            unitOfWork.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task ExecuteWhenVehicleNotAvailableReturnsVehicleNotAvailable()
        {
            var fixture = new Fixture();
            var vehicleId = fixture.Create<Guid>();
            var renterId = fixture.Create<Guid>();
            var vehicle = CreateAvailableVehicle(vehicleId);
            vehicle.MarkAsRented();
            var vehicleRepo = new Mock<IVehicleRepository>();
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            var rentalRepo = new Mock<IRentalRepository>();
            rentalRepo.Setup(r => r.GetActiveByRenter(renterId)).ReturnsAsync((Rental?)null);
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RentVehicleUseCase(vehicleRepo.Object, rentalRepo.Object, unitOfWork.Object);
            var input = new RentVehicleInput(vehicleId, renterId);

            var result = await sut.Execute(input);

            Assert.False(result.IsSuccess);
            Assert.Equal(UseCaseErrorCode.VehicleNotAvailable, result.ErrorCode);
            Assert.Null(result.Data);
            rentalRepo.Verify(r => r.Add(It.IsAny<Rental>()), Times.Never);
            unitOfWork.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task ExecuteWhenRenterAlreadyHasActiveRentalReturnsRenterAlreadyHasActiveRental()
        {
            var fixture = new Fixture();
            var vehicleId = fixture.Create<Guid>();
            var renterId = fixture.Create<Guid>();
            var vehicle = CreateAvailableVehicle(vehicleId);
            var existingRental = new Rental(Guid.NewGuid(), Guid.NewGuid(), renterId, DateTime.UtcNow, null);
            var vehicleRepo = new Mock<IVehicleRepository>();
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            var rentalRepo = new Mock<IRentalRepository>();
            rentalRepo.Setup(r => r.GetActiveByRenter(renterId)).ReturnsAsync(existingRental);
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RentVehicleUseCase(vehicleRepo.Object, rentalRepo.Object, unitOfWork.Object);
            var input = new RentVehicleInput(vehicleId, renterId);

            var result = await sut.Execute(input);

            Assert.False(result.IsSuccess);
            Assert.Equal(UseCaseErrorCode.RenterAlreadyHasActiveRental, result.ErrorCode);
            Assert.Null(result.Data);
            rentalRepo.Verify(r => r.Add(It.IsAny<Rental>()), Times.Never);
            vehicleRepo.Verify(r => r.Update(It.IsAny<Vehicle>()), Times.Never);
            unitOfWork.Verify(u => u.Save(), Times.Never);
        }

        [Theory]
        [AutoMoqData]
        public async Task ExecuteWhenVehicleAvailableAndRenterHasNoActiveRentalAddsRentalAndReturnsSuccess(
            [Frozen] Mock<IVehicleRepository> vehicleRepo,
            [Frozen] Mock<IRentalRepository> rentalRepo,
            [Frozen] Mock<IUnitOfWork> unitOfWork,
            RentVehicleUseCase sut)
        {
            var vehicleId = Guid.NewGuid();
            var renterId = Guid.NewGuid();
            var vehicle = CreateAvailableVehicle(vehicleId);
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            rentalRepo.Setup(r => r.GetActiveByRenter(renterId)).ReturnsAsync((Rental?)null);
            unitOfWork.Setup(u => u.BeginTransactionAsync(It.IsAny<System.Threading.CancellationToken>())).Returns(Task.CompletedTask);
            unitOfWork.Setup(u => u.Save()).Returns(Task.FromResult(1));
            var input = new RentVehicleInput(vehicleId, renterId);

            var result = await sut.Execute(input);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(vehicleId, result.Data.VehicleId);
            Assert.Equal(renterId, result.Data.RenterId);
            rentalRepo.Verify(r => r.Add(It.Is<Rental>(rental => rental.VehicleId == vehicleId && rental.RenterId == renterId)), Times.Once);
            vehicleRepo.Verify(r => r.Update(It.Is<Vehicle>(v => v.Id == vehicleId && !v.IsAvailable)), Times.Once);
            unitOfWork.Verify(u => u.BeginTransactionAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
            unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        private static Vehicle CreateAvailableVehicle(Guid? id = null)
        {
            var vehicleId = id ?? Guid.NewGuid();
            var validDate = DateTime.UtcNow.AddYears(-2).Date;
            var manufacturingDate = new ManufacturingDate(validDate);
            return new Vehicle(vehicleId, manufacturingDate, isAvailable: true);
        }
    }
}
