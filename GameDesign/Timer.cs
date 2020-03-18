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
        noon,
        afternoon,
        night
    }

    public class Timer
    {
        Phase currentPhase;

        long currentTime_ms;
        long phaseStartTime_ms;
        long phaseTime_ms;
        long phaseTimeDifferenceOnPause;

        bool paused;

        public Timer(Phase gamePhase, int seconds)
        {
            currentTime_ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            phaseStartTime_ms = currentTime_ms;
            phaseTime_ms = 1000 * seconds;
            currentPhase = gamePhase;
            paused = false;
        }

        public bool isPhaseOver()
        {
            currentTime_ms = getCurrentTime_ms();
            if (!paused && currentTime_ms > phaseStartTime_ms + phaseTime_ms )
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
                    return Phase.noon;
                case Phase.noon:
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

        public void increaseGameSpeed()
        {
            phaseTime_ms /= 2;
        }

        public void decreaseGameSpeed()
        {
            phaseTime_ms *= 2;
        }

        private void pauseGameTime()
        {
            paused = true;
            phaseTimeDifferenceOnPause = getCurrentTime_ms() - phaseStartTime_ms;
        }

        private void resumeGameTime()
        {
            paused = false;
            phaseStartTime_ms = getCurrentTime_ms() - phaseTimeDifferenceOnPause;
        }

        public void togglePause()
        {
            if (paused)
            {
                resumeGameTime();
                System.Console.WriteLine("resume");
            }
            else
            {
                pauseGameTime();
                System.Console.WriteLine("pause");
            }

        }

        private long getCurrentTime_ms()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

    }
}
