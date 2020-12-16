using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Presenter;

namespace WindowsFormsApp
{
    static class Program
    {
        /// <summary> 
        /// The main entry point for the application. 
        /// </summary> 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new Form1();
            form.Presenter = new CarsPresenter(form);
            Application.Run(form);
        }

    }
}
