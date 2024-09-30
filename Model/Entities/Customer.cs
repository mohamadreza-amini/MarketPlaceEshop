using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Customer : BaseEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber {  get; set; }
    //پایینی یونیک یا ایندکس برا لاگین
    public string MobileNumber {  get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime DateOfRegistration {  get; set; }
    public string Password { get; set; }
    public string NationalCode {  get; set; }
    public ICollection<Address> Addresses { get; set; }
    //کالکشن نیو کنم یا نه لیت یا کالکشن اصلا باشه
    public ICollection<Order> Orders { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Score> Scores { get; set; }
    public ICollection<Cart> Carts { get; set; }
    //

}
