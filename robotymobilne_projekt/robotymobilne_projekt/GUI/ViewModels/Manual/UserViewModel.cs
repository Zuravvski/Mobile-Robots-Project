using System;
using System.Windows;
using System.Windows.Input;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels.Manual
{
    public class UserViewModel : RobotViewModel
    {
        private readonly ManualViewModel context;
        private ICommand delete;

        #region Actions

        public ApplicationService AppService => ApplicationService.Instance;

        public override ICommand Connect
        {
            get
            {
                if (null == connect)
                {
                    connect = new DelegateCommand(delegate
                    {
                        try
                        {
                            if (robot.Status == RobotModel.StatusE.CONNECTED) return;

                            Robot.connect();
                            driver = new ManualRobotDriver(robot, controller);
                        }
                        catch (NotSupportedException)
                        {
                            // NONE (1st element in list) throws exception in order for this message to be handled
                            MessageBox.Show("Please choose valid robot and controller.", "Invalid settings");
                        }
                        catch
                        {
                            // Workaround. C# does not always manage its resources well when it comes to sockets.
                            robot.disconnect();
                        }
                    });
                }
                return connect;
            }
        }

        public override ICommand Disconnect
        {
            get
            {
                if (null == disconnect)
                {
                    disconnect = new DelegateCommand(delegate
                    {
                        if (robot != null && robot.Status == RobotModel.StatusE.CONNECTED)
                        {
                            robot.disconnect();
                        }
                    });
                }
                return disconnect;
            }
        }

        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new DelegateCommand(delegate
                {
                    if (null != context)
                    {
                        if (null != Robot && Robot.Connected)
                        {
                            Robot = null;
                            Controller = null;
                            driver?.Dispose();
                        }
                        context.RemoveUser.Execute(this);
                    }
                }));
            }
        }
        #endregion

        public UserViewModel(ManualViewModel context)
        {
            this.context = context;
        }
    }
}
