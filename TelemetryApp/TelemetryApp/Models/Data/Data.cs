namespace TelemetryApp.Models.Data
{
    public abstract class Data
    {
        public Steering Steering { get; protected set; }
        public GForce.GForce GForce { get; protected set; }
        public Gps.Gps Gps { get; protected set; }
        public Tire.Tire FrontLeftTire { get; protected set; }
        public Tire.Tire FrontRightTire { get; protected set; }
        public Tire.Tire BackLeftTire { get; protected set; }
        public Tire.Tire BackRightTire { get; protected set; }

        public abstract void Update();

        public abstract void Unload();
    }
}