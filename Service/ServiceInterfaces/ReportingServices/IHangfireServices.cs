using Model.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ReportingServices;

public interface IHangfireServices
{
    void AddViewLog(ViewLog log);
    Task SaveViewLogs();

    void LogProductView(Guid ProductId);
    Task SaveProductViewLogs();
}
