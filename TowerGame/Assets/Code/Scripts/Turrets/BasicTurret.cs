public class BasicTurret : Turret
{
    // Constructor to set default values for targeting range and rotation speed
    public BasicTurret()
    {
        targetingRange = 5f; // Default targeting range
        rotationSpeed = 200f; // Default rotation speed
        bps = 5f;
    }
}
