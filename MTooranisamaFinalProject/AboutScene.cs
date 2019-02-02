using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MTooranisamaFinalProject;

namespace AllInOneMono
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D aboutScene;
        public AboutScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            aboutScene = g.Content.Load<Texture2D>("Images/about");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(aboutScene, new Rectangle(0, 0, 1024, 768), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
