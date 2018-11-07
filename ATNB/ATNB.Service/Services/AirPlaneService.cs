using ATNB.Model;
using ATNB.Repository.Abstractions;
using ATNB.Service.Abstractions;

namespace ATNB.Service.Services
{
    public class AirPlaneService:EntityService<AirPlane>, IAirPlaneService
    {
        IUnitOfWork _unitOfWork;
        IAirPlaneRepository _airplaneRepository;

        public AirPlaneService(IUnitOfWork unitOfWork, IAirPlaneRepository airplaneRepository)
            : base(unitOfWork, airplaneRepository)
        {
            _unitOfWork = unitOfWork;
            _airplaneRepository = airplaneRepository;
        }
        public AirPlane GetById(string id)
        {
            return _airplaneRepository.GetById(id);
        }
    }
}
