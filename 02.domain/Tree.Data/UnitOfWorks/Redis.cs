using System;
using System.Collections.Generic;
using System.Text;

namespace Tree.Data.UnitOfWorks
{
    /// <summary>
    /// Reids 参数
    /// </summary>
    public class Redis
    {
        public string Link { get; set; }

        public string TokenDb { get; set; }

        public string DefaultDb { get; set; }
    }
}
