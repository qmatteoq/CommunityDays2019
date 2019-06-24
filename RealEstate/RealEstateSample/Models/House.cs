using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateSample.Models
{
    public class House : BindableBase
    {
        public int HouseId { get; set; }
        public string Longitude { get; set; }

        public string Latitude { get; set; }

       
        public int Beds { get; set; }
        public string Descriptor { get; set; }

        public int SqrFeet { get; set; }

        public int Price { get; set; }

        public int YearBuilt { get; set; }
        public int ContactPhone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public DateTime? DateSold { get; set; }

        public string ZipCode { get; set; }

        public string Region { get; set; }

        public string State { get; set; }
        public int Baths { get; set; }
        public int PhotoCount { get; set; }
        public List<HousePhoto> Photos { get; set; }
        public string Notes { get; set; }

        public List<School> Schools { get; set; }
    }
}