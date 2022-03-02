using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo_1
{
    class Player
    {
        private string name;
        private int hp;

        public Player()
        {
            name = "Unknown";
            hp = 1;
        }

        public Player(string name, int hp)
        {
            this.name = name;
            this.hp = hp;
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
    }
}
