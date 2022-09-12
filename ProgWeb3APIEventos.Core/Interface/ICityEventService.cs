using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Core.Interface
{
    public interface ICityEventService
    {
        List<CityEvent> GetAllEvents();
        List<CityEvent> GetByTitle(string eventTitle);
        List<CityEvent> GetByLocalAndDate(string local, DateTime date);
        bool InsertEvent(CityEvent cityEvent);
        bool UpdateEvent(long id, CityEvent cityEvent);
        bool DeleteEvent(long id);
    }
}
