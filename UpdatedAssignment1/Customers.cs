using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedAssignment1
{
    class Customers
    {
        public int customerID { get; private set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }

        public Customers(int customerID, string title, string firstName, string lastName, string gender, string DOB)
        {
            this.customerID = customerID;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            this.DOB = DOB;
        }
    }
}
