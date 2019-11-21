using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VicSites.Business.Definition;

namespace VicSites.Business.Implementation
{
    public class MainComponent : IMainComponent
    {
        private readonly ILogger<MainComponent> _logger;
        private readonly IDistributedCache _distributedCache;

        public MainComponent(ILogger<MainComponent> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        public async Task<int> GetNumberOfVisits()
        {
            try
            {
                var cacheKey = "numberOfVisits";
                var numberOfVisitsStr = await _distributedCache.GetStringAsync(cacheKey);

                if (string.IsNullOrEmpty(numberOfVisitsStr))
                {
                    await _distributedCache.SetStringAsync(cacheKey, 1.ToString());
                    return 1;
                }
                else
                {
                    var numberOfVisits = int.Parse(numberOfVisitsStr);
                    int newValue = numberOfVisits + 1;

                    await _distributedCache.SetStringAsync(cacheKey, newValue.ToString());
                    return newValue;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetNumberOfVisits");
                throw;
            }
        }
    }
}
