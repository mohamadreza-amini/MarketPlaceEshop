using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Classes;

public class ScoreRepository : BaseRepository<Score, Guid>, IScoreRepository
{
    public ScoreRepository(AppDbContext appDbContext) : base(appDbContext) { }
    public async Task<double> GetAvgScoreByProductId(Guid productId, CancellationToken cancellation)
    {
        var query = _entitySet.Where(x => x.ProductId == productId);
        if (await query.AnyAsync(cancellation))
            return await query.AverageAsync(x => x.StarRating, cancellation);
        return 0;
    }

    public async Task<int> NumberOfScores(Guid productId, CancellationToken cancellation)
    {
        return await _entitySet.Where(X => X.ProductId == productId).CountAsync(cancellation);
    }
}
