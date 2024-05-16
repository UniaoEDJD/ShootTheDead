using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootTheDead.GameEntities.Guns
{
    public class Bullet : Sprite
    {
        public Bullet(Vector2 pos) : base(pos)
        {

        }

        public Vector2 Direction { get; set; }
        public float Lifespan { get; private set; }
        public int Damage { get; }
        public static float TotalSeconds { get; set; }
        public int Speed { get; set; } = 300;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }


        public void Destroy()
        {
            Lifespan = 0;
        }

        public void Update()
        {
            Position += Direction * Speed * TotalSeconds;
            Lifespan -= TotalSeconds;
        }
    }
}
