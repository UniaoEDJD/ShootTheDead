﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using System.ComponentModel;
using System.Diagnostics;

namespace ShootTheDead.Control
{
    public class Button : Component
    {
        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;

        public EventHandler Click;

        public bool Clicked { get; private set; }

        public float Layer {  get; set; }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }

        public Color PenColor { get; set; }

        public Vector2 Position;

        public Rectangle Rectangle;
   

        public string Text;
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle = new Rectangle((int)Position.X - ((int)Origin.X), (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;
            
            spriteBatch.Draw(_texture, Position, null, colour, 0f, Origin, 1f, SpriteEffects.None, Layer);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color.Black, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layer + 0.01f);
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();


            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}