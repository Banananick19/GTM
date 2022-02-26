using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace ConsoleApp4
{
	internal class JoyStickListener : IObservable<JoystickUpdate[]>
	{
		public static bool Listen = true;
		public static readonly JoystickOffset buttonOffListen1 = JoystickOffset.Buttons4;
		public static readonly JoystickOffset buttonOffListen2 = JoystickOffset.Buttons5;
		private Joystick joystick;
		private List<IObserver<JoystickUpdate[]>> observers;

		public JoyStickListener() 
		{
			observers = new List<IObserver<JoystickUpdate[]>>();
		}

		public void AddObserver(IObserver<JoystickUpdate[]> o)
		{
			observers.Add(o);
		}

		public void ConnectJoyStick() 
		{
			DirectInput directInput = new DirectInput();
			var joystickGuid = Guid.Empty;
			foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
						DeviceEnumerationFlags.AllDevices))
				joystickGuid = deviceInstance.InstanceGuid;
			if (joystickGuid == Guid.Empty)
				foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
						DeviceEnumerationFlags.AllDevices))
					joystickGuid = deviceInstance.InstanceGuid;
			if (joystickGuid == Guid.Empty) throw new DeviceNotFoundException("Not found any device");
			joystick = new Joystick(directInput, joystickGuid);
			joystick.Properties.BufferSize = 128;
			joystick.Acquire();
		}

		public JoystickUpdate[] GetBufferedData()
		{
		    joystick.Poll();
			return joystick.GetBufferedData();  
		}

		public void NotifyObservers()
		{
			var datas = GetBufferedData();
			if (!Listen) 
			{
				var list = datas.ToList();
				list.RemoveAll(state => (state.Offset != buttonOffListen1) & (state.Offset != buttonOffListen2));
				datas = list.ToArray();
			}
			observers.ForEach((o) => o.Update(datas));
		}

		public void RemoveObserver(IObserver<JoystickUpdate[]> o)
		{
			observers.Remove(o);
		}
	}
}

public class DeviceNotFoundException : Exception
{
	public DeviceNotFoundException() { }
	public DeviceNotFoundException(string message) : base(message) { }
	public DeviceNotFoundException(string message, Exception inner) : base(message, inner) { }
	protected DeviceNotFoundException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
