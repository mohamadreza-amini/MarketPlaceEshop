using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Service.ServiceInterfaces.ReviewServices;
using Shared.Enums;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class FeedBackController : Controller
{
    private readonly ICommentService _commentService;

    public FeedBackController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<IActionResult> Comments(CancellationToken cancellation, int pageIndex=1)
    {
        var comments = await _commentService.GetAllUnConfirmComments(pageIndex, 1, cancellation);
        return View(comments);
    }

    public async Task ConfirmComment(Guid commentId, CancellationToken cancellation)
    {
        await _commentService.ConfirmCommentAsync(commentId,ConfirmationStatus.Confirmed, cancellation);      
    }

    public async Task RejectComment(Guid commentId, CancellationToken cancellation)
    {
        await _commentService.ConfirmCommentAsync(commentId, ConfirmationStatus.Rejected, cancellation);
    }


   
}
