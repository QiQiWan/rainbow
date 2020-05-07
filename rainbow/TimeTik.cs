using System.Timers;

namespace rainbow
{
    class TimeTick
    {
        public Timer aTimer = new Timer();

        public TimeTick(int interval)
        {
            aTimer.Interval = interval;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        public void Start(Handle handle)
        {
            aTimer.Elapsed += new ElapsedEventHandler(handle);
        }
    }
    public delegate void Handle(object source, System.Timers.ElapsedEventArgs e);
}