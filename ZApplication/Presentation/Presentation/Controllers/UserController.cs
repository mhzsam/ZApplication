using Application.DTO.ResponseModel;
using Application.Interface;
using Application.Service.ResponseService;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UserController : BaseController<UserController>
    {
        private readonly IUnitOfWork<User> _unitOfWork;
        public UserController(IResponseService responseService, IUnitOfWork<User> unitOfWork) : base(responseService)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task< RessponseModel> get()
        {
           var model= await _unitOfWork.GetAllAsync(0,10);

           
            return responseGenerator.SuccssedWithResult(model);
        }
    }
}
