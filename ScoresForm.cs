using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BalloonShooter
{
    public partial class ScoresForm : Form
    {
        private Dictionary<string, int> scores;

        public ScoresForm()
        {
            InitializeComponent();
            scores = LoadScores();
            DisplayScores();
            listViewScores.View = View.Details;
            listViewScores.Columns.Add("Name", 120);
            listViewScores.Columns.Add("Score", 80);
        }
        private Dictionary<string, int> LoadScores()
        {
            Dictionary<string, int> loadedScores = new Dictionary<string, int>();
            string scoresFile = "scores.txt";

            // Zkontrolovat, zda existuje soubor se skóre
            if (File.Exists(scoresFile))
            {
                // Načíst body hráčů ze souboru
                string[] lines = File.ReadAllLines(scoresFile);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        loadedScores[parts[0]] = int.Parse(parts[1]);
                    }
                }
            }
            // Seřadit skóre od nejvyššího po nejnižší
            var sortedScores = loadedScores.OrderByDescending(pair => pair.Value);

            // Konvertovat na Dictionary a vrátit
            return sortedScores.ToDictionary(pair => pair.Key, pair => pair.Value);

        }
        private void DisplayScores()
        {
            listViewScores.Items.Clear(); // Vyčištění seznamu před zobrazením nových dat

            // Procházení slovníku skóre a přidání dat do listView
            foreach (var pair in scores)
            {
                ListViewItem item = new ListViewItem(pair.Key); // Název hráče
                item.SubItems.Add(pair.Value.ToString()); // Skóre hráče
                listViewScores.Items.Add(item); // Přidání položky do seznamu
            }
        }
    }
}
