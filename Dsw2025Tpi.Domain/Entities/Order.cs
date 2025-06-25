using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Enum;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order : EntityBase
    {
        public DateTime Date { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> OrderItems { get; set; }

        // FK de Customer
        public Guid CustomerId { get; set; }

        // Calculo de TotalAmount
        public void CalculateTotalAmount()
        {
            TotalAmount = OrderItems.Sum(item => item.Subtotal);
        }
    }
}