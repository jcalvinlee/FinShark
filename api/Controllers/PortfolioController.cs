using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager,
            IStockRepository stockRepo,
            IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            AppUser? appUser = await GetCurrentUserAsync();
            List<Stock> userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            return Ok(userPortfolio.Select(s => s.ToStockDto()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            AppUser? appUser = await GetCurrentUserAsync();
            Stock? stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                return BadRequest($"Stock {symbol} not found.");
            }

            Portfolio? portfolio = await _portfolioRepo.GetUserStockPair(appUser, stock);

            if (portfolio != null)
            {
                return BadRequest($"Stock {symbol} is already in user's portfolio.");
            }

            Portfolio portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id,
            };

            await _portfolioRepo.CreateAsync(portfolioModel);

            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            AppUser? appUser = await GetCurrentUserAsync();
            Stock? stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                return BadRequest($"Stock {symbol} not found.");
            }

            Portfolio? portfolio = await _portfolioRepo.GetUserStockPair(appUser, stock);

            if (portfolio == null)
            {
                return BadRequest($"Stock {symbol} is not in user's portfolio.");
            }

            Portfolio? portfolioModel = await _portfolioRepo.DeleteAsync(portfolio);
            if (portfolioModel == null)
            {
                return BadRequest($"Stock {symbol} is not in user's portfolio.");
            }

            return Ok();
        }

        private async Task<AppUser?> GetCurrentUserAsync()
        {
            string username = User.GetUsername(); // User is inherited from ControllerBase
            return await _userManager.FindByNameAsync(username);
        }

    }
}
