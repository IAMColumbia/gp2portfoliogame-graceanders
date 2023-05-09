using FinalGame.Crops;
using FinalGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Windows
{
    internal class StatsWindow : Window
    {
        private Vector2 titlePosition;
        internal Texture2D itemSquare;

        internal SpriteFont smallFont;

        internal Rectangle object1Loc, object2Loc, object3Loc, object4Loc, object5Loc, object6Loc, object7Loc, object8Loc, object9Loc, object10Loc;
        internal Vector2 ExcellentAchievedTextLoc;
        internal List<Rectangle> objectLocations;

        GardenManager gardenManager;

        internal Texture2D PoorStar, AcceptableStar, DecentStar, ExcellentStar;
        internal string PoorStarTextureName, AcceptableStarTextureName, DecentStarTextureName, ExcellentStarTextureName;

        public StatsWindow(Game game, GardenManager gm) : base(game)
        {
            gardenManager = gm;
        }

        public override void Initialize()
        {
            // Set up the sprite batch and load content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            title = Game.Content.Load<SpriteFont>("Title");
            smallFont = Game.Content.Load<SpriteFont>("Small");
            background = Game.Content.Load<Texture2D>("ShopBackground");
            itemSquare = Game.Content.Load<Texture2D>("InventorySprite");

            // Set up the sprite batch and load content
            windowBounds = new Rectangle((GraphicsDevice.Viewport.Width - background.Width) / 2,
                                         (GraphicsDevice.Viewport.Height - background.Height) / 2,
                                         background.Width, background.Height);

            // Calculate the positions of the various elements
            titlePosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 40);

            object1Loc = new Rectangle(370, 300, 100, 100);
            object2Loc = new Rectangle(370, 450, 100, 100);
            object3Loc = new Rectangle(370, 600, 100, 100);
            object4Loc = new Rectangle(620, 300, 100, 100);
            object5Loc = new Rectangle(620, 450, 100, 100);
            object6Loc = new Rectangle(620, 600, 100, 100);
            object7Loc = new Rectangle(870, 300, 100, 100);
            object8Loc = new Rectangle(870, 450, 100, 100);
            object9Loc = new Rectangle(1120, 300, 100, 100);
            object10Loc = new Rectangle(1120, 450, 100, 100);

            objectLocations = new List<Rectangle>() { object1Loc, object2Loc, object3Loc, object4Loc, object5Loc, object6Loc, object7Loc, object8Loc, object9Loc, object10Loc };

            ExcellentAchievedTextLoc = new Vector2(900, 600);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            PoorStarTextureName = "Poor_Quality_Icon";
            AcceptableStarTextureName = "Acceptable_Quality_Icon";
            DecentStarTextureName = "Decent_Quality_Icon";
            ExcellentStarTextureName = "Excellent_Quality_Icon";

            PoorStar = Game.Content.Load<Texture2D>(PoorStarTextureName);
            AcceptableStar = Game.Content.Load<Texture2D>(AcceptableStarTextureName); ;
            DecentStar = Game.Content.Load<Texture2D>(DecentStarTextureName);
            ExcellentStar = Game.Content.Load<Texture2D>(ExcellentStarTextureName);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the background
            spriteBatch.Draw(background, windowBounds, Color.White);

            // Draw the title
            spriteBatch.DrawString(title, "Stats                         " +
                "[O] Close", titlePosition, Color.Black);

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


        Rectangle PlantLoc = new Rectangle();
        public void DrawPlants()
        {
            for (int i = 0; i <= 9; i++)
            {
                spriteBatch.Draw(gardenManager.AllPlants[i].DaySixTexture, AdjustPlantRec(i), Color.White);
                if (gardenManager.AllPlants[i].PlantQuality > 0) { DrawQualityStars(i); }
            }
        }

        private Rectangle AdjustPlantRec(int i)
        {
            PlantLoc = objectLocations[i];
            PlantLoc.Width = gardenManager.AllPlants[i].DaySixTexture.Width + 10;
            PlantLoc.Height = gardenManager.AllPlants[i].DaySixTexture.Height + 10;
            PlantLoc.X += 20;

            return PlantLoc;
        }

        private void DrawQualityStars(int i)
        {
            if (gardenManager.AllPlants[i].PlantQuality == Quality.Poor)
            {
                spriteBatch.Draw(PoorStar, objectLocations[i], Color.White);
            }
            if (gardenManager.AllPlants[i].PlantQuality == Quality.Acceptable)
            {
                spriteBatch.Draw(AcceptableStar, objectLocations[i], Color.White);
            }
            if (gardenManager.AllPlants[i].PlantQuality == Quality.Decent)
            {
                spriteBatch.Draw(DecentStar, objectLocations[i], Color.White);
            }
            if (gardenManager.AllPlants[i].PlantQuality == Quality.Excellent)
            {
                spriteBatch.Draw(ExcellentStar, objectLocations[i], Color.White);
            }


        }

        Vector2 TextLoc = new Vector2();
        public void DrawPlantsStats()
        {
            for (int i = 0; i <= 9; i++)
            {
                TextLoc.X = objectLocations[i].X + 110;
                TextLoc.Y = objectLocations[i].Y;
                spriteBatch.DrawString(smallFont, $"{gardenManager.AllPlants[i].Name}\nQuality:\n{gardenManager.AllPlants[i].PlantQuality}", TextLoc, Color.Black);
            }

            spriteBatch.DrawString(font, $"You have achieved:\n{gardenManager.NumOfExcelentPlants()}/10 Excellent Plants", ExcellentAchievedTextLoc, Color.Brown);

        }


    }
}
