
[System.Serializable]
public class PowerupHealth : Powerup
{
    // health powerup variables
    public float maxHealthIncrease;
    public float currentHealthIncrease;
    // overridden function for activated
    public override void OnActivated(TankData data)
    {
        // set health variables
        data.healthMax += maxHealthIncrease;
        data.healthCurrent += currentHealthIncrease;
        base.OnActivated(data);
    }
    // overridden function for deactivated
    public override void OnDeactivated(TankData data)
    {
        // set health variables
        data.healthMax -= maxHealthIncrease;
        data.healthCurrent -= currentHealthIncrease;
        base.OnDeactivated(data);
    }
}
