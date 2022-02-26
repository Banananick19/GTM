using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace ConsoleApp4
{
    internal class MouseMoveObserver : IObserver<JoystickUpdate[]>
    {
        InputSimulator sim = new InputSimulator();

        int offsetX = 0;
        int offsetY = 0;
        JoystickUpdate[] UpdateData;
        public MouseMoveObserver() 
        {
            UpdateData = new JoystickUpdate[0];
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(2);
                    foreach (var state in UpdateData)
                    {
                        if (state.Offset == JoystickOffset.RotationX)
                        {
                            offsetX = Offsets.GetOffset(state.Value);
                        }
                        if (state.Offset == JoystickOffset.RotationY)
                        {
                            offsetY = Offsets.GetOffset(state.Value);
                        }
                    }
                    sim.Mouse.MoveMouseBy(offsetX, offsetY);
                }
            }).Start();
        }
        public void Update(JoystickUpdate[] updateData)
        {
            UpdateData = updateData;
        }
    }
}
