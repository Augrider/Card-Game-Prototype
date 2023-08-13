[System.Serializable]
public struct ControlAxis
{
    public bool ControlX;
    public bool ControlY;
    public bool ControlZ;

    public ControlAxis(bool controlX, bool controlY, bool controlZ)
    {
        ControlX = controlX;
        ControlY = controlY;
        ControlZ = controlZ;
    }
}
