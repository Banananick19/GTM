using SharpDX.DirectInput;
using WindowsInput;

namespace ConsoleApp4
{
    internal class ButtonsActionObserver : IObserver<JoystickUpdate[]>
    {
        InputSimulator sim = new InputSimulator();
        WindowsInput.Native.VirtualKeyCode ignoreCode = WindowsInput.Native.VirtualKeyCode.CANCEL;
        int buttonOffListen1TimeStamp;
        public void Update(JoystickUpdate[] updateData)
        {
            foreach (JoystickUpdate state in updateData)
            {
                WindowsInput.Native.VirtualKeyCode code = ignoreCode;
                if (state.Offset == JoystickOffset.Buttons6) 
                {
                    code = WindowsInput.Native.VirtualKeyCode.ESCAPE;
                }
                if (state.Offset == JoyStickListener.buttonOffListen1)
                {
                    if (state.Value == 128) 
                    {
                        buttonOffListen1TimeStamp = state.Timestamp;
                    }
                    
                }
                if (state.Offset == JoyStickListener.buttonOffListen2)
                {
                    if (state.Value == 128)
                    {
                        if (state.Timestamp - buttonOffListen1TimeStamp < 70)
                        {
                            JoyStickListener.Listen = !JoyStickListener.Listen;
                        }
                    }
                }

                if (code == ignoreCode) continue;
                if (state.Value == 128)
                {
                    sim.Keyboard.KeyPress(code);
                }

                if (state.Value == 0)
                {
                    sim.Keyboard.KeyUp(code);
                }
                
            }
        }
    }
}
