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
using System.Windows;
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
        public ObservableCollection<Item> HeroItems { get; set; }
        public int Id { get; set; }
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

        public HeroInstanceWindowsVM(IItemRepository itemRepository, IHeroInstanceRepository heroInstanceRepository, HeroInstance heroInstance)
        {
            this.SelectedHeroInstance = heroInstance;
            _itemRepository = itemRepository;
            _heroInstanceRepository = heroInstanceRepository;
            try
            {
                SelectedHeroInstance.Id = Id = _heroInstanceRepository.Insert(SelectedHeroInstance);
                Gold = 0;
                Items = new ObservableCollection<Item>(_itemRepository.GetAll());
                HeroItems = new ObservableCollection<Item>();
                InitializeCommands();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing the HeroInstanceWindowsVM: {ex.Message}");
            }
        }

        public void InitializeCommands()
        {
            btnShop = new RelayCommand((object _) =>
            {
                try
                {
                    if (SelectedItem != null)
                    {
                        if (SelectedItem.Cost <= Gold)
                        {
                            SelectedHeroInstance.Gold = Gold -= SelectedItem.Cost;
                            OnPropertyChanged("Gold");
                            _heroInstanceRepository.Update(SelectedHeroInstance);
                            heroinstanceitemId = _heroInstanceRepository.InsertItem(SelectedHeroInstance, SelectedItem);
                            HeroItems.Add(SelectedItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while executing the 'btnShop' command: {ex.Message}");
                }
            });

            btnAdd10 = new RelayCommand((object _) =>
            {
                try
                {
                    Gold += 10;
                    SelectedHeroInstance.GainGold(10);
                    OnPropertyChanged("Gold");
                    _heroInstanceRepository.Update(SelectedHeroInstance);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while executing the 'btnAdd10' command: {ex.Message}");
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
