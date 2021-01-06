using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tree.Core.Helper;
using Tree.Core.Redis;
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
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public byte[] ImageVerificationCode()
        
        {
            var image = ImageHelper.CreateVerifyImage(out var code);
            //将验证码存储到redis中登录时进行判断
            var redis = RedisHelper.RedisClient();
            redis.StringSet($"code:{code.ToLower()}", code.ToLower(), TimeSpan.FromMinutes(5));
            return image.ToArray();
        }

        /// <summary>
        /// 图片验证码
        /// toby 2020-12-11 17:05:57
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ImageVerificationCodes()
        {
            var data = ImageVerificationCode();
            return File(data, @"image/png");
        }
    }
}
