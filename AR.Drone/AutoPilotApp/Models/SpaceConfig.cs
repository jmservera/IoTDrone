namespace AutoPilotApp.Models
{
    public class SpaceConfig:ObservableObject
    {
        private int maxDistance=50;

        public int MaxDistance
        {
            get { return maxDistance; }
            set { Set(ref maxDistance , value); }
        }

        private float turnSpeed=0.15f;

        public float TurnSpeed
        {
            get { return turnSpeed; }
            set { Set(ref turnSpeed , value); }
        }
    }
}