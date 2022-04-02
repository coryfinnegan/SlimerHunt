using Nez;
using Microsoft.Xna.Framework;
using Nez.BitmapFonts;

namespace SlimerHunt.Scenes
{
    public class BaseScene : Scene
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas => CreateEntity("ui").AddComponent(new UICanvas());
        public BitmapFont GhostBusterFont => Content.LoadBitmapFont(Nez.Content.Fonts.GhostbusterNew);

        public BaseScene()
        {
            ClearColor = Color.Black;
            AddRenderer(new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer));
           
        }
    }
}
