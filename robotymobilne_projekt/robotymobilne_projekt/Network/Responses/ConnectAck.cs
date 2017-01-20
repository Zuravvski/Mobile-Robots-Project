using Server.Networking.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robotymobilne_projekt.Devices;

namespace robotymobilne_projekt.Network.Responses
{
    public class ConnectAck : IResponse
    {
        private string data;

        public ConnectAck(string data)
        {
            this.data = data;
        }

        public void execute(RobotModel robot)
        {
            if (robot == null || data.Length != 4 || data.Substring(0, 2) != robot.ID) return;

            var status = Convert.ToInt32(data.Substring(2, 4));

            switch((ResponseStatus)status)
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
