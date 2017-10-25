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
        campos -= -player.Position.X + GameEnvironment.Screen.X / 2- position.X)/GameEnvironment.Screen.X * gameTime.ElapsedGameTime.Milliseconds;

        if (campos > 0)
            campos = 0;

        GameObjectGrid tiles = Find("tiles") as GameObjectGrid;
        if (position.X < tiles.Objects.GetLength(0) * -72 + GameEnvironment.Screen.X) 
            position.X = tiles.Objects.GetLength(0) * -72 + GameEnvironment.Screen.X;

        GameObjectList backgrounds = Find("backgrounds") as GameObjectList;
        backgrounds.Children[0].Position = new Vector2(-position.X, backgrounds.Children[0].Position.Y);


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
