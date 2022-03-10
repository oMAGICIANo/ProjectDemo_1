using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo_1
{
    class Monster
    {
        private string name;
        private string type;
        private int hp;
        private bool infected;

        public Monster()
        {
            name = "Unknown";
            type = "None";
            hp = 1;
            infected = false;
        }

        public Monster(string name, string type, int hp, bool infected)
        {
            this.name = name;
            this.type = type;
            this.hp = hp;
            this.infected = infected;
        }

        public string Name
        {
            get 
            {
                return this.name;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
        }

        public int HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.hp = value;
            }
        }

        public bool Infected
        {
            get
            {
                return this.infected;
            }
            set
            {
                this.infected = value;
            }
        }

    }
}
