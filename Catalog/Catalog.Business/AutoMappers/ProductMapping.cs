// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using AutoMapper;
using Catalog.Business.Models;
using Catalog.Business.Models.Queries;
using Catalog.DataAccess.Models.Filters;
using ORM.Entities;

namespace Catalog.Business.AutoMappers;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductEntity>();
        CreateMap<ProductEntity, Product>();
        CreateMap<ProductEntity, ProductMessage>();
        CreateMap<ProductQueryEntity, ProductFilter>();
    }
}
