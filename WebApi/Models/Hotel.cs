using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    

    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        

        public ICollection<City> City { get; set; }

    }

    public class City
    {
        [Key]
        public int CityID { get; set; }

        public int CountryID { get; set; }

        public string CityName { get; set; }
    }
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string HotelName { get; set; }
        [Required]

        public int CityId { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Adddress { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }

        public City City { get; set; }
    }
    public class HotelVM    {
       
        public int HotelId { get; set; }

        
       
        public string HotelName { get; set; }
        

        public int CityId { get; set; }
       
       
        public string Adddress { get; set; }
      
       
        public string Description { get; set; }

        public int CountryID { get; set; }

        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
