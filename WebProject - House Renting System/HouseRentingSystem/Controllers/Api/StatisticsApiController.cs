
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers.Api
{
    using Core.Contracts;
    using Core.Models.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService _statisticsService)
        {
            this.statistics = _statisticsService;
        }

        [HttpGet]
        public async Task<StatisticsServiceModel> GetStatistics()
            => await statistics.Total();
    }
}
