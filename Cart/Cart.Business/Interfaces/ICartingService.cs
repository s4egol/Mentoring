// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Cart.Business.Models;

namespace Cart.Business.Interfaces;

public interface ICartingService
{
    IEnumerable<CartEntity>? GetAll();
    IEnumerable<ProductItemEntity> GetItems(string cartId);
    void AddItem(string cartId, ProductItemEntity item);
    void DeleteItem(string cartId, int itemId);
    void UpdateItems(IEnumerable<ProductMessage> messages);
}
