using UnityEngine;

public class ElectricNode : Placeable
{
    public float voltage;
    public float current;

    public virtual void ReceivePower(float inputVoltage, float inputCurrent)
    {
        voltage = inputVoltage;
        current = inputCurrent;
    }

    public override void Simulate()
    {
        
    }
}
