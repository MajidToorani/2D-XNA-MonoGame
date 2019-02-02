using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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
    public class Coin : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        public Texture2D coinTexture;
        public Vector2 position;

        Rectangle coinRect;

        List<Rectangle> coinList;
        public List<Rectangle> CoinList { get => coinList; } // list of coins on the playground

        public Coin(Game game, SpriteBatch spriteBatch, Texture2D coinTexture,Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.coinTexture = coinTexture;
            this.position = position;

            coinRect = new Rectangle((int)position.X, (int)position.Y, 50, 50);

            coinList = new List<Rectangle>();
            // position of coins and add to list            
            coinList.Add(new Rectangle(420, 660, 80, 80));
            coinList.Add(new Rectangle(400, 500, 80, 80));
            coinList.Add(new Rectangle(700, 660, 80, 80));
            coinList.Add(new Rectangle(10, 120, 80, 80));
            coinList.Add(new Rectangle(900, 120, 80, 80));
        }

        // Draw each coin in each rectangle of list of rectangle 
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();         
            for(int i = 0; i < coinList.Count; i++)
            {
                //spriteBatch.DrawRectangle(coinList[i], Color.White);
                spriteBatch.Draw(coinTexture, coinList[i], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Update the position of coins
        public override void Update(GameTime gameTime)
        {
            foreach (Rectangle r in coinList)
            {
                position = new Vector2(r.X, r.Y);
            }
          
            base.Update(gameTime);
        }

        // return the rectangle of coins
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, coinRect.Width, coinRect.Height);
        } 
    }
}
