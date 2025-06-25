using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; private set; }

        // FK de Order y Product
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        //Guardar las referencias a las entidades Order y Product
        public Order Order { get; set; }
        public Product Product { get; set; }

        // Calculo de Subtotal
        public void CalculateSubtotal()
        {
            Subtotal = (decimal)(Quantity * UnitPrice);
        }
    }
}