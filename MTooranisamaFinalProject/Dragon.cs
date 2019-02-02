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
    public class Dragon : DrawableGameComponent
    {
        const int STANDFRAMEWIDTH = 191;     //all values from spritesheet.txt
        const int STANDFRAMEHEIGHT = 161;
        const int WALKFRAMEWIDTH = 191;
        const int WALKFRAMEHEIGHT = 161;
        const int JUMPFRAMEWIDTH = 191;
        const int JUMPFRAMEHEIGHT = 161;

        const float SCALE = 0.95f;

        const int STANDFRAME = 0;
        const int FIRSTWALKFRAME = 1;
        const int WALKFRAMES = 3;
        const int JUMPFRAME = 4;

        private int currentFrame = STANDFRAME;

        int currentFrameDelay = 0;
        const int MAXFRAMEDELAY = 3;

        List<Rectangle> dragonFrames;
        SpriteEffects spriteDirection;

        SpriteBatch spriteBatch;
        Texture2D dragonTexture;
        Vector2 position;
        Vector2 velocity;
        PlayGround playGround;
        public static SoundEffect dragonSound; 

        Rectangle dragon;     // containing rectangle
        Rectangle dragonTextureRectangle;  //this is the size of the texture

        // constructor of dragon
        public Dragon(Game game, SpriteBatch spriteBatch, Texture2D dragonTexture, Vector2 position,Vector2 velocity, PlayGround pg) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.dragonTexture = dragonTexture;
            this.position = position;
            this.velocity = velocity;
            this.playGround = pg;
            dragonSound = game.Content.Load<SoundEffect>("HUD/DragonBite");

            dragonTextureRectangle = new Rectangle((int)position.X, (int)position.Y, 191, 161);  //frame
            dragon = new Rectangle((int)position.X, (int)position.Y, (int)(STANDFRAMEWIDTH * SCALE), (int)(STANDFRAMEHEIGHT * SCALE));

            dragonFrames = new List<Rectangle>();

            //add the stop frame
            dragonFrames.Add(new Rectangle(0, 0, STANDFRAMEWIDTH, STANDFRAMEHEIGHT));

            //the fly frames
            dragonFrames.Add(new Rectangle(0, 161, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            dragonFrames.Add(new Rectangle(191, 161, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            dragonFrames.Add(new Rectangle(382, 161, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));

            //the jump frame
            dragonFrames.Add(new Rectangle(382, 322, JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT));

            spriteDirection = SpriteEffects.None;

            velocity = new Vector2(0);
        }

        // draw the dragon
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(dragonTexture,
                   dragon,     //containing rectangle
                   dragonFrames.ElementAt<Rectangle>(currentFrame),     // key frame rectangle
                   Color.White,
                   0f,             //rotation
                   new Vector2(0),     // no change to origin
                   spriteDirection,
                   0f);
            //spriteBatch.DrawRectangle(dragon, Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // update the position and sprite effects and animation
        public override void Update(GameTime gameTime)
        {
            position += velocity;

            if (position.X > 900)
            {
                velocity = -velocity;
                spriteDirection = SpriteEffects.FlipHorizontally;
            }
            if (position.X < 200)
            {
                velocity = -velocity;
                spriteDirection = SpriteEffects.None;
            }
            dragon.X = dragon.X + (int)velocity.X;
            dragon.Y = dragon.Y + (int)velocity.Y;

            position = new Vector2(dragon.X, dragon.Y);

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
            //anim end

            base.Update(gameTime);
        }
        private bool nearlyZero(float f1)
        {
            return (Math.Abs(f1) < float.Epsilon);
        }
        
        // return the rectangle of dragon
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, dragon.Width, dragon.Height);
        }
    }
}