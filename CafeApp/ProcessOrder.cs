using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CafeApp.OrderForm;

namespace CafeApp
{
    class ProcessOrder
    {
        public static void Barista(Order order, orderDelagate p)
        {
            MessageBox.Show("Processing coffee order - Qty =" + order.Coffee);
            order.coffeeReady = true;
            p(order);
        }
        public static void Donuteer(Order order, orderDelagate p)
        {
            MessageBox.Show("Processing Donuts order - Qty =" + order.Donuts);
            order.donutsReady = true;
            p(order);
        }
    }
}
