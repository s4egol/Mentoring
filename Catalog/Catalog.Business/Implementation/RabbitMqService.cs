// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Catalog.Business.Configuration;
using Catalog.Business.Interfaces;
using RabbitMQ;
using RabbitMQ.Client;

namespace Catalog.Business.Implementation;

public class RabbitMqService : IRabbitMqService
{
    private readonly AppSettings _appSettings;
    private readonly Publisher _publisher;

    public RabbitMqService(AppSettings appSettings)
    {
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        _publisher = new Publisher(new ConnectionFactory { HostName = _appSettings.RabbitMqServerSettings.ConnectionString });
    }

    public void SendMessage<T>(string queue, T obj) => _publisher.SendMessage(queue, obj);
}
