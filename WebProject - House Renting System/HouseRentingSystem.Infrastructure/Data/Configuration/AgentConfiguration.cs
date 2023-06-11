using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Infrastructure.Data.Configuration
{
    internal class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasData(CreateAgent());
        }

        private List<Agent> CreateAgent()
        {
            var agents = new List<Agent>();

            var agent = new Agent()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            };
            agents.Add(agent);

            agent = new Agent()
            {
                Id = 2,
                PhoneNumber = "+359123456789",
                UserId = "586513cb-2bad-4ea3-ae33-7b8954efb167"
            };
            agents.Add(agent);

            return agents;
        }
    }
}
