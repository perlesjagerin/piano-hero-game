using System;
using System.Collections.Generic;

namespace PV178.Homeworks.HW03.Utils
{
	public static class DictionaryExtensions
	{
        public static void AddRange<TName>(
            this Dictionary<char, Tone<TName>> tones,
            IEnumerable<Tone<TName>> collection)
		{
            foreach (Tone<TName> tone in collection)
            {
                tones.Add(tone.Key, tone);
            }
        }
	}
}

