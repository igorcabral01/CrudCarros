using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using CrudCarros.Models;
using CrudCarros.Models.DbContext;

public class HomeController : Controller
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public HomeController(IMemoryCache memoryCache, ApplicationDbContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _memoryCache = memoryCache;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CacheExample()
    {
        string cacheKey = "ExampleKey";
        string cachedData;

       
        if (!_memoryCache.TryGetValue(cacheKey, out cachedData!))
        {
            
            cachedData = "Este é um exemplo de cache.";

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            _memoryCache.Set(cacheKey, cachedData, cacheEntryOptions);
        }

        ViewBag.CachedData = cachedData;
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var usuario = await _userManager.FindByEmailAsync(email);
        if (usuario == null)
        {
            ViewBag.MensagemErro = "Usuário não encontrado.";
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(usuario.UserName!, password, false, false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        ViewBag.MensagemErro = "Credenciais inválidas.";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string nome, string email, string password)
    {
        var usuario = new Usuario { UserName = email, Email = email, Nome = nome };
        var result = await _userManager.CreateAsync(usuario, password);

        if (result.Succeeded)
        {
            // Adiciona o usuário ao cargo 'Usuario' por padrão
            await _userManager.AddToRoleAsync(usuario, "Usuario");
            await _signInManager.SignInAsync(usuario, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.MensagemErro = string.Join(" ", result.Errors.Select(e => e.Description));
        return View("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UsuarioLogado");
        return RedirectToAction("Login", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}