// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using AutoMapper;
using Catalog.API.Models.Product;
using Catalog.API.Models.Product.Queries;
using Catalog.Business.Models;
using Catalog.Business.Models.Queries;

namespace Catalog.API.AutoMappers;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<ProductViewModel, ProductEntity>();
        CreateMap<ProductEntity, ProductViewModel>();
        CreateMap<ProductContentViewModel, ProductEntity>();
        CreateMap<ProductQuery, ProductQueryEntity>();
    }
}
