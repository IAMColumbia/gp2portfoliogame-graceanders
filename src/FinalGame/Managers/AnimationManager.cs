using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Managers
{
    internal class AnimationManager : DrawableGameComponent
    {
        Texture2D WateringSprite;
        Rectangle WateringSprite1, WateringSprite2, WateringSprite3, WateringSprite4, WateringSprite5, WateringSprite6,
            WateringSprite7, WateringSprite8, WateringSprite9, WateringSprite10, WateringSprite11;
        List<Rectangle> WateringSprites;

        internal bool Watering;

        public AnimationManager(Game game) : base(game)
        {
            Watering = false;
        }

        protected override void LoadContent()
        {
            WateringSprite = Game.Content.Load<Texture2D>("WateringSprites");
            WateringSprite1 = new Rectangle(0, 0, 100, 90);
            WateringSprite2 = new Rectangle(100, 0, 100, 90);
            WateringSprite3 = new Rectangle(200, 0, 100, 90);
            WateringSprite4 = new Rectangle(0, 90, 100, 90);
            WateringSprite5 = new Rectangle(100, 90, 100, 90);
            WateringSprite6 = new Rectangle(200, 90, 100, 90);
            WateringSprite7 = new Rectangle(0, 180, 100, 90);
            WateringSprite8 = new Rectangle(100, 180, 100, 90);
            WateringSprite9 = new Rectangle(200, 180, 100, 90);
            WateringSprite10 = new Rectangle(0, 270, 100, 90);
            WateringSprite11 = new Rectangle(100, 270, 100, 90);

            WateringSprites = new List<Rectangle> { WateringSprite1, WateringSprite2, WateringSprite3, WateringSprite4, WateringSprite5, WateringSprite6,
                WateringSprite7, WateringSprite8, WateringSprite9, WateringSprite10, WateringSprite11 };
            base.LoadContent();
        }

        float timer = 0;
        int threshold = 80;
        internal byte currentAnimationIndex = 0;
        internal Vector2 Location;
        internal Vector2 WateringLocation = new Vector2(-150, -150);
        internal void WaterAnimation(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(WateringSprite, Location, WateringSprites[currentAnimationIndex], Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0);

            if (timer > threshold)
            {
                if (currentAnimationIndex >= WateringSprites.Count - 1)
                {
                    Watering = false;
                    currentAnimationIndex = 0;
                }
                else { currentAnimationIndex++; }

                timer = 0;
            }
            else { timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; }
        }


    }
}
