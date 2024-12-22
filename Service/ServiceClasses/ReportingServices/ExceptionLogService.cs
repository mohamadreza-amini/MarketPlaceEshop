using Infrastructure.Contracts.Repository;
using Model.Entities.Reports;
using Service.ServiceInterfaces.ReportingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.ReportingServices;

public class ExceptionLogService: IExceptionLogService
{
    private readonly IBaseRepository<ExceptionLog, int> _exceptionRepository;
    public ExceptionLogService(IBaseRepository<ExceptionLog, int> exceptionRepository)
    {
        _exceptionRepository = exceptionRepository;
    }


    public async Task SaveExceptionLog(ExceptionLog log)
    {
        await _exceptionRepository.CreateAsync(log, CancellationToken.None);
        await _exceptionRepository.CommitAsync(CancellationToken.None);
    }

}
