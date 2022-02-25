using SharpDX.DirectInput;
using WindowsInput;

namespace ConsoleApp4
{
    internal class ButtonsActionObserver : IObserver<JoystickUpdate[]>
    {
        InputSimulator sim = new InputSimulator();

        [STAThread]
        public void Update(JoystickUpdate[] updateData)
        {
            foreach (JoystickUpdate state in updateData)
            {
                if (state.Offset == JoystickOffset.Buttons6) 
                {
                    if (state.Value == 128)
                    {
                        sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
                    }

                    if (state.Value == 0)
                    {
                        sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.ESCAPE);
                    }
                }
            }
        }
    }
}
