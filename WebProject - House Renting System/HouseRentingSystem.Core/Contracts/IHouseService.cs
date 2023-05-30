namespace HouseRentingSystem.Core.Contracts
{
    using Models.House;

    public interface IHouseService
    {
        Task<IEnumerable<HouseHomeModel>> LastThreeHouses();
    }
}
