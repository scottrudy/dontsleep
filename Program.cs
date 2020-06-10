using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DontSleep {
    public class Program {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto,SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint {
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002
        }

        private static void Main(string[] args) {
            var timer = new Timer((s) => {
                SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED);
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(9));

            Console.WriteLine($"Tap any key to stop program!");
            Console.ReadKey();
            timer.Dispose();
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
}
