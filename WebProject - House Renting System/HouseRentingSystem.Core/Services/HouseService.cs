namespace HouseRentingSystem.Core.Services
{
    using Contracts;
    using Core.Models.House;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;

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

        public async Task<IEnumerable<HouseCategoryModel>> AllCategories()
            => await repo.All<Category>()
                .OrderBy(c => c.Name)
                .Select(c => new HouseCategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

        public async Task<bool> CategoryExists(int categoryId)
            => await repo.AllReadonly<Category>()
                         .AnyAsync(c => c.Id == categoryId);


        public async Task<int> Create(HouseModel model)
        {
            var house = new House()
            {                
                Description = model.Description,
                Address = model.Address,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Title = model.Title,
                PricePerMonth = model.PricePerMonth
            };

            await repo.AddAsync<House>(house);
            await repo.SaveChangesAsync();

            return house.Id;
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
