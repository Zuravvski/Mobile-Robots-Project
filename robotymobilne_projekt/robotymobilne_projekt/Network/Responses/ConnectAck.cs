using Server.Networking.Responses;
using System;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Network.Responses
{
    public class ConnectAck : IResponse
    {
        private readonly string data;

        public ConnectAck(string data)
        {
            this.data = data;
        }

        public void execute()
        {
            if (data.Length != 4) return;

            var id = Convert.ToInt32(data.Substring(0, 2));
            var status = Convert.ToInt32(data.Substring(2, 2));

            foreach (var robot in RobotSettings.Instance.Robots)
            {
                if (robot.ID == id)
                {
                    switch ((ResponseStatus) status)
                    {
                        case ResponseStatus.Failed:
                            robot.Status = RobotModel.StatusE.DISCONNECTED;
                            break;

                        case ResponseStatus.OK:
                            robot.Status = RobotModel.StatusE.CONNECTED;
                            break;
                    }
                }
            }
        }
    }
}
