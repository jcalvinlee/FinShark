using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            Comment? comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            IQueryable<Comment> comments = _context.Comments.Include(a => a.AppUser).AsQueryable();

            if (!String.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol == queryObject.Symbol);
            }

            if (queryObject.IsDescending)
            {
                comments = comments.OrderByDescending(s => s.CreatedOn);
            }
            else
            {
                comments = comments.OrderBy(s => s.CreatedOn);
            }

            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            Comment? comment = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            Comment? existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}