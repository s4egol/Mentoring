﻿// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Catalog.API.Models.Query;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Models.Product.Queries;

public class ProductQuery : PageQueryParams
{
    [FromQuery(Name = "category-id")]
    public int? CategoryId { get; set; }
}
