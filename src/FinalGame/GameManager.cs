using FinalGame.Crops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Sprite.Extensions;
using MonoGameLibrary.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class GameManager : DrawableGameComponent
    {
        SpriteBatch sb;

        Game g;

        SpriteFont font;

        PlayableCharacter PC;
        GridManager gridManager;
        GardenManager gardenManager;
        ShopManager shopManager;

        IInputHandler Input;

        bool DrawCords;

        float CurrentTime, DayTime, DayDuration;
        int Day;

        List<Hotbar> Hotbar;

        Texture2D InventoryTexture;
        string InventoryTextureName;
        Vector2 InventoryOneLoc, InventoryTwoLoc, InventoryThreeLoc, InventoryFourLoc, InventoryFiveLoc, InventorySixLoc, InventorySevenLoc, InventoryEightLoc, InventoryNineLoc;
        
        public GameManager(Game game) : base(game)
        {
            g = game;
        }

        internal GameManager(Game game, InputHandler input, PlayableCharacter p, GridManager gridM, GardenManager gardenM, ShopManager shopM) : base(game)
        {
            g = game;
            Input = input;
            PC = p;
            gridManager = gridM;
            gardenManager = gardenM;
            shopManager = shopM;

            DrawCords = false;

            InventoryTextureName = "InventorySprite";
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

            LoadInventoryElements();
        }

        int Bottom = 870;
        private void LoadInventoryElements()
        {
            InventoryTexture = this.Game.Content.Load<Texture2D>(InventoryTextureName);

            InventoryOneLoc = new Vector2(300, 870);
            InventoryTwoLoc = new Vector2(425, 870);
            InventoryThreeLoc = new Vector2(550, 870);
            InventoryFourLoc = new Vector2(675, 870);
            InventoryFiveLoc = new Vector2(800, 870);
            InventorySixLoc = new Vector2(925, 870);
            InventorySevenLoc = new Vector2(1050, 870);
            InventoryEightLoc = new Vector2(1175, 870);
            InventoryNineLoc = new Vector2(1300, 870);

            Hotbar = new List<Hotbar> { new Hotbar(InventoryOneLoc, "InventoryOne"), 
                new Hotbar(InventoryTwoLoc,"InventoryTwo"), new Hotbar(InventoryThreeLoc,"InventorThree"), 
                new Hotbar(InventoryFourLoc, "InventoryFour"), new Hotbar(InventoryFiveLoc, "InventoryFive"), 
                new Hotbar(InventorySixLoc, "InventorySix"), new Hotbar(InventorySevenLoc,"InventorySeven"), 
                new Hotbar(InventoryEightLoc,"InventoryEight"), new Hotbar(InventoryNineLoc,"InventoryNine") 
            };
        }

        public override void Update(GameTime gameTime)
        {
            PC.Update(gameTime);
            gridManager.CheckPlayerCollision(PC);
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
            foreach (Plant plant in gardenManager.Garden) { plant.Watered = false; }
            
        }

        public void CheckInteractedSquare()
        {
            foreach (GridSquare gs in gridManager.SoilSquares)
            {
                if (gs.GridState == GridState.Interacted)
                {
                    foreach (Plant p in gardenManager.Garden)
                    {
                        //Water
                        if (p.LocationRect.Intersects(gs.LocationRect) && p.PS == PlantState.Alive)
                        {
                            p.Water();
                        }
                        //Harvest
                        if (p.LocationRect.Intersects(gs.LocationRect) && p.Harvestable == true && p.PS != PlantState.Harvested)
                        {
                            PC.Player.Inventory.Add(p);
                            p.PS = PlantState.Harvested;
                        }

                        gs.GridState = GridState.Free;
                        gardenManager.UpdatePlantState(p);
                    }
                }
            }
        }

        Vector2 TimeLocation = new Vector2(10, 10);
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.DrawString(font, $"Total Time: {(int)CurrentTime} | Day Time: {(int)DayTime} | Day: {Day}                                " +
                $"Click to water plants!                          S: Shop | Money: {PC.Player.gold}", TimeLocation, Color.White);

            if (DrawCords) { DrawGridCords(); }

            foreach(Hotbar hb in Hotbar){ sb.Draw(InventoryTexture, hb.Loc, Color.White); }

            DrawHotbar();

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

        int count;
        Vector2 Center = new Vector2(25, 25);
        public void DrawHotbar()
        {
            foreach (Item item in PC.Player.Inventory)
            {
                count = PC.Player.Inventory.IndexOf(item);
                sb.Draw(item.spriteTexture, Hotbar[count].Loc + Center, Color.White);
            }
        }

    }
}
