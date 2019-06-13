using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIT.UtilitiesComponents.Chains
{
    public interface IChainLink<TSpecification, TResult>
    {
        bool CanHandle(TSpecification specification);
        TResult Handle(TSpecification specification);
        void AddNextLink(IChainLink<TSpecification, TResult> link);
    }
}
