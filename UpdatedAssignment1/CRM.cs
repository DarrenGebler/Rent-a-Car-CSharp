using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedAssignment1
{
    class CRM
    {
        private List<Customers> customers;
        public string CustomerFileName { get; set; }

        public CRM()
        {
            customers = new List<Customers>();
        }

        public CRM(List<Customers> customers)
        {
            this.customers = new List<Customers>(customers);
        }

        public List<Customers> GetCustomer()
        {
            return customers;
        }

        public void AddCustomer(Customers customer)
        {
            customers.Add(customer);
        }

        public void RemoveCustomer(Customers customer)
        {
            customers.Remove(customer);
        }

        public Customers GetCustomer(int customerID)
        {
            foreach (Customers customer in customers)
            {
                if (customer.customerID == customerID)
                {
                    return customer;
                }
            }
            return null;
        }

        public void LoadFromFile()
        {
            System.IO.StreamReader fileReader = new System.IO.StreamReader(@CustomerFileName);

            fileReader.ReadLine();

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                Customers newCustomer = new Customers(int.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], parts[5]);
                customers.Add(newCustomer);
            }
            fileReader.Close();
        }

        public void SaveToFile()
        {
            System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(@CustomerFileName);

            fileWriter.WriteLine("CustomerID", "Title", "First Name", "Last Name", "Gender", "DOB");

            foreach (Customers cust in customers)
            {
                fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", cust.customerID.ToString(), cust.Title, cust.FirstName, cust.LastName, cust.Gender, cust.DOB);
            }
            fileWriter.Close();
        }
    }
}
