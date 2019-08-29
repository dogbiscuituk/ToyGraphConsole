namespace ToyGraf.Engine
{
    using System;

    [Flags]
    public enum ShiftKeys
    {
        None = 0,
        Shift = 1,
        Ctrl = 2,
        CtrlShift = Ctrl | Shift,
        Alt = 4,
        AltShift = Alt | Shift,
        AltCtrl = Alt | Ctrl,
        AltCtrlShift = Alt | CtrlShift
    }
}
