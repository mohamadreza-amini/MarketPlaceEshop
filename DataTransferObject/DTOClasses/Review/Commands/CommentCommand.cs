using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Review.Commands;

public class CommentCommand
{
    [Length(1, 500)]
    [Required(ErrorMessage = "وارد کردن نظر الزامی است")]
    [RegularExpression("[a-zA-Z\u0600-\u06FF0-9]+", ErrorMessage = "نظر باید حداقل شامل حروف فارسی یا انگلیسی باشد.")]
    [Display(Name = "نظر")] 
    public string CommentText { get; set; }
    public Guid ProductId { get; set; }
}




