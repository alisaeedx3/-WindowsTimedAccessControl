using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WLockerService
{
    public class UserSessionHelper
    {
        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSEnumerateSessions(IntPtr hServer, int Reserved, int Version, ref IntPtr ppSessionInfo, ref int pCount);

        [DllImport("Wtsapi32.dll")]
        private static extern void WTSFreeMemory(IntPtr pMemory);

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTSInfoClass wtsInfoClass, out IntPtr ppBuffer, out int pBytesReturned);

        private const int WTS_CURRENT_SERVER_HANDLE = 0;
        private const int WTS_SESSIONSTATE_LOCK = 0x1;

        private enum WTSInfoClass
        {
            WTSSessionId = 4,
            WTSUserName = 5,
            WTSConnectState = 8
        }

        public static void LockAllActiveSessions()
        {
            IntPtr pSessionInfo = IntPtr.Zero;
            int sessionCount = 0;
            if (WTSEnumerateSessions(IntPtr.Zero, 0, 1, ref pSessionInfo, ref sessionCount))
            {
                int dataSize = Marshal.SizeOf(typeof(WTS_SESSION_INFO));
                long current = (long)pSessionInfo;

                for (int i = 0; i < sessionCount; i++)
                {
                    WTS_SESSION_INFO sessionInfo = (WTS_SESSION_INFO)Marshal.PtrToStructure((IntPtr)current, typeof(WTS_SESSION_INFO));
                    current += dataSize;

                    if (sessionInfo.State == WTS_CONNECTSTATE_CLASS.WTSActive || sessionInfo.State == WTS_CONNECTSTATE_CLASS.WTSDisconnected)
                    {
                        // Schedule a lock screen task for each active or disconnected session
                        ScheduleLockTaskForSession(sessionInfo.SessionID);
                    }
                }
                WTSFreeMemory(pSessionInfo);
            }
        }

        private static void ScheduleLockTaskForSession(int sessionId)
        {
            // Create or run the task using schtasks or similar in the specific session context
            string taskName = $"LockWorkstationForSession_{sessionId}";
            DateTime runTime = DateTime.Now; // Schedule to run 10 seconds later

            string command = $@"
            schtasks /create /tn ""{taskName}"" /tr ""rundll32.exe user32.dll,LockWorkStation"" /sc once /st {runTime:HH:mm} /ru """" /it /v1 /f /i /rd /session {sessionId} /ru SYSTEM";

            // Run the command
            System.Diagnostics.Process.Start("cmd.exe", $"/c {command}");
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public int SessionID;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pWinStationName;
            public WTS_CONNECTSTATE_CLASS State;
        }

        private enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }
    }
}
