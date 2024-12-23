using DataTransferObject.DTOClasses.Order.Results;
using Shared;
namespace MarketPlaceEshop.Areas.Admin.Models;

public class ReportViewModel
{
    //مجموع ارزش سبدهای خرید
    public decimal TotalValueOfCarts { get; set; }
    //فروش روزانه
    public List<ReportSalesResult> DailySales { get; set; }
    //فروش کل
    public ReportSalesResult TotalSales {  get; set; }
    //تعداد سفارشات تایید شده
    public int NumberOfOrderConfirmed { get; set; }
    //تعداد سفارشات رد شده
    public int NumberOfOrderRejected { get; set; }
    //تعداد سفارشات در حال بررسی 
    public int NumberOfOrderUnchecked { get; set; }
    //تعداد سفارشات ارسال شده
    public int NumberOfOrderSent { get; set; }
    //تعداد سفارشات در حال ارسال 
    public int NumberOfOrderSending { get; set; }
    //تعداد تامین کنندگان 
    public int NumberOfSupplier { get; set; }
    //تعداد مشتری ها کنندگان 
    public int NumberOfCustomers { get; set; }
    //مجموع ارزش موجودی
    public decimal TotalInventory { get; set; }
    //تعداد بازدید ها
    public int TotalViews { get; set; }
    //بازدید روزانه
    public List<ViewsResult> DailyViews { get; set; }
    //وضعیت فعلی فیلترها
    public int viewDays { get; set; } = 7;
    public int saleDays { get; set; } = 7;
    public DateTime endOfSalePeriod { get; set; } = DateTime.Now.AddDays(1).ToAD();
    public DateTime startOfSalePeriod { get; set; } = DateTime.Now.AddDays(-7).ToAD();

}
