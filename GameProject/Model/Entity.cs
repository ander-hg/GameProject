using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public abstract class Entity : INotifyPropertyChanged
    {
        private bool targetable;
        private bool attackable;

        public bool Targetable
        {
            get { return targetable; }
            set
            {
                if (targetable != value)
                {
                    targetable = value;
                    OnPropertyChanged(nameof(Targetable));
                }
            }
        }

        public bool Attackable
        {
            get { return attackable; }
            set
            {
                if (attackable != value)
                {
                    attackable = value;
                    OnPropertyChanged(nameof(Attackable));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
