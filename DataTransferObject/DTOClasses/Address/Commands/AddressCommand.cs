﻿    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Address.Commands;

public class AddressCommand 
{
    [Required(ErrorMessage = "وارد کردن محله الزامی است")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "ورودی صحیح نمی باشد")]
    [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
    [Display(Name = "محله")]
    public string Neighborhood { get; set; }

    [Required(ErrorMessage = "وارد کردن آدرس الزامی است")]
    [Display(Name = "آدرس")]
    [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]

    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "ورودی صحیح نمی باشد")]
    public string AddressDetail { get; set; }

    [Required(ErrorMessage = "وارد کردن پلاک الزامی است")]
    [Range(1, 100000, ErrorMessage = "بازه وارد شده صحبح نیست")]
    [Display(Name = "پلاک")]
    public int HouseNumber { get; set; }

    [Required(ErrorMessage = "وارد کردن واحد الزامی است")]
    [Range(1, 100000, ErrorMessage = "بازه وارد شده صحبح نیست")]
    [Display(Name = "واحد")]
    public int UnitNumber { get; set; }

    [Required(ErrorMessage = "وارد کردن کد پستی الزامی است")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "ورودی صحیح نمی باشد")]
    [Display(Name = "کد پستی")]
    public string PostalCode { get; set; }

    
    public int CityId { get; set; }

    [Display(Name = "استان")]
    public string? ProvinceName { get; set; }

    [Display(Name = "شهر")]
    public string? CityName { get; set; }

}
