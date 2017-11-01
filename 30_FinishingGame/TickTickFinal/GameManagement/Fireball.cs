using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

class Fireball : AnimatedGameObject
{
    float speed = 1000f;
    public Fireball(Vector2 start, Vector2 direction) : base(1, "fireball") {

        LoadAnimation("Sprites/Player/spr_fireball@5x3", "explode", true, 0.05f);
        position = start;
        velocity = direction * speed;
        PlayAnimation("explode");
        GameEnvironment.AssetManager.PlaySound("Sounds/fireball");
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        HandleCollision();
    }

    protected void HandleCollision() {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        int xFloor = (int)GlobalPosition.X / tiles.CellWidth;
        int yFloor = (int)GlobalPosition.Y / tiles.CellHeight;

        Tile currentTile = tiles.Get(xFloor, yFloor) as Tile;

        Rectangle tileBounds = new Rectangle(xFloor * tiles.CellWidth, yFloor * tiles.CellHeight,
                                                tiles.CellWidth, tiles.CellHeight);
        Rectangle boundingBox = this.BoundingBox;
        boundingBox.Height += 1;
        if (currentTile != null)
        {
            //parent.GameWorld.Remove(this);
        }
    }

}
