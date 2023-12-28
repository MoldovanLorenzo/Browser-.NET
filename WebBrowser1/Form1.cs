using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser1
{
    public partial class Form1 : Form
    {
        private KeywordsForm KeywordsFormInstance;

        public Form1()
        {
            InitializeComponent();
            KeywordsFormInstance = new KeywordsForm();
            webBrowser1.Navigating += webBrowser1_Navigating;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com");
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string searchQuery = toolStripTextBox1.Text;
            if (!string.IsNullOrEmpty(searchQuery))
            {
                string searchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(searchQuery)}";
                webBrowser1.Navigate(searchUrl);
            }
        }

        private async void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (KeywordsFormInstance.Keywords.Any(keyword => e.Url.ToString().Contains(keyword)))
            {
                e.Cancel = true;

                await AsyncTask(e.Url.ToString());

                MessageBox.Show("Accesul la acest site este restricționat!");
            }
        }

        private async Task AsyncTask(string url)
        {
            await Task.Run(() =>
            {
                var filteredUrls = KeywordsFormInstance.Keywords
                    .Where(keyword => url.Contains(keyword))
                    .ToList();

              
                System.Diagnostics.Trace.WriteLine($"Blocked URL: {url}");

               
                System.IO.File.AppendAllText("BlockedUrls.txt", $"Blocked URL: {url}\n");
            });
        }
    }
}
