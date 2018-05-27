using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedAssignment1
{
    class VRM
    {
        private List<Vehicles> vehicles;
        public Dictionary<string, int> rentals;


        public string FleetFileName { get; set; }

        public VRM()
        {
            vehicles = new List<Vehicles>();
            rentals = new Dictionary<string, int>();
        }

        public VRM(List<Vehicles> vehicles) => this.vehicles = new List<Vehicles>(vehicles);

        public List<Vehicles> GetVehicles() => vehicles;

        public List<Vehicles> GetVehiclesSearch(string searchQuery, int minCost, int maxCost)
        {
            List<Vehicles> requestedVehicles = new List<Vehicles>();
            String[] queryParts = searchQuery.Split(' ');
            for (int i = 0; i < queryParts.Length; i+=2)
            {
                if (i == 0 )
                {
                    foreach (Vehicles vehNotRenting in GetVehiclesRented(false))
                    {
                        if (vehNotRenting.GetAttributesList().Contains(queryParts[i]) && vehNotRenting.DailyRate >= minCost && vehNotRenting.DailyRate <= maxCost)
                        {
                            requestedVehicles.Add(vehNotRenting);
                        }
                    }
                }
                else if (queryParts[i - 1] == "OR" )
                {
                    List<Vehicles> tempVehicles = new List<Vehicles>();
                    foreach (Vehicles vehNotRenting in GetVehiclesRented(false))
                    {
                        if (vehNotRenting.GetAttributesList().Contains(queryParts[i]) && vehNotRenting.DailyRate >= minCost && vehNotRenting.DailyRate <= maxCost)
                        {
                            tempVehicles.Add(vehNotRenting);
                        }
                    }
                    requestedVehicles = requestedVehicles.Union(tempVehicles).ToList<Vehicles>();
                }
                else if (queryParts[i - 1] == "AND")
                {
                    List<Vehicles> tempVehicles = new List<Vehicles>();
                    foreach (Vehicles vehNotRenting in GetVehiclesRented(false))
                    {
                        if (vehNotRenting.GetAttributesList().Contains(queryParts[i]) && vehNotRenting.DailyRate >= minCost && vehNotRenting.DailyRate <= maxCost)
                        {
                            tempVehicles.Add(vehNotRenting);
                        }
                    }
                    requestedVehicles = requestedVehicles.Union(tempVehicles).ToList<Vehicles>();
                }
            }
            return requestedVehicles;
        }

        public List<Vehicles> GetVehiclesRented(bool rented)
        {
            List<Vehicles> requestedVehicles = new List<Vehicles>();
            foreach (Vehicles veh in vehicles)
            {
                if (rentals.ContainsKey(veh.rego) == false)
                {
                    requestedVehicles.Add(veh);
                }

            }
            return requestedVehicles;
        }

    
        public bool IsRenting (int customerID)
        {
            foreach (Vehicles veh in vehicles)
            {
                if (rentals.ContainsValue(customerID) == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsRentingCar(string rego)
        {
            foreach (Vehicles veh in vehicles)
            {
                if (rentals.ContainsKey(veh.rego))
                {
                    return true;
                }
            }
            return false;
        }

        /*public bool RentCar(string vehicleRego, int customerID)
        {
            if ()
            {

            }
            else 
            {
                return false;
            }
        }

        public int ReturnCar(string vehicleRego)
        {

        }*/

        public void AddVehicle(Vehicles vehicle)
        {
            vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicles vehicle)
        {
            vehicles.Remove(vehicle);
        }

        public Vehicles GetVehicles(string rego)
        {
            foreach (Vehicles customer in vehicles)
            {
                if (customer.rego == rego)
                {
                    return customer;
                }
            }
            return null;
        }

        public void LoadFromFile()
        {
            System.IO.StreamReader fileReader = new System.IO.StreamReader(@FleetFileName);

            fileReader.ReadLine();

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                Vehicles newVehicle = new Vehicles(parts[0], parts[1], parts[2], int.Parse(parts[3]), parts[4], int.Parse(parts[5]),
                    parts[6], parts[7], bool.Parse(parts[8]), bool.Parse(parts[9]), parts[10], int.Parse(parts[11]));
                vehicles.Add(newVehicle);
            }
            fileReader.Close();
        }

        public Vehicles GetVehiclesrego(string rego)
        {
            foreach (Vehicles Vehicles in vehicles)
            {
                if (Vehicles.rego == rego)
                {
                    return Vehicles;
                }
            }
            return null;
        }

        public void SaveToFile()
        {
            System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(@FleetFileName);

            fileWriter.WriteLine("Rego,Make,Model,Year,Vehicle Class,NumSeats,Transmission,Fuel,GPS,SunRoof,Colour,DailyRate");

            foreach (Vehicles veh in vehicles)
            {
                fileWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", veh.rego, veh.Make, veh.Model, veh.Year, veh.VehClass, veh.Seats, veh.Transmission,
                    veh.Fuel, veh.gps, veh.SunRoof, veh.Colour, veh.DailyRate);
            }
            fileWriter.Close();
        }

    }
}
