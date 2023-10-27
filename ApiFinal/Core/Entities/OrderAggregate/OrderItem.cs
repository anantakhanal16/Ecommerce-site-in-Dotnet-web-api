using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem: BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, 
            decimal price, 
            int quality)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quality = quality;
        }

        public ProductItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }

        public int Quality { get; set; }

    }
}
