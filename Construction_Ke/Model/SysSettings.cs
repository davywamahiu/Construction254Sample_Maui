using System.ComponentModel.DataAnnotations;

namespace Construction_Ke.Model
{
    public class SysSettings
    {
        public SysSettings(bool isEnabled, int baudRate, int startBit, int stopBit, int parity)
        {
            IsEnabled = isEnabled;
            BaudRate = baudRate;
            StartBit = startBit;
            StopBit = stopBit;
            Parity = parity;
        }
        [Key]
        public int Code { get; set; }
        public bool IsEnabled { get; set; }
        public string SetName { get; set; } = null!;
        public int BaudRate { get; set; }
        public int StartBit { get; set; }
        public int StopBit { get; set; }
        public int Parity { get; set; }

        public SysLogin SysLogins { get; set; } = null!;

        public SysSettings(int code, bool isEnabled, string setName, int baudRate, int startBit, int stopBit, int parity)
        {
            Code = code;
            IsEnabled = isEnabled;
            SetName = setName;
            BaudRate = baudRate;
            StartBit = startBit;
            StopBit = stopBit;
            Parity = parity;
        }
    }
}
