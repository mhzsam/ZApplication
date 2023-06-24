using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class DataAnotationException : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // ایجاد شیء جدید برای ریسپانس سفارشی
                var response = new
                {
                    Errors = errors
                };

                // تغییر ریسپانس خطای ورودی به ریسپانس سفارشی
                context.Result = new JsonResult(response)
                {
                    StatusCode = 400 // کد وضعیت Bad Request
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // دسترسی به نتیجه اجرای عملیات کنترل کننده
        }
    }
}
