using System;

namespace RunTimeRecords_CSDNF
{
    public class ProcessDto
    {
        public string WindowTitle { get; set; }
        public DateTime ProcessStartTime { get; set; }
        public TimeSpan RunTime { get; set; }
        public int ProcessId { get; set; }
        public string ExecutablePath { get; set; }

    }
}