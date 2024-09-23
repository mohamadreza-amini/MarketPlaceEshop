using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Comment
{
    public int CommentID { get; set; }
   
    public string Description { get; set; }
    public bool IsConfirmed {  get; set; }
    public Guid CustumerId { get; set; }
    public Customer Author { get; set; }
    public DateTime Created { get; set; }
    
}
