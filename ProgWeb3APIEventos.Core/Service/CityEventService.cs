using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Core.Service
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;

        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public List<CityEvent> GetAllEvents()
        {
            return _cityEventRepository.GetAllEvents();
        }

        public List<CityEvent> GetByTitle(string title)
        {
            return _cityEventRepository.GetByTitle(title);
        }

        public List<CityEvent> GetByLocalAndDate(string local, DateTime date)
        {
            return _cityEventRepository.GetByLocalAndDate(local, date);
        }

        public bool InsertEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertEvent(cityEvent);
        }

        public bool UpdateEvent(long id, CityEvent cityEvent)
        {
            return _cityEventRepository.UpdateEvent(id, cityEvent);
        }

        public bool DeleteEvent(long id)
        {
            return _cityEventRepository.DeleteEvent(id);
        }
    }
}
