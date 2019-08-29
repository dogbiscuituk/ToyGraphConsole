namespace ToyGraf.Engine.Types
{
    using System;

    [Flags]
    public enum GPUStatus
    {
        OK = 0,
        Error = 1,
        Warning = 2
    }
}
