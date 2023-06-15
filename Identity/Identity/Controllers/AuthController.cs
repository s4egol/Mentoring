// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IIdentityServerInteractionService interactionService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _interactionService = interactionService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel { ReturnUrl = returnUrl };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        var user = await _userManager.FindByNameAsync(login.UserName);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(login);
        }

        var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

        if (result.Succeeded)
        {
            return Redirect(login.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Login error");

        return View(login);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var viewModel = new RegistrationViewModel { ReturnUrl = returnUrl };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel registration)
    {
        if (!ModelState.IsValid)
        {
            return View(registration);
        }

        var user = new AppUser { UserName = registration.UserName, };
        var result = await _userManager.CreateAsync(user, registration.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Redirect(registration.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Registration error");

        return View(registration);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();

        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
}
