using Nez.UI;
using Nez.Tiled;
using Nez;
using Nez.Sprites;
using SlimerHunt.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SlimerHunt.Scenes
{
    public class LevelOne : BaseScene
    {
        public LevelOne() : base()
        {
            ClearColor = Color.DarkGray;
            var background = CreateEntity("Background");
            var map = Content.LoadTiledMap(Nez.Content.Maps.LevelOne);
            background.AddComponent(new TiledMapRenderer(map, "Blocks"));

            var player = CreateEntity("Slimer", new Vector2(800 / 2, 600 / 2));
            player.AddComponent(new Slimer());

            var spawnLayer = map.GetObjectGroup("Spawns");
            var spawns = spawnLayer.Objects;
            var count = 1;
            foreach(var spawn in spawns)
            {
                var number = CreateEntity($"{count}", new Vector2(spawn.X, spawn.Y));
                var text = new TextComponent(GhostBusterFont, $"{count}", Vector2.Zero, Color.Green);
                number.AddComponent(text);
                count++;
            }
        }
    }
}
