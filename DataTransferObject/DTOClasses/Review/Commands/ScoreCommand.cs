using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Review.Commands;

public class ScoreCommand
{
    [Range(1, 5)]
    [Required(ErrorMessage = "وارد کردن امتیاز الزامی است")]
    [Display(Name = "امتیاز")]
    public int StarRating { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
}
