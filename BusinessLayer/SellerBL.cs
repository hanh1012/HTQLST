using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class SellerBL
    {
        private SellerDL sellerDL;

        public SellerBL()
        {
            sellerDL = new SellerDL();
        }

        public bool LoginAdmin(SellerDTO seller)
        {
            return seller.SellerName == "Admin" && seller.SellerPassword == "Password";
        }

        public bool LoginSeller(SellerDTO seller)
        {
            return sellerDL.Login(seller);
        }
    }
}
