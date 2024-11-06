using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Person;

namespace Model.Entities;

public abstract class BaseEntity<T> where T : struct
{
    public T Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid? CreatorUserId { get; set; }
    public virtual User? CreatorUser { get; set; }
    public Guid? UpdaterUserId { get; set; }
    public virtual User? UpdaterUser { get; set; }
    public DateTime? CreateDatetime { get; set; }
    public DateTime? UpdateDatetime { get; set; }
    //enums
    public byte IsConfirmed { get; set; } 
    public DateTime? ConfirmedDate { get; set; }
    public Guid? AdminConfirmedId { get; set; }
    public virtual Admin? AdminConfirmed { get; set; }
  
}

