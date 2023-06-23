using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.UserService
{
    public interface IUserService
    {
        public string Login(string Email, string PassWord);
    }
}
