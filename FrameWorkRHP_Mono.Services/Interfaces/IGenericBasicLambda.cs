using System.Linq.Expressions;

namespace FrameWorkRHP_Mono.Services.Interfaces
{
    public interface IGenericBasicLambda<T> where T : class
    {
         Task<Expression<Func<T, bool>>> BuildDynamicFilter(string name);
    }
}
