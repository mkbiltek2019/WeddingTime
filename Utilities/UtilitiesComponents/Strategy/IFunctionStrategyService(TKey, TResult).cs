using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public interface IFunctionStrategyService<TKey, TResult>
    {
        IFunctionStrategyService<TKey, TResult> SetDefaultStrategy(Func<TResult> func);
        IFunctionStrategyService<TKey, TResult> AddStrategy(TKey key, Func<TResult> func);
        IFunctionStrategyService<TKey, TResult> AddStrategy(IDictionary<TKey, Func<TResult>> strategies);
        TResult Execute(TKey key);
    }
}
