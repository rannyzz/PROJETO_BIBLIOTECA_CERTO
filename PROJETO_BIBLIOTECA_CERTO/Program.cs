using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto.Macs;

namespace PROJETO_BIBLIOTECA_CERTO
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form2 splash = new Form2();
            splash.Show();
            Application.DoEvents();
            Thread.Sleep(3000);
            splash.Close();
            Application.Run(new Form1());
        }
    }
}
