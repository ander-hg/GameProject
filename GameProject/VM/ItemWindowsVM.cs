using GameProject.DataAccess;
using GameProject.DataAccess.Postgres;
using GameProject.View.ManipulationWindows;
using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GameProject
{
    public class ItemWindowsVM : INotifyPropertyChanged
    {
        private readonly IItemRepository _itemRepository;

        public ObservableCollection<Item> Items { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public int Value { get; set; }
        public int Cost { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edit { get; private set; }
        public Item SelectedItem { get; set; }

        public ItemWindowsVM(IItemRepository itemRepository)
        {
            this._itemRepository = itemRepository;
            Items = new ObservableCollection<Item>(_itemRepository.GetAll());

            IniciaComandos();
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {
                Item manipulatingItem = new Item(Id, Name, Description, Type, Attribute, Value, Cost);

                ItemManipulationWindow tela = new ItemManipulationWindow();
                tela.DataContext = manipulatingItem;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    Items.Add(manipulatingItem);
                    _itemRepository.Insert(manipulatingItem);
                }
            });

            Edit = new RelayCommand((object _) =>
            {
                if (SelectedItem != null)
                {
                    Item manipulatingItem = new Item(SelectedItem);

                    ItemManipulationWindow tela = new ItemManipulationWindow();
                    tela.DataContext = manipulatingItem;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                    {
                        SelectedItem.Name = manipulatingItem.Name;
                        SelectedItem.Description = manipulatingItem.Description;
                        SelectedItem.Type = manipulatingItem.Type;
                        SelectedItem.Attribute = manipulatingItem.Attribute;
                        SelectedItem.Value = manipulatingItem.Value;
                        _itemRepository.Update(SelectedItem);
                    }
                }
            });

            Remove = new RelayCommand((object _) =>
            {
                if (SelectedItem != null)
                {
                    _itemRepository.Delete(SelectedItem);
                    Items.Remove(SelectedItem);
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