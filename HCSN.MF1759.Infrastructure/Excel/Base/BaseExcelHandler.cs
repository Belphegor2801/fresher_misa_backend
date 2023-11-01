using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Base xử lý file excel
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// Created by: nxhinh (04/10/2023)
    public class BaseExcelHandler<TEntityDto, TEntityCreateDto> : IBaseExcelHandler<TEntityDto, TEntityCreateDto>
    {
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="entitityDto">Dữ liệu</param>
        /// <returns>Dòng của file</returns>
        /// Created by: nxhinh (04/10/2023) 
        public async Task<byte[]> ExportToExcel(IEnumerable<TEntityDto> entitityDto, ExcelOptions excelOptions)
        {
            using var stream = new MemoryStream();

            using (var package = new ExcelPackage())
            {
                // Chuyển đổi dữ liệu từ dạng IEnumerable sang List
                var listEntities = entitityDto.ToList();


                // Tạo sheet mới
                var workSheet = package.Workbook.Worksheets.Add(excelOptions.SheetName);

                // Bảng dữ liệu
                var table = workSheet.Cells;
                var header = workSheet.Cells[1, 1];

                // Gán dữ liêu vào bảng
                var columnsList = excelOptions.Columns;
                if (columnsList != null)
                {
                    table = workSheet.Cells[1, 1, listEntities.Count + 1, columnsList.Count + 1];
                    header = workSheet.Cells[1, 1, 1, columnsList.Count + 1];

                    // Cột STT 
                    workSheet.Cells[1, 1].Value = "STT";
                    workSheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Style cho dòng header
                    header.Style.Font.Bold = true;
                    header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    header.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#c6efce"));

                    // Các cột còn lại
                    for (int col = 0; col < columnsList.Count; col++)
                    {
                        // Gán header cho các cột
                        workSheet.Cells[1, col + 2].Value = columnsList[col].ColumnName;

                        // Căn chỉnh và format theo kiểu dữ liệu
                        switch (columnsList[col].DataType)
                        {
                            case DataType.Number:
                                workSheet.Column(col + 2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                workSheet.Column(col + 2).Style.Numberformat.Format = "#,##0";
                                break;
                            case DataType.Date:
                                workSheet.Column(col + 2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                workSheet.Column(col + 2).Style.Numberformat.Format = "dd/mm/yyyy";
                                break;
                            case DataType.Text:
                                workSheet.Column(col + 2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                break;
                            case DataType.Percentage:
                                workSheet.Column(col + 2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                workSheet.Column(col + 2).Style.Numberformat.Format = "0.00";
                                break;
                            default:
                                break;
                        }

                        // Gán giá trị cho các cột
                        if (listEntities != null)
                        {
                            // Gán giá trị cho cột STT
                            for (int row = 0; row < listEntities.Count; row++)
                            {
                                workSheet.Cells[row + 2, 1].Value = row + 1;
                            }

                            for (int row = 0; row < listEntities.Count; row++)
                            {
                                // Gán giá trị cho các cột tương ứng 
                                var entity = listEntities[row];
                                var propertyName = columnsList[col].FieldName;
                                if (!string.IsNullOrEmpty(propertyName) && entity != null)
                                {
                                    var property = entity.GetType().GetProperty(propertyName);
                                    if (property != null)
                                    {
                                        workSheet.Cells[row + 2, col + 2].Value = property.GetValue(entity);
                                    }
                                }
                            }
                        }

                        // Chỉnh lại độ rộng cột
                        var columnWidth = columnsList[col].ColumnWidth ?? 15;
                        workSheet.Column(col + 2).AutoFit(columnWidth, columnWidth + 15);
                    }
                }

                // Đặt lại độ rộng cho cột STT
                workSheet.Column(1).Width = 5;
                // Đặt chiều cao cho các dòng
                workSheet.DefaultRowHeight = 25;
                // Căn giữa theo chiều dọc
                table.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Thêm viền
                table.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                table.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                table.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                table.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                await package.SaveAsAsync(stream);
            }
            stream.Position = 0;

            var fileBytes = stream.ToArray();
            return fileBytes;
        }

        public virtual Task<IEnumerable<TEntityCreateDto>> ImportFromExcel(byte[] fileBytes)
        {

            throw new NotImplementedException();
        }

        public virtual Task<byte[]> ExampleFileExcel(TEntityDto entityDto, ExcelOptions excelOptions)
        {
            throw new NotImplementedException();
        }
    }
}
