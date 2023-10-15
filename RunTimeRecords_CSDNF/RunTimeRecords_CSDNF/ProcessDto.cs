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

        public override bool Equals(object obj)
        {
            // objがnullか、型が不一致の時はfalse
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            // キー項目で比較する。
            ProcessDto objDto = obj as ProcessDto;
            return (WindowTitle == objDto.WindowTitle) && (ProcessStartTime == objDto.ProcessStartTime);
        }

        public override int GetHashCode()
        {
            // XORで返す
            return WindowTitle.GetHashCode() ^ ProcessStartTime.GetHashCode();
        }
    }
}