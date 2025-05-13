using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        public int BillId { get; set; }
        public int ProdId { get; set; }
        public int Quantity { get; set; }
    }
}