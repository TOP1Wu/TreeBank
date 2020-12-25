
using System.Data;
using System;
using Tree.Core.Domain.Repositories;
using Tree.Core.Domain.UnitOfWork;
using Tree.Data.Entities.Test;
using Tree.Data.Repositories.Test.Abstractions;
using System.Threading.Tasks;

namespace Tree.Data.Repositories.Test
{
   public class TestStudent : Repository<Student, Guid>, ITestStudent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public TestStudent(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 课节下拉列表
        /// </summary>
        /// <returns></returns>
        public async Task<object> Query()
        {
            var sql = $@"select * from Student";
            var list = await QueryAsync<Student>(sql, CommandType.Text);
            return list;
        }
    }
}
