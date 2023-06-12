// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using AutoMapper;
using Cart.Business.Exceptions;
using Cart.Business.Interfaces;
using Cart.Business.Models;
using Cart.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using NoSql.Models;

namespace Cart.Business.Implementations;

public sealed class CartingService : ICartingService
{
    private readonly IProductItemRepository _productItemRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CartingService> _logger;

    public CartingService(IProductItemRepository productItemRepository,
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<CartingService> logger)
    {
        _productItemRepository = productItemRepository ?? throw new ArgumentNullException(nameof(productItemRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void AddItem(string cartId, ProductItemEntity item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (!_cartRepository.IsExists(cartId))
        {
            _cartRepository.Create(cartId);
        }

        NoSql.Models.Cart? cart = _cartRepository.GetById(cartId);

        if (cart == null)
        {
            _logger.LogError($"Cart with ID: {cartId} wasn't found");
            throw new EntityNotFoundException($"Cart with ID: {cartId} wasn't found");
        }

        var dbProductItem = _mapper.Map<ProductItem>(item);

        dbProductItem.CartId = cart.Id;

        _productItemRepository.Add(dbProductItem);
    }

    public void DeleteItem(string cartId, int itemId)
    {
        _productItemRepository.Delete(cartId, itemId);
    }

    public IEnumerable<CartEntity>? GetAll()
    {
        var carts = _cartRepository.GetAll()?
            .Select(_mapper.Map<CartEntity>);

        return carts;
    }

    public IEnumerable<ProductItemEntity> GetItems(string cartId)
    {
        var cart = _cartRepository.GetById(cartId);

        if (cart == null)
        {
            _logger.LogError($"Cart with ID: {cartId} wasn't found");
            throw new EntityNotFoundException($"Cart with ID: {cartId} wasn't found");
        }

        var productItems = _productItemRepository.GetProductItems(cart.Id)
            .Select(_mapper.Map<ProductItemEntity>);

        return productItems;
    }

    public void UpdateItems(IEnumerable<ProductMessage> messages)
    {
        var messageProductIds = messages.Select(message => message.Id);
        var productsById = _productItemRepository.GetProductItems(messageProductIds)
            .GroupBy(product => product.ExternalId)
            .ToDictionary(product => product.Key, product => product.Single());

        foreach (var message in messages.Where(message => productsById.ContainsKey(message.Id)))
        {
            var product = productsById[message.Id];

            product.Name = message.Name;
            product.Price = message.Price;
            product.Quantity = message.Amount;
        }

        _productItemRepository.UpdateRange(productsById.Values);
    }
}
