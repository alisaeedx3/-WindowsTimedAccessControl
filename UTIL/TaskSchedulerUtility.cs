using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UTIL
{
    public class TaskSchedulerUtility
    {
        public void CreateLockTask(DateTime lockStartTime, string userName = null)
        {
            string taskName = "LockWorkstationDailyTask";
            string lockCommand = "rundll32.exe user32.dll,LockWorkStation";

            // Format the start time for the task
            string startTime = lockStartTime.ToString("HH:mm");

            // Construct the schtasks command based on whether a specific user or all users are targeted
            string command;
            if (string.IsNullOrEmpty(userName))
            {
                // Schedule task for all users using SYSTEM account
                command = $"schtasks /create /tn \"{taskName}\" /tr \"{lockCommand}\" /sc daily /st {startTime} /ru SYSTEM /f";
            }
            else
            {
                // Schedule task for a specific user
                command = $"schtasks /create /tn \"{taskName}\" /tr \"{lockCommand}\" /sc daily /st {startTime} /ru \"{userName}\" /f";
            }

            RunCommand(command);
        }

        private void RunCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Error executing task scheduler command:\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(output, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
