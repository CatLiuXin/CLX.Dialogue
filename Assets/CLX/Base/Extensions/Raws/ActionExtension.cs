using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX.Extensions.Raws
{
    public static class ActionExtension
    {
        /// <summary>
        /// 运行times次action
        /// </summary>
        public static void Loops(this Action action,int times)
        {
            times.LoopsFor(action);
        }
    }
}
