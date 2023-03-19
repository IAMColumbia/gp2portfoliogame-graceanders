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

        SpriteFont font;

        PlayableCharacter playableCharacter;
        GridManager gridManager;

        float CurrentTime, DayTime, DayDuration;
        int Day;

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
            font = this.Game.Content.Load<SpriteFont>("Arial");
            Day = 1;
            DayDuration = 10;//Going to change to 300 (5 min)
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            playableCharacter.Update(gameTime);
            gridManager.CheckCollision(playableCharacter);
            UpdateTime(gameTime);

            base.Update(gameTime);
        }


        public void UpdateTime(GameTime gameTime) 
        {
            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            DayTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (DayTime >= DayDuration) 
            {
                Day++;
                DayTime = 0;
            }
        }

        public void HandleInput(GameTime gameTime)
        {

        }

        Vector2 TimeLocation = new Vector2(10, 10);
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.DrawString(font, $"Total Time: {(int)CurrentTime} Day Time: {(int)DayTime} Day: {Day}", TimeLocation, Color.White);

            sb.End();

            base.Draw(gameTime);
        }

    }
}
