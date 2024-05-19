using Microsoft.Xna.Framework.Audio;
using ShootTheDead.GameEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ShootTheDead.Managers
{
    public static class EnemyManager
    {
        public static List<Enemy> Zombies { get; } = [];
        private static Texture2D _texture;
        private static float _spawnCooldown;
        private static float _spawnTime;
        private static Random _random;
        private static int _padding;
        static float cool = 3;
        static SoundEffect hitSound;

        public static void Init(ContentManager content)
        {
            _texture = content.Load<Texture2D>("skeleton-move_");
            hitSound = content.Load<SoundEffect>("SFX/hitHurt");
            _spawnCooldown = 1f;
            _spawnTime = _spawnCooldown;
            _random = new();
            _padding = _texture.Width / 2;
        }

        public static void Reset()
        {
            Zombies.Clear();
            _spawnTime = _spawnCooldown;
        }

        private static Vector2 RandomPosition()
        {
            float w = Globals.Bounds.X;
            float h = Globals.Bounds.Y;
            Vector2 pos = new();

            if (_random.NextDouble() < w / (w + h))
            {
                pos.X = (int)(_random.NextDouble() * w);
                pos.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : h + _padding);
            }
            else
            {
                pos.Y = (int)(_random.NextDouble() * h);
                pos.X = (int)(_random.NextDouble() < 0.5 ? -_padding : w + _padding);
            }

            return pos;
        }

        public static void AddZombie()
        {
            Zombies.Add(new(RandomPosition(), _texture));
        }

        public static void Update(Player player, GameTime gameTime)
        {
            _spawnTime -= Globals.deltaTime;
            while (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                AddZombie();
            }

            foreach (var z in Zombies.ToList())
            {
                z.Update(player, gameTime);
                if (player.playerRect.Intersects(z.collider))
                {
                    if(!z.isKilling)
                    {
                        cool = 3;
                        player.TakeDamage(1); 
                        z.isKilling = true;
                    }
                    else
                    {
                        cool -= 1 * Globals.deltaTime;
                        if(cool <= 0)
                        {
                            z.isKilling = false;
                        }
                    }
                    
                }
                foreach (var b in player.Bullets.ToList())
                {
                    if (z.collider.Intersects(b.bulletRect))
                    {
                        hitSound.Play();
                        z.TakeDamage(1);
                        player.Bullets.RemoveAll((bullet) => bullet.bulletRect.Intersects(z.collider));
                        Debug.WriteLine("He takin damage cuh");
                    }
                }
                if (z.HP <= 0)
                {
                    player.score += 1;
                    Zombies.RemoveAll((z) => z.HP <= 0);
                    Debug.WriteLine($"score: {player.score}");
                }
            }
            
            
        }

        public static void Draw(SpriteBatch sprite)
        {
            foreach (var z in Zombies)
            {
                z.Draw(sprite);
            }
        }
    }
}
