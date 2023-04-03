﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        bool GridVisible;

        public GridManager(Game game, Rectangle SS, GridTerrain gT) : base(game) 
        {
            //this.GridBoard = new List<GridSquare>();
            this.GridBoard = new GridSquare[17, 9];
            ScreenSize = SS;

            GridVisible = false;

            GT = gT;
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

            //[Rows, Columns]


            for (int w = 0; w < GridBoard.GetLength(0); w++)
            {
                for (int h = 0; h < GridBoard.GetLength(1); h++)
                {
                    gs = new GridSquare(this.Game);

                    if (!GridVisible) { gs.TerrainMode(gs); }
                    gs.spriteTexture = GT.ReturnTexture(GT.TerrainGuide[h, w]);

                    gs.Initialize();

                    gs.Cords = new Vector2(w, h);

                    gs.Location = new Vector2(x + (w * (gs.SpriteTexture.Width)), 60 + (h * gs.SpriteTexture.Height + (h * Margin)));

                    GridBoard[w,h] = gs;
                }
            }
        }

        public virtual void CheckCollision(PlayableCharacter p)
        {
            if (GridVisible)
            {
                foreach (GridSquare gs in GridBoard)
                {
                    if (gs.LocationRect.Intersects(p.PlayerReach)) { gs.PlayerOnGrid(gs); }
                    else { gs.PlayerOffGrid(gs); }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
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

    }
}
