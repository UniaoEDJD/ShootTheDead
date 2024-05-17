﻿using System;
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
        protected int maxAmmo;
        public int Ammo { get; protected set; }
        protected float reloadTime;
        public bool Reloading { get; protected set; }
        public static float TotalSeconds { get; set; }

        protected Gun()
        {
            cooldownLeft = 0f;
            Reloading = false;
        }
        public virtual void Reload()
        {
            if (Reloading || (Ammo == maxAmmo)) return;
            cooldownLeft = reloadTime;
            Reloading = true;
            Ammo = maxAmmo;
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
            else
            {
                Reload();
            }

            CreateProjectiles(player);
        }

        public virtual void Update()
        {
            if (cooldownLeft > 0)
            {
                cooldownLeft -= TotalSeconds;
            }
            else if (Reloading)
            {
                Reloading = false;
            }
        }
    }
}