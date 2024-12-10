﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObject.DTOClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Model.Entities.Person;
using Service.ServiceInterfaces;

namespace MarketPlaceEshop.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
       /* private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserService _userService;
        // private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger
,
            IUserService userService
            //, IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _userService = userService;
            // _emailSender = emailSender;
        }*/


       /* [BindProperty]
        public UserDTO Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }*/


        /* public class InputModel
         {

             [Required]
             [EmailAddress]
             [Display(Name = "Email")]
             public string Email { get; set; }

             [Required]
             [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
             [DataType(DataType.Password)]
             [Display(Name = "Password")]
             public string Password { get; set; }


             [DataType(DataType.Password)]
             [Display(Name = "Confirm password")]
             [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
             public string ConfirmPassword { get; set; }
         }*/


        public async Task OnGetAsync(string returnUrl = null)
        {
          /*  ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();*/
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
           /* returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var result = await _userService.Create(Input);

                //var user = CreateUser();
*//*
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);*//*
                //دستی پاک کردم نباید کنم
                //var result = await _userManager.CreateAsync(user, Input.Password);
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var userId = await _userManager.GetUserIdAsync(user);
                    *//* var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                     code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                     var callbackUrl = Url.Page(
                         "/Account/ConfirmEmail",
                         pageHandler: null,
                         values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                         protocol: Request.Scheme);*/

                    /* await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*//*

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            // If we got this far, something failed, redisplay form*/
            return Page();
        }

       /* private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }*/

       /* private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }*/
    }
}
