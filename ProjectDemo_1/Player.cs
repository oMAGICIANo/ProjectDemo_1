using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo_1
{
    class Player
    {
        private string name;
        private int hp;
        private int score;

        public Player()
        {
            name = "Unknown";
            hp = 1;
            score = 0;
        }

        public Player(string name, int hp, int score)
        {
            this.name = name;
            this.hp = hp;
            this.score = score;
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

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int Score
        {
            get
            {
                return this.score;            
            }
            set 
            {
                this.score = value;
            }
        }
    }
}
