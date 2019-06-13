using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public interface IActionStrategyService<TKey, TIn>
    {
        IActionStrategyService<TKey, TIn> SetDefaultStrategy(Action<TIn> action);
        IActionStrategyService<TKey, TIn> AddStrategy(TKey key, Action<TIn> action);
        IActionStrategyService<TKey, TIn> AddStrategy(IDictionary<TKey, Action<TIn>> strategies);
        void Execute(TKey key, TIn obj);
    }
}
