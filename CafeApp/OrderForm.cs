using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeApp
{
    public partial class OrderForm : Form
    {
        public delegate void orderDelagate(Order order);
        public delegate double calculateTotalDelegate(Order order);
        public delegate double calculateIncomeDelegate(List<Order>lo);

        // Calculate total anonymous method
        calculateTotalDelegate c = delegate (Order or)
        {
            return ((or.Coffee * 19.95) + (or.Donuts * 29.95));
        };

        // Calculate total anonymous method
        calculateIncomeDelegate ci = delegate (List<Order> lOrders) {
            double sum = 0;
            foreach (Order od in lOrders)
            {
                sum += ((od.Coffee * 19.95) + (od.Donuts * 29.95));

            }
            return sum;
        };

        public OrderForm()
        {
            InitializeComponent();
        }
        public void Odercollection(Order order)
        {
            if (order.coffeeReady == true && order.donutsReady == true)
            {
                MessageBox.Show(order.Name+" is ready for collection");
            }
        }
        
        public void saveToDB(Order order)
        {
            MessageBox.Show("Save to DB");
        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string phone = txtName.Text;
            int donuts = Convert.ToInt32(txtDonuts.Text);
            int coffee = Convert.ToInt32(txtCoffee.Text);

            Order order = new Order(name, phone, donuts, coffee);
            // Example of an obsolete method
            //MessageBox.Show(order.getValues());

            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext criteria = new ValidationContext(order);
            bool validCheck = Validator.TryValidateObject(order, criteria, validationResults,true);
            if (!validCheck)
            {
                foreach (ValidationResult result in validationResults)
                {
                    MessageBox.Show(result.ErrorMessage);
                }
            }
            else
            {
                orderDelagate placedOder = new orderDelagate(Odercollection);
                placedOder += saveToDB;

                ProcessOrder.Barista(order, placedOder);
                ProcessOrder.Donuteer(order, placedOder);

                // Invoking the calculateTotalDelegate 
                MessageBox.Show(c(order).ToString());

                // Invoking the calculateIncomeDelegate
                List<Order> lo = new List<Order>();
                MessageBox.Show(ci(lo).ToString());
            }
            
           

        }
    }
}
