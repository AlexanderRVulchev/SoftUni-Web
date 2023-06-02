namespace HouseRentingSystem.Core.Services
{
    using Core.Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Microsoft.EntityFrameworkCore;

    public class AgentService : IAgentService
    {
        private readonly IRepository repo;

        public AgentService(IRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await repo.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }
    }
}
