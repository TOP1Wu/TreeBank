using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tree.Data.Repositories.ImportAndExport.Abstractions;
using Tree.IO.Services.Abstractions;

namespace Tree.IO
{
    public class ImportAndExportService:IImportAndExportService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customerTagRepository"></param>
        public ImportAndExportService(IImportAndExport importAndExport)
        {
            ImportAndExport = importAndExport;
        }

        /// <summary>
        /// 到访记录注入
        /// </summary>
        private IImportAndExport ImportAndExport { get; }

        

        /// <summary>
        /// 学生导出
        /// </summary>
        /// <returns></returns>
        public async Task<MemoryStream> ExportStudent()
        {
            var ExportMembers = await ImportAndExport.ImportAndExports();
            if (ExportMembers == null) throw new Exception("未找到可导出的数据！");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var result = new MemoryStream();
            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("导出会员表");
            sheet.OutLineApplyStyle = true;
            sheet.Cells[1, 1].Value = "序号";
            sheet.Cells[1, 2].Value = "ID";
            sheet.Cells[1, 3].Value = "姓名";
            sheet.Cells[1, 4].Value = "年龄";
            sheet.Cells[1, 5].Value = "班级";
            sheet.Cells[1, 5].Value = "备注";
            for (int i = 0; i < ExportMembers.Count; i++)
            {
                sheet.Cells[2 + i, 1].Value = i + 1;
                sheet.Cells[2 + i, 2].Value = ExportMembers[i].Id;
                sheet.Cells[2 + i, 3].Value = ExportMembers[i].Name;
                sheet.Cells[2 + i, 4].Value = ExportMembers[i].Age;
                sheet.Cells[2 + i, 5].Value = ExportMembers[i].Class;
                sheet.Cells[2 + i, 6].Value = ExportMembers[i].Description;
            }
            package.SaveAs(result);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }
    }
}
