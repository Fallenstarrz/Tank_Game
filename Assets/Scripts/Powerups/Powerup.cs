
[System.Serializable]
public class Powerup
{
    // durations
    public float buffDurationMax;
    public float buffDurationCurrent;
    public bool isPerm;

    // virtual functions
    public virtual void OnActivated(TankData data)
    {

    }
    public virtual void OnDeactivated(TankData data)
    {

    }
}
