using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Statistics;
using HouseRentingSystem.Infrastructure.Data;
using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository repo;

        public StatisticsService(IRepository _repo)
        {
            this.repo = _repo;  
        }

        public async Task<StatisticsServiceModel> Total()
        {
            var houses = await repo
                .All<House>()
                .ToArrayAsync();

            int totalHousesCount = houses
                .Count();

            int totalRentsCount = houses
                .Where(h => h.RenterId != null)
                .Count();

            return new StatisticsServiceModel
            {
                TotalHouses = totalHousesCount,
                TotalRents = totalRentsCount
            };
        }
    }
}
