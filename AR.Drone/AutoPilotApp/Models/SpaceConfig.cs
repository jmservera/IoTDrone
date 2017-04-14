namespace AutoPilotApp.Models
{
    public class SpaceConfig:ObservableObject
    {
        public SpaceConfig()
        {
        }

        private int maxSize;

        public int MaxSize
        {
            get { return maxSize; }
            set { Set(ref maxSize , value); }
        }

        private float turnSpeed;

        public float TurnSpeed
        {
            get { return turnSpeed; }
            set { Set(ref turnSpeed , value); }
        }



    }
}