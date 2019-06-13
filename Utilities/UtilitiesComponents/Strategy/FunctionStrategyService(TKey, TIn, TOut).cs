using AIT.UtilitiesComponents.Extensions;
using System;
using System.Collections.Generic;

namespace AIT.UtilitiesComponents.Strategy
{
    public class FunctionStrategyService<TKey, TIn, TOut> : IFunctionStrategyService<TKey, TIn, TOut>
    {
        private IDictionary<TKey, Func<TIn, TOut>> _strategies = new Dictionary<TKey, Func<TIn, TOut>>();
        private Func<TIn, TOut> _defaultStrategy;

        public IFunctionStrategyService<TKey, TIn, TOut> SetDefaultStrategy(Func<TIn, TOut> func)
        {
            _defaultStrategy = func;
            return this;
        }

        public IFunctionStrategyService<TKey, TIn, TOut> AddStrategy(TKey key, Func<TIn, TOut> func)
        {
            _strategies.Add(key, func);
            return this;
        }

        public IFunctionStrategyService<TKey, TIn, TOut> AddStrategy(IDictionary<TKey, Func<TIn, TOut>> strategies)
        {
            _strategies = strategies;
            return this;
        }

        public TOut Execute(TKey key, TIn param)
        {
            return _strategies.ContainsKey(key) ? _strategies[key].Invoke(param) : _defaultStrategy.IfNotNull(fn => _defaultStrategy(param));
        }
    }
}
