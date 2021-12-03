
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Windows.Printing;
//using System.Windows.Controls;
//using Telerik.Windows.Controls;
//using Telerik.Windows.Data;
//using Telerik.Windows.Documents.Model;
//using System;
//using System.Windows;
//using Telerik.Windows.Documents.FormatProviders.Html;
//using System.Windows.Media;
//using Telerik.Windows.Documents.Spreadsheet.Model;
//using Telerik.Windows.Documents.Spreadsheet.Model.Printing;
//using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
//using Telerik.Windows.Controls.ColorEditor;
//using Telerik.Windows.Documents.FormatProviders.Txt;
//using Telerik.Windows.Documents.Model.Styles;
//using System.IO.IsolatedStorage;
//using System.Globalization;
//using System.Runtime.InteropServices.Automation;
//using SampleFrameWork.Common.Silverlight.Helpers;
//using Telerik.Blazor.Components;

//namespace TelerikBlazorApp2.Services
//{
//    public sealed class RadGridFunction
//    {
//        public static void ExportPdf(object paramRadGridView, object paramRadGridpager = null)
//        {
           
//            RadGridView radGridDataView = paramRadGridView as RadGridView;

//            Control radGridPager = null;
//            int originalPgIndex = 0;
//            int originalPgSize = 0;

//            SaveFileDialog dialog = new SaveFileDialog();
//            dialog.DefaultExt = "*.pdf";
//            dialog.Filter = "Adobe PDF Document (*.pdf)|*.pdf";

//            if (dialog.ShowDialog() == true)
//            {
//                //Get original values for pager
//                if (paramRadGridpager != null)
//                {
//                    radGridPager = paramRadGridpager as RadDataPager;
//                    if (radGridPager == null)
//                        radGridPager = (DataPager)paramRadGridpager;

//                    if (radGridPager != null)
//                    {
//                        originalPgIndex = (int)radGridPager.GetType().GetProperty("PageIndex").GetValue(radGridPager, null);
//                        originalPgSize = (int)radGridPager.GetType().GetProperty("PageSize").GetValue(radGridPager, null);

//                        //Set page size to 0
//                        radGridPager.GetType().GetProperty("PageSize").SetValue(radGridPager, 0, null);
//                    }
//                }

//                var gridSelectedItem = radGridDataView.SelectedItem;


//                RadGridExportExcel clsExcel = new RadGridExportExcel();

//                var workbook = clsExcel.CreateWorkBook(radGridDataView, true);

//                if (workbook != null)
//                {
//                    Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider provider = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider();

//                    using (Stream output = dialog.OpenFile())
//                    {
//                        try
//                        {
//                            provider.Export(workbook, output);
//                        }
//                        catch (Exception ex)
//                        {
//                            string exDetail = ex.Message;
//                            //throw;
//                        }
//                        finally
//                        {
//                            output.Flush();
//                            output.Dispose();
//                        }
//                    }
//                }

//                //Set original values for pager
//                if (radGridPager != null)
//                {
//                    radGridPager.GetType().GetProperty("PageSize").SetValue(radGridPager, originalPgSize, null);
//                    radGridPager.GetType().GetProperty("PageIndex").SetValue(radGridPager, originalPgIndex, null);
//                }
//                radGridDataView.SelectedItem = gridSelectedItem;

//            }
//        }
//    }

//    public sealed class RadGridExportExcel
//    {
//        public readonly IFill HeaderRowFill = new GradientFill(GradientType.Horizontal,
//                                                    ColorConverter.ColorFromString("#FF4472C4"),
//                                                    ColorConverter.ColorFromString("#FF4472C4"));
//        public readonly IFill DataRowFill = new GradientFill(GradientType.Horizontal,
//                                                    ColorConverter.ColorFromString("#FFB4C6E7"),
//                                                    ColorConverter.ColorFromString("#FFB4C6E7"));

//        readonly IFill FooterRowFill = new GradientFill(GradientType.Horizontal,
//                                                    new ThemableColor(Colors.LightGray),
//                                                    new ThemableColor(Colors.LightGray));
//        //#FFB2B2
//        public readonly IFill NegativeFill = new GradientFill(GradientType.Horizontal,
//                                                    ColorConverter.ColorFromString("#FFB2B2"),
//                                                    ColorConverter.ColorFromString("#FFB2B2"));

//        public readonly CellBorders Border = new CellBorders(new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Black)),   // Left border
//                new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Black)),   // Top border
//                new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Black)),   // Right border
//                new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Black)),   // Bottom border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)),   // Inside horizontal border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)),   // Inside vertical border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)),   // Diagonal up border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)));  // Diagonal down border

