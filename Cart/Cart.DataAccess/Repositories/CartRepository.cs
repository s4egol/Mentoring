// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Cart.DataAccess.Interfaces;
using NoSql.Context;

namespace Cart.DataAccess.Repositories;

public class CartRepository : ICartRepository
{
    public readonly CartContext _dbContext;

    public CartRepository(CartContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void Create(string id)
        => _dbContext.Carts.Add(new NoSql.Models.Cart { Id = id });

    public IEnumerable<NoSql.Models.Cart>? GetAll()
    {
        var carts = _dbContext.Carts
            .AsEnumerable();

        return carts;
    }

    public NoSql.Models.Cart? GetById(string id)
    {
        var cart = _dbContext.Carts
            .FirstOrDefault(cart => cart.Id == id);

        return cart;
    }

    public bool IsExists(string id)
    {
        var isCartExists = _dbContext.Carts
            .Where(cart => cart.Id == id)
            .Any();

        return isCartExists;
    }
}
