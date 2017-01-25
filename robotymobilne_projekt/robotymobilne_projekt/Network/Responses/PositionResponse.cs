using robotymobilne_projekt.Settings;
using Server.Networking.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.Network.Responses
{
    public class PositionResponse : IResponse
    {
        private string frame;

        public PositionResponse(string frame)
        {
            this.frame = frame;
        }

        public void execute()
        {
            if (frame.Length %13 != 0) return;
            for (int i = 0; i < frame.Length; i+= 13)
            {
                var id = Convert.ToInt32(frame.Substring(i, 2));
                var x = Convert.ToInt32(frame.Substring(i + 2, 4));
                var y = Convert.ToInt32(frame.Substring(i + 6, 4));
                var angle = Convert.ToInt32(frame.Substring(i + 10, 3));

                foreach (var robot in RobotSettings.Instance.Robots)
                {
                    if (robot.ID == id)
                    {
                        robot.X = x;
                        robot.Y = y;
                        robot.Angle = angle;
                    }
                }
            }
        }
    }
}
