using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetAllEvents()
        {
            var query = "SELECT * FROM CityEvent;";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public List<CityEvent> GetByTitle(string eventTitle)
        {
            var query = "SELECT * FROM CityEvent WHERE Title LIKE CONCAT ('%', @title, '%');";

            var parameter = new DynamicParameters();
            parameter.Add("title", eventTitle);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameter).ToList();
        }

        public List<CityEvent> GetByLocalAndDate(string local, DateTime date)
        {
            var query = "SELECT * FROM CityEvent WHERE Local LIKE CONCAT ('%', @local, '%') AND CONVERT(DATE, DateHourEvent) = @dateHourEvent;";

            var parameter = new DynamicParameters();
            parameter.Add("local", local);
            parameter.Add("dateHourEvent", date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameter).ToList();
        }

        public List<CityEvent> GetByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime date)
        {
            var query = "SELECT * FROM CityEvent WHERE (Price >= @minPrice AND Price <= @maxPrice) AND CONVERT(DATE, DateHourEvent) = @dateHourEvent;";

            var parameter = new DynamicParameters();
            parameter.Add("minPrice", minPrice);
            parameter.Add("maxPrice", maxPrice);
            parameter.Add("dateHourEvent", date);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameter).ToList();
        }

        public bool InsertEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price, 1);";

            var parameter = new DynamicParameters();
            parameter.Add("title", cityEvent.Title);
            parameter.Add("description", cityEvent.Description);
            parameter.Add("dateHourEvent", cityEvent.DateHourEvent);
            parameter.Add("local", cityEvent.Local);
            parameter.Add("address", cityEvent.Address);
            parameter.Add("price", cityEvent.Price);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) == 1;
        }

        public bool UpdateEvent(long id, CityEvent cityEvent)
        {
            cityEvent.IdEvent = id;

            var query = @"UPDATE CityEvent SET 
Title = @title,
Description = @description,
DateHourEvent = @dateHourEvent,
Local = @local,
Address = @address,
Price = @price,
Status = @status
WHERE IdEvent = @idEvent;";

            var parameter = new DynamicParameters();
            parameter.Add("idEvent", id);
            parameter.Add("title", cityEvent.Title);
            parameter.Add("description", cityEvent.Description);
            parameter.Add("dateHourEvent", cityEvent.DateHourEvent);
            parameter.Add("local", cityEvent.Local);
            parameter.Add("address", cityEvent.Address);
            parameter.Add("price", cityEvent.Price);
            parameter.Add("status", cityEvent.Status);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) == 1;
        }

        public bool DeleteEvent(long id)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @idEvent";

            var parameter = new DynamicParameters();
            parameter.Add("idEvent", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) == 1;
        }

        public bool HaveReservation(long id)
        {
            var query = @"SELECT * FROM CityEvent ce
INNER JOIN EventReservation er on er.IdEvent = ce.IdEvent
WHERE ce.IdEvent = @idEvent";

            var parameter = new DynamicParameters();
            parameter.Add("idEvent", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) > 0;
        }

        public bool IsActive(long id)
        {
            var query = "SELECT Status FROM CityEvent WHERE IdEvent = @idEvent";

            var parameter = new DynamicParameters();
            parameter.Add("idEvent", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            if (conn.QueryFirstOrDefault(query, parameter) == null)
            {
                return false;
            }

            return conn.QueryFirstOrDefault(query, parameter);
        }
    }
}
