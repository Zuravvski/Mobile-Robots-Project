using System;
using System.Threading;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.Devices
{
    public abstract class RobotDriver : IDisposable
    {
        protected RobotModel robot;
        protected AbstractController controller;
        protected Thread handlerThread;
        protected bool isDisposed;

        public RobotModel Robot
        {
            get { return robot; }
        }

        public RobotDriver(RobotModel robot, AbstractController controller)
        {
            this.robot = robot;
            this.controller = controller;

            this.robot.IsNotReserved = false;
            this.controller.IsNotReserved = false;

            handlerThread = new Thread(run);
            handlerThread.Start();
        }

        protected abstract void run();

        // Clean-up code
        public virtual void Dispose()
        {
            if (isDisposed) return;

            robot.IsNotReserved = true;
            controller.IsNotReserved = true;
            robot.Status = RemoteDevice.StatusE.DISCONNECTED;
            robot.disconnect();
            robot = null;
            controller = null;
            handlerThread.Abort();
            isDisposed = true; // mark as already disposed
        }
    }
}
