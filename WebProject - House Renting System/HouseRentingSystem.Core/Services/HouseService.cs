namespace HouseRentingSystem.Core.Services
{
    using Contracts;
    using Core.Models.Agent;
    using Core.Models.House;
    using HouseRentingSystem.Core.Exceptions;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;

    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HouseService : IHouseService
    {
        private readonly IRepository repo;

        private readonly IGuard guard;

        public HouseService(
            IRepository _repo,
            IGuard _guard)
        {
            this.repo = _repo;
            this.guard = _guard;
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
                    IsRented = h.RenterId != null,
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

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId)
            => await repo.All<House>()
            .Where(c => c.AgentId == agentId)
            .Select(c => new HouseServiceModel
            {
                Address = c.Address,
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                IsRented = c.RenterId == null,
                PricePerMonth = c.PricePerMonth,
                Title = c.Title
            })
            .ToArrayAsync();


        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
             => await repo.All<House>()
            .Where(c => c.RenterId == userId)
            .Select(c => new HouseServiceModel
            {
                Address = c.Address,
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                IsRented = c.RenterId != null,
                PricePerMonth = c.PricePerMonth,
                Title = c.Title                
            })
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

        public async Task Delete(int houseId)
        {
            var house = await repo.GetByIdAsync<House>(houseId);

            if (house != null)
            {
                await repo.DeleteAsync<House>(houseId);
                await repo.SaveChangesAsync();
            }
        }

        public async Task Edit(int houseId, HouseModel model)
        {
            var house = await repo.GetByIdAsync<House>(houseId);

            house.Title = model.Title;
            house.Address = model.Address;
            house.Description = model.Description;
            house.ImageUrl = model.ImageUrl;
            house.PricePerMonth = model.PricePerMonth;
            house.CategoryId = model.CategoryId;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
            => await repo.AllReadonly<House>()
                .AnyAsync(h => h.Id == id);

        public async Task<int> GetAgentId(string userId)
            => (await repo.AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0;

        public async Task<int> GetHouseCategoryId(int houseId)
            => (await repo.GetByIdAsync<House>(houseId)).CategoryId;


        public async Task<bool> HasAgentWithId(int houseId, string currentUserId)
        {
            var house = await repo.AllReadonly<House>()
                .Where(h => h.Id == houseId)
                .Include(h => h.Agent)
                .FirstOrDefaultAsync();

            if (house?.Agent != null && house.Agent.UserId == currentUserId)
            {
                return true;
            }

            return false;
        }

        public async Task<HouseDetailsModel> HouseDetailsbyId(int id)
            => await repo.AllReadonly<House>()
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsModel
                {
                    Address = h.Address,
                    Description = h.Description,
                    Category = h.Category.Name,
                    Id = id,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title,
                    Agent = new AgentServiceModel
                    {
                        Email = h.Agent.User.Email,
                        PhoneNumber = h.Agent.User.PhoneNumber
                    }
                })
                .FirstAsync();

        public async Task<bool> IsRented(int id)
            => (await repo.GetByIdAsync<House>(id)).RenterId != null;

        public async Task<bool> IsRentedByUserWithId(int houseId, string userId)
        {
            var house = await repo.GetByIdAsync<House>(houseId);

            if (house == null)
            {
                return false;
            }

            if (house.RenterId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<HouseHomeModel>> LastThreeHouses()
        {
            return await repo.AllReadonly<House>()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseHomeModel
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title,
                    Address = h.Address,
                })
                .Take(3)
                .ToArrayAsync();
        }

        public async Task Leave(int houseId)
        {
            var house = await repo.GetByIdAsync<House>(houseId);
            guard.AgainstNull(house, "House cannot be found");
            house.RenterId = null;

            await repo.SaveChangesAsync();
        }

        public async Task Rent(int houseId, string userId)
        {
            var house = await repo.GetByIdAsync<House>(houseId);

            if (house.RenterId != null && userId != null)
            {
                throw new ArgumentException("House is already rented");
            }

            guard.AgainstNull(house, "House cannot be found");

            house.RenterId = userId;
            await repo.SaveChangesAsync();
        }
    }
}
