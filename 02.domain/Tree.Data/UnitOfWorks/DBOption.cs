using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Tree.Data.UnitOfWorks
{
    public class DBOption : Controller
    {
        public string ConnectionString { get; set; }
    }
}
