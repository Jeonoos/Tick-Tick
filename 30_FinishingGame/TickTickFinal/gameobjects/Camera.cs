using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Camera: GameObject
{
    public Camera(int layer, string id = ""):base(layer,id) {

    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        Camera camera = GameWorld.Find("camera") as Camera;
        Vector2 campos = camera.Position;
        campos += (player.GlobalPosition + Vector2.UnitY * GameEnvironment.Screen.Y / 7 - new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - campos) / GameEnvironment.Screen.X * gameTime.ElapsedGameTime.Milliseconds;

        GameObjectGrid tiles = GameWorld.Find("tiles") as GameObjectGrid;
        campos.X = MathHelper.Clamp(campos.X, 0, tiles.Objects.GetLength(0) * 72 - GameEnvironment.Screen.X);
        campos.Y = MathHelper.Clamp(campos.Y, -GameEnvironment.Screen.Y / 4, tiles.Objects.GetLength(1) * 55 - GameEnvironment.Screen.Y);

        camera.Position = campos;
    }
}
