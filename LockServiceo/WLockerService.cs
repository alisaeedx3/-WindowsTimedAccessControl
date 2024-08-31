using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using UTIL;

namespace WLockerService
{
    public partial class WLockerService : ServiceBase
    {
        [DllImport("user32.dll")]
        private static extern bool LockWorkStation();

        Data m_Data;

        private DateTime LastLockPasswordSet = DateTime.MinValue;
        private DateTime LastUnLockPasswordSet = DateTime.MinValue;

        private System.Timers.Timer _timer;
        public WLockerService()
        {
            InitializeComponent();

            _timer = new System.Timers.Timer();
            _timer.Interval = 10000; // Check every 10 seconds
            _timer.AutoReset = false;
            _timer.Elapsed += OnTimerElapsed;
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (m_Data == null)
                {
                    m_Data = clsUtil.ReadData();
                    if (m_Data == null)
                        return;
                }

                DateTime currentTime = DateTime.Now;
                bool isWithinLockTime = IsWithinLockTime(currentTime, m_Data.LockStartTime, m_Data.LockEndTime);

                if (isWithinLockTime && LastLockPasswordSet.Date != DateTime.Today)
                {
                    ChangePasswords(m_Data, true);
                    LastLockPasswordSet = DateTime.Now;
                    // LockWorkStation(); //<-- cause windows dose not allow service to entract with UI , this will work only in debug
                    //ScheduleLockWorkstationTask(); // this will not work either
                    // UserSessionHelper.LockAllActiveSessions(); // this will not work either
                }
                else if (!isWithinLockTime && LastUnLockPasswordSet.Date != DateTime.Today)
                {
                    ChangePasswords(m_Data, false);
                    LastUnLockPasswordSet = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                _timer.Start();
            }
        }

        private bool IsWithinLockTime(DateTime currentTime, DateTime lockStartTime, DateTime lockEndTime)
        {
            TimeSpan current = currentTime.TimeOfDay;
            TimeSpan start = lockStartTime.TimeOfDay;
            TimeSpan end = lockEndTime.TimeOfDay;

            if (start < end)
            {
                // Simple case: lock period does not span midnight
                return current >= start && current <= end;
            }
            else
            {
                // Lock period spans midnight
                return current >= start || current <= end;
            }
        }

        private void ChangePasswords(Data data, bool useLockPassword)
        {
            using (var context = new PrincipalContext(ContextType.Machine))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var user in searcher.FindAll()?.OfType<UserPrincipal>().Where(u => data.IsAllUsers || (u.Name == data.UserName)))
                    {
                        try
                        {
                            // Check if the user is a local account
                            if (user.ContextType == ContextType.Machine)
                            {
                                user.SetPassword(useLockPassword ? data.LockPass : data.UnlockPass);
                                WriteLog($"Set {(useLockPassword ? "LockPass" : "UnLockPass")} for user {user.Name} , {DateTime.Now}", subfolder: "Info");
                            }
                            else
                            {
                                WriteLog($"Skipping user {user.Name} because it is not a local account.", subfolder: "Info");
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLog($"Error changing password for user {user.Name}: {ex.Message}", subfolder: "Error");
                        }
                    }
                }
            }
        }

        void WriteLog(string message, string subfolder = "ErrorLog")
        {
            try
            {
                //**********************************************
                //Define log folder
                string logFolder = String.Format("{0}\\log\\{1}\\", Application.StartupPath, subfolder);

                //Create a folder if it doesn't exist
                DirectoryInfo directoryInfo = new DirectoryInfo(logFolder);
                if (!directoryInfo.Exists) directoryInfo.Create();

                //Demon Log File Name Definition
                string logFile = String.Format("{0}{1}.log", logFolder, DateTime.Now.ToString("yyyy-MM-dd"));

                //Open a file
                using (FileStream fileStream = new FileStream(logFile, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        streamWriter.WriteLine(String.Format("[{0}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        streamWriter.WriteLine(message);
                        streamWriter.WriteLine(string.Empty);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    fileStream.Close();
                }
            }
            catch { /*Log record does not handle error*/ }
        }
    }
}
