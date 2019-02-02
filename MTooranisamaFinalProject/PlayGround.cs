using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
{
    public class PlayGround : DrawableGameComponent
    {
        SpriteBatch spriteBatch;        
        SpriteFont spriteFont;
        Texture2D playgroundTexture;
        Texture2D rigidTexture;
        Rectangle playGroundRect;
        Vector2 position1;
        Vector2 position2;
        Vector2 speed;
        Song playSong;

        int mouseX = 0;
        int mouseY = 0;

        // list of rigid bodies
        List<Rectangle> rigidBodyList;
        public List<Rectangle> RigidBodyList { get => rigidBodyList; }

        // constructor of PlayGround
        public PlayGround(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D playgroundTexture, Texture2D rigidTexture, Vector2 position, Rectangle playGroundRect, Vector2 speed, Song playSong) : base(game)
        {
            this.spriteBatch = spriteBatch;            
            this.spriteFont = spriteFont;
            this.playgroundTexture = playgroundTexture;
            this.rigidTexture = rigidTexture;
            this.playGroundRect = playGroundRect;
            this.position1 = position;
            this.position2 = new Vector2(position.X + playgroundTexture.Width, position.Y);
            this.speed = speed;
            this.playSong = playSong;

            playSong = game.Content.Load<Song>("HUD/Forest"); 

            rigidBodyList = new List<Rectangle>();
            // position of rigid bodies and add to list
            rigidBodyList.Add(new Rectangle(0, 740, 1024, 20));
            rigidBodyList.Add(new Rectangle(200, 640, 20, 100));
            rigidBodyList.Add(new Rectangle(500, 680, 20, 60));
            rigidBodyList.Add(new Rectangle(-20, 0, 5, 768));
            rigidBodyList.Add(new Rectangle(300, 570, 250, 20));
            rigidBodyList.Add(new Rectangle(1040, 0, 5, 768));
            rigidBodyList.Add(new Rectangle(600, 380, 200, 20));
            rigidBodyList.Add(new Rectangle(800, 200, 200, 20));
            rigidBodyList.Add(new Rectangle(10, 200, 100, 20));
            rigidBodyList.Add(new Rectangle(200, 350, 150, 20));
            rigidBodyList.Add(new Rectangle(50, 475, 100, 20));
        }

        //Draw all playground components
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playgroundTexture, position1, playGroundRect, Color.White);
            spriteBatch.Draw(playgroundTexture, position2, playGroundRect, Color.White);

            for (int i = 1; i < rigidBodyList.Count; i++)
            {
                spriteBatch.Draw(rigidTexture, rigidBodyList[i], Color.White);
                //spriteBatch.DrawRectangle(rigidBodyList[i], Color.White);
            }
            spriteBatch.DrawString(spriteFont, mouseX.ToString() + ", " + mouseY.ToString(), new Vector2(0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Update mouse position
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();

            mouseX = ms.X;
            mouseY = ms.Y;

            position1 -= speed;
            position2 -= speed;
            if (position1.X < -playgroundTexture.Width)
            {
                position1.X = position2.X + playgroundTexture.Width;
            }
            if (position2.X < -playgroundTexture.Width)
            {
                position2.X = position1.X + playgroundTexture.Width;
            }
            base.Update(gameTime);
        }
    }
}