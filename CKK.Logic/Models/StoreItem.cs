using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    internal class StoreItem
    {
        private Product storeProduct;
        private int storeQuantity;

        public StoreItem(Product product, int Quantity)

        {
            storeProduct = product;
            storeQuantity = Quantity;
        }

        public Product GetProduct()

        {
            return storeProduct;
        }

        public void SetProduct(Product product)

        {
            storeProduct = product;
        }

        public int GetQuantity()

        {
            return storeQuantity;
        }

        public void SetQuantity(int Quantity)

        {
            storeQuantity = Quantity;
        }
    }
}
