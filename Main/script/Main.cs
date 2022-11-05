using Godot;
using System;

public class Main : Node
{
// #pragma warning disable 649
//     [Export] public PackedScene MobScene;
//     [Export] public PackedScene BoosterScene;
//     [Export] public PackedScene Explosion;
// #pragma warning restore 649
    //
    // public int Score;
    // public int Lives;
    // public bool BossScene = false;
    // public bool Playing = false;

    // todo переделать сигналы
    public override void _Ready()
    {
    //     GetNode<GameOver>("GameOver").Hide();
    //     GetNode<GameUI>("GameUI").Hide();
    //     GetNode<Player>("Player").Connect("Hit", this, nameof(_on_crash));
    //     GetNode<Player>("Player").Connect("CrashBoss", this, nameof(GameOver));
    //     GetNode<Player>("Player").Connect("Score", this, nameof(AddScore));
    //     GetNode<Player>("Player").Connect("HP", this, nameof(AddLife));
    //     GetNode<Boss>("Boss").Connect("Win", this, nameof(Win));
    //     GetNode<MainMenu>("MainMenu").Connect("Start", this, nameof(NewGame));
    //     GetNode<GameOver>("GameOver").Connect("YesPressed", this, nameof(NewGame));
        GD.Randomize();
    }

    
    // todo перенести в игрока
    // public void _on_crash()
    // {
    //     Lives -= 1;
    //     GetNode<GameUI>("GameUI").SetLifeUI(Lives);
    //     
    //     if (Lives < 0)
    //     {
    //         var boom = (Explosion)Explosion.Instance();
    //         boom.Position = GetNode<Player>("Player").Position;
    //         AddChild(boom);
    //         GameOver();
    //     }
    // }
    
    // public void AddScore(int score)
    // {
    //     Score += score;
    //     GoBossScene();
    //     GetNode<GameUI>("GameUI").SetScoreUI(Score);
    // }

    // todo перенести в игрока
    // public void AddLife()
    // {
    //     if (Lives < 5)
    //     {
    //         Lives++;
    //         GetNode<GameUI>("GameUI").SetLifeUI(Lives);
    //     }
    // }
    
    // public void GameOver()
    // {
    //     GetTree().CallGroup("mobs", "queue_free");
    //     GetTree().CallGroup("boosters", "queue_free");
    //     Playing = false;
    //     BossScene = false;
    //     GetNode<Timer>("Boss/Reload").Stop();
    //     GetNode<Boss>("Boss").Hide();
    //     GetNode<CollisionShape2D>("Boss/CollisionShape2D").SetDeferred("disabled", true);
    //     GetNode<GameUI>("GameUI").Hide();
    //     GetNode<Player>("Player").CanPlay = false;
    //     GetNode<CollisionShape2D>("Player/CollisionShape2D").SetDeferred("disabled", true);
    //     GetNode<Player>("Player").Hide();
    //     GetNode<Timer>("MobTimer").Stop();
    //     GetNode<Timer>("BoosterTimer").Stop();
    //     GetNode<GameOver>("GameOver").Show();
    // }
    
    // public void NewGame()
    // {
    //     GetNode<GameOver>("GameOver").Hide();
    //     GetNode<Node2D>("GameUI/Win").Hide();
    //
    //     Score = 0;
    //     Lives = 3;
    //
    //     var player = GetNode<Player>("Player");
    //     var ui = GetNode<GameUI>("GameUI");
    //     
    //     ui.SetLifeUI(Lives);
    //     ui.SetScoreUI(Score);
    //     
    //     player.CanPlay = true;
    //     player.Upgrade = false;
    //     Playing = true;
    //
    //     ui.Show();
    //     
    //     var startPosition = GetNode<Position2D>("StartPosition");
    //     player.Start(startPosition.Position);
    //
    //     GetNode<Timer>("StartTimer").Start();
    // }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        // GetNode<Timer>("BoosterTimer").Start();
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

    // public void _on_BoosterTimer_timeout()
    // {
    //     var booster = (Booster)BoosterScene.Instance();
    //
    //     var boosterSpawnLocation = GetNode<PathFollow2D>("BoosterPath/BoosterSpawnLocation");
    //     boosterSpawnLocation.Offset = GD.Randi();
    //     
    //     booster.Position = boosterSpawnLocation.Position;
    //     
    //     string[] types = { "gun", "hp" };
    //     
    //     booster.SetType(types[new Random().Next(0, 2)]);
    //     
    //     GetTree().CallGroup("boosters", "queue_free");
    //
    //     AddChild(booster);
    // }

    // public void GoBossScene()
    // {
    //     if(Score < 5000 || BossScene || !Playing) return;
    //     BossScene = true;
    //     GetNode<Timer>("BeforeTimerBoss").Start();
    // }

    // public void _on_Delay_timeout()
    // {
    //     GetNode<CollisionShape2D>("Player/CollisionShape2D").Disabled = false;
    //     GetNode<Player>("Player").CanPlay = true;
    //     var boss = GetNode<Boss>("Boss");
    //     var bossStartPosition = GetNode<Position2D>("BossStartPosition");
    //     boss.StartBoss(bossStartPosition.Position);
    // }

    // public void _on_BeforeTimerBoss_timeout()
    // {
    //     if(!Playing) return;
    //     GetNode<CollisionShape2D>("Player/CollisionShape2D").SetDeferred("disabled", true);
    //     GetNode<Player>("Player").CanPlay = false;
    //     GetNode<Timer>("Boss/Delay").Start();
    //     GetNode<Timer>("MobTimer").Stop();
    //     GetNode<Timer>("BoosterTimer").Stop();
    // }

    // public void Win()
    // {
    //     Score += 5555;
    //     GetNode<GameUI>("GameUI").SetScoreUI(Score);
    //
    //     GetNode<Player>("Player").Hide();
    //     GetNode<Player>("Player").SetDeferred("disabled", true);
    //     GetNode<Player>("Player").CanPlay = false;
    //     Playing = false;
    //     
    //     GetNode<AnimatedSprite>("GameUI/HP").Hide();
    //     GetNode<Node2D>("GameUI/Win").Show();
    //     GetNode<Timer>("WinTimer").Start();
    // }

    // public void _on_WinTimer_timeout()
    // {
    //     GoToMainMenu();
    // }
    // public void GoToMainMenu()
    // {
    //     GetNode<AnimatedSprite>("GameUI/HP").Show();
    //     GetNode<GameUI>("GameUI").Hide();
    //     GetNode<MainMenu>("MainMenu").Show();
    // }
}
