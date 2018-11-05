
[System.Serializable]
public class PowerupMissileCD : Powerup
{
    public float missileCooldownReduction;
    public override void OnActivated(TankData data)
    {
        data.missileCooldownMax -= missileCooldownReduction;
        base.OnActivated(data);
    }
    public override void OnDeactivated(TankData data)
    {
        data.missileCooldownMax += missileCooldownReduction;
        base.OnDeactivated(data);
    }
}
