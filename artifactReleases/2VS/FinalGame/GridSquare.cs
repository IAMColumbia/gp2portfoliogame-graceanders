using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace FinalGame
{
    public class GridSquare : DrawableSprite
    {
        protected Square square;

        protected string FreeTextureName, OccupiedTextureName;
        protected Texture2D FreeTexture, OccupiedTexture;

        private SquareState squarestate;
        public SquareState SquareState 
        {
            get { return this.square.SquareState; }
            set { this.square.SquareState = value; }
        }

        public GridSquare(Game game) : base(game)
        {
            this.square = new Square();
            FreeTextureName = "Grid";
            OccupiedTextureName = "GridGreen";

        }

        protected virtual void updateSquareTexture()
        {
            this.squarestate = this.square.SquareState;
            switch (square.SquareState) 
            {
                case SquareState.Free:
                    //this.Visible = true;
                    this.spriteTexture = FreeTexture;
                    break;
                case SquareState.Occupied:
                    //this.Visible = true;
                    this.spriteTexture = OccupiedTexture;
                    break;
            }
        }

        protected override void LoadContent()
        {
            this.FreeTexture = this.Game.Content.Load<Texture2D>(FreeTextureName);
            this.OccupiedTexture = this.Game.Content.Load<Texture2D>(OccupiedTextureName);
            updateSquareTexture();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.updateSquareTexture();
            base.Update(gameTime);
            square.UpdateBlockState();
            
        }

        public void PlayerOnGrid(GridSquare square) { this.square.Occupied(); }
        public void PlayerOffGrid(GridSquare square) { this.square.Free(); }
    }
}
