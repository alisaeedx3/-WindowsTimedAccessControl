using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using UTIL;

namespace WindowsPasswordLockerByTime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radoAllUsers_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = radoSelectrUser.Checked;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Data data = clsUtil.ReadData();
                if (data != null)
                {

                    dtpStartTime.Value = data.LockStartTime;
                    dtpEndTime.Value = data.LockEndTime;
                    radoAllUsers.Checked = data.IsAllUsers;
                    radoSelectrUser.Checked = !data.IsAllUsers;
                    txtUserName.Text = data.UserName;
                    txtLockTimePass.Text = data.LockPass;
                    txtUnlockPass.Text = data.UnlockPass;
                }

                txtUserName.Enabled = radoSelectrUser.Checked;

                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    // Load current logged-in user name
                    txtUserName.Text = Environment.UserName;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUserName.Text.Trim();
                string lockPassword = txtLockTimePass.Text.Trim();
                bool isAllUsers = radoAllUsers.Checked;

                if (!isAllUsers && string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter a username.");
                    return;
                }

                if (string.IsNullOrEmpty(lockPassword))
                {
                    MessageBox.Show("Please enter a lock Password.");
                    return;
                }

                clsUtil.SaveData(new Data
                {
                    LockStartTime = dtpStartTime.Value,
                    LockEndTime = dtpEndTime.Value,
                    LockPass = lockPassword,
                    UnlockPass = txtUnlockPass.Text.Trim(),
                    IsAllUsers = isAllUsers,
                    UserName = username,
                });

#if !DEBUG
                // Ensure the service is installed and running
                EnsureServiceIsRunning("WLockerService");
#endif

                // Create or update a task to lock the workstation at the lock start time every day
                TaskSchedulerUtility taskScheduler = new TaskSchedulerUtility();
                if (isAllUsers)
                {
                    taskScheduler.CreateLockTask(dtpStartTime.Value); // Schedule task for all users
                }
                else
                {
                    taskScheduler.CreateLockTask(dtpStartTime.Value, username); // Schedule task for specific user
                }

                MessageBox.Show("success");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void EnsureServiceIsRunning(string serviceName)
        {
            string servicePath = Path.Combine(Application.StartupPath, $"{serviceName}.exe");
            if (!File.Exists(servicePath))
            {
                MessageBox.Show($"Service executable not found at {servicePath}. Please ensure the file exists and try again.");
                return;
            }

            // if the service is installed but is not runining
            if (ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == serviceName) != null)
            {

                using (ServiceController sc = new ServiceController(serviceName))
                {
                    if (sc.Status != ServiceControllerStatus.Running)
                    {
                        //unistall
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", servicePath });
                        //install
                        ManagedInstallerClass.InstallHelper(new string[] { servicePath });
                        //start
                        sc.Start();
                    }
                    else // if it is running then restart it
                    {
                        sc.Stop();
                        sc.Start();
                    }
                }

            }
            else // the service is not installed
            {

                // install
                ManagedInstallerClass.InstallHelper(new string[] { servicePath });

                using (ServiceController sc = new ServiceController(serviceName))
                {
                    if (sc.Status != ServiceControllerStatus.Running)
                    {
                        //start
                        sc.Start();
                    }
                }
            }
        }

    }
}

