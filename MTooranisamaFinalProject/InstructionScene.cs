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
    public class InstructionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D instructionScene;
        public InstructionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            instructionScene = g.Content.Load<Texture2D>("Images/instruction");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(instructionScene, new Rectangle(0, 0, 1024, 768), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
