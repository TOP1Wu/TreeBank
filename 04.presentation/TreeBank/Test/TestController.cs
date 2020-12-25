using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tree.Data.Repositories.Test.Abstractions;

namespace TreeBank.Test
{
    [Route("Test/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        ///// <summary>
        ///// 附件删除
        ///// leo 2020年11月25日17:28:24
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(typeof(ApiResponseContent<object>), 200)]
        //public async Task<IActionResult> Delete(IdInput input)
        //{
        //    await AnnexService.Delete(input);
        //    return JsonSuccess();
        //}
        public TestController(ITestStudent testStudent)
        {
            TestStudent = testStudent;
        }

        public ITestStudent TestStudent { get; }
        public Task<object> Get()
        {
            var list = TestStudent.Query();

            return list;
        }
    }
}
