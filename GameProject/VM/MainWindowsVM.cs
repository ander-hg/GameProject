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

        private readonly IHeroRepository _heroRepository;
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

        public MainWindowsVM()
        {
            this.databaseConnection = new PostgresConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sua-senha");
            _heroRepository = new PostgresHeroRepository(databaseConnection);
            Heroes = new ObservableCollection<Hero>(_heroRepository.GetAll());
            IniciaComandos();
        }

        public void IniciaComandos()
        {
            btnAdd = new RelayCommand((object _) =>
            {
                Hero manipulatingHero = new Hero(Id, Name, Health, Mana, Attack, Defense);

                HeroManipulationWindow tela = new HeroManipulationWindow();
                tela.DataContext = manipulatingHero;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    _heroRepository.Insert(manipulatingHero);
                    Heroes.Add(manipulatingHero);
                }
            });
            btnEdit = new RelayCommand((object _) =>
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
            });
            btnRemove = new RelayCommand((object _) =>
            {
                if (SelectedHero != null)
                {
                    _heroRepository.Delete(SelectedHero);
                    Heroes.Remove(SelectedHero);
                }
            });

            btnItem = new RelayCommand((object _) =>
            {
                ItemWindow tela = new ItemWindow(databaseConnection);
                tela.Show();

            });

            btnPlay = new RelayCommand((object _) =>
            {
                if (SelectedHero != null)
                {
                    HeroInstance manipulatingHeroInstance = new HeroInstance(new Hero(SelectedHero), 1, 0, new List<Item>(), 0);
                    HeroInstanceWindow tela = new HeroInstanceWindow(databaseConnection, manipulatingHeroInstance);
                    tela.Show();
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
