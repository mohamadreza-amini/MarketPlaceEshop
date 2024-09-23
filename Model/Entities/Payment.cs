using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Order Order { get; set; }

    public decimal Price {  get; set; }
    public bool IsSucceeded {  get; set; }
}