//        public readonly CellBorders ColumnBorder = new CellBorders(new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Gray)),   // Left border
//                new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Gray)),   // Top border
//                new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Gray)),   // Right border
//                new CellBorder(CellBorderStyle.Thin, new ThemableColor(Colors.Gray)),   // Bottom border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Gray)),   // Inside horizontal border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Gray)),   // Inside vertical border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Gray)),   // Diagonal up border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Gray)));  // Diagonal down border

//        public readonly CellBorders footerBorder = new CellBorders(new CellBorder(CellBorderStyle.Thick, new ThemableColor(Colors.Black)),   // Left border
//                new CellBorder(CellBorderStyle.Thick, new ThemableColor(Colors.Black)),   // Top border
//                new CellBorder(CellBorderStyle.Thick, new ThemableColor(Colors.Black)),   // Right border
//                new CellBorder(CellBorderStyle.Thick, new ThemableColor(Colors.Black)),   // Bottom border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)),   // Inside horizontal border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)),   // Inside vertical border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)),   // Diagonal up border
//                new CellBorder(CellBorderStyle.None, new ThemableColor(Colors.Black)));  // Diagonal down border


//        public readonly double maxPageWidthPotrait = 756;   //Maximum unit width in double for Paper Type = Letter & Orientation = Potrait.
//        public readonly double maxPageWidthLandscape = 1050;    //Maximum unit width in double for Paper Type = Letter & Orientation = Landscape.
//        public readonly double maxPageWidthLandscapeLegal = 1284;   //Maximum unit width in double for Paper Type = Legal & Orientation = Landscape.

//        /// <summary>
//        /// Creates a Workbook object using RadGridView object.
//        /// </summary>
//        /// <param name="grid">Object of RadGridView to create the Workbook.</param>
//        /// <returns></returns>
//        internal Workbook CreateWorkBook(RadGridView grid, bool isPDF)
//        {

//            Workbook book = new Workbook();
//            Worksheet worksheet = null;

//            IList<GridViewBoundColumnBase> columns = null;
//            book.History.IsEnabled = false;
//            int startRowIndex = grid.ShowColumnHeaders ? 1 : 0;

//            IList dataItems = grid.Items;

//            if (grid.SortDescriptors.Count > 0)
//            {
//                columns = (from c in grid.Columns.OfType<GridViewBoundColumnBase>().Where(x => x.SortingState != SortingState.None && (x.Header != null && x.Header.ToString() != string.Empty))
//                           orderby c.DisplayIndex
//                           select c).ToList();
//            }
//            else
//            {
//                columns = (from c in grid.Columns.OfType<GridViewBoundColumnBase>().Where(x => x.IsVisible == true && (x.Header != null && x.Header.ToString() != string.Empty))
//                           orderby c.DisplayIndex
//                           select c).ToList();
//            }

//            worksheet = book.Worksheets.Add();
//            int columnCount = columns.Count - 1;
//            int rowCount = grid.ShowColumnHeaders ? dataItems.Count + 1 : dataItems.Count;

//            if (grid.ShowColumnHeaders)
//            {
//                AddHeaderRow(worksheet, columns);

//                CellSelection headerRow = worksheet.Cells[0, 0, 0, columnCount]; // Get header row section of the sheet.
//                headerRow.SetFill(this.HeaderRowFill); // Fill background color to header row section.
//                headerRow.SetBorders(this.Border); // Set border for header row.
//                headerRow.SetForeColor(new ThemableColor(Colors.White)); // Set font color to white for header row.
//                headerRow.SetFontSize(12); // Set font size to 12 for header row.
//                headerRow.SetHorizontalAlignment(RadHorizontalAlignment.Center); // Set Horizontal Alignment to left for header row.
//                headerRow.SetVerticalAlignment(RadVerticalAlignment.Center); // Set Vertical Alignment to right for header row.
//                headerRow.SetIsBold(true); // Set font weight to Bold for header row.
//            }

//            if (grid.ShowColumnFooters)
//            {
//                //CellSelection footerRow = worksheet.Cells[0, 0, 0, columnCount]; // Get header row section of the sheet.
//                CellSelection footerRow = worksheet.Cells[rowCount, 0, rowCount, columnCount];
//                footerRow.SetFill(this.FooterRowFill); // Fill background color to header row section.

//                AddFooterRow(worksheet, rowCount, grid, columns);

//                footerRow.SetBorders(this.footerBorder); // Set border for header row.
//                footerRow.SetForeColor(new ThemableColor(Colors.Black)); // Set font color to white for header row.
//                footerRow.SetFontSize(12); // Set font size to 12 for header row.
//                //footerRow.SetHorizontalAlignment(RadHorizontalAlignment.Center); // Set Horizontal Alignment to left for header row.
//                footerRow.SetVerticalAlignment(RadVerticalAlignment.Center); // Set Vertical Alignment to right for header row.
//                footerRow.SetIsBold(true); // Set font weight to Bold for header row.
//            }


