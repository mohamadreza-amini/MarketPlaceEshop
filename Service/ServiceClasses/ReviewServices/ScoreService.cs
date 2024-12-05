using DataTransferObject.DTOClasses.Review.Commands;
using DataTransferObject.DTOClasses.Review.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Review;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReviewServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.ReviewServices;

public class ScoreService : ServiceBase<Score, ScoreResult, Guid>, IScoreService
{
    private readonly IScoreRepository _scoreRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public ScoreService(IScoreRepository scoreRepository, IUserService userService, IProductService productService)
    {
        _scoreRepository = scoreRepository;
        _userService = userService;
        _productService = productService;
    }

    public async Task<bool> AddScoreAsync(ScoreCommand scoreDto, CancellationToken cancellation)
    {
        Guid.TryParse(_userService.RequesterId(), out Guid customerId);

        if (!_userService.IsInRole("Customer") || customerId == Guid.Empty)
            throw new AccessDeniedException();

        var score = Translate<ScoreCommand, Score>(scoreDto);

        if (await _productService.ProductExists(scoreDto.ProductId, cancellation) == false)
            throw new BadRequestException("محصول نامعتبر");

        score.Validate();
        var hasSscoreComment = await HasCustomerScoreAsync(customerId, scoreDto.ProductId, cancellation);
        if (hasSscoreComment)
            throw new BadRequestException("برای این محصول امتیاز ثبت شده دارید");

        await _scoreRepository.CreateAsync(score, cancellation);
        return await _scoreRepository.CommitAsync(cancellation) > 0;
    }

    public async Task<bool> HasCustomerScoreAsync(Guid customerid, Guid productId, CancellationToken cancellation)
    {
        var score = await _scoreRepository.GetAsync(x => x.CustomerId == customerid && x.ProductId == productId, cancellation);
        if (score == null)
            return false;
        return true;
    }

    public async Task<double> GetProductAverageRating(Guid productId, CancellationToken cancellation)
    {
        return await _scoreRepository.GetAvgScoreByProductId(productId, cancellation);
    }

}
