using GameProject.DataAccess;
using GameProject.DataAccess.Postgres;
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
using System.Windows.Shapes;

namespace GameProject.View.InitialWindows
{
    /// <summary>
    /// Lógica interna para HeroWindow.xaml
    /// </summary>
    public partial class HeroWindow : Window
    {
        public HeroWindow(IDatabaseConnection databaseConnection)
        {
            // Cria uma instância do repositório de itens (por exemplo, PostgresItemRepository)
            IHeroRepository heroRepository = new PostgresHeroRepository(databaseConnection);

            // Define o view model como o contexto de dados da janela
            DataContext = new HeroWindowsVM(heroRepository); ;
            InitializeComponent();
        }
    }
}
