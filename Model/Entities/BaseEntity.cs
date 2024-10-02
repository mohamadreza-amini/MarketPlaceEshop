using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class BaseEntity<T> where T : struct
{
    public T Id { get; set; }
    public Guid? CreatorId { get; set; }
    public Guid? UpdaterId { get; set; }
    public DateTime? CreateDatetime { get; set; }
    public DateTime? UpdateDatetime { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ConfirmationStatus IsConfirmed { get; set; } 
    public DateTime? ConfirmedDate { get; set; }
    public Guid? AdminConfirmedId { get; set; }
    public Admin? AdminConfirmed { get; set; }

    

}


