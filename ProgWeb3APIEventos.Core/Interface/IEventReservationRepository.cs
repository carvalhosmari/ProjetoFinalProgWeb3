using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Core.Interface
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetAllReservations();
        List<EventReservation> GetByPersonNameAndTitle(string personName, string title);
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservation(long id, EventReservation eventReservation);
        bool DeleteReservation(long id);
    }
}
