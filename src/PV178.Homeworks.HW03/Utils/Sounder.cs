using System;
using System.IO;
using System.Threading;
using System.Media;

namespace PV178.Homeworks.HW03.Utils
{
    /// <summary>
    /// Class for making sound in other thread.
    /// </summary>
    public static class Sounder
    {
        private const string path = "../../../Sounds/";

        /// <summary>
        /// Makes sound with given frequency and duration.
        /// </summary>
        /// <param name="frequency">frequency</param>
        /// <param name="duration">duration</param>
        public static void MakeSound(int frequency, int duration = 300)
        {
            ThreadPool.QueueUserWorkItem(state =>
                Console.Beep(frequency, duration));
        }

        public static void MakeCoolSound(char key)
        {
            string soundFile = $"{path}piano-{key}.wav";

            if (File.Exists(soundFile))
            {
                /*using (SoundPlayer player = new SoundPlayer(soundFile))
                {
                    player.Play();
                }*/
            }
            else
            {
                throw new FileNotFoundException($"Sound file piano-{key}.wav not found");
            }
        }
    }
}
