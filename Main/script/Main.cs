using Godot;
using System;

public class Main : Node
{
    private bool _bossScene;
    private bool _playing;

    public override void _Process(float delta)
    {
        if (_playing && Input.IsActionPressed("restart"))
        {
            GetNode<Timer>("StartTimer").Stop();
            GameOver();
            GetNode<GameOver>("GameOver").Hide();
            GetNode<Player>("Player").ZIndex = -10;
            GoToMainMenu();
        }
    }

    public override void _Ready()
    {
        GetNode<GameOver>("GameOver").Hide();
        GetNode<GameUI>("GameUI").Hide();
        EventsHolder.events.Connect("Start", this, nameof(NewGame));
        EventsHolder.events.Connect("GameOver", this, nameof(GameOver));
        EventsHolder.events.Connect("Win", this, nameof(Win));
        EventsHolder.events.Connect("AddScore", this, nameof(GoBossScene));
        GD.Randomize();
    }

    public void GameOver()
    {
        GetTree().CallGroup("bullets", "queue_free");
        GetTree().CallGroup("mobs", "queue_free");
        GetTree().CallGroup("boosters", "queue_free");
        _playing = false;
        _bossScene = false;
        GetNode<Timer>("Boss/Reload").Stop();
        GetNode<Boss>("Boss").Hide();
        GetNode<CollisionShape2D>("Boss/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<GameUI>("GameUI").Hide();
        GetNode<Player>("Player").CanPlay = false;
        GetNode<CollisionShape2D>("Player/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<Player>("Player").Hide();
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("BoosterTimer").Stop();
        GetNode<GameOver>("GameOver").Show();
        GetNode<Player>("Player").ZIndex = -10;
    }
    
    public void NewGame()
    {
        GetNode<GameOver>("GameOver").Hide();
        GetNode<Node2D>("GameUI/Win").Hide();
        
        EventsHolder.events.EmitSignal("ResetUI");
    
        var player = GetNode<Player>("Player");
        var ui = GetNode<GameUI>("GameUI");
        
        _playing = true;
    
        ui.Show();
        
        var startPosition = GetNode<Position2D>("StartPosition");
        player.Start(startPosition.Position);
    
        GetNode<Timer>("StartTimer").Start();
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("BoosterTimer").Start();
    }

    public void OnMobTimerTimeout()
    {
        Mob mob;
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = GD.Randi();
        
        if (new Random().Next(0, 5) > 1)
        {
            var purpleMob = GD.Load<PackedScene>("res://Mob/scene/PurpleMob.tscn");
            mob = (PurpleMob)purpleMob.Instance();
        }
        else
        {
            var redMob = GD.Load<PackedScene>("res://Mob/scene/RedMob.tscn");
            mob = (RedMob)redMob.Instance();
        }
        
        mob.Position = mobSpawnLocation.Position;
        
        var velocity = new Vector2(-64, 0);
        mob.LinearVelocity = velocity;
        
        AddChild(mob);
    }

    public void OnBoosterTimerTimeout()
    {
        
        var booster = (Booster) GD.Load<PackedScene>("res://Powerup/scene/Booster.tscn").Instance();

        var boosterSpawnLocation = GetNode<PathFollow2D>("BoosterPath/BoosterSpawnLocation");
        boosterSpawnLocation.Offset = GD.Randi();
        
        booster.Position = boosterSpawnLocation.Position;
        
        string[] types = { "gun", "hp" };
        
        booster.SetType(types[new Random().Next(0, 2)]);
        
        GetTree().CallGroup("boosters", "queue_free");
    
        AddChild(booster);
    }

    public void GoBossScene(int _)
    {
        if(GetNode<GameUI>("GameUI").Score < 5000 || _bossScene || !_playing) return;
        _bossScene = true;
        GetNode<Timer>("BeforeTimerBoss").Start();
    }

    public void OnDelayTimeout()
    {
        Console.WriteLine("da");
        GetNode<CollisionShape2D>("Player/CollisionShape2D").Disabled = false;
        GetNode<Player>("Player").CanPlay = true;
        var boss = GetNode<Boss>("Boss");
        var bossStartPosition = GetNode<Position2D>("BossStartPosition");
        boss.StartBoss(bossStartPosition.Position);
    }

    public void OnBeforeTimerBossTimeout()
    {
        if(!_playing) return;
        GetNode<CollisionShape2D>("Player/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<Player>("Player").CanPlay = false;
        GetNode<Timer>("Delay").Start();
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("BoosterTimer").Stop();
    }

    public void Win()
    {
        EventsHolder.events.EmitSignal("AddScore", 5555);
    
        GetNode<Player>("Player").Hide();
        GetNode<Player>("Player").SetDeferred("disabled", true);
        GetNode<Player>("Player").CanPlay = false;
        _playing = false;
        
        GetNode<AnimatedSprite>("GameUI/HP").Hide();
        GetNode<Node2D>("GameUI/Win").Show();
        GetNode<Timer>("WinTimer").Start();
    }

    public void OnWinTimerTimeout()
    {
        GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        GetNode<AnimatedSprite>("GameUI/HP").Show();
        GetNode<GameUI>("GameUI").Hide();
        GetNode<MainMenu>("MainMenu").Show();
    }
}
