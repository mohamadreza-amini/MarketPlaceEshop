using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface IScoreRepository:IBaseRepository<Score,Guid>
{
    Task<double> GetAvgScoreByProductId(Guid productId,CancellationToken cancellation);
}
