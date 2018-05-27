using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedAssignment1
{
    class Vehicles
    {
        public string rego { get; private set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VehClass { get; set; }
        public int Seats { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public bool gps { get; set; }
        public bool SunRoof { get; set; }
        public int DailyRate { get; set; }
        public string Colour { get; set; }

        public Vehicles(string rego, string make, string model, int year, string vehClass, int seats, 
            string transmission, string fuel, bool GPS, bool sunRoof, string colour, int dailyRate)
        {
            this.rego = rego;
            Make = make;
            Model = model;
            Year = year;
            VehClass = vehClass;
            Seats = seats;
            Transmission = transmission;
            Fuel = fuel;
            gps = GPS;
            SunRoof = sunRoof;
            DailyRate = dailyRate;
            Colour = colour;
        }

        public List<string> GetAttributesList()
        {
            List<string> attributes = new List<string>();

            attributes.Add(rego);
            attributes.Add(Make);
            attributes.Add(Model);
            attributes.Add(Year.ToString());
            attributes.Add(VehClass);
            attributes.Add(Seats.ToString());
            attributes.Add(Transmission);
            attributes.Add(Fuel);
            if (gps == true)
            {
                attributes.Add("gps");
            }
            attributes.Add(SunRoof.ToString());
            if (SunRoof == true)
            {
                attributes.Add("sunroof");
            }
            attributes.Add(DailyRate.ToString());
            attributes.Add(Colour);

            return attributes;
        }
    }
}
