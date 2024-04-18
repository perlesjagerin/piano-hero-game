using System;

namespace PV178.Homeworks.HW03.Utils
{
	public class Game : IGame
	{
        public static bool IsPremium { get; set; }

        private Reader reader;
        private int score;
        private string songName;
        private string code;

        public void Run()
        {
            Console.WriteLine("Welcome to the Music Game!");

            AskUserForSongAndCode();
            CheckPremiumCode();
            PlayGame();
            DisplayScoreAndDispose();
        }

        private void AskUserForSongAndCode()
        {
            Console.WriteLine("Enter name of the song file (without extension):");
            songName = Console.ReadLine();

            Console.WriteLine("Enter special code to unlock Cool Sounds:");
            code = Console.ReadLine();
        }

        private void CheckPremiumCode()
        {
            if (code.IsCodePremium())
            {
                IsPremium = true;
                Console.WriteLine("Congrats! You unlocked Cool Sounds");
            }
            else
            {
                IsPremium = false;
                Console.WriteLine("Code is invalid. Basic sounds will be used");
            }
        }

        private void PlayGame()
        {
            Console.Write("Press any key to start game: ");
            Console.ReadKey();

            reader = new Reader(songName);
            score = reader.Text.Length;

            reader.KeyPressed += HandleKeypressed;
            reader.KeyNotPressed += HandleKeyNotPressed;

            reader.ReadKeys();

            reader.KeyPressed -= HandleKeypressed;
            reader.KeyNotPressed -= HandleKeyNotPressed;
        }

        private void DisplayScoreAndDispose()
        {
            Console.WriteLine($"Congratulations! You scored {score} points.");
            Console.WriteLine("Press any key to dispose and close this window");
            Console.ReadKey();
            reader.Dispose();
        }

        private void ScoreCalculate(char key, int position)
        {
            if (key != reader.Text[position])
            {
                score--;
            }
        }

        private void HandleKeypressed(object sender, KeyPressedEventArgs args)
        {
            ScoreCalculate(args.Key, args.Position);
        }

        private void HandleKeyNotPressed(object sender, int position)
        {
            ScoreCalculate(' ', position);
        }
    }
}

