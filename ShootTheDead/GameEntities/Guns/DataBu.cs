﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShootTheDead.GameEntities.Guns
{
    public sealed class DataBu
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Lifespan { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
    }
}
