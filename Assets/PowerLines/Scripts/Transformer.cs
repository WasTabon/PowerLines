public class Transformer : ElectricNode
{
    public float ratio = 2f; // 2:1 → voltage *2, current /2

    public override void ReceivePower(float inputVoltage, float inputCurrent)
    {
        voltage = inputVoltage * ratio;
        current = inputCurrent / ratio;
        Simulate();
    }

    public override void Simulate()
    {
        // same as Wire
    }
}