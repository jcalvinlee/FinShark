
using api.Data;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;    
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio?> DeleteAsync(Portfolio portfolio)
        {
            Portfolio? portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == portfolio.AppUserId && x.StockId == portfolio.StockId);
            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(portfolio => 
                    new Stock
                    {
                        Id = portfolio.Stock.Id,
                        Symbol = portfolio.Stock.Symbol,
                        CompanyName = portfolio.Stock.CompanyName,
                        Purchase = portfolio.Stock.Purchase,
                        LastDiv = portfolio.Stock.LastDiv,
                        Industry = portfolio.Stock.Industry,
                        MarketCap = portfolio.Stock.MarketCap,
                    }
                ).ToListAsync();
        }

        public async Task<Portfolio?> GetUserStockPair(AppUser user, Stock stock)
        {
            return await _context.Portfolios.SingleOrDefaultAsync(x => x.AppUserId == user.Id && x.StockId == stock.Id);
        }
    }
}
