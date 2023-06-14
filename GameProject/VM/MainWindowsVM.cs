using GameProject.DataAccess;
using GameProject.DataAccess.Postgres;
using GameProject.View.InitialWindows;
using GameProject.View.ManipulationWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace GameProject
{
    public class MainWindowsVM : INotifyPropertyChanged
    {
        public IDatabaseConnection databaseConnection;
        public ICommand btnPlay { get; private set; }
        public ICommand btnItem { get; private set; }
        public ICommand btnScore { get; private set; }

        private readonly IHeroRepository _heroRepository;
        private readonly IHeroInstanceRepository _heroInstanceRepository;
        public ObservableCollection<Hero> Heroes { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public ICommand btnAdd { get; private set; }
        public ICommand btnRemove { get; private set; }
        public ICommand btnEdit { get; private set; }
        public Hero SelectedHero { get; set; }
        public ObservableCollection<HeroInstance> HeroInstances { get; set; }

        public MainWindowsVM()
        {
            try
            {
                this.databaseConnection = new PostgresConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sua-senha");
                _heroRepository = new PostgresHeroRepository(databaseConnection);
                _heroInstanceRepository = new PostgresHeroInstanceRepository(databaseConnection);

                Heroes = new ObservableCollection<Hero>(_heroRepository.GetAll());

                IniciaComandos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing the MainWindowsVM: {ex.Message}");
            }
        }

        public void IniciaComandos()
        {
            btnAdd = new RelayCommand((object _) =>
            {
                try
                {
                    Hero manipulatingHero = new Hero(Id=0, Name="name", Health=100, Mana=50, Attack=10, Defense = 0);

                    HeroManipulationWindow tela = new HeroManipulationWindow();
                    tela.DataContext = manipulatingHero;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                    {
                        manipulatingHero.Id = _heroRepository.Insert(manipulatingHero);
                        Heroes = new ObservableCollection<Hero>(_heroRepository.GetAll());
                        Notifica(nameof(Heroes));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding the hero: {ex.Message}");
                }
            });
            btnEdit = new RelayCommand((object _) =>
            {
                try
                {
                    if (SelectedHero != null)
                    {
                        Hero manipulatingHero = new Hero(SelectedHero);

                        HeroManipulationWindow tela = new HeroManipulationWindow();
                        tela.DataContext = manipulatingHero;
                        tela.ShowDialog();

                        if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                        {
                            SelectedHero.updateHero(manipulatingHero);
                            _heroRepository.Update(SelectedHero);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while editing the hero: {ex.Message}");
                }
            });
            btnRemove = new RelayCommand((object _) =>
            {
                try
                {
                    if (SelectedHero != null)
                    {
                        _heroRepository.Delete(SelectedHero);
                        Heroes = new ObservableCollection<Hero>(_heroRepository.GetAll());
                        Notifica(nameof(Heroes));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while editing the hero: {ex.Message}");
                }
            });

            btnItem = new RelayCommand((object _) =>
            {
                try
                {
                    ItemWindow tela = new ItemWindow(databaseConnection);
                    tela.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while opening the item window: {ex.Message}");
                }
            });

            btnPlay = new RelayCommand((object _) =>
            {
                try
                {
                    if (SelectedHero == null) {
                        throw new Exception("No hero selected");
                    }
                    HeroInstance manipulatingHeroInstance = new HeroInstance(new Hero(SelectedHero), 1, 0, new List<Item>(), 0);
                    HeroInstanceWindow tela = new HeroInstanceWindow(databaseConnection, manipulatingHeroInstance);
                    tela.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
            btnScore = new RelayCommand((object _) =>
            {
                try
                {
                    HeroInstances = new ObservableCollection<HeroInstance>(_heroInstanceRepository.GetAll());
                    foreach (HeroInstance hi in HeroInstances)
                    {
                        hi.Items = _heroInstanceRepository.GetAllHeroItems(hi);
                        hi.Hero = _heroInstanceRepository.GetHero(hi);
                    }
                    ScoreWindow tela = new ScoreWindow();
                    tela.DataContext = HeroInstances;
                    tela.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while oppening the score window: {ex.Message}");
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
