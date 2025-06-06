using UnityEngine;

public class Resistor : ElectricNode
{
    public float resistance = 2f;

    public override void ReceivePower(float inputVoltage, float inputCurrent)
    {
        voltage = Mathf.Max(0, inputVoltage - resistance);
        current = Mathf.Max(0, inputCurrent - resistance);
        Simulate();
    }

    public override void Simulate()
    {
        // same as Wire
    }
}