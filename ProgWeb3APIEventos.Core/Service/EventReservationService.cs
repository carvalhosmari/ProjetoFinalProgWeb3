using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> GetAllReservations()
        {
            return _eventReservationRepository.GetAllReservations();
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertReservation(eventReservation);
        }

        public bool UpdateReservation(long id, EventReservation eventReservation)
        {
            return _eventReservationRepository.UpdateReservation(id, eventReservation);
        }

        public bool DeleteReservation(long id)
        {
            return _eventReservationRepository.DeleteReservation(id);
        }
        
    }
}