//            if (book.Sheets.Count > 0)
//            {
//                worksheet = (Worksheet)book.Sheets[0];
//                WorksheetPageSetup pageSetup = book.ActiveWorksheet.WorksheetPageSetup;
//                pageSetup.Margins = new PageMargins(30.0);

//                // Set alternate row color to sheet.
//                for (int rowIdx = startRowIndex; rowIdx < rowCount; rowIdx++)
//                {
//                    if (rowIdx % 2 == 0)
//                    {
//                        worksheet.Cells[rowIdx, 0, rowIdx, columnCount].SetFill(this.DataRowFill);
//                    }
//                }

//                AddDataRows(worksheet, startRowIndex, 0, dataItems, columns);

//                CellSelection allDataRows = worksheet.Cells[startRowIndex, 0, startRowIndex + dataItems.Count - 1, columnCount]; //Get data row section of the sheet.                    
//                allDataRows.SetFontSize(12); // Set font size to 12 for data row.
//                allDataRows.SetBorders(this.Border); // Set border for data row.
//                allDataRows.SetVerticalAlignment(RadVerticalAlignment.Center);

//                double totalColumnWidth = 0;
//                int pageCount = 1;

//                // Set column size to autofit. Automanage the size of document under Paper Type=Letter/Legal & Orientation = Potrait/Landscape.
//                for (int columnIdx = 0; columnIdx <= columnCount; columnIdx++)
//                {
//                    worksheet.Columns[columnIdx].AutoFitWidth();
//                    double cWidth = 0;
//                    var colData = worksheet.Columns[columnIdx].GetWidth().Value;

//                    if (colData != null)
//                    {
//                        cWidth = colData.Value;
//                    }

//                    if (cWidth > 0)
//                    {
//                        cWidth = Convert.ToDouble(Math.Round(cWidth, 0));
//                        cWidth += 50;

//                        totalColumnWidth += cWidth;
//                        worksheet.Columns[columnIdx].SetWidth(new ColumnWidth(cWidth, true));

//                        if (isPDF)
//                        {
//                            if (pageCount == 1)
//                            {
//                                double tempWdth = totalColumnWidth - cWidth;

//                                if ((totalColumnWidth > maxPageWidthLandscapeLegal) && (tempWdth < maxPageWidthLandscapeLegal))
//                                {
//                                    var lastColdata = worksheet.Columns[columnIdx - 1].GetWidth().Value;
//                                    double lastColWidth = lastColdata.Value;

//                                    double reqWidth = maxPageWidthLandscapeLegal - tempWdth;

//                                    lastColWidth += reqWidth;
//                                    totalColumnWidth += reqWidth;

//                                    worksheet.Columns[columnIdx - 1].SetWidth(new ColumnWidth(lastColWidth, true));

//                                    pageCount += 1;
//                                }
//                            }

//                            if (pageCount > 1)
//                            {
//                                double newMaxWdth = maxPageWidthLandscapeLegal * pageCount;

//                                if ((totalColumnWidth > newMaxWdth) && (totalColumnWidth - cWidth < newMaxWdth))
//                                {
//                                    var lastColdata = worksheet.Columns[columnIdx - 1].GetWidth().Value;
//                                    double lastColWidth = lastColdata.Value;

//                                    double tempWdth = totalColumnWidth - cWidth;

//                                    double reqWidth = newMaxWdth - tempWdth;

//                                    lastColWidth += reqWidth;
//                                    totalColumnWidth += reqWidth;

//                                    worksheet.Columns[columnIdx - 1].SetWidth(new ColumnWidth(lastColWidth, true));

//                                    pageCount += 1;
//                                }
//                            }
//                        }
//                    }

//                    if (columns[columnIdx].DataType == typeof(string) || columns[columnIdx].DataType == typeof(decimal?))
//                    {
//                        if (worksheet.Rows.GetDefaultHeight() != RowHeight.AutoFit)
//                        {
//                            worksheet.Rows.SetDefaultHeight(RowHeight.AutoFit);
//                        }

//                        worksheet.Cells[0, columnIdx, rowCount, columnIdx].SetIsWrapped(true);
//                    }

//                    worksheet.Cells[0, columnIdx, rowCount, columnIdx].SetBorders(this.ColumnBorder);
//                }

//                pageSetup.PaperType = (totalColumnWidth > maxPageWidthPotrait ? (totalColumnWidth > maxPageWidthLandscape ? PaperTypes.Legal : PaperTypes.Letter) : PaperTypes.Letter);
//                pageSetup.PageOrientation = (totalColumnWidth > maxPageWidthPotrait ? PageOrientation.Landscape : PageOrientation.Portrait);
//            }

