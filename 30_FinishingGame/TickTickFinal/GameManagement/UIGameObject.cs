using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UIGameObject : SpriteGameObject //class voor de UI objecten die niet meebewegen met het level
{
    public UIGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0) : base(assetName, layer, id, sheetIndex) {
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        if (!visible || sprite == null)
        {
            return;
        }
        sprite.Draw(spriteBatch, this.GlobalPosition, origin);
    }
}
