
[System.Serializable]
public class PowerupMissileCD : Powerup
{
    // variables for missile powerup
    public float missileCooldownReduction;
    // overridden function for activated
    public override void OnActivated(TankData data)
    {
        data.missileCooldownMax -= missileCooldownReduction;
        base.OnActivated(data);
    }
    // overridden function for deactivated
    public override void OnDeactivated(TankData data)
    {
        data.missileCooldownMax += missileCooldownReduction;
        base.OnDeactivated(data);
    }
}
