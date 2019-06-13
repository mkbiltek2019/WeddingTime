using AIT.UtilitiesComponents.Extensions;
using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public class FunctionStrategyService<TKey, TResult> : IFunctionStrategyService<TKey, TResult>
    {
        private IDictionary<TKey, Func<TResult>> _strategies = new Dictionary<TKey, Func<TResult>>();
        private Func<TResult> _defaultStrategy;

        public IFunctionStrategyService<TKey, TResult> SetDefaultStrategy(Func<TResult> func)
        {
            _defaultStrategy = func;
            return this;
        }

        public IFunctionStrategyService<TKey, TResult> AddStrategy(TKey key, Func<TResult> func)
        {
            _strategies.Add(key, func);
            return this;
        }

        public IFunctionStrategyService<TKey, TResult> AddStrategy(IDictionary<TKey, Func<TResult>> strategies)
        {
            _strategies = strategies;
            return this;
        }

        public TResult Execute(TKey key)
        {
            return _strategies.ContainsKey(key) ? _strategies[key].Invoke() : _defaultStrategy.IfNotNull(fn => _defaultStrategy.Invoke());
        }
    }
}
