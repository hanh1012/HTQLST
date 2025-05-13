using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class BillDTO
    {
        public int BillId { get; set; }
        public string SellerName { get; set; }
        public string BillDate { get; set; }
        public int TotalAmount { get; set; }
    }
}
