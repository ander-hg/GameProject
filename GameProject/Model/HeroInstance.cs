using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class HeroInstance : Entity
    {
        private Hero hero;
        private int currentLevel;
        private int currentExperience;
        private List<Item> items;
        private int gold;

        public HeroInstance(Hero hero, int currentLevel, int currentExperience, List<Item> items, int gold)
        {
            this.hero = hero;
            this.currentLevel = currentLevel;
            this.currentExperience = currentExperience;
            this.items = items;
            this.gold = gold;
            this.Targetable = true;
            this.Attackable = true;
        }

        public Hero Hero
        {
            get { return hero; }
            set
            {
                if (hero != value)
                {
                    hero = value;
                    OnPropertyChanged(nameof(Hero));
                }
            }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                if (currentLevel != value)
                {
                    currentLevel = value;
                    OnPropertyChanged(nameof(CurrentLevel));
                }
            }
        }

        public int CurrentExperience
        {
            get { return currentExperience; }
            set
            {
                if (currentExperience != value)
                {
                    currentExperience = value;
                    OnPropertyChanged(nameof(CurrentExperience));
                }
            }
        }

        public List<Item> Items
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public int Gold
        {
            get { return gold; }
            set
            {
                if (gold != value)
                {
                    gold = value;
                    OnPropertyChanged(nameof(Gold));
                }
            }
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            OnPropertyChanged(nameof(Items));
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
            OnPropertyChanged(nameof(Items));
        }

        public void GainExperience(int amount)
        {
            currentExperience += amount;
            OnPropertyChanged(nameof(CurrentExperience));
        }

        public void LevelUp()
        {
            // Lógica para aumentar o nível do herói
            CurrentLevel++;
            OnPropertyChanged(nameof(CurrentLevel));
        }

        public void UseItem(Item item)
        {
            // Lógica para usar um item
        }

        public void Attack(HeroInstance target)
        {
            // Lógica para realizar um ataque ao alvo
        }

        public void updateHeroInstance(HeroInstance instance)
        {
            this.Hero = instance.Hero;
            this.CurrentLevel = instance.CurrentLevel;
            this.CurrentExperience = instance.CurrentExperience;
            this.Items = instance.Items;
            this.Gold = instance.Gold;
            // this.Targetable = instance.Targetable;
            // this.Attackable = instance.Attackable;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
