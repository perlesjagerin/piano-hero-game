using System;
namespace PV178.Homeworks.HW03.Utils
{
	public class KeyPressedEventArgs : EventArgs
	{
		public char Key { get; set; }
		public int Position { get; set; }

		public KeyPressedEventArgs(char key, int position)
		{
			Key = key;
			Position = position;
		}
	}
}

