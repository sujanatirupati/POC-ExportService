using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Blazor.Components;

namespace ExportService
{
    public class TelerikGridData<T>
    {
        public TelerikGrid<T> grid { get; set; }

        public Dictionary<string, string> dicColumnHeaders { get; set; }

        // Need to work on
        //public bool AllPages { get; set;  }

        //public int PageSize { get; set; }
    }
}
