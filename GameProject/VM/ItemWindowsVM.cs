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
        private readonly IItemRepository itemRepository;

        public ObservableCollection<Item> Items { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public int Value { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edit { get; private set; }
        public Item SelectedItem { get; set; }

        public ItemWindowsVM(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
            Items = new ObservableCollection<Item>(itemRepository.GetAllItems());

            IniciaComandos();
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {
                Item manipulatingItem = new Item(Name, Description, Type, Attribute, Value);

                ItemManipulationWindow tela = new ItemManipulationWindow();
                tela.DataContext = manipulatingItem;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    Items.Add(manipulatingItem);
                    itemRepository.AddItem(manipulatingItem);
                }
            });

            Edit = new RelayCommand((object _) =>
            {
                if (SelectedItem != null)
                {
                    Item manipulatingItem = new Item(SelectedItem.Name, SelectedItem.Description, SelectedItem.Type, SelectedItem.Attribute, SelectedItem.Value);

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
                        itemRepository.UpdateItem(SelectedItem);
                    }
                }
            });

            Remove = new RelayCommand((object _) =>
            {
                if (SelectedItem != null)
                {
                    Items.Remove(SelectedItem);
                    itemRepository.DeleteItem(SelectedItem.Id);
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