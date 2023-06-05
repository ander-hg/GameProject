using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Hero : INotifyPropertyChanged
    {
        private string name;
        private int health;
        private int mana;
        private int attack;
        private int defense;

        public Hero(string name, int health, int mana, int attack, int defense)
        {
            this.name = name;
            this.health = health;
            this.mana = mana;
            this.attack = attack;
            this.defense = defense;
        }

        public Hero() { }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                if (health != value)
                {
                    health = value;
                    OnPropertyChanged(nameof(Health));
                }
            }
        }

        public int Mana
        {
            get { return mana; }
            set
            {
                if (mana != value)
                {
                    mana = value;
                    OnPropertyChanged(nameof(Mana));
                }
            }
        }

        public int Attack
        {
            get { return attack; }
            set
            {
                if (attack != value)
                {
                    attack = value;
                    OnPropertyChanged(nameof(Attack));
                }
            }
        }

        public int Defense
        {
            get { return defense; }
            set
            {
                if (defense != value)
                {
                    defense = value;
                    OnPropertyChanged(nameof(Defense));
                }
            }
        }

        public void updateHero(Hero hero)
        {
            this.Name = hero.Name;
            this.Health = hero.Health;
            this.Mana = hero.Mana;
            this.Attack = hero.Attack;
            this.Defense = hero.defense;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
