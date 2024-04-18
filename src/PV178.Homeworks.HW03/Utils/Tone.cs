using System;
using System.Collections.Generic;

namespace PV178.Homeworks.HW03.Utils
{
	public class Tone<TName>
	{
		public char Key { get; }
		public TName Name { get; }
		public int Frequency { get; }

		public Tone(char key, TName name, int frequency)
		{
			Key = key;
			Name = name;
			Frequency = frequency;
		}
	}
}

