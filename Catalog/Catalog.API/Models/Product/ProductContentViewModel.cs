// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Product;

public class ProductContentViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }

    [Required]
    public int? CategoryId { get; set; }
}
