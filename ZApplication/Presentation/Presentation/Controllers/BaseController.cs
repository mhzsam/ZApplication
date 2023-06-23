using Application.DTO.ResponseModel;
using Application.Service.ResponseService;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<T>:  ControllerBase where T : BaseController<T>
    {
        protected readonly IResponseService responseGenerator;

        public BaseController(IResponseService res)
        {
            responseGenerator = res;
        }

        
    }
}
