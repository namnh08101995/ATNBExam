using ATNB.Model;
using ATNB.Repository.Abstractions;
using ATNB.Service.Abstractions;

namespace ATNB.Service.Services
{
    public class AirPortService : EntityService<AirPort> ,IAirPortService
    {
        IUnitOfWork _unitOfWork;
        IAirPortRepository _airportRepository;

        public AirPortService(IUnitOfWork unitOfWork, IAirPortRepository airportRepository)
            :base(unitOfWork, airportRepository)
        {
            _unitOfWork = unitOfWork;
            _airportRepository = airportRepository;
        }

        public AirPort GetById(string id)
        {
            return _airportRepository.GetById(id);
        }
    }
}
