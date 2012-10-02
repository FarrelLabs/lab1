using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1
{
    interface ITransformer
    {
        int ActionCount { get; }
        FunctionTableItem[] Transform(FunctionTableItem[] original);
        FunctionTableItem[] Inverse(FunctionTableItem[] image);
    }
}
