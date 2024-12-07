using DataTransferObject.DTOClasses.Review.Commands;
using DataTransferObject.DTOClasses.Review.Results;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ReviewServices;

public interface IScoreService:IServiceBase<Score,ScoreResult,Guid>
{
    Task<bool> AddScoreAsync(ScoreCommand scoreDto,CancellationToken cancellation);
    Task<bool> HasCustomerScoreAsync(Guid customerid, Guid productId, CancellationToken cancellation);
    Task<double> GetProductAverageRating(Guid productId, CancellationToken cancellation);
    Task<int> NumberOfScore(Guid productId, CancellationToken cancellation);
}
