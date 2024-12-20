using DataTransferObject.DTOClasses.Review.Commands;
using DataTransferObject.DTOClasses.Review.Results;
using Model.Entities.Review;
using Service.ServiceClasses;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.ServiceInterfaces.ReviewServices;

public interface ICommentService:IServiceBase<Comment,CommentResult,Guid>
{
    Task<bool> AddCommentAsync(CommentCommand commentDto,CancellationToken cancellation);
    Task<bool> HasCustomerCommentAsync(Guid customerid,Guid productId ,CancellationToken cancellation);
    Task<PaginatedList<CommentResult>> GetAllUnConfirmComments(int pageIndex, int pageSize,CancellationToken cancellation);
    Task<bool> ConfirmCommentAsync(Guid commentId, ConfirmationStatus confirmation, CancellationToken cancellation);
    Task<List<CommentResult>> GetAllCommentByProductId(Guid ProductId, CancellationToken cancellation);
}
