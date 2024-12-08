using Planificador_Fabrica.Enums;
using System.Linq.Expressions;

namespace Planificador_Fabrica.Filters
{
    public static class FilterExtensions
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Filter filter)
        {
            foreach(var criteria in filter.Filters)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, criteria.FieldName);
                var constant = Expression.Constant(criteria.Value);
                Expression comparison = null;
                switch (criteria.FilterCondition)
                {
                    case FilterCondition.Equals:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case FilterCondition.NotEquals:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    case FilterCondition.GreaterThan:
                        comparison = Expression.GreaterThan(property, constant);
                        break;
                    case FilterCondition.GreaterThanOrEqual:
                        comparison = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case FilterCondition.LessThan:
                        comparison = Expression.LessThan(property, constant);
                        break;
                    case FilterCondition.LessThanOrEqual:
                        comparison = Expression.LessThanOrEqual(property, constant);
                        break;
                    case FilterCondition.Contains:
                        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        comparison = Expression.Call(property, method!, constant);
                        break;
                    default:
                        throw new NotSupportedException($"Condition {criteria.FilterCondition} is not supported.");
                }
                var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                query = query.Where(lambda);
            }
            return query;
        }
    }
}
