using AIT.UtilitiesComponents.Extensions;
using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public class ActionStrategyService<TKey, TIn> : IActionStrategyService<TKey, TIn>
    {
        private IDictionary<TKey, Action<TIn>> _strategies = new Dictionary<TKey, Action<TIn>>();
        private Action<TIn> _defaultStrategy;

        public IActionStrategyService<TKey, TIn> SetDefaultStrategy(Action<TIn> action)
        {
            _defaultStrategy = action;
            return this;
        }

        public IActionStrategyService<TKey, TIn> AddStrategy(TKey key, Action<TIn> action)
        {
            _strategies.Add(key, action);
            return this;
        }

        public IActionStrategyService<TKey, TIn> AddStrategy(IDictionary<TKey, Action<TIn>> strategies)
        {
            _strategies = strategies;
            return this;
        }

        public void Execute(TKey key, TIn obj)
        {
            if (_strategies.ContainsKey(key))
            {
                _strategies[key].Invoke(obj);
                return;
            }

            _defaultStrategy.IfNotNull(_defaultStrategy, obj);
        }
    }
}
