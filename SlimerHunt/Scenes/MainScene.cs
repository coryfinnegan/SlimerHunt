using Microsoft.Xna.Framework;
using Nez.UI;

namespace SlimerHunt.Scenes
{
    public class MainScene : BaseScene
    {
        public MainScene()
        {
            var table = Canvas.Stage.AddElement(new Table());
            table.SetFillParent(true);
            table.Add(new Label("SLIMER HUNT", GhostBusterFont, Color.Green, 1, 1));
            table.Row();
            table.Add(new Image(Content.LoadTexture(Nez.Content.Images.Logo)));
            table.Row();
            var buttonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Transparent, 10f), new PrimitiveDrawable(Color.Transparent, 10f), new PrimitiveDrawable(Color.Transparent, 10f), GhostBusterFont)
            {
                DownFontColor = Color.DarkGreen,
                OverFontColor = Color.Green
            };
            var playButton = new TextButton("PLAY", buttonStyle);
            playButton.OnClicked += x => Game1.LoadScene("LevelOne");

            table.Add(playButton);


        }

    }
}
