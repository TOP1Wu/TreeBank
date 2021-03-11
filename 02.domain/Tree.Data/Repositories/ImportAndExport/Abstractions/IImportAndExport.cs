using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tree.Core.Domain.Repositories;
using Tree.Data.Entities.Test;

namespace Tree.Data.Repositories.ImportAndExport.Abstractions
{
    /// <summary>
    /// 导出
    /// </summary>
    public interface IImportAndExport : IRepository<Student, Guid>
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        Task<List<Student>> ImportAndExports();
    }
}
