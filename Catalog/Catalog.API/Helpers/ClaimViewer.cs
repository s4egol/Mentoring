// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Catalog.API.Helpers;

public class ClaimViewer
{
    public ClaimViewer(string name, IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims, nameof(claims));

        Name = name ?? throw new ArgumentNullException(nameof(name));
        Claims = claims.ToList();
        Token = "N/A";
    }

    public ClaimViewer(string name, string tokenJson, bool skipParsing = false)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));

        if (!skipParsing)
        {
            Claims = ((JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(tokenJson)).Claims?.ToList() ?? new List<Claim>();
        }

        Token = tokenJson;
    }

    public List<Claim> Claims { get; } = default!;

    public string Name { get; }

    public string Token { get; }
}
