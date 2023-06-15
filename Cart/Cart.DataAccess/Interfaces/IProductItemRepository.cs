// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using NoSql.Models;

namespace Cart.DataAccess.Interfaces;

public interface IProductItemRepository
{
    IEnumerable<ProductItem> GetProductItems(string cartId);
    IEnumerable<ProductItem> GetProductItems(IEnumerable<int> productIds);
    void Add(ProductItem productItem);
    void UpdateRange(IEnumerable<ProductItem> productItems);
    void Delete(string cartId, int productItemId);
}
