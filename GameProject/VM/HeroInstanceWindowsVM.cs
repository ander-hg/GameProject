using GameProject.View.ManipulationWindows;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameProject
{
    public class HeroInstanceWindowsVM : INotifyPropertyChanged
    {
        public ObservableCollection<HeroInstance> HeroInstances { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edit { get; private set; }
        public HeroInstance SelectedHeroInstance { get; set; }
        
        public HeroInstanceWindowsVM()
        {
            HeroInstances = new ObservableCollection<HeroInstance>();

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sua-senha";
            // Cria a conexão com o banco de dados
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Cria o comando SQL para selecionar todas as instâncias de herói
                string query = "SELECT * FROM HeroInstance";

                // Cria o objeto NpgsqlCommand com a consulta SQL e a conexão
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Executa a consulta e obtém o resultado
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Recupera os valores das colunas da instância de herói
                            int id = reader.GetInt32(0);
                            int currentLevel = reader.GetInt32(1);
                            int currentExperience = reader.GetInt32(2);
                            int gold = reader.GetInt32(3);
                            int heroId = reader.GetInt32(4);
                            bool targetable = reader.GetBoolean(5);
                            bool attackable = reader.GetBoolean(6);

                            // Crie um objeto HeroInstance com os valores recuperados
                            //HeroInstances.Add(new HeroInstance(heroId, currentExperience, gold, heroId));

                        }
                    }
                }
            }
            HeroInstances = new ObservableCollection<HeroInstance>() {
                new HeroInstance(new Hero(), 1, 0, new List<Item>(), 0)
            };

            InitializeCommands();
        }

        public void InitializeCommands()
        {
            Add = new RelayCommand((object _) =>
            {
                HeroInstance manipulatingHeroInstance = new HeroInstance(new Hero(), 1, 0, new List<Item>(), 0);

                HeroInstanceManipulationWindow window = new HeroInstanceManipulationWindow();
                window.DataContext = manipulatingHeroInstance;
                bool? dialogResult = window.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value)
                {
                    HeroInstances.Add(manipulatingHeroInstance);
                }
            });

            Edit = new RelayCommand((object _) =>
            {
                if (SelectedHeroInstance != null)
                {
                    HeroInstance manipulatingHeroInstance = new HeroInstance(
                        new Hero(SelectedHeroInstance.Hero.Name, SelectedHeroInstance.Hero.Health, SelectedHeroInstance.Hero.Mana, SelectedHeroInstance.Hero.Attack, SelectedHeroInstance.Hero.Defense),
                        SelectedHeroInstance.CurrentLevel,
                        SelectedHeroInstance.CurrentExperience,
                        SelectedHeroInstance.Items.ToList(),
                        SelectedHeroInstance.Gold
                    );

                    HeroInstanceManipulationWindow window = new HeroInstanceManipulationWindow();
                    window.DataContext = manipulatingHeroInstance;
                    bool? dialogResult = window.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value)
                    {
                        SelectedHeroInstance.updateHeroInstance(manipulatingHeroInstance);
                    }
                }
            });

            Remove = new RelayCommand((object _) =>
            {
                if (SelectedHeroInstance != null)
                {
                    HeroInstances.Remove(SelectedHeroInstance);
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
