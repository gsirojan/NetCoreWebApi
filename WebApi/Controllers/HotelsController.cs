using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public HotelsController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelVM>>> GetHotel()
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString =
            "Server=localhost;Database=PaymentDetailDB;Trusted_Connection=True;MultipleActiveResultSets=True;";

            connection.Open();
            string procedureName = "[dbo].[GetHotels]";
            var result = new List<HotelVM>();
            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@HotelId", null));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int HotelId = int.Parse(reader[0].ToString());
                        string HotelName = reader[1].ToString();
                        int CityId = int.Parse(reader[2]?.ToString());
                        string? Address = reader[3].ToString();
                        string? Description = reader[4].ToString();
                        string CityName = reader[5]?.ToString();
                        int CountryId = int.Parse(reader[6]?.ToString());
                        string CountryName = reader[7]?.ToString();
                        HotelVM tmpRecord = new HotelVM()
                        {
                            HotelId = HotelId,
                            HotelName = HotelName,
                            CityId = CityId,
                            Adddress = Address,
                            Description= Description,
                            CityName= CityName,
                            CountryID= CountryId,
                            CountryName= CountryName
                        };
                        result.Add(tmpRecord);
                    }
                }
            }
            return result;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelVM>> GetHotel(int id)
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString =
            "Server=localhost;Database=PaymentDetailDB;Trusted_Connection=True;MultipleActiveResultSets=True;";

            connection.Open();
            string procedureName = "[dbo].[GetHotels]";
            var result = new List<HotelVM>();
            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@HotelId", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int HotelId = int.Parse(reader[0].ToString());
                        string HotelName = reader[1].ToString();
                        int CityId = int.Parse(reader[2]?.ToString());
                        string? Address = reader[3].ToString();
                        string? Description = reader[4].ToString();
                        string CityName = reader[5]?.ToString();
                        int CountryId = int.Parse(reader[6]?.ToString());
                        string CountryName = reader[7]?.ToString();
                        HotelVM tmpRecord = new HotelVM()
                        {
                            HotelId = HotelId,
                            HotelName = HotelName,
                            CityId = CityId,
                            Adddress = Address,
                            Description = Description,
                            CityName = CityName,
                            CountryID = CountryId,
                            CountryName = CountryName
                        };
                        result.Add(tmpRecord);
                    }
                }
            }
            

            if (result == null)
            {
                return NotFound();
            }

            return result.FirstOrDefault();
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.HotelId }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        private bool HotelExists(int id)
        {
            return _context.Hotel.Any(e => e.HotelId == id);
        }
    }
}
