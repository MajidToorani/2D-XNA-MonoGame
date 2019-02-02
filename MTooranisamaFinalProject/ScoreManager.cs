using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using PROG2370CollisionLibrary;
using C3.XNA;

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
    public class ScoreManager : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont1;
        SpriteFont spriteFont2;

        Player player;
        Coin coin;
        Dragon dragon;
        TwinHeadedDragon twinHeaded;

        private int _score;
        private HighScore _highScore;

        SoundEffect scoreSound;        
        SoundEffect laughSound;

        Rectangle proposedPlayer;
        
        // constructor of ScoreManager
        public ScoreManager(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont1,
            SpriteFont spriteFont2,
            Player player,
            Coin coin,
            Dragon dragon,
            TwinHeadedDragon twinHeaded,
            SoundEffect scoreSound,
            SoundEffect laughSound) : base(game)
        {
            
            this.spriteBatch = spriteBatch;
            this.spriteFont1 = spriteFont1;
            this.spriteFont2 = spriteFont2;
            this.player = player;
            this.coin = coin;
            this.dragon = dragon;
            this.twinHeaded = twinHeaded;
            this.scoreSound = scoreSound;
            this.laughSound = laughSound;

            _highScore = HighScore.Load();
        }
        // Draw score and rectangle surrounding the score
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont1, "Score : " + player.Score.ToString(), new Vector2(15, 226), Color.White);
            spriteBatch.DrawRectangle(new Rectangle(10, 220, 200, 30), Color.White,5);
            // Render text
            string text = string.Empty;

            if (player.Score >= 20000)
            {
                text = "You won ! Press R to restart";
                player.GameOver = true;
            }
            else if (player.Score > 0 && player.Score <= 5000)
            {
                text = "You are at half health !";
            }
            else if (player.Score <= 0)
            {
                text = "You died ! Press R to restart";
                player.GameOver = true;
            }
            Vector2 textMeasure = spriteFont2.MeasureString(text);
            Vector2 textPosition = new Vector2((GraphicsDevice.Viewport.Width - textMeasure.X) / 2,
                (GraphicsDevice.Viewport.Height - textMeasure.Y) / 2 + 20);
            spriteBatch.DrawString(spriteFont2, text, textPosition, Color.Red);
            spriteBatch.DrawString(spriteFont2, "HighScores:\n" + string.Join("\n", _highScore.HighScores.Select(c => c.Value).ToArray()), new Vector2(400, 50), Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        //update the rectangles of components and checks the collisions
        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.getBounds();
            Rectangle dragonRect = dragon.getBounds();
            Rectangle twinRect = twinHeaded.getBounds();

            proposedPlayer = new Rectangle(playerRect.X,
                                           playerRect.Y,
                                           playerRect.Width,
                                           playerRect.Height);
            
            Rectangle proposedDragon = new Rectangle(dragonRect.X,
                                                     dragonRect.Y,
                                                     dragonRect.Width,
                                                     dragonRect.Height);
            Sides collisionSides1 = proposedPlayer.CheckCollisions(proposedDragon);

            if ((collisionSides1 & Sides.RIGHT) == Sides.RIGHT ||
                (collisionSides1 & Sides.LEFT) == Sides.LEFT ||
                (collisionSides1 & Sides.BOTTOM) == Sides.BOTTOM ||
                (collisionSides1 & Sides.TOP) == Sides.TOP)
            {
                player.LosePoints(); //calls the method for losing score
            }

            Rectangle twinDragon = new Rectangle(twinRect.X,
                                                 twinRect.Y,
                                                 twinRect.Width,
                                                 twinRect.Height);
            Sides collisionSides2 = proposedPlayer.CheckCollisions(twinDragon);

            if ((collisionSides2 & Sides.RIGHT) == Sides.RIGHT ||
                (collisionSides2 & Sides.LEFT) == Sides.LEFT ||
                (collisionSides2 & Sides.BOTTOM) == Sides.BOTTOM ||
                (collisionSides2 & Sides.TOP) == Sides.TOP)
            {
                player.LosePoints();  //calls the method for losing score 
            }

            CoinUpdate();

            base.Update(gameTime);
        }

        // update the coin in the playground and delete the proposed coin
        public void CoinUpdate()
        {
            for (int i = 0; i < coin.CoinList.Count;)
            {
                if (coin.CoinList[i].Intersects(proposedPlayer))
                {
                    coin.CoinList.RemoveAt(i);
                    player.HitCoins();     // calls the method for collecting score
                    scoreSound.Play();
                    _score = player.Score;
                    _highScore.Add(new Score()
                    {
                        Value = _score,
                    });
                    HighScore.Save(_highScore);
                }
                else
                {
                    i++;
                }
            }
        }                
    }
}
