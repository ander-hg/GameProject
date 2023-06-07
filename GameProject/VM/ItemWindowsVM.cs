using GameProject.DataAccess;
using GameProject.DataAccess.Postgres;
using GameProject.View.ManipulationWindows;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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
            try
            {
                this._itemRepository = itemRepository;
                Items = new ObservableCollection<Item>(_itemRepository.GetAll());

                IniciaComandos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing the ItemWindowsVM: {ex.Message}");
            }
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {
                try
                {
                    Item manipulatingItem = new Item(Id, Name, Description, Type, Attribute, Value, Cost);

                    ItemManipulationWindow tela = new ItemManipulationWindow();
                    tela.DataContext = manipulatingItem;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                    {
                        manipulatingItem.Id = _itemRepository.Insert(manipulatingItem);
                        Items.Add(manipulatingItem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while executing the 'Add' command: {ex.Message}");
                }
            });

            Edit = new RelayCommand((object _) =>
            {
                try
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while executing the 'Edit' command: {ex.Message}");
                }
            });

            Remove = new RelayCommand((object _) =>
            {
                try
                {
                    if (SelectedItem != null)
                    {
                        _itemRepository.Delete(SelectedItem);
                        Items.Remove(SelectedItem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while executing the 'Remove' command: {ex.Message}");
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
