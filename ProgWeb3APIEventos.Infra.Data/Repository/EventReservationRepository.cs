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

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity);";

            var parameter = new DynamicParameters();
            parameter.Add("idEvent", eventReservation.IdEvent);
            parameter.Add("personName", eventReservation.PersonName);
            parameter.Add("quantity", eventReservation.Quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) == 1;
        }

        public bool UpdateReservation(long id, EventReservation eventReservation)
        {
            eventReservation.IdReservation = id;

            var query = "UPDATE EventReservation SET Quantity = @quantity";

            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            parameter.Add("quantity", eventReservation.Quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) == 1;
        }

        public bool DeleteReservation(long id)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @idReservation";

            var parameter = new DynamicParameters();
            parameter.Add("idReservation", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameter) == 1;
        }
    }
}
