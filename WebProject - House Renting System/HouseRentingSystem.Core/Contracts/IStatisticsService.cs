
namespace HouseRentingSystem.Core.Contracts
{
    using Core.Models.Statistics;

    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total();
    }
}
