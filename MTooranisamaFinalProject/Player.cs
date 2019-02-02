using C3.XNA;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using System;
using Microsoft.Xna.Framework.Audio;

// Final Project
//    
// Majid Tooranisama
// 10/12/2018
//
// Revision history
//  28/11/2018    Created
//  28/11/2018    UI Designed
//  05/12/2018    Bug Fixed
//  09/12/2018    Comments Added

namespace MTooranisamaFinalProject
{
    public class Player : DrawableGameComponent
    {
        const int STANDFRAMEWIDTH = 76;     //all values from spritesheet.txt
        const int STANDFRAMEHEIGHT = 128;
        const int WALKFRAMEWIDTH = 76;
        const int WALKFRAMEHEIGHT = 128;
        const int JUMPFRAMEWIDTH = 68;
        const int JUMPFRAMEHEIGHT = 160;
        const int FALLFRAMEWIDTH = 72;
        const int FALLFRAMEHEIGHT = 119;

        const float SCALE = 0.95f;

        const int STANDFRAME = 0;
        const int FIRSTWALKFRAME = 1;
        const int WALKFRAMES = 3;
        const int JUMPFRAME = 4;
        const int FALLFRAME =7;

        private int currentFrame = STANDFRAME;

        int currentFrameDelay = 0;
        const int MAXFRAMEDELAY = 3;


        List<Rectangle> playerFrames;
        SpriteEffects spriteDirection;

        const float SPEED = 2.3f;
        const float GRAVITY = 0.02f;

        bool isJumping = false;
        bool isGrounded = false;
        const int JUMPPOWER = -13;
        int currentJumpPower = 0;
        const float JUMPSTEP = 1.3f;

        public bool GameOver;    // boolean for ending game

        public Vector2 velocity;
        
        SpriteBatch spriteBatch;
        Texture2D playerTexture;
        public Vector2 location;
        SoundEffect hitSound;
        PlayGround playGround;

        private int score;
        public int Score { get { return score; } }

        Rectangle player;     // containing rectangle
        Rectangle playerTextureRectangle;  //this is the size of the texture        

        //constructor of player
        public Player(Game game, SpriteBatch spriteBatch, Texture2D playerTexture,Vector2 location, SoundEffect hitSound,  PlayGround p, int score) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.playerTexture = playerTexture;
            this.location = location;
            this.hitSound = hitSound;
            this.playGround = p;
            this.score = score;

            playerTextureRectangle = new Rectangle((int)location.X, (int)location.Y, (int)STANDFRAMEWIDTH, (int)STANDFRAMEHEIGHT);  //frame
            player = new Rectangle((int)location.X, (int)location.Y, (int)(STANDFRAMEWIDTH * SCALE), (int)(STANDFRAMEHEIGHT * SCALE));

            playerFrames = new List<Rectangle>();

            //add the stand frame
            playerFrames.Add(new Rectangle(8, 0, STANDFRAMEWIDTH, STANDFRAMEHEIGHT));

