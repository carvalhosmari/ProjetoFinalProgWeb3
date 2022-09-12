using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Core.Interface
{
    public interface IEventReservationService
    {
        List<EventReservation> GetAllReservations();
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservation(long id, EventReservation eventReservation);
        bool DeleteReservation(long id);
    }
}
