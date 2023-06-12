// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

namespace Catalog.API.Extensions;

public static class HttpContextExtensions
{
    public static string GetCorrelationId(this HttpContext context)
    {
        var correlationId = Guid.NewGuid().ToString();
        var isHeadersNotDefined = context?.Request?.Headers == null || !context.Request.Headers.Any();

        if (isHeadersNotDefined)
        {
            return correlationId;
        }

        var header = context.Request.Headers[Constants.Constants.Headers.CorrelationIdHeader];

        if (header.Count > 0)
        {
            correlationId = header[0] ?? correlationId;
        }

        context.Items[Constants.Constants.Headers.CorrelationIdHeader] = correlationId;

        return correlationId;
    }
}
