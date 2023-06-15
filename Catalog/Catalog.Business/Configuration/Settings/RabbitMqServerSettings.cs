// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

namespace Catalog.Business.Configuration.Settings;

public class RabbitMqServerSettings
{
    public string ConnectionString { get; set; } = default!;
    public string Queue { get; set; } = default!;
}
