using FinalGame.Crops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
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
        GardenManager gardenManager;
        ShopManager shopManager;

        IInputHandler Input;

        bool DrawCords;

        float CurrentTime, DayTime, DayDuration;
        int Day;

        public GameManager(Game game) : base(game)
        {
            g = game;
        }

        internal GameManager(Game game, InputHandler input, PlayableCharacter p, GridManager gridM, GardenManager gardenM, ShopManager shopM) : base(game)
        {
            g = game;
            Input = input;
            playableCharacter = p;
            gridManager = gridM;
            gardenManager = gardenM;
            shopManager = shopM;

            DrawCords = false;
            
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
            DayDuration = 10;
        }

        public override void Update(GameTime gameTime)
        {
            playableCharacter.Update(gameTime);
            gridManager.CheckPlayerCollision(playableCharacter);
            UpdateTime(gameTime);

            CheckInteractedSquare();

            HandleInput();

            base.Update(gameTime);
        }


        public void UpdateTime(GameTime gameTime)
        {
            if (shopManager.IsShopOpen)
            {
                return;
            }

            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            DayTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (DayTime >= DayDuration)
            {
                Day++;
                DayTime = 0;
                NextDay(gameTime);
            }
        }

        public void HandleInput()
        {
            if (!shopManager.IsShopOpen && Input.KeyboardState.WasKeyPressed(Keys.D4))
            {
                shopManager.OpenShopWindow();
            }
            else if (shopManager.IsShopOpen && Input.KeyboardState.WasKeyPressed(Keys.D4))
            {
                shopManager.CloseShopWindow();
            }

        }

        public void NextDay(GameTime gameTime)
        {
            gardenManager.GrowPlants();
        }

        public void CheckInteractedSquare()
        {
            foreach (GridSquare gs in gridManager.GridBoard)
            {
                if (gs.GridState == GridState.Interacted)
                {
                    foreach (Plant p in gardenManager.Garden)
                    {
                        if (p.LocationRect.Intersects(gs.LocationRect) && p.Harvestable == true)
                        {
                            playableCharacter.Player.Inventory.Add(p);
                            p.PS = PlantState.Harvested;
                            gardenManager.UpdatePlantState(p);

                            gs.GridState = GridState.Free;
                        }
                    }

                }
            }
        }

        Vector2 TimeLocation = new Vector2(10, 10);
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.DrawString(font, $"Total Time: {(int)CurrentTime} | Day Time: {(int)DayTime} | Day: {Day}", TimeLocation, Color.White);

            if (DrawCords) { DrawGridCords(); }

            sb.End();

            base.Draw(gameTime);
        }

        public void DrawGridCords() 
        {
            foreach(GridSquare gs in gridManager.GridBoard) 
            {
                sb.DrawString(font, $"({gs.Cords.X},{gs.Cords.Y})", gs.Location, Color.Black);
            }
        }

    }
}
