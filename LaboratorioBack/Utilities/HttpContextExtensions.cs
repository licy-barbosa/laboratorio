using Microsoft.EntityFrameworkCore;

namespace LaboratorioBack.Utilities
{
    //video 108
    public static class HttpContextExtensions
    {
        public async static Task InjectPaginationParameters<T>(this HttpContext httpContext, IQueryable<T> queryble) {

            if (httpContext is null) { 
                throw new ArgumentNullException(nameof(httpContext));
            }

            double quantity = await queryble.CountAsync();
            httpContext.Response.Headers.Append("records-total", quantity.ToString());
        }
    }
}