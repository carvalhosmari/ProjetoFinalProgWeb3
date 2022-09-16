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

        public List<EventReservation> GetByPersonNameAndTitle(string personName, string title)
        {
            return _eventReservationRepository.GetByPersonNameAndTitle(personName, title);
        }

        public bool InsertReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertReservation(eventReservation);
        }

        public bool UpdateReservation(long id, long quantity)
        {
            return _eventReservationRepository.UpdateReservation(id, quantity);
        }

        public bool DeleteReservation(long id)
        {
            return _eventReservationRepository.DeleteReservation(id);
        }
        
    }
}
