using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tree.Core.Domain.Repositories;
using Tree.Data.Entities.Test;

namespace Tree.Data.Repositories.Test.Abstractions
{
    public interface ITestStudent : IRepository<Student, Guid>
    {
        public Task<object> Query();
    }
}
