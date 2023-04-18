using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace FinalGame
{
    class GridManager : DrawableGameComponent
    {
        public GridSquare[,] GridBoard;

        Texture2D GridTexture;
        Rectangle ScreenSize;

        int SquaresWide;
        int SquaresHeigh;
        int Margin = 1;

        GridTerrain GT;

        //InputHandler input;


        bool GridVisible;

        public GridManager(Game game, Rectangle SS, GridTerrain gT, InputHandler IH) : base(game) 
        {
            this.GridBoard = new GridSquare[17, 9];
            ScreenSize = SS;

            GridVisible = false;

            GT = gT;

            //input = IH;
        }       

        public override void Initialize()
        {
            GridTexture = this.Game.Content.Load<Texture2D>("Grid");

            SquaresWide = ScreenSize.Width / GridTexture.Width;
            SquaresHeigh = ScreenSize.Height / GridTexture.Height;

            LoadGrid();
            base.Initialize();
        }
        
        protected virtual void LoadGrid() { CreateGrid(SquaresWide, SquaresHeigh, Margin); }

        int x = 0;

        //uses multi-dimensional array
        private void CreateGrid(int width, int height, int margin)
        {
            GridSquare gs;

            for (int w = 0; w < GridBoard.GetLength(0); w++)
            {
                for (int h = 0; h < GridBoard.GetLength(1); h++)
                {
                    gs = new GridSquare(this.Game);

                    if (!GridVisible) 
                    {
                        gs.TerrainMode(gs); 
                    }
                    else 
                    {
                        gs.GridMode(gs); 
                    }

                    gs.spriteTexture = GT.ReturnTexture(GT.TerrainGuide[h, w]);

                    gs.Initialize();

                    gs.Cords = new Vector2(w, h);

                    gs.Location = new Vector2(x + (w * (gs.SpriteTexture.Width)), 60 + (h * gs.SpriteTexture.Height + (h * Margin)));

                    GridBoard[w,h] = gs;
                }
            }
        }

        public virtual void CheckPlayerCollision(PlayableCharacter p)
        {
            if (GridVisible)
            {
                foreach (GridSquare gs in GridBoard)
                {
                    if (gs.LocationRect.Intersects(p.PlayerReach)) { gs.Occupied(); }
                    else { gs.Free(); }
                }
            }
        }

        private List<GridSquare> SoilSquares = new List<GridSquare>();
        //public virtual List<GridSquare> ReturnSoilSquares()
        //{
        //    foreach (GridSquare gs in GridBoard)
        //    {
                
        //    }
        //}

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            UpdateSquares(gameTime);
            base.Update(gameTime);
        }

        private void UpdateSquares(GameTime gameTime)
        {
            foreach (var square in GridBoard)
            {
                square.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(var square in this.GridBoard)
            {
                square.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        bool isClicked;
        public void HandleInput(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            isClicked = mouseState.LeftButton == ButtonState.Pressed;

            if (isClicked)
            {
                foreach (GridSquare gs in GridBoard)
                {
                    if (gs.Rectagle.Contains(mousePoint))
                    {
                        gs.Interacted();
                    }
                }
            }
        }

    }
}
