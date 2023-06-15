// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Newtonsoft.Json;

namespace Catalog.Business.Models;

public class ProductMessage
{
    [JsonProperty("id")]
    public int Id { get; init; }
    [JsonProperty("name")]
    public string Name { get; init; } = default!;
    [JsonProperty("description")]
    public string? Description { get; init; }
    [JsonProperty("price")]
    public float Price { get; init; }
    [JsonProperty("amount")]
    public int Amount { get; init; }
}
