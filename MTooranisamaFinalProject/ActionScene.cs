 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AllInOneMono;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using PROG2370CollisionLibrary;

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
{   // class ActionScene inherited from GameScene => actual game 
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;
               
        Player player;
        Dragon dragon;
        TwinHeadedDragon twinHeadedDragon;
        SpriteFont defaultFont;
        SpriteFont defaultFont1;
        Coin coin;
        PlayGround pg;
        ScoreManager SM;
       
        public ActionScene (Game game, SpriteBatch spriteBatch): base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            g.IsMouseVisible = true;
            
            SoundEffect scoreSound = g.Content.Load<SoundEffect>("HUD/coin2");
            SoundEffect hitSound = g.Content.Load<SoundEffect>("HUD/interface6");
            SoundEffect laughSound = g.Content.Load<SoundEffect>("HUD/DragonLaughing");

            defaultFont = g.Content.Load<SpriteFont>("Fonts/defaultFont2");
            defaultFont1 = g.Content.Load<SpriteFont>("Fonts/defaultFont1");

            Vector2 stage = new Vector2(g.graphics.PreferredBackBufferWidth, g.graphics.PreferredBackBufferHeight);
            Texture2D playGroundTexture = g.Content.Load<Texture2D>("Images/background");
            Rectangle playGroundRect = new Rectangle(0, 0, playGroundTexture.Width, 768);
            Vector2 pos0 = new Vector2(0);
            Vector2 speed = new Vector2(1, 0); 
            Texture2D rigidTexture = g.Content.Load<Texture2D>("Images/wood");
            Song playSong = game.Content.Load<Song>("HUD/Forest");
            pg = new PlayGround(g, spriteBatch, defaultFont, playGroundTexture, rigidTexture, pos0, playGroundRect, speed, playSong);
            Components.Add(pg);
            
            Texture2D coinTexture = g.Content.Load<Texture2D>("Images/coin");
            coin = new Coin(g, spriteBatch, coinTexture, new Vector2(0));
            Components.Add(coin);
            
            Vector2 location = new Vector2(0, 600);
            Texture2D playerTexture = g.Content.Load<Texture2D>("Images/Teddy_spritesheet");
            player = new Player(g, spriteBatch, playerTexture, location, hitSound, pg, 10000);
            Components.Add(player);

            Vector2 position = new Vector2(200, 500);
            Texture2D dragonTexture = g.Content.Load<Texture2D>("Images/flying_dragon-red");
            dragon = new Dragon(g, spriteBatch, dragonTexture, position, new Vector2(2,-1), pg);
            Components.Add(dragon);

            Vector2 pos = new Vector2(50, 300);
            Texture2D dragonTex = g.Content.Load<Texture2D>("Images/flying_twin_headed_dragon-blue");
            twinHeadedDragon = new TwinHeadedDragon(g, spriteBatch, dragonTex, pos, new Vector2(3, 2), pg);
            Components.Add(twinHeadedDragon);

            SM = new ScoreManager(g, spriteBatch, defaultFont, defaultFont1, player, coin, dragon, twinHeadedDragon, scoreSound, laughSound);
            Components.Add(SM);            
        }
        
        // Update 
        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
        }

        // Draw game components : 
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }       
    }
}
