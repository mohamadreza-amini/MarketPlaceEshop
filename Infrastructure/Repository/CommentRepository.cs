using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CommentRepository : ICommentRepository
    {
        AppDbContext _AppDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }
        public async Task<Comment> CreateComment(Comment comment)
        {
            await _AppDbContext.Comments.AddAsync(comment);
            await _AppDbContext.SaveChangesAsync();
            return comment;

        }

        public async Task<bool> DeleteComment(Guid id)
        {
            var Comment = await GetComment(id);
            _AppDbContext.Comments.Remove(Comment);
            await _AppDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            _AppDbContext.Comments.Update(comment);
            await _AppDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return await _AppDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<Comment> GetComment(Expression<Func<Comment, bool>> predicate=null)
        {
            return await _AppDbContext.Comments.FirstOrDefaultAsync(predicate);
        }

        public async Task<IQueryable<Comment>> GetComments(Expression<Func<Comment, bool>> predicate = null)
        {
            return await Task.Run(()=>_AppDbContext.Comments.Where(predicate));
        }

       
    }
}
