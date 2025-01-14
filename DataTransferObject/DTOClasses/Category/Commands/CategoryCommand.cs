﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Commands;

public class CategoryCommand
{
    [Required(ErrorMessage = "وارد کردن نام دسته بندی الزامی است")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "ورودی صحیح نمی باشد")]
    [Display(Name = "دسته بندی")]
    public string CategoryName { get; set; }
    public int? ParentCategoryId { get; set; }
}


