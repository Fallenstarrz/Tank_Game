
[System.Serializable]
public class PowerupSpeed : Powerup
{
    // speed powerup variables
    public float speedIncrease;
    public float rotationIncrease;
    // override for activated on speed powerup
    public override void OnActivated(TankData data)
    {
        data.movementSpeed += speedIncrease;
        data.rotationSpeed += rotationIncrease;
        base.OnActivated(data);
    }
    // override for deactivated on speed powerup
    public override void OnDeactivated(TankData data)
    {
        data.movementSpeed -= speedIncrease;
        data.rotationSpeed -= rotationIncrease;
        base.OnDeactivated(data);
    }
}
