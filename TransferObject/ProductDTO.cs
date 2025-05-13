using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class ProductDTO
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public int ProdQty { get; set; }
        public string ProdPrice { get; set; }
        public string ProdCat { get; set; }
    }
}
