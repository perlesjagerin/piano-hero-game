using System;
using System.Collections.Generic;

namespace PV178.Homeworks.HW03.Utils
{
	public class Piano
	{
		private ToneCollection<char> toneCollection;

		public Piano(IEnumerable<Tone<char>> tones)
		{
            toneCollection = new ToneCollection<char>();
            toneCollection.AddRange(tones);
		}

		public void Play(char key)
		{
			if (Game.IsPremium)
			{
				Sounder.MakeCoolSound(key);
			}
			else
			{
                Tone<char> tone = toneCollection.GetToneByKey(key);
                Sounder.MakeSound(tone.Frequency);
            }
		}
	}
}

