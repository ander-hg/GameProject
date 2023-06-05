using GameProject.DataAccess;
using GameProject.View.InitialWindows;
using GameProject.View.ManipulationWindows;
using System;
using System.Collections.Generic;
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
        public ICommand btnHero { get; private set; }   
        public ICommand btnPlay { get; private set; }
        public ICommand btnItem { get; private set; }

        public MainWindowsVM(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
            IniciaComandos();
        }

        public void IniciaComandos()
        {
            btnItem = new RelayCommand((object _) =>
            {
                ItemWindow tela = new ItemWindow(databaseConnection);
                tela.Show();

            });

            btnPlay = new RelayCommand((object _) =>
            {
                HeroInstanceWindow tela = new HeroInstanceWindow(databaseConnection);
                tela.Show();
            });

            btnHero = new RelayCommand((object _) =>
            {
                HeroWindow tela = new HeroWindow(databaseConnection);
                tela.Show();
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
