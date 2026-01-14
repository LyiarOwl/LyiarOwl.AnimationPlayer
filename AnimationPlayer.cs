using System;
using System.Collections.Generic;

namespace LyiarOwl.AnimationPlayer
{
    public class AnimationPlayer
    {
        private Dictionary<string, Animation> _animations;

        public Animation CurrentAnimation { get; private set; }
        public bool IsPlaying { get; private set; }
        public AnimationPlayer()
        {
            _animations = new Dictionary<string, Animation>();
        }
        public void Update()
        {
            if (!IsPlaying || CurrentAnimation == null) return;

            CurrentAnimation.Update();

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

            _animations.Add(animation.Name, animation);
            if (CurrentAnimation == null)
                CurrentAnimation = animation;

            if (playOnStart)
            {
                CurrentAnimation = animation;
                PlayAnimation(animation.Name);
            }
        }
        public void PlayAnimation(string name)
        {
            if (CurrentAnimation != null && CurrentAnimation.Name == name && IsPlaying)
            {
                return;
            }

            if (!_animations.TryGetValue(name, out Animation anim))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(PlayAnimation), $"there is no animation named as \"{name}\""));
                return;
            }

            CurrentAnimation = anim;
            IsPlaying = true;
            CurrentAnimation.Enter();
        }
    }
}