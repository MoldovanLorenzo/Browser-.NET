using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser1
{
    public partial class KeywordsForm : Form
    {
        public List<string> Keywords { get; set; }

        public KeywordsForm()
        {
            InitializeComponent();
            Keywords = LoadKeywordsFromSQLite();
        }

        private void KeywordsForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Keywords;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                Keywords.Add(keyword);
                SaveKeywordsToSQLite();
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedKeyword = comboBox1.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedKeyword))
            {
                Keywords.Remove(selectedKeyword);
                SaveKeywordsToSQLite();
            }
        }
        private List<string> LoadKeywordsFromSQLite()
        {
            var keywords = new List<string>();
            using (var connection = new SQLiteConnection("Data Source=keywords.db;Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS Keywords (Keyword TEXT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT Keyword FROM Keywords";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            keywords.Add(reader["Keyword"].ToString());
                        }
                    }
                }
            }
            return keywords;
        }

        private void SaveKeywordsToSQLite()
        {
            using (var connection = new SQLiteConnection("Data Source=keywords.db;Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS Keywords (Keyword TEXT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE FROM Keywords";
                    command.ExecuteNonQuery();

                    foreach (var keyword in Keywords)
                    {
                        command.CommandText = $"INSERT INTO Keywords (Keyword) VALUES ('{keyword}')";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
