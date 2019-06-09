namespace TelemetryApp.Models.Data
{
    public class RandomData : Data
    {
        public RandomData()
        {
            Steering = new Steering();
            GForce = new GForce.GForce();
            Gps = new Gps.Gps();
            FrontLeftTire = new Tire.Tire();
            FrontRightTire = new Tire.Tire();
            BackLeftTire = new Tire.Tire();
            BackRightTire = new Tire.Tire();
        }

        public override void Update()
        {
            Steering.Update();
            GForce.Update();
            Gps.Update();
            FrontLeftTire.Update();
            FrontRightTire.Update();
            BackLeftTire.Update();
            BackRightTire.Update();
        }

        public override void Unload()
        {
        }
    }
}