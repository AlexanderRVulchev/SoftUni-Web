namespace HouseRentingSystem.Core.Services
{
    using Contracts;
    using Core.Models.House;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HouseService : IHouseService
    {
        private readonly IRepository repo;

        public HouseService(IRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task<HouseQueryModel> All(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1)
        {
            HouseQueryModel result = new();
            var houses = repo.AllReadonly<House>();

            if (!String.IsNullOrEmpty(category))
            {
                houses = houses.Where(h => h.Category.Name == category);
            }

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = $"{searchTerm.ToLower()}";
                houses = houses.Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm)
                    || EF.Functions.Like(h.Description.ToLower(), searchTerm)
                    || EF.Functions.Like(h.Address.ToLower(), searchTerm));
            }

            houses = sorting switch
            {
                HouseSorting.Price => houses
                    .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => houses
                    .OrderBy(h => h.RenterId != null)
                    .ThenByDescending(h => h.Id),
                _ => houses.OrderByDescending(h => h.Id)
            };

            var housesResult = await houses
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Address = h.Address,
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    isRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .ToListAsync();

            int housesCount = await houses.CountAsync();

            return new HouseQueryModel
            {
                Houses = housesResult,
                TotalHousesCount = housesCount
            };
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

        public async Task<IEnumerable<string>> AllCategoriesNames()
            => await repo.AllReadonly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToArrayAsync();

        public async Task<bool> CategoryExists(int categoryId)
            => await repo.AllReadonly<Category>()
                         .AnyAsync(c => c.Id == categoryId);


        public async Task<int> Create(HouseModel model, int agentId)
        {
            var house = new House()
            {
                Description = model.Description,
                Address = model.Address,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Title = model.Title,
                PricePerMonth = model.PricePerMonth,
                AgentId = agentId
            };

            await repo.AddAsync<House>(house);
            await repo.SaveChangesAsync();

            return house.Id;
        }

        public async Task<int> GetAgentId(string userId)
            => (await repo.AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0;

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
