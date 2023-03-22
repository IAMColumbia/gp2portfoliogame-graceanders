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

        public GridManager(Game game, Rectangle SS) : base(game) 
        {
            this.GridBoard = new List<GridSquare>();

            ScreenSize = SS;
            
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
