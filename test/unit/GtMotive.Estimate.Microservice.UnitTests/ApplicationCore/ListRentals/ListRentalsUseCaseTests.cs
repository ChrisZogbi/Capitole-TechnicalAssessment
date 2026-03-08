#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.ListRentals
{
    public sealed class ListRentalsUseCaseTests
    {
        [Fact]
        public async Task ExecuteWhenInputIsNullThrowsArgumentNullException()
        {
            var rentalRepo = new Mock<IRentalRepository>();
            var appLogger = new Mock<IAppLogger<ListRentalsUseCase>>();
            var sut = new ListRentalsUseCase(rentalRepo.Object, appLogger.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Execute(null!));

            rentalRepo.Verify(r => r.GetRentals(It.IsAny<bool?>()), Times.Never);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public async Task ExecuteCallsRepositoryWithCorrectActiveOnlyAndMapsResult(bool? activeOnly)
        {
            var rentalRepo = new Mock<IRentalRepository>();
            var appLogger = new Mock<IAppLogger<ListRentalsUseCase>>();
            var rentalId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var renterId = Guid.NewGuid();
            var startDate = DateTime.UtcNow.AddDays(-1);
            DateTime? endDate = activeOnly == true ? null : DateTime.UtcNow;
            var rental = new Rental(rentalId, vehicleId, renterId, startDate, endDate);
            var rentals = new List<Rental> { rental };
            rentalRepo.Setup(r => r.GetRentals(activeOnly)).ReturnsAsync(rentals);
            var sut = new ListRentalsUseCase(rentalRepo.Object, appLogger.Object);
            var input = new ListRentalsInput(activeOnly);

            var result = await sut.Execute(input);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data.Rentals);
            var item = result.Data.Rentals[0];
            Assert.Equal(rentalId, item.RentalId);
            Assert.Equal(vehicleId, item.VehicleId);
            Assert.Equal(renterId, item.RenterId);
            Assert.Equal(startDate, item.StartDate);
            Assert.Equal(endDate, item.EndDate);
            rentalRepo.Verify(r => r.GetRentals(activeOnly), Times.Once);
        }

        [Fact]
        public async Task ExecuteWhenRepositoryReturnsEmptyListReturnsEmptyOutput()
        {
            var rentalRepo = new Mock<IRentalRepository>();
            rentalRepo.Setup(r => r.GetRentals(It.IsAny<bool?>())).ReturnsAsync((IReadOnlyList<Rental>)[]);
            var appLogger = new Mock<IAppLogger<ListRentalsUseCase>>();
            var sut = new ListRentalsUseCase(rentalRepo.Object, appLogger.Object);
            var input = new ListRentalsInput(null);

            var result = await sut.Execute(input);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data.Rentals);
            rentalRepo.Verify(r => r.GetRentals(null), Times.Once);
        }
    }
}
