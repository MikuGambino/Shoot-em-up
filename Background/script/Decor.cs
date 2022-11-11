using Godot;
using System;

public class Decor : Node2D
{
    public override void _Process(float delta)
  {
      if(Position.x < -768)
      {
          Position = new Vector2(0, 72);
      }
      Position += Vector2.Left * 0.5f;
  }
}
