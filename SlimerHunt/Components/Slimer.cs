using Nez.Sprites;
using Nez.Textures;
using Nez;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SlimerHunt.Components
{
    public class Slimer : Component, IUpdatable
    {
        enum Animations
        {
            South,
            Horizontal,
            North,
            Idle
        }

        SpriteAnimator _animator;
        Mover _mover;
        float _moveSpeed = 100f;
        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();

        public override void OnAddedToEntity()
        {
            var atlas = Entity.Scene.Content.LoadSpriteAtlas("Content/Images/Slimer/SlimerAtlas.atlas");
            
            _mover = Entity.AddComponent(new Mover());
            _animator = Entity.AddComponent<SpriteAnimator>();
            _animator.AddAnimationsFromAtlas(atlas);
            _animator.Play($"{Animations.South}");

            var shadow = Entity.AddComponent(new SpriteMime(Entity.GetComponent<SpriteRenderer>()));
            shadow.Color = Color.Black;
            shadow.Material = Material.StencilRead();
            shadow.RenderLayer = -2; // ABOVE our tiledmap layer so it is visible

            SetupInput();
            Entity.Scale = new Vector2(3, 3);
            var collider = Entity.AddComponent<CircleCollider>();
            Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);
            base.OnAddedToEntity();
        }

        private void SetupInput()
        {
            // horizontal input from dpad, left stick or keyboard left/right
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));

            // vertical input from dpad, left stick or keyboard up/down
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));
        }
 

        public void Update()
        {
            var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
          
            var animation = Animations.South;

            if (moveDir.X < 0)
            {
                animation = Animations.Horizontal;
                _animator.FlipX = false;
            }

            else if (moveDir.X > 0)
            {
                animation = Animations.Horizontal;
                _animator.FlipX = true;
            }

            if (moveDir.Y < 0)
            {
                animation = Animations.North;
            }
   
            else if (moveDir.Y > 0)
            {
                animation = Animations.South;
            }

            if (moveDir != Vector2.Zero)
            {
                if (!_animator.IsAnimationActive($"{animation}"))
                    _animator.Play($"{animation}");
                else
                    _animator.UnPause();

                var movement = moveDir * _moveSpeed * Time.DeltaTime;

                _mover.CalculateMovement(ref movement, out var res);
                _subpixelV2.Update(ref movement);
                _mover.ApplyMovement(movement);
            }
            else
            {
                _animator.Pause();
            }

        }
    }
}
