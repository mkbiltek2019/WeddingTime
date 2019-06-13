using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIT.UtilitiesComponents.Chains
{
    public static class ChainLinkExtensions
    {
        public static IChainLink<TSpecification, TResult> Finally<TSpecification, TResult>(this IChainLink<TSpecification, TResult> chain, IChainLink<TSpecification, TResult> link)
        {
            chain.AddNextLink(link);
            return chain;
        }
    }
}
