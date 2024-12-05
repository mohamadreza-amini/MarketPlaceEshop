using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Commands;

public class OrderCommand
{
    public DateTime ShippedDate { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid AddressId { get; set; }
    public Guid CustomerId { get; set; }
}
