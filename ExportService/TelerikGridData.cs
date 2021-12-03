using System.Collections.Generic;
using Telerik.Blazor.Components;

namespace ExportService
{
    public class TelerikGridData<T>
    {
        public TelerikGrid<T> Grid { get; set; }

        public Dictionary<string, string> ColumnHeaders { get; set; }

        public TelerikGridData(TelerikGrid<T> grid, Dictionary<string, string> columnHeaders)
        {
            Grid = grid;
            ColumnHeaders = columnHeaders;
        }

        // Need to work on
        //public bool AllPages { get; set;  }

        //public int PageSize { get; set; }
    }
}
