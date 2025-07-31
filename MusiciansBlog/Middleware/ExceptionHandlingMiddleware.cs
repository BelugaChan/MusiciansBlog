using Microsoft.EntityFrameworkCore;
using MusiciansBlog.API.Exceptions;
using System.Net;

namespace MusiciansBlog.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ExceptionResponse response = ex switch
            {
                EntityAlreadyExistsException _ => new ExceptionResponse(HttpStatusCode.Conflict, "объект уже существует"),
                EntityNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "объект не найден"),
                DbUpdateException _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "DB update wasn't successfull"),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "ошибка сервера")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
