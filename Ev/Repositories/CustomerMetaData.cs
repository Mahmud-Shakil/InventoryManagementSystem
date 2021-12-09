using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ev.Repositories
{
    public class CustomerMetaData
    {
        [Remote("IsCustomerAvailable", "Customer", ErrorMessage = "Name already is in use")]
        public string EmployeeName { get; set; }
    }
}