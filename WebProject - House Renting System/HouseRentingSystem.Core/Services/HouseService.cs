namespace HouseRentingSystem.Core.Services
{
    using Contracts;
    using HouseRentingSystem.Core.Models.House;
    using HouseRentingSystem.Infrastructure.Data;
    using HouseRentingSystem.Infrastructure.Data.Common;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HouseService : IHouseService
    {
        private readonly IRepository repo;

        public HouseService(IRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task<IEnumerable<HouseHomeModel>> LastThreeHouses()
        {
            return await repo.AllReadonly<House>()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseHomeModel
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title
                })
                .Take(3)
                .ToArrayAsync();
        }
    }
}
