using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

partial class Level : GameObjectList
{
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }      
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        TimerGameObject timer = Find("timer") as TimerGameObject;

        Player player = Find("player") as Player;
        Camera camera = Find("camera") as Camera;
        float campos = camera.Position.X;
        campos += (player.GlobalPosition.X - GameEnvironment.Screen.X / 2 - campos) / GameEnvironment.Screen.X * gameTime.ElapsedGameTime.Milliseconds;

        GameObjectGrid tiles = Find("tiles") as GameObjectGrid;
        campos = MathHelper.Clamp(campos, 0, tiles.Objects.GetLength(0) * 72 - GameEnvironment.Screen.X);

        camera.Position = new Vector2(campos, camera.Position.Y);

        //GameObjectList backgrounds = Find("backgrounds") as GameObjectList;
        //backgrounds.Children[0].Position = new Vector2(campos, backgrounds.Children[0].Position.Y);


        // check if we died
        if (!player.IsAlive)
        {
            timer.Running = false;
        }

        // check if we ran out of time
        if (timer.GameOver)
        {
            player.Explode();
        }
                       
        // check if we won
        if (Completed && timer.Running)
        {
            player.LevelFinished();
            timer.Running = false;
        }
    }

    public override void Reset()
    {
        base.Reset();
        VisibilityTimer hintTimer = Find("hintTimer") as VisibilityTimer;
        hintTimer.StartVisible();
    }
}
