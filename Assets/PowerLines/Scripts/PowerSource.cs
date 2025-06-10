public class PowerSource : Building
{
    private void Awake()
    {
        checkDirections = Direction.Up | Direction.Left;
    }
}