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
        private int hpMax;
        private int power;
        private int speed;

        public Monster()
        {
            name = "Unknown";
            type = "None";
            hp = 1;
            infected = false;
            hpMax = 1;
            power = 1;
            speed = 1;
        }

        public Monster(string name, string type, int hp, bool infected, int power)
        {
            this.name = name;
            this.type = type;
            this.hp = hp;
            this.hpMax = hp;
            this.infected = infected;
            this.power = power;

            if (type == "Doma")
            {
                this.speed = 3;
            }
            else if (type == "Giant")
            {
                this.speed = 1;
            }
            else 
            {
                this.speed = 5;    
            }
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

        public int HP_Max
        {
            get
            {
                return this.hpMax;
            }
        }

        public int Power
        {
            get
            {
                return this.power;
            }
        }

        public int Speed
        {
            get
            {
                return this.speed;
            }
        }
    }
}
