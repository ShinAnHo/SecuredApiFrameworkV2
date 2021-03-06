using Domain.Api;
using Domain.Resource.Business;
using Domain.Resource.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Domain.Resource.Web
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly string _label = "News";
        private readonly INews _news;
        public NewsController(IServiceProvider serviceProvider)
        {
            this._news = serviceProvider.GetRequiredService<INews>();
        }
        // Return all news in database and can be accessed by everyone
        [HttpGet, Route("")]
        //[Authorize, RequireClaim("user.role", "reader", "writer", "supervisor"), RequireClaim("scope", "samplenewsapi.read")]
        public async Task<IActionResult> Get()
        {
            var userRole = HttpContext.Request.GetClaim("user.role");

            if (userRole == "writer")
            {
                var currentUser = HttpContext.Request.GetClaim("sub");
                var data = await _news.GetByUser(currentUser);
                var result = new ApiResult(_label, ApiMessageType.SuccessfullyFetch, data);
                var response = new ApiResponse(HttpStatusCode.OK, result);

                return this.SendResponse(response);
            }
            else
            {
                var data = await _news.GetAll();
                var result = new ApiResult(_label, ApiMessageType.SuccessfullyFetch, data);
                var response = new ApiResponse(HttpStatusCode.OK, result);

                return this.SendResponse(response);
            }
        }
        // Return single news in database and can be accessed by everyone
        [HttpGet, Route("{Id}")]
        //[Authorize, RequireClaim("user.role", "reader", "writer", "supervisor")]
        public async Task<IActionResult> Get(long Id)
        {
            var data = await _news.GetById(Id);
            var result = new ApiResult(_label, ApiMessageType.SuccessfullyFetch, data);
            var response = new ApiResponse(HttpStatusCode.OK, result);

            return this.SendResponse(response);
        }
        // write a news in database and can be accessed only by writer and supervisor
        [HttpPost, Route("insert")]
        //[Authorize, RequireClaim("user.role", "writer", "supervisor"), RequireClaim("scope", "samplenewsapi.write")]
        public async Task<IActionResult> Insert([FromBody] News data)
        {
            var currentUser = HttpContext.Request.GetClaim("sub");
            var claims = HttpContext.Request.GetClaims();

            if (!claims.Any(c => c.Type == "scope" && c.Value == "samplenewsapi.write"))
                return Unauthorized();
            else
            {
                await _news.Insert(data, currentUser);
                var result = new ApiResult(_label, ApiMessageType.SuccessfullyWrite, data);
                var response = new ApiResponse(HttpStatusCode.OK, result);

                return this.SendResponse(response);
            }
        }
        // update a news in database and can be accessed only by writer and supervisor
        // writer can only update their own news while supervisor can access all news
        [HttpPost, Route("update")]
        //[Authorize, RequireClaim("user.role", "writer", "supervisor"), RequireClaim("scope", "samplenewsapi.write")]
        public async Task<IActionResult> Update([FromBody] News data)
        {
            var currentRole = HttpContext.Request.GetClaim("user.role");
            var currentUser = HttpContext.Request.GetClaim("sub");

            var news = await _news.GetById(data.Id);

            if (currentRole != "supervisor" && news.CreatedBy != currentUser)
                return BadRequest();
            else
            {
                await _news.Update(data, currentUser);
                var result = new ApiResult(_label, ApiMessageType.SuccessfullyUpdate, data);
                var response = new ApiResponse(HttpStatusCode.OK, result);

                return this.SendResponse(response);
            }
        }
        // delete a news in database and can be accessed only by supervisor
        [HttpPost, Route("delete")]
        //[Authorize, RequireClaim("user.role", "supervisor"), RequireClaim("scope", "samplenewsapi.write")]
        public async Task<IActionResult> Delete([FromBody] News data)
        {
            var currentUser = HttpContext.Request.GetClaim("sub");

            await _news.Delete(data, currentUser);
            var result = new ApiResult(_label, ApiMessageType.SuccessfullyDelete, data);
            var response = new ApiResponse(HttpStatusCode.OK, result);

            return this.SendResponse(response);
        }
    }
}
