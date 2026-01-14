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
        public void AddAnimation(Animation animation, bool playOnStart = false)
        {
            if (_animations.ContainsKey(animation.Name))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(AddAnimation), "This animation was already added!"));
                return;
            }

            // animation.OnAnimationEnd = OnAnimationEnd;
            _animations.Add(animation.Name, animation);
            if (CurrentAnimation == null)
                CurrentAnimation = animation;

            if (playOnStart)
            {
                CurrentAnimation = animation;
                PlayAnimation(animation.Name);
            }
        }
        public void RemoveAnimation(string name)
        {
            if (!_animations.ContainsKey(name))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(RemoveAnimation), $"There is no animation named as \"{name}\""));
                return;
            }
            _animations.Remove(name);
        }
        public void PlayAnimation(string name)
        {
            if (CurrentAnimation != null && CurrentAnimation.Name == name && IsPlaying)
                return;

            if (!_animations.TryGetValue(name, out Animation anim))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(PlayAnimation), $"there is no animation named as \"{name}\""));
                return;
            }
            if (CurrentAnimation != null)
                CurrentAnimation.OnAnimationEnd -= OnAnimationEnd;

            CurrentAnimation = anim;
            CurrentAnimation.OnAnimationEnd += OnAnimationEnd;
            IsPlaying = true;
            CurrentAnimation.Enter();
        }
        public void PauseAnimation()
        {
            if (CurrentAnimation != null)
                if (IsPlaying)
                    IsPlaying = false;
        }
        public void ResetAnimation()
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Enter();
                CurrentAnimation.Exit();
            }
        }
        public void ResumeAnimation()
        {
            if (CurrentAnimation != null)
                if (!IsPlaying)
                    IsPlaying = true;
        }
    }
}