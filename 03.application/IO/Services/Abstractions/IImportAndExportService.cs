using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tree.IO.Services.Abstractions
{
    /// <summary>
    /// 导入
    /// </summary>
    public interface IImportAndExportService
    {
        /// <summary>
        /// 学生导出
        /// </summary>
        /// <returns></returns>
        Task<MemoryStream> ExportStudent();

        object Main(string Content, string Contents);
    }
}
