using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class InvalidPhaseException : Exception
    {
        public InvalidPhaseException()
            : base("Invalid dayPhase")
        {
        }

        public InvalidPhaseException(string message)
            : base(message)
        {
        }

        public InvalidPhaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public enum Phase
    {
        morning,
        afternoon,
        night
    }

    public class Timer
    {
        Phase currentPhase;
        long currentTime_ms;
        long phaseStartTime_ms;
        long phaseTime_ms;

        public Timer()
        {
            currentTime_ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            phaseStartTime_ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            phaseTime_ms = 1000 * 20;
            currentPhase = Phase.morning;
        }

        public bool isPhaseOver()
        {
            currentTime_ms = getCurrentTime_ms();
            if (currentTime_ms > phaseStartTime_ms + phaseTime_ms)
            {
                phaseStartTime_ms = currentTime_ms;
                currentPhase = getNextPhase(currentPhase);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Phase getCurrentPhase()
        {
            return currentPhase;
        }

        public Phase getNextPhase(Phase currentPhase)
        {
            switch (currentPhase)
            {
                case Phase.morning:
                    return Phase.afternoon;
                case Phase.afternoon:
                    return Phase.night;
                case Phase.night:
                    return Phase.morning;
                default:
                    throw new InvalidPhaseException();
            }
        }

        public void setPhaseTime_s(long newPhaseTime_s)
        {
            phaseTime_ms = newPhaseTime_s * 1000;
        }

        public void setPhaseTime_ms(long newPhaseTime_ms)
        {
            phaseTime_ms = newPhaseTime_ms;
        }

        private long getCurrentTime_ms()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

    }
}
