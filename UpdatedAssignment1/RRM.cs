using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedAssignment1
{
    class Rental
    {

        public int customerID { get; private set; }
        public string Rego { get; set; }
        public int RentDuration { get; set; }



        public Rental(string rego, int customerID, int rentDuration)
        {
            this.customerID = customerID;
            Rego = rego;
            RentDuration = rentDuration;
        }
    }
    class RRM
    {
        // public Dictionary<string, int> rentals;

        private List<Rental> rentals;
        public string RentalFileName { get; set; }

        public RRM() => rentals = new List<Rental>();

        public RRM(List<Rental> rentals)
        {
            this.rentals = new List<Rental>(rentals);
        }

        public List<Rental> GetRental()
        {
            return rentals;
        }

        public void AddRental(Rental rental)
        {
            rentals.Add(rental);
        }

        public void Removerental(Rental rental)
        {
            rentals.Remove(rental);
        }

        public Rental GetRental(int customerID)
        {
            foreach (Rental rental in rentals)
            {
                if (rental.customerID == customerID)
                {
                    return rental;
                }
            }
            return null;
        }

        public void LoadFromFile()
        {
            System.IO.StreamReader fileReader = new System.IO.StreamReader(@RentalFileName);

            fileReader.ReadLine();

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                Rental newRental = new Rental(parts[0], int.Parse(parts[1]), int.Parse(parts[2]));

                rentals.Add(newRental);
            }
            fileReader.Close();
        }

        public void SaveToFile()
        {
            System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(@RentalFileName);

            fileWriter.WriteLine("Rego,CustomerID,Duration");

            foreach (Rental cust in rentals)
            {
                fileWriter.WriteLine("{0},{1},{2}", cust.Rego, cust.customerID.ToString(),cust.RentDuration);
            }
            fileWriter.Close();
        }
    }
}



