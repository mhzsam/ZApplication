using Application.DTO.UserService;
using Application.Helper;
using Application.Interface;
using Domain.Common;
using Domain.Entites;
using Mapster;

namespace Application.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> _unitOfWork;

        public UserService(IUnitOfWork<User> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAll(int PageNumber, int pageSize)
        {
            return await _unitOfWork.GetAllAsync(PageNumber,pageSize);
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.GetAsync(id);
        }

        public string Login(string Email, string PassWord)
        {
            throw new NotImplementedException();
        }

        public Task<User> SingUp(AddUserModel addUserModel)
        {
            addUserModel.Password = PasswordHelper.PasswordToSHA256(addUserModel.Password);

            //var res = addUserModel.Adapt<User>();
            var user = Mapper<User>.ToEntity(addUserModel);
            _unitOfWork.Insert(user);
            _unitOfWork.SaveChanges();
            return Task.FromResult(user);

        }

    }
}
