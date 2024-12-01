using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Mastermind
{
    public static class Data
    {
        private static Random random = new Random();
        private static DispatcherTimer timer = new DispatcherTimer();
        private static int secondsPassed;
        public static int Attempts { get; set; }
        public static int Score { get; set; }
        static Dictionary<SolidColorBrush, string> colors = new Dictionary<SolidColorBrush, string>();
        public static Dictionary<SolidColorBrush, string> Colors
        {
            get { return colors; }
        }
        private static List<string> colorCode = new List<string>();
        public static List<string> ColorCode
        {
            get {  return colorCode; }
        }
        public static bool Color1LabelAdded { get; set; }
        public static bool Color2LabelAdded { get; set; }
        public static bool Color3LabelAdded { get; set; }
        public static bool Color4LabelAdded { get; set; }
        static Data()
        {
            colors.Add(Brushes.Red, "Red");
            colors.Add(Brushes.Orange, "Orange");
            colors.Add(Brushes.Yellow, "Yellow");
            colors.Add(Brushes.Green, "Green");
            colors.Add(Brushes.Blue, "Blue");
            colors.Add(Brushes.White, "White");
            Attempts = 1;
            Score = 100;
            timer.Tick += new EventHandler(CheckTimer);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        public static void GenerateRandomColorCode()
        {
            colorCode.Clear();
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(0, colors.Count);
                colorCode.Add(colors.ElementAt(index).Value);
            }
            StartCountdown();
        }
        public static List<int> ValidateColorCode(List<string> inputColors)
        {
            List<int> points = new List<int>();
            for(int i = 0; i < inputColors.Count; i++)
            {
                if (inputColors[i].Equals(ColorCode[i]))
                {
                    points.Add(2);
                }
                else if (ColorCode.Contains(inputColors[i]))
                {
                    points.Add(1);
                    Score -= 1;
                }
                else
                {
                    points.Add(0);
                    Score -= 2;

                }
            }
            ResetBooleans();
            return points;
        }
        public static void ResetBooleans()
        {
            Color1LabelAdded = false;
            Color2LabelAdded = false;
            Color3LabelAdded = false;
            Color4LabelAdded = false;
        }

        public static void IncreaseAttempst()
        {
            Attempts++;
        }
        /// <summary>
        /// De ToogleDebug methode zorgt er voor dat de debug
        /// mode in- of uitgeschakeld wordt.
        /// </summary>
        /// <param name="textBox">Naam van de Textbox die verborgen/weergegeven wordt door de debug methode.</param>
        public static void ToggleDebug(TextBox textBox)
        {
            if (textBox.Text == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < colorCode.Count; i++)
                {
                    stringBuilder.Append($"{colorCode[i]} ");
                }
                textBox.Text = stringBuilder.ToString();
            }
            if (textBox.Visibility == System.Windows.Visibility.Visible)
            {
                textBox.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                textBox.Visibility = System.Windows.Visibility.Visible;
            }
        }
        /// <summary>
        /// De StartCountdown functie start een timer met een
        /// interval van 1 seconde.
        /// </summary>
        public static void StartCountdown()
        {
            secondsPassed = 0;
            timer.Start();
        }
        /// <summary>
        /// De StopCountdown methode laat de timer na 10 seconden stoppen.
        /// </summary>
        public static void CheckTimer(object sender, EventArgs e)
        {
            secondsPassed++;
            if (secondsPassed == 10)
            {
                StopCountdown();
                IncreaseAttempst();
                Score -= 8;
            }
        }
        public static void StopCountdown()
        {
            timer.Stop();
            secondsPassed = 0;
        }
        public static bool CheckGameOver()
        {
            if (Attempts > 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void ResetGame()
        {
            Score = 100;
            secondsPassed = 0;
            Attempts = 1;

            GenerateRandomColorCode();
        }
    }
}