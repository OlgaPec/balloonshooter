using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using static BalloonShooter.ScoresForm;

namespace BalloonShooter
{
    public partial class Form1 : Form
    {
        private Random random;
        private int score;
        private int missedBalloons;
        private int nechyceno;
        private int limit = 5;
        private string scoresFile = "scores.txt";
        private Dictionary<string, int> scores = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            score = 0;
            missedBalloons = 0;
            nechyceno = 0;
            LoadScores(); // Načíst body hráčů při startu hry
        }
        private void LoadScores()
        {
            // Zkontrolovat, zda existuje soubor se skóre
            if (File.Exists(scoresFile))
            {
                // Načíst body hráčů ze souboru
                string[] lines = File.ReadAllLines("scores.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        scores[parts[0]] = int.Parse(parts[1]);
                    }
                }
            }
        }
        private void SaveScores()
        {
            using (StreamWriter writer = new StreamWriter(scoresFile))
            {
                foreach (var pair in scores)
                {
                    writer.WriteLine($"{pair.Key}:{pair.Value}");
                }
            }
        }
        private void UpdateScores(string playerName, int playerScore)
        {
            // Aktualizovat skóre hráče
            if (scores.ContainsKey(playerName))
            {
                scores[playerName] += playerScore;
            }
            else
            {
                scores[playerName] = playerScore;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Vytvoření nového balonku
            PictureBox balloon = new PictureBox();
            balloon.BackColor = GetRandomColor(); // Náhodná barva balonku
            int size = random.Next(30, 50); // Náhodná velikost balonku
            balloon.Size = new Size(size, size);
            SetCircularRegion(balloon);
            int x = random.Next(panel1.Width - size); // Náhodná pozice x
            int y = panel1.Height - size; // Začínáme zespodu
            balloon.Location = new Point(x, y);
            balloon.MouseDown += Balloon_MouseDown; // Přidání události pro chytání balonku myší
            panel1.Controls.Add(balloon);

            // Animace balonku
            int speed = random.Next(3, 8); // Náhodná rychlost
            MoveBalloon(balloon, speed);
        }

        private void Balloon_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox balloon = sender as PictureBox;
            if (balloon != null)
            {
                panel1.Controls.Remove(balloon); // Odebrat balonek z panelu
                score++; // Přičíst body
                nechyceno = 0;
                labelBody.Text = $"{score}"; // Aktualizovat zobrazení skóre
            }
        }

        private void MoveBalloon(PictureBox balloon, int speed)
        {
            Timer balloonTimer = new Timer();
            balloonTimer.Interval = 50; // Interval pro pohyb balonku
            balloonTimer.Tick += (sender, e) =>
            {
                balloon.Top -= speed; // Pohyb balonku nahoru
                if (balloon.Top + balloon.Height < 0) // Pokud balonek zmizí z horního okraje panelu
                {
                    if (!timer1.Enabled) // Pokud hra již skončila
                    {
                        balloonTimer.Stop(); // Zastavit timer pro tento balonek
                        balloonTimer.Dispose(); // Uvolnit zdroje timeru
                        return; // Ukončit metodu, balonek se již dále nemá pohybovat
                    }
                    balloonTimer.Stop(); // Zastavit timer pro tento balonek
                    balloonTimer.Dispose(); // Uvolnit zdroje timeru
                    missedBalloons++;
                    nechyceno++;
                    panel1.Controls.Remove(balloon); // Odebrat balonek z panelu
                    labelUteklo.Text = $"{missedBalloons}";
                    if (nechyceno >= limit)
                    {
                        GameOver();
                    }
                }
            };
            balloonTimer.Start(); // Spustit timer pro pohyb balonku
        }

        private void SetCircularRegion(PictureBox balloon)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new Rectangle(Point.Empty, balloon.Size));
            balloon.Region = new Region(path);
        }

        private Color GetRandomColor()
        {
            // Náhodně vybrat barvu z předdefinovaného seznamu
            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };
            return colors[random.Next(colors.Length)];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            buttonKonec.Visible = false;
            score = 0;
            missedBalloons = 0;
            nechyceno = 0;
            labelBody.Text = "0";
            labelUteklo.Text = "0";
        }

        private void GameOver()
        {
            // Zastavit hru
            timer1.Stop();
            buttonKonec.Visible = true;

            // Přidat aktuální skóre hráče do slovníku skóre
            string playerName = ShowInputDialog("Game over","Enter your name:");
            UpdateScores(playerName, score);
            // Uložit skóre do souboru
            SaveScores();

            // Zobrazit výsledky v novém okně
            ScoresForm scoresForm = new ScoresForm();
            scoresForm.ShowDialog();
        }
        private string ShowInputDialog(string caption, string text)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        private void buttonKonec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}