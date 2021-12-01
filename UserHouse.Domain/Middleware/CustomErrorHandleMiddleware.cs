using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserHouse.Application.Helpers;

namespace UserHouse.Application.Middleware
{
    public class CustomErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case CustomUserFriendlyException e:
                        //Custom application error
                        //Use it when you expect an error for user
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        //Built-in not found exception
                        //Use it when you expect to get 404 error
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    default:
                        //Any other unhandled errors
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {message = error?.Message});

                await response.WriteAsync(result);
            }
        }
    }
}
