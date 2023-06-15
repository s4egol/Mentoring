// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using NoSql.Context.Abstract;
using NoSql.Models;

namespace NoSql.Context;

public class CartContext : LiteDbContext
{
    public readonly LiteDbSet<Cart> Carts;

    public readonly LiteDbSet<ProductItem> ProductItems;

    public CartContext(string databasePath) : base(databasePath)
    {
        Carts = new LiteDbSet<Cart>(InternalDatabase);
        Carts.ConfigureIndices(x => x.Id);
        ProductItems = new LiteDbSet<ProductItem>(InternalDatabase);
    }
}
