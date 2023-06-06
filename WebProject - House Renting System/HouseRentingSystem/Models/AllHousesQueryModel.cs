using HouseRentingSystem.Core.Models.House;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Models
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string? Category { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; set; }

        public HouseSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<HouseServiceModel> Houses { get; set; } = Enumerable.Empty<HouseServiceModel>();
    }
}
