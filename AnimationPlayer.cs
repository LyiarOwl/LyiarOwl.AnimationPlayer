using System;
using System.Collections.Generic;

namespace LyiarOwl.AnimationPlayer
{
    public class AnimationPlayer
    {

        private float _speed = 1f;
        private Dictionary<string, Animation> _animations;

        public Animation CurrentAnimation { get; private set; }
        public bool IsPlaying { get; private set; }
        public event Action<string> OnAnimationEnd;
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = float.Max(0f, value);
            }
        }
        public AnimationPlayer()
        {
            _animations = new Dictionary<string, Animation>();
        }
        public void Update()
        {
            if (!IsPlaying || CurrentAnimation == null) return;

            CurrentAnimation.Update(AnimationPlayerCore.DeltaTime * Speed);

            if (CurrentAnimation.IsFinished())
                IsPlaying = false;
        }
        public void Add(Animation animation)
        {
            if (_animations.ContainsKey(animation.Name))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Add), "This animation was already added!"));
                return;
            }

            // animation.OnAnimationEnd = OnAnimationEnd;
            _animations.Add(animation.Name, animation);
            if (CurrentAnimation == null)
                CurrentAnimation = animation;
        }
        public void Remove(string name)
        {
            if (!_animations.ContainsKey(name))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Remove), $"There is no animation named as \"{name}\""));
                return;
            }
            _animations.Remove(name);
        }
        public void Play(string name, bool playBackwards = false)
        {
            if (CurrentAnimation != null && CurrentAnimation.Name == name && IsPlaying)
                return;

            if (!_animations.TryGetValue(name, out Animation anim))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Play), $"there is no animation named as \"{name}\""));
                return;
            }
            if (CurrentAnimation != null)
                CurrentAnimation.OnAnimationEnd -= OnAnimationEnd;

            CurrentAnimation = anim;
            CurrentAnimation.OnAnimationEnd += OnAnimationEnd;
            IsPlaying = true;
            CurrentAnimation.Enter();
        }
        public void Pause()
        {
            if (CurrentAnimation != null)
                if (IsPlaying)
                    IsPlaying = false;
        }
        public void Reset()
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Enter();
                CurrentAnimation.Exit();
            }
        }
        public void Resume()
        {
            if (CurrentAnimation != null)
                if (!IsPlaying)
                    IsPlaying = true;
        }
    }
}