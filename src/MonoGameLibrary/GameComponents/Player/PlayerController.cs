using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.GameComponents.Player
{
    public enum PlayerFacingState { Front, Back, Right, Left  }
    public interface IPlayerController
    {
        PlayerFacingState PlayerFacingState { get; set; }

        Vector2 Direction { get;  }
        float Rotate { get; }
        IInputHandler Input { get; }

        Keys UpKey { get; set; }
        Keys DownKey { get; set; }
        Keys LeftKey { get; set; }
        Keys RightKey { get; set; }

        void Update(GameTime gameTime);

        bool HasInputForMoverment { get; }
    }

    public class PlayerController : Microsoft.Xna.Framework.GameComponent, IPlayerController
    {
        protected PlayerFacingState playerFacingState;

        PlayerFacingState IPlayerController.PlayerFacingState
        {
            get { return this.playerFacingState; }
            set { this.playerFacingState = value; }
        }

        public Keys upKey, downKey, leftKey, rightKey;

        Keys IPlayerController.UpKey 
        { get { return this.upKey; } set { this.upKey = value; } }
        Keys IPlayerController.DownKey
        { get { return this.downKey; } set { this.downKey = value; } }
        Keys IPlayerController.LeftKey
        { get { return this.leftKey; } set { this.leftKey = value; } }
        Keys IPlayerController.RightKey
        { get { return this.rightKey; } set { this.rightKey = value; } }


        public Vector2 StickDir;
        public Vector2 DPadDir;
        public Vector2 KeyDir;
        public Vector2 Direction { get; protected set; }
        public float Rotate { get; protected set; }

        private float rotationAngle;
        private float gamePadRotationAngle;
        private float dPadRotationAngleKey;

        KeyboardState keyboardState;
        GamePadState gamePad1State;

        //Player controller depends on MonogaemLibrary.Util.InputHandler
        protected IInputHandler input;
        public IInputHandler Input
        {
            get { return input; }
        }

        //Checks to see if there is any movement
        public bool HasInputForMoverment
        {
            get
            {
                if (Direction.Length() == 0) return false;
                return true;
            }
        }

        
        public PlayerController(Game game) : base(game)
        {
            this.Rotate = 0;
            this.StickDir = Vector2.Zero;
            SetupIInputHander(game);
        }

        protected virtual void SetupIInputHander(Game game)
        {
            //get input from game service
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            if (input == null) //failed to get input
            {
                //add inputHandler if it hasn't been added to the game yet
                input = new InputHandler(game);
                game.Components.Add((InputHandler)input);
            }
        }

        /// <summary>
        /// Update the current frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            HandleGamePad();    //Get input from gampads

            HandleKeyboard();   //Get input from keyboard
            
            #region SumAllInput
            this.Direction = this.KeyDir + this.DPadDir + this.StickDir;
            if (this.Direction.Length() > 0)
            {
                this.Direction = Vector2.Normalize(this.Direction);

                //calculate angle in radians
                rotationAngle = (float)Math.Atan2(
                        this.Direction.X,
                        this.Direction.Y * -1);

                //This converts angle back to degree and uses art facing left as 0 degreees
                //Art that start sfacing left = rotationAngle - (float)(Math.PI / 2)
                //right = 
                //TODO add rotations in radians
                this.Rotate = (float)MathHelper.ToDegrees(rotationAngle - (float)(Math.PI / 2));
            }
            #endregion

            

            base.Update(gameTime);
        }

        //Vector2 touchDir;

        

        private void HandleKeyboard()
        {
            //Update for input from Keyboard
#if !XBOX360
            #region KeyBoard
            keyboardState = Keyboard.GetState(); //Keyboard is static no need to get it from input

            KeyDir = new Vector2(0, 0);

            if (keyboardState.IsKeyDown(leftKey))
            {
                KeyDir += new Vector2(-1, 0);
                this.playerFacingState = PlayerFacingState.Left;
            }
            if (keyboardState.IsKeyDown(rightKey))
            {
                KeyDir += new Vector2(1, 0);
                this.playerFacingState = PlayerFacingState.Right;
            }
            if (keyboardState.IsKeyDown(upKey))
            {
                KeyDir += new Vector2(0, -1);
                this.playerFacingState = PlayerFacingState.Back;
            }
            if (keyboardState.IsKeyDown(downKey))
            {
                KeyDir += new Vector2(0, 1);
                this.playerFacingState = PlayerFacingState.Front;
            }
            if (KeyDir.Length() > 0)
            {
                //Normalize NewDir to keep agled movement at same speed as horilontal/Vert
                KeyDir = Vector2.Normalize(KeyDir);
            }
            #endregion
#endif
        }

        private void HandleGamePad()
        {
            HandleLeftThumbStick();

            HandleDPad();
        }

        private void HandleDPad()
        {
            //Update for input from DPad
            #region DPad
            DPadDir = Vector2.Zero;
            if (gamePad1State.DPad.Left == ButtonState.Pressed)
            {
                //Orginal Position is Right so flip X
                DPadDir += new Vector2(-1, 0);
            }
            if (gamePad1State.DPad.Right == ButtonState.Pressed)
            {
                //Original Position is Right
                DPadDir += new Vector2(1, 0);
            }
            if (gamePad1State.DPad.Up == ButtonState.Pressed)
            {
                //Up
                DPadDir += new Vector2(0, -1);
            }
            if (gamePad1State.DPad.Down == ButtonState.Pressed)
            {
                //Down
                DPadDir += new Vector2(0, 1);
            }
            if (DPadDir.Length() > 0)
            {

                //Normalize NewDir to keep agled movement at same speed as horilontal/Vert
                DPadDir = Vector2.Normalize(DPadDir);

                //Angle in radians from vector
                dPadRotationAngleKey = (float)Math.Atan2(
                        DPadDir.X,
                        DPadDir.Y * -1);
            }
            #endregion
        }

        private void HandleLeftThumbStick()
        {
            gamePad1State = input.GamePads[0];
            //Input for update from analog stick
            #region LeftStick
            StickDir = Vector2.Zero;
            if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            {
                StickDir = gamePad1State.ThumbSticks.Left;
                StickDir.Y *= -1;      //Invert Y Axis

                //this is private and calculating will slow processor down may not want to do this for all input
                gamePadRotationAngle = (float)Math.Atan2(
                    gamePad1State.ThumbSticks.Left.X,
                    gamePad1State.ThumbSticks.Left.Y);
            }
            #endregion
        }
    }
}
