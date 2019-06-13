using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIT.UtilitiesComponents.Chains
{
    public interface IChainService<TSpecifaction, TResult>
    {
        TResult Process(TSpecifaction specification);
    }
}
