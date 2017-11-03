using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

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
        GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
        foreach (AnimatedGameObject enemy in enemies.Children)
        {
            if (CollidesWith(enemy))
            {
                if (enemy is Rocket)
                    enemy.Reset();
                else
                    enemy.Visible = false;
                GameWorld.MarkForRemove(this);
            }
        }

        TileField tiles = GameWorld.Find("tiles") as TileField;
        int xFloor = (int)(position.X / tiles.CellWidth);
        int yFloor = (int)((position.Y-sprite.Height/2) / tiles.CellHeight);

        Tile currentTile = tiles.Get(xFloor, yFloor) as Tile;
        Tile lowerTile = tiles.Get(xFloor, yFloor-1) as Tile;

        if (lowerTile != null && lowerTile.TileType != TileType.Background && CollidesWith(lowerTile))
        {
            GameWorld.MarkForRemove(this);
        }else
        if (currentTile != null && currentTile.TileType != TileType.Background)
        {
            GameWorld.MarkForRemove(this);
        }
        Camera camera = GameWorld.Find("camera") as Camera;
        if (position.X - camera.Position.X > GameEnvironment.Screen.X || position.X - camera.Position.X < 0)
            GameWorld.MarkForRemove(this);

    }

}
