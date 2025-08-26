using LaboratorioBack.DTOs;
using System.Linq;

namespace LaboratorioBack.Utilities
{
    //video 108
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> queryble, PaginationDTO pagination) {
            return queryble
                .Skip((pagination.PageNumber -1) * pagination.RecordsPage)
                .Take(pagination.RecordsPage);
        }
    }
}
