using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShootTheDead.GameEntities.Guns
{
    public abstract class Gun 
    {
        protected float cooldown;
        protected float cooldownLeft;
        public int Ammo { get; protected set; }
        public bool Reloading { get; protected set; }
        public static float TotalSeconds { get; set; }

        protected Gun()
        {
            cooldownLeft = 0f;
        }

        public abstract void CreateProjectiles(Player player);
        public virtual void Fire(Player player)
        {
            if (cooldownLeft > 0 || Reloading) return;

            Ammo--;
            if (Ammo > 0)
            {
                cooldownLeft = cooldown;
            }

            CreateProjectiles(player);
        }

        public virtual void Update()
        {
            if (cooldownLeft > 0)
            {
                cooldownLeft -= TotalSeconds;
            }
        }
    }
}
