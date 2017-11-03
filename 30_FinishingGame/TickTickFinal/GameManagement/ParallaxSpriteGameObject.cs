using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ParallaxSpriteGameObject : SpriteGameObject
{
    public ParallaxSpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0) : base(assetName, layer, id, sheetIndex) {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        if (!visible || sprite == null)
        {
            return;
        }

        Camera camera = Root.GameWorld.Find("camera") as Camera;
        if (camera != null)
            sprite.Draw(spriteBatch, this.GlobalPosition - (camera.Position*(layer + parent.Layer) * 0.1f), origin);
        else
            sprite.Draw(spriteBatch, this.GlobalPosition, origin);
    }
}

