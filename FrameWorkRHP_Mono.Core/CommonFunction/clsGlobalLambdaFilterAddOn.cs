using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.CommonFunction
{
    public class clsGlobalLambdaFilterAddOn<TEntity> where TEntity : class
    {
        public async Task<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> GetOrderBy(string orderColumn, string orderType)
        {
            #region Sample to call
            /*
             * INI HANYA MENGMABLIKAN METHOD ORDER BY JIKA ADA YANG MAU DICUSTOM BASED ON 1 KOLOM DESC/ASC
             * 
             // Define your order column and order type
                string orderColumn = "PropertyName"; // Specify the property name you want to order by
                string orderType = "asc"; // Specify either "asc" for ascending or "desc" for descending

                // Call the GetOrderBy method to get the order expression
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByFunc = GetOrderBy(orderColumn, orderType);

                // BARU DISINI DI GUNAKAN UNTUK FILTER DI FUNGSI GET REPOSITORY NYA
                IEnumerable<TEntity> result = repository.Get(filter: null, orderBy: orderByFunc); 

             */
            #endregion


            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumn.Split('.');
            IQueryable<TEntity> query = new List<TEntity>().AsQueryable<TEntity>();
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type, "x");

            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();
        }


    }
}
