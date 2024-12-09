using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Results;

public class ReportSalesResult
{
    public DateTime DateTime {  get; set; }
    public decimal TotalAmountPaid { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalSales { get; set; }

    //به صورت لیستی گزارش روزانه میشه
}
