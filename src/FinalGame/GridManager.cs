using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;

namespace FinalGame
{
    class GridManager : DrawableGameComponent
    {
        public List<GridSquare> GridBoard { get; private set; }

        Texture2D GridTexture;
        Rectangle ScreenSize;

        int SquaresWide;
        int SquaresHeigh;
        int Margin = 1;

        List<Terrain> Terrains;
        //read this from text file
        //Grass = 0, Water = 1, Soil = 2, Sand = 3
        int[,] TerrainGuide = new int[9, 17] {
            {0,3,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0},
            {3,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,3,3,0,0,0,0,0,0,0,0,0,0,0,0}, 
            {1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 
            {1,1,3,0,0,2,2,0,2,2,0,2,2,0,2,2,0}, 
            {1,3,0,0,0,2,2,0,2,2,0,2,2,0,2,2,0}, 
            {1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };
        //for this too work I need the grid to always be 17 x 9 the grid builds dynamically on screen size, so how will that affect this?

        public GridManager(Game game, Rectangle SS, List<Terrain> terrains) : base(game) 
        {
            this.GridBoard = new List<GridSquare>();
            ScreenSize = SS;
            Terrains = terrains;
            
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

        private void CreateGrid(int width, int height, int margin)
        {
            GridSquare gs;

            for(int w = 0; w < width; w++)
            {
                for(int h = 0; h < height; h++)
                {
                    gs = new GridSquare(this.Game);
                    gs.Initialize();

                    gs.Cords = new Vector2(w, h);

                    gs.Location = new Vector2(x + (w * (gs.SpriteTexture.Width)), 60 + (h * gs.SpriteTexture.Height + (h * Margin)));

                    GridBoard.Add(gs);
                }
            }
        }

        public virtual void CheckCollision(PlayableCharacter p)
        {
            foreach (GridSquare gs in GridBoard)
            {
                if (gs.LocationRect.Intersects(p.PlayerReach)) { gs.PlayerOnGrid(gs);}
                else { gs.PlayerOffGrid(gs); }
            }
        }

        //Is this the correct location? Should this be in a seprate class?
        public void DrawTerrain() 
        {
            foreach (GridSquare gs in GridBoard)
            {
                gs.spriteTexture = Terrains[0].Texture;
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
