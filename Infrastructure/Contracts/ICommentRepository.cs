using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface ICommentRepository
    {
        Task<Comment> CreateComment(Comment comment);
        Task<bool> DeleteComment(Guid comment);
        Task<Comment> UpdateComment(Comment comment);

        Task<Comment> GetComment(Guid Id);
        Task<Comment> GetComment(Expression<Func<Comment,bool>> predicate);
        Task<IQueryable<Comment>> GetComments(Expression<Func<Comment,bool>> predicate);
    }




    public interface ICommentRepositorya<T> where T : BaseEntity<Guid>
    {
        Task<T> CreateComment(T comment);
        Task<bool> DeleteComment(Guid comment);
        Task<T> UpdateComment(Comment comment);

        Task<T> GetComment(Guid Id);
        Task<T> GetComment(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetComments(Expression<Func<T, bool>> predicate);
    }
}
