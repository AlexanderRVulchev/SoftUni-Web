using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Infrastructure.Data.Configuration
{
    internal class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasData(CreateHouses());
        }

        private List<House> CreateHouses()
            => new()
            {
                new House()
                {
                    Id = 1,
                    Title = "Big House Marina",
                    Address = "North London, UK (near the border)",
                    Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                    ImageUrl = "https://images.coolhouseplans.com/plans/44207/44207-b600.jpg",
                    PricePerMonth = 2100.00M,
                    CategoryId = 3,
                    AgentId = 1,
                    RenterId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
                },
                new House()
                {
                    Id = 2,
                    Title = "Family House Comfort",
                    Address = "Near the Sea Garden in Burgas, Bulgaria",
                    Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                    ImageUrl = "https://i0.wp.com/smallhouse-design.com/wp-content/uploads/2020/04/Cool-House-Plans-316-Square-Meters-4-Bedrooms.jpg?fit=1880%2C1180&ssl=1",
                    PricePerMonth = 1200.00M,
                    CategoryId = 2,
                    AgentId = 1
                },
                new House()
                {
                    Id = 3,
                    Title = "Grand House",
                    Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                    Description = "This luxurious house is everything you will need. It is just excellent.",
                    ImageUrl = "https://media.architecturaldigest.com/photos/61b0ce48dccdb75fa170f8f7/16:9/w_2560%2Cc_limit/PurpleCherry_Williams_0012.jpg",
                    PricePerMonth = 2000.00M,
                    CategoryId = 2,
                    AgentId = 1
                }
            };
    }
}
