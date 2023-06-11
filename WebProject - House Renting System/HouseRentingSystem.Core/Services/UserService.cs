
namespace HouseRentingSystem.Core.Services
{
    using Core.Contracts;
    using HouseRentingSystem.Infrastructure.Data;
    using HouseRentingSystem.Infrastructure.Data.Common;

    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task<string?> UserFullName(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);
            if (String.IsNullOrEmpty(user.FirstName)
                || String.IsNullOrEmpty(user.LastName))
            {
                return null;
            }

            return user.FirstName + " " + user.LastName;
        }
    }
}
