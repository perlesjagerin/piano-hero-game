using System;
using System.Collections.Generic;

namespace PV178.Homeworks.HW03.Utils
{
	public class ToneCollection<TName>
	{
		private Dictionary<char, Tone<TName>> tones;

		public ToneCollection()
		{
			tones = new Dictionary<char, Tone<TName>>();
		}

		public void AddRange(IEnumerable<Tone<TName>> collection)
		{
			foreach (Tone<TName> tone in collection)
			{
				tones.Add(tone.Key, tone);
			}

		}

        public Tone<TName> GetToneByKey(char key)
        {
			if (tones.TryGetValue(key, out Tone<TName> value))
			{
				return value;
			}

            throw new KeyNotFoundException($"Tone with key '{key}' not found");
        }

        public string GetInfo(char key)
		{
			Tone<TName> tone = GetToneByKey(key);
			return $"Key: {tone.Key}, Name: {tone.Name}, Frequency: {tone.Frequency} Hz";
		}
	}
}
