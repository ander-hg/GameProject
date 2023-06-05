using GameProject.DataAccess.Postgres;
using GameProject.DataAccess;
using GameProject.View.InitialWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameProject
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IDatabaseConnection databaseConnection = new PostgresConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sua-senha");
            DataContext = new MainWindowsVM(databaseConnection);
            InitializeComponent();
        }
    }
}
