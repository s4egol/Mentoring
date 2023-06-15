// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Catalog.API.Helpers;

public class ClaimManager
{
    public ClaimManager(HttpContext context, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        Items = new List<ClaimViewer>();

        var claims = user.Claims.ToList();

        string idTokenJson = context.GetTokenAsync("id_token").GetAwaiter().GetResult() ?? string.Empty;
        string accessTokenJson = context.GetTokenAsync("access_token").GetAwaiter().GetResult() ?? string.Empty;
        string refreshTokenJson = context.GetTokenAsync("refresh_token").GetAwaiter().GetResult() ?? string.Empty;

        AddTokenInfo("Refresh Token", refreshTokenJson, true);
        AddTokenInfo("Identity Token", idTokenJson);
        AddTokenInfo("Access Token", accessTokenJson);
        AddTokenInfo("User Claims", claims);
    }

    public List<ClaimViewer> Items { get; }

    public string AccessToken
    {
        get
        {
            if (Items == null || Items.Count == 0)
            {
                throw new InvalidOperationException("Not tokens found");
            }

            var token = Items.SingleOrDefault(x => x.Name == "Access Token");

            if (token == null)
            {
                throw new InvalidOperationException("Not tokens found");
            }

            return token.Token;
        }
    }

    public string RefreshToken
    {
        get
        {
            if (Items == null || Items.Count == 0)
            {
                throw new InvalidOperationException("Not tokens found");
            }

            var token = Items.SingleOrDefault(x => x.Name == "Refresh Token");

            if (token == null)
            {
                throw new InvalidOperationException("Not tokens found");
            }

            return token.Token;
        }
    }

    private void AddTokenInfo(string nameToken, string idTokenJson, bool skipParsing = false)
    {
        if (string.IsNullOrWhiteSpace(idTokenJson))
        {
            return;
        }

        Items.Add(new ClaimViewer(nameToken, idTokenJson, skipParsing));
    }

    private void AddTokenInfo(string nameToken, IEnumerable<Claim> claims)
    {
        Items.Add(new ClaimViewer(nameToken, claims));
    }
}
