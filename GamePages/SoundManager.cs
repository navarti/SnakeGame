using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using SnakeGame.Properties;

namespace SnakeGame.GamePages
{
    public class SoundManager
    {
        bool play = Settings.Default.Sound;
        
        SoundPlayer gameOver_sp = new SoundPlayer();
        SoundPlayer eat_sp = new SoundPlayer();

        public SoundManager()
        {
            gameOver_sp.Stream = SnakeGame.Properties.Resources.GameOver;
            eat_sp.Stream = SnakeGame.Properties.Resources.Eat;
        }

        public void PlayGameOverSound() 
        {
            if(play)
                gameOver_sp.Play();
        }

        public void PlayEatSound()
        {
            if(play)
                eat_sp.Play();
        }

    }
}
