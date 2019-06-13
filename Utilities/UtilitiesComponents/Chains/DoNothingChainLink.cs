using System;

namespace AIT.UtilitiesComponents.Chains
{
    public class DoNothingChainLink<TSpecification, TResult> : ChainLink<TSpecification, TResult>
    {
        public override TResult Handle(TSpecification spec)
        {
            return default(TResult);
        }

        public override Boolean CanHandle(TSpecification spec)
        {
            return true;
        }
    }
}
