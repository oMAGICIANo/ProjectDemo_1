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

        public Monster()
        {
            name = "Unknown";
            type = "None";
            hp = 1;
        }

        public Monster(string name, string type, int hp)
        {
            this.name = name;
            this.type = type;
            this.hp = hp;
        }


    }
}
