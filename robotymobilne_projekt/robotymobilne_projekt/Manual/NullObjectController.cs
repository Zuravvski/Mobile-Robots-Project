using MobileRobots.Manual;

namespace robotymobilne_projekt.Manual
{
    public class NullObjectController : AbstractController
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public NullObjectController(string name)
        {
            this.name = name;
        }

        public override string execute()
        {
            return null;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
