﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class HeroInstance : Entity
    {
        private int id;
        private Hero hero;
        private int currentLevel;
        private int currentExperience;
        private List<Item> items;
        private int gold;

        public HeroInstance(Hero hero, int currentLevel = 1, int currentExperience = 0, List<Item> items = null, int gold = 0)
        {
            this.hero = hero;
            this.currentLevel = currentLevel;
            this.currentExperience = currentExperience;
            this.items = items;
            this.gold = gold;
            this.Targetable = true;
            this.Attackable = true;
        }

        public HeroInstance(int id, int currentLevel, int currentExperience, int gold, bool targetable, bool attackable, int heroid)
        {
            this.id = id;
            this.currentLevel = currentLevel;
            this.currentExperience = currentExperience;
            this.gold = gold;
            this.Targetable = true;
            this.Attackable = true;
        }

        public HeroInstance() { }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
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

        public void GainGold(int amount)
        {
            // Check if the addition will result in an overflow
            if (amount > 0 && Gold > int.MaxValue - amount)
            {
                // Handle the overflow case, if necessary
                Gold = int.MaxValue;
            }
            // Check if the subtraction will result in a value below 0
            else if (amount < 0 && Gold < 0 - amount)
            {
                // Handle the case, if necessary
                Gold = 0;
            }
            else
            {
                // Perform the operation normally
                Gold += amount;
            }
            OnPropertyChanged(nameof(Gold));
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
            this.Targetable = instance.Targetable;
            this.Attackable = instance.Attackable;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
