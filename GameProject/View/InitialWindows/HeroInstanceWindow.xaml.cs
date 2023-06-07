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
    /// Lógica interna para HeroInstanceWindow.xaml
    /// </summary>
    public partial class HeroInstanceWindow : Window
    {
        public HeroInstanceWindow(IDatabaseConnection databaseConnection, HeroInstance heroInstance)
        {
            // Cria uma instância do repositório (PostgresItemRepository)
            IHeroRepository heroRepository = new PostgresHeroRepository(databaseConnection);
            IItemRepository itemRepository = new PostgresItemRepository(databaseConnection);
            IHeroInstanceRepository heroInstanceRepository = new PostgresHeroInstanceRepository(databaseConnection);

            // Define o view model como o contexto de dados da janela
            DataContext = new HeroInstanceWindowsVM(heroRepository, itemRepository, heroInstanceRepository, heroInstance); ;
            InitializeComponent();
        }
    }
}
