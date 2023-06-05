namespace HouseRentingSystem.Core.Models.House
{
    public class HouseQueryModel
    {
        public int TotalHousesCount { get; set; }

        public List<HouseServiceModel> Houses { get; set; } = new();
    }
}
