﻿// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.Security.Claims;
using Identity.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure;

public class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly string[] _availableRoles =
    {
        Constants.Roles.Buyer,
        Constants.Roles.Manager
    };

    public ProfileService(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);

        if (user == null)
        {
            return;
        }

        var claims = new List<Claim>();

        foreach (var availableRole in _availableRoles)
        {
            var hasRole = await _userManager.IsInRoleAsync(user, availableRole);

            if (hasRole)
            {
                var role = await _roleManager.FindByNameAsync(availableRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                claims.AddRange(roleClaims);
            }
        }

        context.IssuedClaims.AddRange(claims);
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
