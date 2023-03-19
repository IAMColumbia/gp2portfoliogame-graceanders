using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.GameComponents.Player;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace FinalGame
{
    internal class PlayableCharacter : Character
    {
        public IPlayerController Contoller { get; set; }

        //internal GameConsolePlayer Player { get; private set; }
        internal GameConsolePlayer Player { get; private set; }

        protected PlayerState playerState;

        public Rectangle PlayerReach;

        Rectangle TopLeftReach, TopRightReach, BottomLeftReach, BottomRightReach;

        public PlayerState PlayerState
        {
            get { return this.playerState; }
            //Change pacman state also
            set
            {
                if (this.playerState != value)
                {
                    this.playerState = this.Player.State = value; //change PacMan state that is encasulated
                    this.playerStateChanged();
                }
            }
        }

        /// <summary>
        /// Hook to allow child classes to recieve state change calls
        /// </summary>
        protected virtual void playerStateChanged()
        {
            //nothing yet
        }


        public PlayableCharacter(Game game) : base(game) 
        {
            SetupIPlayerController(game);
            Player = new GameConsolePlayer((GameConsole)game.Services.GetService<IGameConsole>());
        }

        protected virtual void SetupIPlayerController(Game game)
        {
            this.Contoller = new PlayerController(game);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("FrontSprite");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            //this.Scale = .40f;
            this.Location = new Vector2(200, 700);
            this.Speed = 150;

            this.showMarkers= true;
            //this.locationRect = CalculateBoundingRectangle(new Rectangle(0, 0, (int)(this.spriteTexture.Width * Scale), (int)(this.spriteTexture.Height * Scale)), spriteTransform);

        }

        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            UpdatePlayerWithControler(gameTime, time);
            UpdatedSpriteDirectionByKey();

            UpdateKeepPlayerOnScreen();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
#if DEBUG
            DrawPlayerReach(sb);
#endif
            base.Draw(sb);
        }

        protected virtual void UpdatedSpriteDirectionByKey()
        {
            if (Contoller.PlayerFacingState == PlayerFacingState.Front)
            {
                this.SpriteTexture = this.Game.Content.Load<Texture2D>("FrontSprite");
            }
            if (Contoller.PlayerFacingState == PlayerFacingState.Back)
            {
                this.SpriteTexture = this.Game.Content.Load<Texture2D>("BackSprite");
            }
            if (Contoller.PlayerFacingState == PlayerFacingState.Right)
            {
                this.SpriteTexture = this.Game.Content.Load<Texture2D>("RightSprite");
            }
            if (Contoller.PlayerFacingState == PlayerFacingState.Left)
            {
                this.SpriteTexture = this.Game.Content.Load<Texture2D>("LeftSprite");
            }
        }

        int Margin = 20;
        public void DrawPlayerReach(SpriteBatch sb)
        {
            //Rect Top Left
            Rectangle TopLeftReach = new Rectangle(this.locationRect.Left - Margin, this.locationRect.Top + Margin * 7, SpriteMarkersTexture.Width, SpriteMarkersTexture.Height);
            sb.Draw(this.SpriteMarkersTexture, TopLeftReach, Color.Blue);
            //Rect Top Right
            Rectangle TopRightReach = new Rectangle(this.locationRect.Right + Margin, this.locationRect.Top + Margin * 7, SpriteMarkersTexture.Width, SpriteMarkersTexture.Height);
            sb.Draw(this.SpriteMarkersTexture, TopRightReach, Color.Blue);
            //Rect Bottom Left
            Rectangle BottomLeftReach = new Rectangle(this.locationRect.Left - Margin, this.locationRect.Bottom + Margin * 4, SpriteMarkersTexture.Width, SpriteMarkersTexture.Height);
            sb.Draw(this.SpriteMarkersTexture, BottomLeftReach, Color.Blue);
            //Rect Bottom Right
            Rectangle BottomRightReach = new Rectangle(this.locationRect.Right + Margin, this.locationRect.Bottom + Margin * 4, SpriteMarkersTexture.Width, SpriteMarkersTexture.Height);
            sb.Draw(this.SpriteMarkersTexture, BottomRightReach, Color.Blue);


            UpdateReachRectangle(TopRightReach, TopLeftReach, BottomRightReach);

        }

        public void UpdateReachRectangle(Rectangle TopRightReach, Rectangle TopLeftReach, Rectangle BottomRightReach)
        {
            this.PlayerReach = CalculateBoundingRectangle(new Rectangle(0, Margin * 7, (int)TopRightReach.X - (int)TopLeftReach.X, (int)BottomRightReach.Y - (int)TopRightReach.Y), spriteTransform);
        }

        protected virtual void UpdatePlayerWithControler(GameTime gameTime, float time)
        {
            this.Contoller.Update(gameTime);

            this.Location += ((this.Contoller.Direction * (time / 1000)) * Speed);

            this.LocationRect = CalculateBoundingRectangle(new Rectangle(0, 200, (int)TopRightReach.X - (int)TopLeftReach.X, (int)BottomRightReach.Y - (int)TopRightReach.Y), spriteTransform);

            //Change state based on movement
        }

        protected void UpdateKeepPlayerOnScreen()
        {
            if (this.Location.X > Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width * .40f / 4))
            {
                this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width * .40f / 4);
            }
            if (this.Location.X < (this.spriteTexture.Width * .40f / 4))
                this.Location.X = (this.spriteTexture.Width * .40f / 4);

            if (this.Location.Y > Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2))
                this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2);

            if (this.Location.Y < -10)
                this.Location.Y = -10;
        }

    }
}
