using System;

namespace TelerikBlazorApp2.Models
{
    public sealed class MockData
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Units { get; set; }

        public decimal Price { get; set; }

        public bool Discontinued { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
