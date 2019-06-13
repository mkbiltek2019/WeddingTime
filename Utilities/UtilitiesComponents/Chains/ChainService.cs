using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIT.UtilitiesComponents.Chains
{
    public abstract class ChainService<TSpecification, TResult> : IChainService<TSpecification, TResult>
    {
        public TResult Process(TSpecification specification)
        {
            var chain = CreateChain();
            return chain.Handle(specification);
        }

        protected abstract IChainLink<TSpecification, TResult> CreateChain();
    }
}
