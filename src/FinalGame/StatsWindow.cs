using FinalGame.Crops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class StatsWindow : Window
    {
        private Vector2 titlePosition;
        internal Texture2D itemSquare;

        internal SpriteFont smallFont;

        internal Rectangle object1Loc, object2Loc, object3Loc, object4Loc, object5Loc, object6Loc, object7Loc, object8Loc, object9Loc, object10Loc;
        internal List<Rectangle> objectLocations;

        internal List<Plant> AllPlants;

        public StatsWindow(Game game, List<Plant> plants) : base(game)
        {
            AllPlants = plants;
        }

        public override void Initialize()
        {
            // Set up the sprite batch and load content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            title = Game.Content.Load<SpriteFont>("Title");
            smallFont = Game.Content.Load <SpriteFont>("Small");
            background = Game.Content.Load<Texture2D>("ShopBackground");
            itemSquare = Game.Content.Load<Texture2D>("InventorySprite");

            // Set up the sprite batch and load content
            windowBounds = new Rectangle((GraphicsDevice.Viewport.Width - background.Width) / 2,
                                         (GraphicsDevice.Viewport.Height - background.Height) / 2,
                                         background.Width, background.Height);

            // Calculate the positions of the various elements
            titlePosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 40);

            object1Loc = new Rectangle(380, 300, 100, 100);
            object2Loc = new Rectangle(380, 450, 100, 100);
            object3Loc = new Rectangle(380, 600, 100, 100);
            object4Loc = new Rectangle(630, 300, 100, 100);
            object5Loc = new Rectangle(630, 450, 100, 100);
            object6Loc = new Rectangle(630, 600, 100, 100);
            object7Loc = new Rectangle(850, 300, 100, 100);
            object8Loc = new Rectangle(850, 450, 100, 100);
            object9Loc = new Rectangle(1070, 300, 100, 100);
            object10Loc = new Rectangle(1070, 450, 100, 100);

            objectLocations = new List<Rectangle>() { object1Loc, object2Loc, object3Loc, object4Loc, object5Loc, object6Loc, object7Loc, object8Loc, object9Loc, object10Loc };


            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the background
            spriteBatch.Draw(background, windowBounds, Color.White);

            // Draw the title
            spriteBatch.DrawString(title, "Stats", titlePosition, Color.Black);

            //Draw display squares
            DrawSquares();

            //Draw Plants
            DrawPlants();

            DrawPlantsStats();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawSquares()
        {
            spriteBatch.Draw(itemSquare, object1Loc, Color.White);
            spriteBatch.Draw(itemSquare, object2Loc, Color.White);
            spriteBatch.Draw(itemSquare, object3Loc, Color.White);

            spriteBatch.Draw(itemSquare, object4Loc, Color.White);
            spriteBatch.Draw(itemSquare, object5Loc, Color.White);
            spriteBatch.Draw(itemSquare, object6Loc, Color.White);

            spriteBatch.Draw(itemSquare, object7Loc, Color.White);
            spriteBatch.Draw(itemSquare, object8Loc, Color.White);

            spriteBatch.Draw(itemSquare, object9Loc, Color.White);
            spriteBatch.Draw(itemSquare, object10Loc, Color.White);
        }

        

        public void DrawPlants()
        {
            for (int i =0; i <=9; i++)
            {
                spriteBatch.Draw(AllPlants[i].DaySixTexture, objectLocations[i], Color.White);
            }
        }

        Vector2 TextLoc = new Vector2();
        public void DrawPlantsStats()
        {
            for (int i = 0; i <= 9; i++)
            {
                TextLoc.X = objectLocations[i].X + 110;
                TextLoc.Y = objectLocations[i].Y;
                spriteBatch.DrawString(smallFont, $"{AllPlants[i].Name}\nQuality: {AllPlants[i].ReturnQuality()}", TextLoc, Color.Black);
            }
        }
    }
}
