using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Windows
{
    public class Window : DrawableGameComponent
    {
        internal SpriteBatch spriteBatch;
        internal SpriteFont font, title;
        internal Texture2D background;
        internal Rectangle windowBounds;

        public Window(Game game) : base(game) { }
    }
}
