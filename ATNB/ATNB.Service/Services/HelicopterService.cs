using ATNB.Model;
using ATNB.Repository.Abstractions;
using ATNB.Service.Abstractions;

namespace ATNB.Service.Services
{
    public class HelicopterService : EntityService<Helicopter>, IHelicopterService
    {
        IUnitOfWork _unitOfWork;
        IHelicopterRepository _helicopterRepository;

        public HelicopterService(IUnitOfWork unitOfWork, IHelicopterRepository helicopterRepository)
            :base(unitOfWork, helicopterRepository)
        {
            _unitOfWork = unitOfWork;
            _helicopterRepository = helicopterRepository;
        }

        public Helicopter GetById(string id)
        {
            return _helicopterRepository.GetById(id);
        }
    }
}
