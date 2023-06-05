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
    /// Lógica interna para ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {

        public ItemWindow(IDatabaseConnection databaseConnection)
        {
            // Cria uma instância do repositório de itens (por exemplo, PostgresItemRepository)
            IItemRepository itemRepository = new PostgresItemRepository(databaseConnection);

            // Define o view model como o contexto de dados da janela
            DataContext = new ItemWindowsVM(itemRepository);;
            InitializeComponent();
        }
    }
}
