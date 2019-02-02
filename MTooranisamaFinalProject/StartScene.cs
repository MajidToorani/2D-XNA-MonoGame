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
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }

        private SpriteBatch spriteBatch;
        private string[] menuItems =
            {
            "Start Game",
            "How to play",
            "Help",
            "About",
            "Quit"
        };
        public StartScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("Fonts/regularfont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("Fonts/hilightfont");

            Menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems);
            this.Components.Add(Menu);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
