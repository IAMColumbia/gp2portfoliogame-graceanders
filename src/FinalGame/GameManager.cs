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

        SpriteFont font, small;

        PlayableCharacter PC;
        GridManager gridManager;
        GardenManager gardenManager;
        ShopManager shopManager;
        StatsManager statsManager;

        IInputHandler Input;

        bool DrawCords;

        float CurrentTime, DayTime, DayDuration;
        int Day;

        List<Hotbar> Hotbar;
        Item SelectedItem;

        Texture2D InventoryTexture, SelectedTexture;
        string InventoryTextureName, SelectedTextureName;
        Vector2 InventoryOneLoc, InventoryTwoLoc, InventoryThreeLoc, InventoryFourLoc, InventoryFiveLoc, InventorySixLoc, InventorySevenLoc, InventoryEightLoc, InventoryNineLoc;

        public GameManager(Game game) : base(game) { g = game; }

        internal GameManager(Game game, InputHandler input, PlayableCharacter p, GridManager gridM, GardenManager gardenM, ShopManager shopM, StatsManager statsM) : base(game)
        {
            g = game;
            Input = input;
            PC = p;
            gridManager = gridM;
            gardenManager = gardenM;
            shopManager = shopM;
            statsManager = statsM;

            DrawCords = false;

            InventoryTextureName = "InventorySprite";
            SelectedTextureName = "SelectedSprite";
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
            small = this.Game.Content.Load<SpriteFont>("Small");
            Day = 1;
            DayDuration = 10;

            LoadInventoryElements();
        }

        int Bottom = 870;
        private void LoadInventoryElements()
        {
            InventoryTexture = this.Game.Content.Load<Texture2D>(InventoryTextureName);
            SelectedTexture = this.Game.Content.Load<Texture2D>(SelectedTextureName);

            InventoryOneLoc = new Vector2(300, Bottom); InventoryTwoLoc = new Vector2(425, Bottom); InventoryThreeLoc = new Vector2(550, Bottom);
            InventoryFourLoc = new Vector2(675, Bottom); InventoryFiveLoc = new Vector2(800, Bottom); InventorySixLoc = new Vector2(925, Bottom);
            InventorySevenLoc = new Vector2(1050, Bottom); InventoryEightLoc = new Vector2(1175, Bottom); InventoryNineLoc = new Vector2(1300, Bottom);

            Hotbar = new List<Hotbar> { new Hotbar(InventoryOneLoc, "InventoryOne"), 
                new Hotbar(InventoryTwoLoc,"InventoryTwo"), new Hotbar(InventoryThreeLoc,"InventorThree"), 
                new Hotbar(InventoryFourLoc, "InventoryFour"), new Hotbar(InventoryFiveLoc, "InventoryFive"), 
                new Hotbar(InventorySixLoc, "InventorySix"), new Hotbar(InventorySevenLoc,"InventorySeven"), 
                new Hotbar(InventoryEightLoc,"InventoryEight"), new Hotbar(InventoryNineLoc,"InventoryNine") 
            };

            Hotbar[0].Selected = true;
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
            if (shopManager.IsShopOpen || statsManager.IsStatsOpen)
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
            //Shop Window
            if (!shopManager.IsShopOpen && Input.KeyboardState.WasKeyPressed(Keys.P))
            {
                shopManager.OpenShopWindow();
            }
            else if (shopManager.IsShopOpen && Input.KeyboardState.WasKeyPressed(Keys.P))
            {
                shopManager.CloseShopWindow();
            }

            //Stats Window
            if (!statsManager.IsStatsOpen && Input.KeyboardState.WasKeyPressed(Keys.O))
            {
                statsManager.OpenStatsWindow(ref gardenManager.AllPlants);
            }
            else if (statsManager.IsStatsOpen && Input.KeyboardState.WasKeyPressed(Keys.O))
            {
                statsManager.CloseStatsWindow();
            }

            #region Hotbar Select

            if (Input.KeyboardState.WasKeyPressed(Keys.D1)){
                UnselectHotbar();
                Hotbar[0].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D2)){
                UnselectHotbar();
                Hotbar[1].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D3)){
                UnselectHotbar();
                Hotbar[2].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D4)){
                UnselectHotbar();
                Hotbar[3].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D5)){
                UnselectHotbar();
                Hotbar[4].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D6)){
                UnselectHotbar();
                Hotbar[5].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D7)){
                UnselectHotbar();
                Hotbar[6].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D8)){
                UnselectHotbar();
                Hotbar[7].Selected = true;
            }
            if (Input.KeyboardState.WasKeyPressed(Keys.D9)){
                UnselectHotbar();
                Hotbar[8].Selected = true;
            }
            #endregion
        }

        private void UnselectHotbar() { foreach (Hotbar hb in Hotbar) { hb.Selected = false; } }

        public void NextDay(GameTime gameTime)
        {
            gardenManager.GrowPlants();
            foreach (Plant plant in gardenManager.Garden) { plant.Watered = false; }

            //gardenManager.UpdatePlantQuality();

            shopManager.RandomItems();

        }
        
        int OldPlantIndex, NewPlantIndex;
        bool Planted;
        int i;
        bool Contains;
        public void CheckInteractedSquare()
        {
            foreach (GridSquare gs in gridManager.SoilSquares)
            {
                if (gs.GridState == GridState.Interacted)
                {
                    Planted = false;
                    foreach (Plant p in gardenManager.Garden)
                    {
                        //Water
                        if (p.LocationRect.Intersects(gs.LocationRect) && p.PS == PlantState.Alive)
                        {
                            p.Water();
                        }

                        //Replant
                        if (p.LocationRect.Intersects(gs.LocationRect) && p.DrawColor == Color.Transparent)
                        {
                            if (SelectedItem != null)
                            {
                                if (SelectedItem.ItemType == ItemType.Seed)
                                {
                                    OldPlantIndex = gardenManager.Garden.IndexOf(p);
                                    NewPlantIndex = SelectedItem.ReturnPlantIndex();

                                    if (SelectedItem.Count > 1) { SelectedItem.Count--; }
                                    else { PC.Player.RemoveItem(SelectedItem); }
                                    
                                    Planted = true;
                                }
                            }
                        }

                        //Harvest
                        if (p.LocationRect.Intersects(gs.LocationRect) && p.Harvestable == true && p.PS != PlantState.Harvested)
                        {
                            //Plants Stack
                            //Contains = false;
                            //foreach(Item item in PC.Player.Inventory)
                            //{
                            //    if(item.Name == p.Name) { item.Count++; }
                            //    else { Contains = true; }
                            //}
                            //if (Contains || PC.Player.Inventory.Count == 0) { PC.Player.AddItem(p); }
                            
                            PC.Player.AddItem(p);//If the plants stack then they just default to the first quality. dismisses each plants calculations

                            p.Harvest();
                        }

                        gs.GridState = GridState.Free;
                        gardenManager.UpdatePlantState(p);
                    }

                    if (Planted)
                    {
                        gardenManager.Garden[OldPlantIndex] = gardenManager.NewPlant(NewPlantIndex, g); 

                        gardenManager.ResetPlant(gardenManager.Garden[OldPlantIndex]);
                        gardenManager.UpdatePlantState(gardenManager.Garden[OldPlantIndex]);
                    }

                    Planted = false;
                }
            }
        }

        Vector2 TimeLocation = new Vector2(10, 10);
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.DrawString(font, $"Total Time: {(int)CurrentTime} | Day Time: {(int)DayTime} | Day: {Day}                                " +
                $"Click to water plants!                 O: Stats | P: Shop | Money: {PC.Player.gold}", TimeLocation, Color.White);

            if (DrawCords) { DrawGridCords(); }

            int i;
            foreach(Hotbar hb in Hotbar)
            { 
                sb.Draw(InventoryTexture, hb.Loc, Color.White); 
                if(hb.Selected == true) 
                { 
                    sb.Draw(SelectedTexture, hb.Loc, Color.White);
                    i = Hotbar.IndexOf(hb);
                    if(PC.Player.Inventory.Count >= i + 1){ 
                        SelectedItem = PC.Player.Inventory[i]; 
                    }
                    else { SelectedItem = null; }
                }
            }

            DrawHotbarItems();

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

        int count = 0;
        Vector2 Center = new Vector2(25, 25);
        Vector2 BottomCorner = new Vector2(70, 70);
        public void DrawHotbarItems()
        {
            foreach (Item item in PC.Player.Inventory)
            {
                count = PC.Player.Inventory.IndexOf(item);
                if(item.ItemType == ItemType.Plant) { sb.Draw(item.ItemTexture, Hotbar[count].Loc + Center, Color.White); }
                else 
                { 
                    sb.Draw(item.spriteTexture, Hotbar[count].Loc + Center, Color.White); 
                }

                sb.DrawString(small, $"{item.Count}", Hotbar[count].Loc + BottomCorner, Color.Black);
                    
            }
        }

    }
}
