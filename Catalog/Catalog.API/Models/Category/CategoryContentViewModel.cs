// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Category;

public class CategoryContentViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }
    [Url]
    public string? Image { get; set; }
    public int? ParentId { get; set; }
}
