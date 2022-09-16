using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EventReservation> GetAllReservations()
        {
            var query = "SELECT * FROM EventReservation;";

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<EventReservation>(query).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao fazer conexão com o banco de dados.\nMessage: {ex.Message}\nTarget site: {ex.TargetSite}\nStack trace: {ex.StackTrace}");

                return null;
            }
        }

        public List<EventReservation> GetByPersonNameAndTitle(string personName, string title)
        {
            var query = @"SELECT * FROM CityEvent ce
                    INNER JOIN EventReservation er on er.IdEvent = ce.IdEvent
                    WHERE er.PersonName = @personName AND ce.Title LIKE CONCAT ('%', @title, '%')";

            var parameter = new DynamicParameters();
            parameter.Add("personName", personName);
            parameter.Add("title", title);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Query<EventReservation>(query, parameter).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao fazer conexão com o banco de dados.\nMessage: {ex.Message}\nTarget site: {ex.TargetSite}\nStack trace: {ex.StackTrace}");

                return null;
            }
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity);";

            var parameter = new DynamicParameters();
            parameter.Add("idEvent", eventReservation.IdEvent);
            parameter.Add("personName", eventReservation.PersonName);
            parameter.Add("quantity", eventReservation.Quantity);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameter) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao fazer conexão com o banco de dados.\nMessage: {ex.Message}\nTarget site: {ex.TargetSite}\nStack trace: {ex.StackTrace}");

                return false;
            }
        }

        public bool UpdateReservation(long id, EventReservation eventReservation)
        {
            eventReservation.IdReservation = id;

            var query = "UPDATE EventReservation SET Quantity = @quantity";

            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            parameter.Add("quantity", eventReservation.Quantity);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameter) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao fazer conexão com o banco de dados.\nMessage: {ex.Message}\nTarget site: {ex.TargetSite}\nStack trace: {ex.StackTrace}");

                return false;
            }
        }

        public bool DeleteReservation(long id)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @idReservation";

            var parameter = new DynamicParameters();
            parameter.Add("idReservation", id);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameter) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao fazer conexão com o banco de dados.\nMessage: {ex.Message}\nTarget site: {ex.TargetSite}\nStack trace: {ex.StackTrace}");

                return false;
            }
        }
    }
}
