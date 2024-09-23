using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Score
{
    public byte ScoreNumber {  get; set; }
    public Guid OrderItemId { get; set; }
    public OrderItem orderItem {  get; set; }
    public Product product { get; set; }
        
    public Guid ProductId { get; set; }

    

}