using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using C3.XNA;
using Microsoft.Xna.Framework.Media;

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
{   //A class for drawing menu background texture and components
    class Background : DrawableGameComponent
    {       
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        SpriteFont spriteFont1, spriteFont2;
        
        int mouseX = 0; 
        int mouseY = 0;        

        // constructor of background
        public Background(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont1, SpriteFont spriteFont2, Texture2D backgroundTexture) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.backgroundTexture = backgroundTexture;
            this.spriteFont1 = spriteFont1;
            this.spriteFont2 = spriteFont2;
        }  
        
        //Draw all background components
        public override void Draw(GameTime gameTime)
        {   
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1024, 768), Color.White);

            spriteBatch.DrawString(spriteFont2, mouseX.ToString() + ", " + mouseY.ToString(), new Vector2(0), Color.White);
            spriteBatch.DrawString(spriteFont1, "Teddy in the Forest", new Vector2(280,330), Color.White);
            spriteBatch.DrawString(spriteFont2, "Design and Programming: \nMajid Tooranisama - 2018 \nProfessor: \nSteve Hendrikse", new Vector2(730, 670), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Update mouse position
        public override void Update(GameTime gameTime)
        {           
            MouseState ms = Mouse.GetState();

            mouseX = ms.X;
            mouseY = ms.Y;

            base.Update(gameTime);
        }
    }
}