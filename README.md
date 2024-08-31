# Windows Timed Access Control

**WindowsTimedAccessControl** is a Windows Service that allows administrators to control user access to a Windows machine based on specified time schedules. Users can be restricted from using the machine during certain hours unless they use a specific lock password. This service also allows password changes and screen locking to be managed efficiently, ensuring security and compliance with usage policies.

## Features

- **Timed Access Control**: Restrict user access to a Windows machine based on predefined time intervals.
- **Password Management**: Automatically change user passwords during lock times to enforce security.
- **Screen Locking**: Automatically lock the screen when the system is within the restricted time period.
- **Selective User Management**: Apply access control to all users or specific selected users.
- **Daily Operations**: Ensure that operations (password change, lock screen) are performed only once per day.

## Installation

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/yourusername/WindowsTimedAccessControl.git
    ```
   
2. **Build the Project**:
   - Open the solution in Visual Studio.
   - Build the project to generate the executable files.

3. **Install the Service**:
   - Open Command Prompt as Administrator.
   - Navigate to the directory containing the compiled service executable.
   - Install the service using:
     ```bash
     WLockerService.exe --install
     ```

4. **Start the Service**:
   - Use the Windows Services Manager (`services.msc`) or the following command:
     ```bash
     net start WLockerService
     ```

## Usage

1. **Configuration**:
   - Configure the lock and unlock times, lock and unlock passwords, and the target users (all or specific) by editing the `LockSettings.txt` file.
   - Example configuration in `LockSettings.txt`:
     ```
     Target=AllUsers
     LockStartTime=22:00
     LockEndTime=04:00
     LockPassword=YourLockPassword
     UnlockPassword=YourUnlockPassword
     ```

2. **Monitoring and Logs**:
   - The service logs operations and errors. Ensure to monitor these logs for troubleshooting and audit purposes.

3. **Uninstalling the Service**:
   - To uninstall the service, run:
     ```bash
     WLockerService.exe --uninstall
     ```

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. If you have suggestions or find bugs, feel free to open an issue.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- This project utilizes .NET's `System.DirectoryServices.AccountManagement` for user management.
- Special thanks to all contributors and users who have provided feedback to improve this service.

