using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public enum dayPhase
    {
        morning,
        afternoon,
        night
    }

    public class Timer
    {
        long currentTime_ms;
        long phaseStartTime_ms;
        long phaseTime_ms;

        public Timer()
        {
            currentTime_ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            phaseStartTime_ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            phaseTime_ms = 1000 * 20;
        }

        public bool isPhaseOver()
        {
            updateCurrentTime();
            if (currentTime_ms > phaseStartTime_ms + phaseTime_ms)
            {
                phaseStartTime_ms = currentTime_ms;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setPhaseTime(long newPhaseTime_ms)
        {
            phaseTime_ms = newPhaseTime_ms;
        }

        private void updateCurrentTime()
        {
            currentTime_ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

    }
}
