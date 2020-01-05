using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX.Extensions.Raws
{
    public static class BoolExtension
    {
        public static bool Then(this bool trigger,Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (trigger==true) action();
            return trigger;
        }

        public static bool Else(this bool trigger,Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (trigger==false) action();
            return trigger;
        }
    }
}
