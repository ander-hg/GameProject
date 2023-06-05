using GameProject.View.ManipulationWindows;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameProject
{
    public class HeroWindowsVM : INotifyPropertyChanged
    {
        public ObservableCollection<Hero> lista { get; set; }
        public string nome { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edit { get; private set; }
        public Hero SelectedHero { get; set; }
        public HeroWindowsVM()
        {
            lista = new ObservableCollection<Hero>();

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sua-senha";

            // Cria a conexão com o banco de dados
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Cria o comando SQL para selecionar todos os heróis
                string query = "SELECT * FROM Hero";

                // Cria o objeto NpgsqlCommand com a consulta SQL e a conexão
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Executa a consulta e obtém o resultado
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Recupera os valores das colunas do herói
                            int id = reader.GetInt32(0); // Substitua o número pelo índice da coluna desejada
                            string name = reader.GetString(1);
                            int health = reader.GetInt32(2);
                            int mana = reader.GetInt32(3);
                            int attack = reader.GetInt32(4);
                            int defense = reader.GetInt32(5);
                            

                            // Crie um objeto Hero com os valores recuperados
                            lista.Add(new Hero(name, health, mana, attack, defense));
                        }
                    }
                }
            }

            IniciaComandos();

        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {
                Hero manipulatingHero = new Hero();

                HeroManipulationWindow tela = new HeroManipulationWindow();
                tela.DataContext = manipulatingHero;
                tela.ShowDialog();
                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sua-senha";

                    // Cria a conexão com o banco de dados
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        // Cria o comando SQL para inserir um herói
                        string query = "INSERT INTO Hero (name, health, mana, attack, defense) VALUES (@Name,  @Health, @Mana, @Attack, @Defense)";

                        // Cria o objeto NpgsqlCommand com a consulta SQL e a conexão
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            // Define os parâmetros da consulta
                            command.Parameters.AddWithValue("@Name", manipulatingHero.Name);
                            command.Parameters.AddWithValue("@Health", manipulatingHero.Health);
                            command.Parameters.AddWithValue("@Mana", manipulatingHero.Mana);
                            command.Parameters.AddWithValue("@Attack", manipulatingHero.Attack);
                            command.Parameters.AddWithValue("@Defense", manipulatingHero.Defense);

                            // Executa o comando de inserção
                            command.ExecuteNonQuery();
                        }
                    }
                    lista.Add(manipulatingHero);

                } 
            });
            Edit = new RelayCommand((object _) =>
            {
                if (SelectedHero != null)
                {
                    Hero manipulatingHero = new Hero(SelectedHero.Name, SelectedHero.Health, SelectedHero.Mana, SelectedHero.Attack, SelectedHero.Defense);

                    HeroManipulationWindow tela = new HeroManipulationWindow();
                    tela.DataContext = manipulatingHero;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                    {
                        SelectedHero.updateHero(manipulatingHero);

                    }
                }
            });
            Remove = new RelayCommand((object _) =>
            {
                if (SelectedHero != null)
                {
                    lista.Remove(SelectedHero);
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
