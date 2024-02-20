/*
Script Source: https://coffeebraingames.wordpress.com/2014/10/20/a-generic-duration-timer-class/
*/

using UnityEngine;

namespace Egitech.Utils.Runtime.DurationTimer
{
    /**
 * Generic class for implementing timers (specified in seconds)
 */
    public class DurationTimer
    {
        /**
     * Constructor with a specified duration time
     */
        public DurationTimer(float durationTime)
        {
            Reset(durationTime);
        }

        /**
     * Returns whether or not the timed duration has elapsed
     */
        public bool HasElapsed => Comparison.TolerantGreaterThanOrEquals(PolledTime, DurationTime);

        /**
     * Returns the ratio of polled time to duration time. Returned value is 0 to 1 only
     */
        public float Ratio
        {
            get
            {
                if (Comparison.TolerantLesserThanOrEquals(DurationTime, 0))
                {
                    // bad duration time value
                    // if countdownTime is zero, ratio will be infinity (divide by zero)
                    // we just return 1.0 here for safety
                    return 1.0f;
                }

                var ratio = PolledTime / DurationTime;
                return Mathf.Clamp(ratio, 0, 1);
            }
        }

        /**
     * Returns the polled time since it started
     */
        public float PolledTime { get; private set; }

        /**
     * Returns the durationTime
     */
        public float DurationTime { get; private set; }

        /**
     * Updates the timer manually
     */
        public void UpdateTimer()
        {
            PolledTime += Time.deltaTime;
        }

        /**
     * Resets the timer
     */
        public void Reset()
        {
            PolledTime = 0;
        }

        /**
     * Resets the timer and assigns a new duration time
     */
        public void Reset(float durationTime)
        {
            Reset();
            DurationTime = durationTime;
        }

        /**
     * Forces the timer to end
     */
        public void EndTimer()
        {
            PolledTime = DurationTime;
        }
    }
}