using Microsoft.AspNetCore.Identity;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Person;
public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string NationalCode { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? CreatorUserId { get; set; }
    public virtual User? CreatorUser { get; set; }
    public Guid? UpdaterUserId { get; set; }
    public virtual User? UpdaterUser { get; set; }
    public DateTime? CreateDatetime { get; set; }
    public DateTime? UpdateDatetime { get; set; }





    public void Validate()
    {
        if(string.IsNullOrWhiteSpace(FirstName)||
            string.IsNullOrWhiteSpace(LastName)||
            string.IsNullOrEmpty(MobileNumber)||
            string.IsNullOrWhiteSpace(NationalCode)||
            string.IsNullOrEmpty(Email)||
            string.IsNullOrEmpty(UserName)){
            throw new ModelValidationException("User");
        }
    }





}
