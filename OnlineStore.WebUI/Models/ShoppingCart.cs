using System.Collections.Generic;
using System.Linq;
using OnlineStore.WebUI.ApplicationData;

namespace OnlineStore.WebUI.Models
{
    public class ShoppingCart {
        private List<ShoppingCartLine> lineCollection = new List<ShoppingCartLine>();

        public int? SaleId { get; private set; }

        public void AddItem(OnlineSaleProduct saleProduct, int quantity) {
            if(!this.SaleId.HasValue)
            {
                this.SaleId = saleProduct.Id;
            }

            ShoppingCartLine line = lineCollection
                .Where(sp => sp.SaleProduct.Id == saleProduct.Id)
                .FirstOrDefault();

           
                lineCollection.Add(new ShoppingCartLine {
                    
                   SaleProduct = saleProduct,
                   Quantity = quantity
                });         
        }

        public void RemoveLine(OnlineSaleProduct saleProduct) {
            lineCollection.RemoveAll(l => l.SaleProduct.Id == saleProduct.Id);
        }

        public decimal ComputeTotalValue() {
            return lineCollection.Sum(e => e.SaleProduct.PriceIncGST * e.Quantity);

        }
        public void Clear() {
            lineCollection.Clear();
        }

        public IEnumerable<ShoppingCartLine> Lines {
            get { return lineCollection; }
        }
    }

    public class ShoppingCartLine {
        public OnlineSaleProduct SaleProduct { get; set; }
   
        public int Quantity { get; set; }
    }
}
