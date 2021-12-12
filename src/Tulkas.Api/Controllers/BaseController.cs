using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tulkas.Core.Components;

namespace Tulkas.Api.Controllers
{
    [ApiController]
    public class BaseController: ControllerBase
    {
        protected async Task<IActionResult> GetResponse<T>(Func<Task<T>> func)
        {
            try
            {
                var response = await func();
                return Ok(new ApiResponse<T>(response));
            }
            catch (CustomInformationException e)
            {
                return StatusCode(e.Code, new ApiResponse(e));
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ApiResponse(e.Message));
            }
        }
        
        protected async Task<IActionResult> GetResponse(Func<Task> func)
        {
            try
            {
                await func();
                return Ok(new ApiResponse());
            }
            catch (CustomInformationException e)
            {
                return StatusCode(e.Code, new ApiResponse(e));
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ApiResponse(e.Message));
            }
        }
    }
}