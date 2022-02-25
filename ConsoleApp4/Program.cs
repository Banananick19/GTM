// See https://aka.ms/new-console-template for more information
using SharpDX.DirectInput;
using System.Drawing;
using System.Runtime.InteropServices;
using ConsoleApp4;
using System.Diagnostics;

JoyStickListener joystickListener = new JoyStickListener();
while (true) 
{
    try
    {
        joystickListener.ConnectJoyStick();
        joystickListener.AddObserver(new MouseMoveObserver());
        joystickListener.AddObserver(new MouseClickObserver());
        joystickListener.AddObserver(new ButtonsActionObserver());
        break;
    }
    catch (Exception ex)
    {
        Thread.Sleep(500);
        continue;
    }
}


while (true)
{
    Thread.Sleep(2);
    joystickListener.NotifyObservers();
}

        






[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;

    public static implicit operator Point(POINT point)
    {
        return new Point(point.X, point.Y);
    }
}