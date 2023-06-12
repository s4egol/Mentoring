// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

namespace Catalog.Business.Models;

public class CategoryEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public int? ParentId { get; set; }
    public CategoryEntity? Parent { get; set; }
}
