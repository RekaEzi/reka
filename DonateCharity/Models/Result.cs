using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateCharity.Models
{
    public class Result
    {
        public string Status { get; set; }
        public string TransactionID { get; set; }
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
    }
}