//            return book;
//        }

//        /// <summary>
//        /// Adds the header row to the worksheet.
//        /// </summary>
//        /// <param name="worksheet">Instance of worksheet.</param>
//        /// <param name="columns">Collection of columns.</param>
//        private void AddHeaderRow(Worksheet worksheet, IList<GridViewBoundColumnBase> columns)
//        {
//            for (int i = 0; i < columns.Count; i++)
//            {
//                string headerValue = columns[i].Header.ToString();
//                if (columns[i].Header.GetType() == typeof(System.Windows.Controls.TextBlock))
//                {
//                    headerValue = ((System.Windows.Controls.TextBlock)columns[i].Header).Text;
//                }

//                worksheet.Cells[0, i].SetValue(headerValue);
//            }
//        }

//        /// <summary>
//        /// Adds the footer row to the worksheet.
//        /// </summary>
//        /// <param name="worksheet">Instance of worksheet.</param>
//        /// <param name="columns">Collection of columns.</param>
//        private void AddFooterRow(Worksheet worksheet, int rowCount, RadGridView grid, IList<GridViewBoundColumnBase> dataColumns)
//        {
//            var footerData = ((Telerik.Windows.Controls.GridView.GridViewDataControl)(grid)).AggregateResults;

//            for (int i = 0; i < dataColumns.Count; i++)
//            {
//                var aggFunctions = dataColumns[i].AggregateFunctions;
//                if (aggFunctions != null)
//                {
//                    foreach (var item in aggFunctions)
//                    {
//                        string fName = item.FunctionName;
//                        var dataValue = footerData.Where(x => x.FunctionName == fName).Select(x => x.FormattedValue).FirstOrDefault();

//                        if (dataColumns[i].DataType == typeof(System.Decimal?))
//                        {
//                            double fooValue = (dataValue != null) ? Convert.ToDouble(dataValue) : 0;
//                            CellValueFormat format = new CellValueFormat("#,##0.00");
//                            worksheet.Cells[rowCount, i].SetFormat(format);
//                            worksheet.Cells[rowCount, i].SetValue(fooValue);
//                        }
//                        else
//                        {
//                            string fooValue = (dataValue != null) ? Convert.ToString(dataValue) : string.Empty;
//                            CellValueFormat format = new CellValueFormat("@");
//                            worksheet.Cells[rowCount, i].SetFormat(format);
//                            worksheet.Cells[rowCount, i].SetValue(fooValue);
//                        }
//                    }
//                }
//            }
//        }


//        /// <summary>
//        /// Adds the data rows to the worksheet.
//        /// </summary>
//        /// <param name="worksheet">Instance of worksheet.</param>
//        /// <param name="startRowIndex">Starting index for the data row.</param>
//        /// <param name="startColumnIndex">Starting index for the column.</param>
//        /// <param name="items">Collection of data grid items.</param>
//        /// <param name="columns">Collection of columns.</param>
//        private void AddDataRows(Worksheet worksheet, int startRowIndex, int startColumnIndex, IList items, IList<GridViewBoundColumnBase> columns)
//        {
//            for (int rowIndex = 0; rowIndex < items.Count; rowIndex++)
//            {
//                for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
//                {
//                    var value = columns[columnIndex].GetValueForItem(items[rowIndex]);
//                    int currentRowIndex = startRowIndex + rowIndex;
//                    int currentColumnIndex = startColumnIndex + columnIndex;

//                    if (columns[columnIndex].DataType == typeof(System.Decimal?))
//                    {
//                        double dataValue = (value != null && value.ToString() != "") ? Convert.ToDouble(value) : 0;

//                        CellValueFormat format = new CellValueFormat("#,##0.00");
//                        worksheet.Cells[currentRowIndex, currentColumnIndex].SetFormat(format);
//                        worksheet.Cells[currentRowIndex, currentColumnIndex].SetValue(dataValue);
//                    }
//                    else
//                    {
//                        string dataValue = Convert.ToString(value).Replace("`", string.Empty);
//                        if ((dataValue != null || dataValue != string.Empty) && dataValue.Contains("<html"))
//                        {
//                            HtmlFormatProvider provider = new HtmlFormatProvider();
//                            RadDocument document = provider.Import(dataValue);
//                            dataValue = new TxtFormatProvider().Export(document);
//                        }
//                        CellValueFormat format = new CellValueFormat("@");
//                        worksheet.Cells[currentRowIndex, currentColumnIndex].SetFormat(format);
//                        worksheet.Cells[currentRowIndex, currentColumnIndex].SetValue(dataValue);
//                    }
//                }
//            }
//        }
//    }
//}
