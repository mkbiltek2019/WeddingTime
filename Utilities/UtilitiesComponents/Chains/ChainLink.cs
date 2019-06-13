using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIT.UtilitiesComponents.Chains
{
    public abstract class ChainLink<TSpecification, TResult> : IChainLink<TSpecification, TResult>
    {
        private IChainLink<TSpecification, TResult> _successor;

        protected ChainLink()
            : this(null)
        {
        }

        protected ChainLink(IChainLink<TSpecification, TResult> successor)
        {
            _successor = successor;
        }

        public IChainLink<TSpecification, TResult> Successor
        {
            get { return _successor; }
        }

        public void AddNextLink(IChainLink<TSpecification, TResult> link)
        {
            if (_successor == null)
                _successor = link;
            else
                _successor.AddNextLink(link);
        }

        public abstract bool CanHandle(TSpecification specification);
        public abstract TResult Handle(TSpecification specification);
    }
}