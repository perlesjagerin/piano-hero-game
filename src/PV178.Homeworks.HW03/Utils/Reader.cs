using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PV178.Homeworks.HW03.Utils
{
    /// <summary>
    /// Class responsible for reading songs from textfiles and handling user input.
    /// </summary>
    public class Reader : IDisposable
    {
        public string Text { get; set; }
        public event EventHandler<KeyPressedEventArgs> KeyPressed;
        public event EventHandler<int> KeyNotPressed;

        private const int Timeout = 300;
        private readonly Displayer displayer = new Displayer();
        private readonly AutoResetEvent trackDone;
        private readonly Thread checkingThread;
        private readonly Thread gettingThread;
        private readonly string path = $"../../../Songs/";
        private char? input;
        private bool end;
        private Piano piano;

        public Reader(string songName)
        {
            if (File.Exists($"{path}{songName}.txt"))
            {
                Text = File.ReadAllText($"{path}{songName}.txt");
                trackDone = new AutoResetEvent(false);
                checkingThread = new Thread(CheckInput) { IsBackground = true };
                gettingThread = new Thread(GetInput) { IsBackground = true };

                var tones = new List<Tone<char>>
                {
                    new Tone<char>('a', 'C', 261),
                    new Tone<char>('s', 'D', 293),
                    new Tone<char>('d', 'E', 330),
                    new Tone<char>('f', 'F', 349),
                    new Tone<char>('g', 'G', 392),
                    new Tone<char>('h', 'A', 440),
                    new Tone<char>('j', 'H', 494),
                };

                piano = new Piano(tones);
            }
            else
            {
                throw new ArgumentException("Wrong song path");
            }
        }

        /// <summary>
        /// Starts reading keys and checking whether user pressed some.
        /// </summary>
        public void ReadKeys()
        {
            gettingThread.Start();
            checkingThread.Start();
            trackDone.WaitOne();
        }

        /// <summary>
        /// Performs cleanup.
        /// </summary>
        public void Dispose()
        {
            end = true;
            trackDone.Dispose();
            Console.Clear();
        }

        /// <summary>
        /// Invokes event that says which key was pressed and what is actual reading position.
        /// </summary>
        /// <param name="key">pressed key</param>
        /// <param name="position">actual reading position</param>
        public void OnKeyPressed(char key, int position)
        {
            KeyPressed?.Invoke(this, new KeyPressedEventArgs(key, position));
        }

        /// <summary>
        /// Invokes event that says no key was pressed and what is actual reading position.
        /// </summary>
        /// <param name="position">actual reading position</param>
        public void OnKeyNotPressed(int position)
        {
            KeyNotPressed?.Invoke(this, position);
        }

        /// <summary>
        /// Periodically checks if some key was pressed. 
        /// </summary>
        private void CheckInput()
        {
            for (var i = -6; i < Text.Length; i++)
            {
                displayer.ActualDisplay(Text, i + 6);
                Thread.Sleep(Timeout);
                // First chars just skip (because animation)
                if (i < 0)
                {
                    continue;
                }
                if (input != null)
                {
                    OnKeyPressed((char)input, i);
                    input = null;
                }
                else
                {
                    OnKeyNotPressed(i);
                }
            }
            trackDone.Set();
        }

        /// <summary>
        /// Gets input from the user.
        /// </summary>
        private void GetInput()
        {
            while (true)
            {
                input = Console.ReadKey(true).KeyChar;
                if (input.HasValue && !end)
                {
                    //Sounder.MakeSound(400); // old
                    //piano.Play(input.Value); // actual
                }
            }
        }
    }
}
