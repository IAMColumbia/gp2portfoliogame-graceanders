using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace FinalGame
{
    public enum GridState { Occupied, Free, Interacted }

    public class GridSquare : DrawableSprite
    {
        protected Square square;

        protected string FreeTextureName, OccupiedTextureName, InteractedTextureName;
        protected Texture2D FreeTexture, OccupiedTexture, InteractedTexture;

        public Vector2 Cords;
        
        //Manages whether the square square is showing terrain or grid
        private SquareState squarestate;
        public SquareState SquareState 
        {
            get { return squarestate; }
            set { this.squarestate = value; }
        }

        //If the SquareState is "Grid" this manages whether the grid is Occupied, Free, or Interacted
        private GridState gridstate;
        public GridState GridState
        {
            get { return this.gridstate; }
            set { this.gridstate = value; }
        }

        public GridSquare(Game game) : base(game)
        {
            this.square = new Square();
            FreeTextureName = "Grid";
            OccupiedTextureName = "GridGreen";
            InteractedTextureName = "GridRed";

            this.GridState = GridState.Free;

        }

        protected virtual void updateGridTexture()
        {
            this.gridstate = this.GridState;
            switch (GridState)
            {
                case GridState.Free:
                    //this.Visible = true;
                    this.spriteTexture = FreeTexture;
                    break;
                case GridState.Occupied:
                    //this.Visible = true;
                    this.spriteTexture = OccupiedTexture;
                    break;
                case GridState.Interacted:
                    this.spriteTexture = InteractedTexture;
                    break;
            }
        }

        protected virtual void UpdateSquareTexture()
        {
            this.squarestate = this.SquareState;
            switch (SquareState) 
            {
                case SquareState.Grid:
                    updateGridTexture();
                    break;
                case SquareState.Terrain:
                    break;
            }

        }

        protected override void LoadContent()
        {
            this.FreeTexture = this.Game.Content.Load<Texture2D>(FreeTextureName);
            this.OccupiedTexture = this.Game.Content.Load<Texture2D>(OccupiedTextureName);
            this.InteractedTexture = this.Game.Content.Load<Texture2D>(InteractedTextureName);

            UpdateSquareTexture();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateSquareTexture();

            base.Update(gameTime);
        }

        public void TerrainMode(GridSquare s) { this.SquareState = SquareState.Terrain; }
        public void GridMode(GridSquare s) { this.SquareState = SquareState.Grid; }


        public virtual void Occupied() { this.GridState = GridState.Occupied; }
        public virtual void Free() { this.GridState = GridState.Free; }
        public virtual void Interacted() { this.GridState = GridState.Interacted; }
    }
}
