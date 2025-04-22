using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class HomeController : Controller
{
    private readonly IMemoryCache _memoryCache;

    public HomeController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CacheExample()
    {
        string cacheKey = "ExampleKey";
        string cachedData;

        // Tenta obter o valor do cache
        if (!_memoryCache.TryGetValue(cacheKey, out cachedData!))
        {
            // Se não estiver no cache, define o valor
            cachedData = "Este é um exemplo de cache.";

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            _memoryCache.Set(cacheKey, cachedData, cacheEntryOptions);
        }

        ViewBag.CachedData = cachedData;
        return View();
    }
}