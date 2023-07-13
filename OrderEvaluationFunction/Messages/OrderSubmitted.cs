using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEvaluationFunction.Messages
{
    public interface OrderSubmitted
    {
        Guid OrderId { get; }
        DateTime DateTime { get; }
        string Name { get; }
    }
}
