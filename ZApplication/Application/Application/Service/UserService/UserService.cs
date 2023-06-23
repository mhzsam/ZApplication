using Application.Interface;
using Domain.Entites;


namespace Application.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> unitOfWork;
        public string Login(string Email, string PassWord)
        {
            throw new NotImplementedException();
        }
    }
}
