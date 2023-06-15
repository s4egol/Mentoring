// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

namespace Catalog.Business.Models.Queries;

public class PageQueryEntity
{
    private const int DEFAULT_PAGE_NUMBER = 1;
    private const int DEFAULT_PAGE_SIZE = 5;

    /// <summary>
    /// A page number having a numeric value of 1 or greater
    /// </summary>
    public int Page { get; set; } = DEFAULT_PAGE_NUMBER;

    /// <summary>
    /// A page size having a numeric value of 1 or greater.static Represents the number of tracks returned per page.
    /// </summary>
    public int Limit { get; set; } = DEFAULT_PAGE_SIZE;
}
