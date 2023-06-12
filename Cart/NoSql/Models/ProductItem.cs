// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

namespace NoSql.Models;

public class ProductItem
{
    public int ExternalId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public string CartId { get; set; } = default!;
}
