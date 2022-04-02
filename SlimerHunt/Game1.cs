using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using Nez.Tiled;
using Nez.UI;
using System;
using System.Collections.Generic;

namespace SlimerHunt
{
    public class Game1 : Nez.Core
    {
        public static Game1 gameRef;
        private Scene.SceneResolutionPolicy policy;
        public const int SCREEN_SPACE_RENDER_LAYER = 999;
        public UICanvas canvas;

        public Game1() : base()
        {
            policy = Scene.SceneResolutionPolicy.BestFit;
            Window.AllowUserResizing = true;
            gameRef = this;
            DebugRenderEnabled = false;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            base.Initialize();
            base.Update(new GameTime());
            base.Draw(new GameTime());

            Scene.SetDefaultDesignResolution(800, 600, Scene.SceneResolutionPolicy.BestFit);
            Screen.SetSize(800, 600);
            Scene = new Scenes.MainScene();
        }

        public static void LoadScene(string scene)
        {
            var type = Type.GetType($"SlimerHunt.Scenes.{scene}") ?? throw new Exception($"Unable to locate scene with name {scene}");
            Scene = (Scene)Activator.CreateInstance(type);
        }
    }
}
