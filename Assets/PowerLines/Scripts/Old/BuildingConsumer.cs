using UnityEngine;

public class BuildingConsumer : ElectricNode
{
    public float requiredVoltage = 5f;
    public float requiredCurrent = 2f;
    public float maxCurrent = 5f;

    public SpriteRenderer renderer;

    public override void ReceivePower(float inputVoltage, float inputCurrent)
    {
        base.ReceivePower(inputVoltage, inputCurrent);

        if (inputCurrent > maxCurrent)
        {
            renderer.color = Color.red; // 💥 Перегрузка
        }
        else if (inputVoltage >= requiredVoltage && inputCurrent >= requiredCurrent)
        {
            renderer.color = Color.green; // ✅ Включено
        }
        else
        {
            renderer.color = Color.yellow; // ⚠ Недостаточно
        }
    }

    public override void Simulate() { }
}