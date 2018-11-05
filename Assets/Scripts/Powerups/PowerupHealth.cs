
[System.Serializable]
public class PowerupHealth : Powerup
{
    public float maxHealthIncrease;
    public float currentHealthIncrease;
    public override void OnActivated(TankData data)
    {
        data.healthMax += maxHealthIncrease;
        data.healthCurrent += currentHealthIncrease;
        base.OnActivated(data);
    }
    public override void OnDeactivated(TankData data)
    {
        data.healthMax -= maxHealthIncrease;
        data.healthCurrent -= currentHealthIncrease;
        base.OnDeactivated(data);
    }
}
