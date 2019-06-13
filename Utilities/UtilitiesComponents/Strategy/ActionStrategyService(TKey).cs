using AIT.UtilitiesComponents.Extensions;
using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public class ActionStrategyService<TKey> : IActionStrategyService<TKey>
    {
        private IDictionary<TKey, Action> _strategies = new Dictionary<TKey, Action>();
        private Action _defaultStrategy;

        public IActionStrategyService<TKey> SetDefaultStrategy(Action action)
        {
            _defaultStrategy = action;
            return this;
        }

        public IActionStrategyService<TKey> AddStrategy(TKey key, Action action)
        {
            _strategies.Add(key, action);
            return this;
        }

        public IActionStrategyService<TKey> AddStrategy(IDictionary<TKey, Action> strategies)
        {
            _strategies = strategies;
            return this;
        }

        public void Execute(TKey key)
        {
            if (_strategies.ContainsKey(key))
            {
                _strategies[key].Invoke();
                return;
            }

            _defaultStrategy.IfNotNull(_defaultStrategy);
        }
    }
}
