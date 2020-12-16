using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Models;
using static WindowsFormsApp.Views.CarViewModel;
using WindowsFormsApp.Presenter;

namespace WindowsFormsApp
{
    public partial class Form1 : Form, ICarsView
    {
        public CarsPresenter Presenter { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        public IList<Car> List
        {
            get => (IList<Car>)dataGridView1.DataSource;
            set => dataGridView1.DataSource = value;
          
        }

        private async void Save_Click(object sender, EventArgs e)
        {
          await Presenter.SaveCars(List);
        }

        private async void Add_Click(object sender, EventArgs e)
        {
            await Presenter.AddCar(List);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
