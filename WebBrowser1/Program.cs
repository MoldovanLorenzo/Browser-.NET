
using System;
using System.Windows.Forms;
using WebBrowser1;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        using (KeywordsForm keywordsForm = new KeywordsForm())
        {
            Application.Run(keywordsForm);
        }

        using (Form1 form1 = new Form1())
        {
            Application.Run(form1);
        }
    }
}