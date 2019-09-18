using System.Collections.Generic;
using System.Linq;
using OnlineStore.WebUI.ApplicationData;

namespace OnlineStore.WebUI.Models
{
    public class Cart {
        private List<CartLine> lineCollection = new List<CartLine>();

        public int? SaleId { get; private set; }

        public void AddItem(SaleProduct saleProduct, int quantity, AssetAllocation assetAllocation) {
            if(!this.SaleId.HasValue)
            {
                this.SaleId = saleProduct.Sale.Id;
            }

            CartLine line = lineCollection
                .Where(sp => sp.SaleProduct.Id == saleProduct.Id)
                .FirstOrDefault();

            if (line == null) {
                lineCollection.Add(new CartLine {
                    SaleProduct = saleProduct,
                    AssetAllocation = assetAllocation,
                    Quantity = quantity
                });
            } else if (assetAllocation == null) {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(SaleProduct saleProduct) {
            lineCollection.RemoveAll(l => l.SaleProduct.Id == saleProduct.Id);
        }

        public decimal ComputeTotalValue() {
            return lineCollection.Sum(e => e.SaleProduct.PriceIncGST.Value * e.Quantity);

        }
        public void Clear() {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines {
            get { return lineCollection; }
        }
    }

    public class CartLine {
        public SaleProduct SaleProduct { get; set; }
        public AssetAllocation AssetAllocation { get; set; }
        public int Quantity { get; set; }
    }
}
