using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX.Extensions.Helper
{
    public static class TimerHelper
    {
        public static float TimeCost(this Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds / 1000f;
        }

        public static float TimeCostFor(this Action action,int count)
        {
            float time = 0;
            for(int i = 0; i < count; i++)
            {
                time += action.TimeCost();
            }
            return time / count;
        }

        public async static Task<float> GetFunctionTimeCostAsync(this Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Run(action);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds / 1000f;
        }
        
    }
}