            //the walk frames
            playerFrames.Add(new Rectangle(118, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(224, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(8, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));

            //the jump frame
            playerFrames.Add(new Rectangle(119, 163, JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(119, 163, JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(119, 163, JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT));

            //the fall frame
            playerFrames.Add(new Rectangle(432, 203, FALLFRAMEWIDTH, FALLFRAMEHEIGHT));

            spriteDirection = SpriteEffects.None;

            velocity = new Vector2(0);
        }

        // draw the player 
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture,
                   player,     //containing rectangle
                   playerFrames.ElementAt<Rectangle>(currentFrame),     // key frame rectangle
                   Color.White,
                   0f,             //rotation
                   new Vector2(0),     // no change to origin
                   spriteDirection,
                   0f);
            //spriteBatch.DrawRectangle(player, Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // update the position and animation of player and checks collisions
        public override void Update(GameTime gameTime)
        {           
            velocity.X = 0;  //always evaluate from standing

            float deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds;
            velocity.Y += deltaTime * GRAVITY;    // enable/disable gravity
            if (!GameOver)
            {
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                    velocity.X = SPEED;

                if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                    velocity.X = -SPEED;

                //if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
                //    velocity.Y = SPEED;

                //if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                //    velocity.Y = -SPEED;

                if (keyState.IsKeyDown(Keys.Space))
                {
                    if (!isJumping && isGrounded) // not already jumping, and not falling
                    {
                        isJumping = true;
                        isGrounded = false;
                        currentJumpPower = JUMPPOWER;
                    }
                }
                if (isJumping)
                {
                    if (currentJumpPower < 0)
                    {
                        velocity.Y -= JUMPSTEP;
                        currentJumpPower++;
                    }
                    else
                    {
                        isJumping = false; //falling
                        currentFrame = FALLFRAME;
                    }
                }

                //check to make sure there isn't anything in the way 

                // where we plan to go:
                Rectangle proposedPlayer = new Rectangle(player.X + (int)velocity.X,
                                                         player.Y + (int)velocity.Y,
                                                         player.Width,
                                                         player.Height);
                // will plan work?
                Sides collisionSides = proposedPlayer.CheckCollisions(playGround.RigidBodyList);

                if ((collisionSides & Sides.RIGHT) == Sides.RIGHT)
                {
                    if (velocity.X > 0)
                        velocity.X = 0;
                }
                if ((collisionSides & Sides.LEFT) == Sides.LEFT)
                {
                    if (velocity.X < 0)
                        velocity.X = 0;
                }
                if ((collisionSides & Sides.TOP) == Sides.TOP)
                {
                    hitSound.Play();
                    if (velocity.Y < 0)
                        velocity.Y = 0;
                }
                if ((collisionSides & Sides.BOTTOM) == Sides.BOTTOM && (currentJumpPower != JUMPPOWER))
                {
                    velocity.Y = 0;
                    isGrounded = true;
                }
                player.X = player.X + (int)velocity.X;
                player.Y = player.Y + (int)velocity.Y;

                location = new Vector2(player.X, player.Y);

                //anim start
                //walking
                if (velocity.X > 0)          // face the right direction
                {
                    spriteDirection = SpriteEffects.None;
                }
                else if (velocity.X < 0)
                {
                    spriteDirection = SpriteEffects.FlipHorizontally;
                }

                if (isGrounded)      // player on ground
                {
                    if (nearlyZero(velocity.X))         // and not moving
                    {
                        currentFrame = STANDFRAME;
                    }
                    else                                // not not-moving.
                    {
                        currentFrameDelay++;
                        if (currentFrameDelay > MAXFRAMEDELAY)
                        {
                            currentFrameDelay = 0;
                            currentFrame++;
                        }
                        if (currentFrame > WALKFRAMES)
                            currentFrame = FIRSTWALKFRAME;
                    }
                }
                //jumping
                if (isJumping)
                {
                    currentFrame = JUMPFRAME;
                    currentFrameDelay++;
                    if (currentFrameDelay > MAXFRAMEDELAY)
                    {
                        currentFrameDelay = 0;
                        currentFrame++;
                    }
                }
                //anim end
            }                           
            base.Update(gameTime);
        }
        private bool nearlyZero(float f1)
        {
            return (Math.Abs(f1) < float.Epsilon);
        }

        //  returns the rectangle of player
        public Rectangle getBounds()
        {
            return new Rectangle((int)location.X, (int)location.Y, player.Width, player.Height);
        }

        // method for collecting the score if game not over
        public void HitCoins()
        {
            if (!GameOver)
            {
                score += 4000;
            }                    
        }

        // method for losing score if game not over
        public void LosePoints()
        {
            if (!GameOver)
            {
                score -= 10;
                Dragon.dragonSound.Play();
            }
        }
    }
}