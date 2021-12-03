using System;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorApp2.Models;

namespace TelerikBlazorApp2.Services
{
    public sealed class MockDataService
    {
        public IReadOnlyCollection<MockData> GetData()
        {
            var list1 = Enumerable.Range(1, 3).Select(n => new MockData
            {
                Id = n,
                ProductName = $"Product {n}",
                Units = n * 2,
                Price = 3.14159m * n,
                Discontinued = n % 4 == 0,
                ReleaseDate = DateTime.Now.AddDays(-n)
            });

            var list2 = Enumerable.Range(4, 6).Select(n => new MockData
            {
                Id = n,
                ProductName = $"Product is in stock {n}",
                Units = n * 2,
                Price = 3.14159m * n,
                Discontinued = n % 4 == 0,
                ReleaseDate = DateTime.Now.AddDays(-n)
            });

            var list3 = Enumerable.Range(7, 10).Select(n => new MockData
            {
                Id = n,
                ProductName = $"Products out of stock {n}",
                Units = n * 2,
                Price = 3.14159m * n,
                Discontinued = n % 4 == 0,
                ReleaseDate = DateTime.Now.AddDays(-n)
            });

            return list1.Concat(list2).Concat(list3).ToList();
        }
    }
}
