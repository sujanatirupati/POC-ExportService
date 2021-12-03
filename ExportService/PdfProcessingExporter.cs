﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Graphics;
using Telerik.Windows.Documents.Fixed.Model.Editing.Tables;
using System.Threading.Tasks;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using System.Reflection;
using Telerik.Windows.Documents.Spreadsheet.Model;
using System.Data;
using Telerik.Blazor.Components;
using System.ComponentModel.DataAnnotations;
using Telerik.Documents.Primitives;

namespace ExportService
{
    public class PdfProcessingExporter
    {
        public async Task<byte[]> ExportData<T>(TelerikGridData<T> gridData)
        {
            PdfFormatProvider provider = new PdfFormatProvider();

            var document = await GenerateDocumentFromGrid(gridData);

            byte[] fileBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                provider.Export(document, ms);
                fileBytes = ms.ToArray();
            }

            return await Task.FromResult(fileBytes);
        }

        private async Task<RadFixedDocument> GenerateDocumentFromGrid<T>(TelerikGridData<T> gridData)
        {
            var table = await CreateDataTableFromGrid<T>(gridData);
            var document = new RadFixedDocument();
            using var editor = new RadFixedDocumentEditor(document);

            editor.InsertTable(table);

            return await Task.FromResult(document);
        }

        private async Task<Table> CreateDataTableFromGrid<T>(TelerikGridData<T> gridData)
        {
            List<T> dataToExport = (List<T>)gridData.grid.Data;
            var columnHeaders = gridData.dicColumnHeaders;
            Type typeParameterType = typeof(T);
            var fieldsList = typeParameterType.GetProperties();
            List<string> ColumnFields = fieldsList.Select(c => c.Name).ToList();
            var columnsState = gridData.grid.GetState().ColumnStates;
            Dictionary<int, string> dicColumns = new Dictionary<int, string>();
            int index = 0;
            foreach (var columnState in columnsState)
            {
                var columnField = ColumnFields[columnState.Index];
                dicColumns.Add(index, columnField);
                index++;
            }

            Table table = new Table();
            table.DefaultCellProperties.Padding = new Thickness(10, 6, 10, 6);
            Border blackBorder = new Border(2, new RgbColor(0, 0, 0));
            table.DefaultCellProperties.Borders = new TableCellBorders(blackBorder, blackBorder, blackBorder, blackBorder);
            var headerRow = table.Rows.AddTableRow();
            foreach (var item in dicColumns)
            {
                string headerText = columnHeaders[item.Value];
                TableCell cell = headerRow.Cells.AddTableCell();
                cell.Blocks.AddBlock().InsertText(headerText);
                cell.Background = new RgbColor(61, 176, 247);
            }

            for (int i = 0; i < dataToExport.Count; i++)
            {
                var row = table.Rows.AddTableRow();
                foreach (var item in dicColumns)
                {
                    var cellValue = GetFieldValue(dataToExport[i], item.Value);

                    row.Cells
                        .AddTableCell().Blocks.AddBlock()
                        .InsertText(cellValue.ToString());
                }
            }

            return await Task.FromResult(table);
        }

        private object GetFieldValue(object item, string fieldName)
        {
            PropertyInfo propertyInfo = item.GetType().GetProperty(fieldName);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(item);
            }
            return null;
        }


        /// <summary>
        ///  Below methods are old which are working
        /// </summary>

        public async Task<byte[]> ExportData1<T>(List<T> data)
        {
            PdfFormatProvider provider = new PdfFormatProvider();

            var document = await GenerateDocument(data);

            byte[] fileBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                provider.Export(document, ms);
                fileBytes = ms.ToArray();
            }

            return await Task.FromResult(fileBytes);
        }     

        private async Task<RadFixedDocument> GenerateDocument<T>(List<T> data)
        {
            var table = await CreateDataTable<T>(data);
            var document = new RadFixedDocument();
            using var editor = new RadFixedDocumentEditor(document);

            editor.InsertTable(table);

            return await Task.FromResult(document);
        }

        private async Task<Table> CreateDataTable<T>(List<T> data)
        {
            // var dataResult = await data.ToDataSourceResultAsync(gridRequest);
            List<T> dataToExport = data;

            int currRow = 0;

            Type typeParameterType = typeof(T);
            var fieldsList = typeParameterType.GetProperties();

            Table table = new Table();

            Border blackBorder = new Border(2, new RgbColor(0, 0, 0));
            table.DefaultCellProperties.Borders = new TableCellBorders(blackBorder, blackBorder, blackBorder, blackBorder);

            var headerRow = table.Rows.AddTableRow();
            for (int i = 0; i < fieldsList.Length; i++)
            {
                TableCell cell = headerRow.Cells.AddTableCell();
                cell.Blocks.AddBlock().InsertText(fieldsList[i].Name);
                cell.Background = new RgbColor(111, 111, 111);
            }

            for (int i = 0; i < dataToExport.Count; i++)
            {
                var row = table.Rows.AddTableRow();
                for (int columnIndex = 0; columnIndex < fieldsList.Length; columnIndex++)
                {
                    var cellValue = GetFieldValue(dataToExport[i], fieldsList[columnIndex].Name);

                    row.Cells
                        .AddTableCell().Blocks.AddBlock()
                        .InsertText(cellValue.ToString());
                }
                currRow++;
            }

            return await Task.FromResult(table);
        }      
    }
}
