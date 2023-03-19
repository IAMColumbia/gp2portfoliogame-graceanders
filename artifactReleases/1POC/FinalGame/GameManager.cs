using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class GameManager : DrawableGameComponent
    {
        SpriteBatch sb;

        Game g;

        PlayableCharacter playableCharacter;
        GridManager gridManager;

        public GameManager(Game game) : base(game)
        {
            g = game;
        }

        internal GameManager(Game game, PlayableCharacter p, GridManager gm) : base(game)
        {
            g = game;
            playableCharacter= p;
            gridManager = gm;
        }

        protected override void LoadContent()
        {
            LoadGameElements();
            base.LoadContent();
        }

        private void LoadGameElements()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            playableCharacter.Update(gameTime);
            gridManager.CheckCollision(playableCharacter);

            base.Update(gameTime);
        }


        public void HandleInput(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.End();

            base.Draw(gameTime);
        }

    }
}
