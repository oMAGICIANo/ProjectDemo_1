using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo_1
{
    class Monster
    {
        private string name;
        private string color;
        private int hp;

        public Monster()
        {
            name = "Unknown";
            color = "1";
            hp = 1;
        }

        public Monster(string name, string color, int hp)
        {
            this.name = name;
            this.color = color;
            this.hp = hp;
        }


    }
}
