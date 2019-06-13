using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public interface IActionStrategyService<TKey>
    {
        IActionStrategyService<TKey> SetDefaultStrategy(Action action);
        IActionStrategyService<TKey> AddStrategy(TKey key, Action action);
        IActionStrategyService<TKey> AddStrategy(IDictionary<TKey, Action> strategies);
        void Execute(TKey key);
    }
}
