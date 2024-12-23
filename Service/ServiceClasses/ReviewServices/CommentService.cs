using DataTransferObject.DTOClasses.Review.Commands;
using DataTransferObject.DTOClasses.Review.Results;
using Infrastructure.Contracts.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Products;
using Model.Entities.Review;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReviewServices;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.ReviewServices;

public class CommentService : ServiceBase<Comment, CommentResult, Guid>, ICommentService
{
    private readonly IBaseRepository<Comment, Guid> _commentRepository;
    private readonly IUserService _userService;
    private readonly IBaseRepository<Product, Guid> _productRepository;


    public CommentService(IBaseRepository<Comment, Guid> commentRepository, IUserService userService, IBaseRepository<Product, Guid> productRepository)
    {
        _commentRepository = commentRepository;
        _userService = userService;
        _productRepository = productRepository;
    }

    public async Task<bool> AddCommentAsync(CommentCommand commentDto, CancellationToken cancellation)
    {
        var customerId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        if (!_userService.IsInRole("Customer") || customerId == Guid.Empty)
            throw new AccessDeniedException();
        var comment = Translate<CommentCommand, Comment>(commentDto);
        comment.DateOfRegistration = DateTime.Now;
        comment.CustomerId = customerId;
        comment.IsConfirmed = 1;

        comment.Validate();

        if (await _productRepository.GetByIdAsync(commentDto.ProductId, cancellation) == null)
            throw new BadRequestException("محصول نامعتبر");

        var hasCustomerComment = await HasCustomerCommentAsync(customerId, comment.ProductId, cancellation);
        if (hasCustomerComment)
            throw new BadRequestException("برای این محصول نظری ثبت شده دارید");

        await _commentRepository.CreateAsync(comment, cancellation);
        return await _commentRepository.CommitAsync(cancellation) > 0;
    }

    public async Task<bool> HasCustomerCommentAsync(Guid customerid, Guid productId, CancellationToken cancellation)
    {
        var comment = await _commentRepository.GetAsync(x => x.CustomerId == customerid && x.ProductId == productId, cancellation);
        if (comment == null)
            return false;
        return true;
    }

    public async Task<bool> ConfirmCommentAsync(Guid commentId, ConfirmationStatus confirmation, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var comment = await _commentRepository.GetByIdAsync(commentId, cancellation);
        if (comment == null)
            throw new BadRequestException("کامنت پیدا نشد");

        Guid.TryParse(_userService.RequesterId(), out Guid adminId);

        comment.IsConfirmed = (byte)confirmation;
        comment.ConfirmedDate = DateTime.Now;
        comment.AdminConfirmedId = adminId;
        comment.Validate();

        return await _commentRepository.CommitAsync(cancellation) > 0;
    }


    public async Task<PaginatedList<CommentResult>> GetAllUnConfirmComments(int pageIndex, int pageSize, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var unConfirmComments = await _commentRepository.GetAllDataAsync(x => x, cancellation, x => x.IsConfirmed == 1);

        if (unConfirmComments == null)
            return new PaginatedList<CommentResult>(new List<CommentResult>(),0,1,pageSize);

        return await PaginatedList<CommentResult>.CreateAsync(unConfirmComments.ProjectToType<CommentResult>(), pageIndex, pageSize, cancellation);
    }

    public async Task<List<CommentResult>> GetAllCommentByProductId(Guid productId, CancellationToken cancellation)
    {
        var confirmComments = await _commentRepository.GetAllDataAsync( x =>x,cancellation, x => x.IsConfirmed == 2 && x.ProductId == productId);

        if (confirmComments == null)
            return new List<CommentResult>();
        return await confirmComments.ProjectToType<CommentResult>().ToListAsync(cancellation);
    }
}

