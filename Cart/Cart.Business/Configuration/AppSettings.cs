// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Cart.Business.Configuration.Settings;
using Microsoft.Extensions.Options;

namespace Cart.Business.Configuration;

public class AppSettings
{
    private readonly RabbitMqServerSettings _rabbitMqServerSettings;

    public AppSettings(IOptions<RabbitMqServerSettings> rabbitMqServerSettings)
    {
        _rabbitMqServerSettings = rabbitMqServerSettings?.Value ?? throw new ArgumentNullException(nameof(rabbitMqServerSettings));
    }

    public RabbitMqServerSettings RabbitMqServerSettings => _rabbitMqServerSettings;
}
