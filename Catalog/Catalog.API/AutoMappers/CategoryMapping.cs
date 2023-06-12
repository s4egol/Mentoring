// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using AutoMapper;
using Catalog.API.Models.Category;
using Catalog.Business.Models;

namespace Catalog.API.AutoMappers;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<CategoryViewModel, CategoryEntity>();
        CreateMap<CategoryEntity, CategoryViewModel>();
        CreateMap<CategoryContentViewModel, CategoryEntity>();
    }
}
