using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Core.Domain.Repositories;
using Tree.Core.Domain.UnitOfWork;
using Tree.Data.Entities.Test;

namespace Tree.Data.Repositories.ImportAndExport.Abstractions
{
    /// <summary>
    /// 导出
    /// </summary>
   public class ImportAndExport  : Repository<Student, Guid>, IImportAndExport
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ImportAndExport(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<List<Student>> ImportAndExports()
        {
            var sql = $@"select * from Student";
            var data = await QueryAsync<Student>(sql);
            return data.ToList();


        }
    }
}
