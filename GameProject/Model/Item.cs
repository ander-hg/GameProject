using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Item : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private string type;
        private string attribute;
        private int value;
        private int cost;


        public Item(int id, string name, string description, string type, string attribute, int value, int cost)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.attribute = attribute;
            this.value = value;
            this.cost = cost;
        }
        public Item(string name, string description, string type, string attribute, int value, int id, int cost)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.attribute = attribute;
            this.value = value;
            this.cost = cost;
        }

        public Item(Item i)
        {
            this.id = i.id;
            this.name = i.Name;
            this.description = i.Description;
            this.type = i.Type;
            this.attribute = i.Attribute;
            this.value = i.Value;
        }

        public Item() { }

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

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        public string Attribute
        {
            get { return attribute; }
            set
            {
                if (attribute != value)
                {
                    attribute = value;
                    OnPropertyChanged(nameof(Attribute));
                }
            }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public int Cost
        {
            get { return cost; }
            set
            {
                if (this.cost != value)
                {
                    this.cost = value;
                    OnPropertyChanged(nameof(Cost));
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
