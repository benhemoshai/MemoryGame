using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class MemoryGameUI
    { 
        public void startGame()
        {
            GameLogic game = new GameLogic();
            game.mainLoop();
        }

    }

    
}
