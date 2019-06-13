using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public interface IFunctionStrategyService<TKey, TIn, TOut>
    {
        IFunctionStrategyService<TKey, TIn, TOut> SetDefaultStrategy(Func<TIn, TOut> func);
        IFunctionStrategyService<TKey, TIn, TOut> AddStrategy(TKey key, Func<TIn, TOut> func);
        IFunctionStrategyService<TKey, TIn, TOut> AddStrategy(IDictionary<TKey, Func<TIn, TOut>> strategies);
        TOut Execute(TKey key, TIn param);
    }
}
