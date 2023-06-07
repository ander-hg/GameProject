using GameProject.DataAccess;
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
        private readonly IHeroRepository _heroRepository;
        private readonly IItemRepository _itemRepository;
        private IHeroInstanceRepository _heroInstanceRepository;
        public ObservableCollection<Hero> Heroes { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public int Id { get; set; }
        public int id;
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }
        public ICommand Play { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edit { get; private set; }
        public ICommand btnShop { get; private set; }
        public ICommand btnAdd10 { get; private set; }
        public HeroInstance SelectedHeroInstance { get; set; }
        public Hero SelectedHero { get; set; }
        public Item SelectedItem { get; set; }
        int heroinstanceitemId;

        public HeroInstanceWindowsVM(IHeroRepository heroRepository, IItemRepository itemRepository, IHeroInstanceRepository heroInstanceRepository, HeroInstance heroInstance)
        {
            this.SelectedHeroInstance = heroInstance;
            _heroRepository = heroRepository;
            _itemRepository = itemRepository;
            _heroInstanceRepository = heroInstanceRepository;
            _heroInstanceRepository.Insert(SelectedHeroInstance, ref id);
            Id = id;
            SelectedHeroInstance.Id = id;
            Gold = 0;
            Heroes = new ObservableCollection<Hero>(_heroRepository.GetAll());
            Items = new ObservableCollection<Item>(_itemRepository.GetAll());
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            btnShop = new RelayCommand((object _) =>
            {
                if (SelectedItem != null)
                { 
                    if (SelectedItem.Cost <= Gold)
                    { 
                        SelectedHeroInstance.Gold = Gold -= SelectedItem.Cost;
                        _heroInstanceRepository.Update(SelectedHeroInstance);
                        OnPropertyChanged("Gold");
                        _heroInstanceRepository.InsertItem(SelectedHeroInstance, SelectedItem, ref heroinstanceitemId);
                    }
                }

            });

            btnAdd10 = new RelayCommand((object _) =>
            {
                Gold += 10;
                SelectedHeroInstance.GainGold(10);
                OnPropertyChanged("Gold");
                _heroInstanceRepository.Update(SelectedHeroInstance);
            });

            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
