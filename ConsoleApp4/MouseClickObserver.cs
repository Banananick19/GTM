using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;
using WindowsInput;

namespace ConsoleApp4
{
    internal class MouseClickObserver : IObserver<JoystickUpdate[]>
    {

        InputSimulator sim = new InputSimulator();
        public void Update(JoystickUpdate[] updateData)
        {
            
            foreach(var state in updateData)
            {
                
                if (state.Offset == JoystickOffset.Z)
                {
                    if (state.Value == 0)
                    {
                        sim.Mouse.LeftButtonDown();
                        continue;
                    }
                    if (state.Value <= 32767)
                    {
                        sim.Mouse.LeftButtonUp();
                        continue;
                    }
                    if (state.Value == 65535)
                    {
                        sim.Mouse.RightButtonDown();
                        continue;
                    }
                    if (state.Value >= 32767)
                    {
                        sim.Mouse.RightButtonUp();
                        continue;
                    }
                }
            }
        }
    }
}
