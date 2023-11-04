using System;

namespace RunTimeRecords_CSDNF
{
    internal class ProcessSummaryDto
    {
        public string WindowTitle { get; set; }
        public TimeSpan TotalRunTime { get; set; }
        public DateTime LastDate { get; set; }

        public override bool Equals(object obj)
        {
            // objがnullか、型が不一致の時はfalse
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            // キー項目で比較する。
            ProcessDto objDto = obj as ProcessDto;
            return (WindowTitle == objDto.WindowTitle);
        }

        public override int GetHashCode()
        {
            // XORで返す
            return WindowTitle.GetHashCode();
        }

    }
}
