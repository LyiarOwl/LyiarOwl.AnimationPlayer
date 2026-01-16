using System;
using System.Collections.Generic;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Manager of animations.
    /// </summary>
    public class AnimationPlayer
    {

        private float _speed = 1f;
        private Dictionary<string, Animation> _animations;

        public Animation CurrentAnimation { get; private set; }
        /// <summary>
        /// If this animation is playing or not.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// <para>Called when the current animation ends.</para>
        /// <para>The <c>string</c> parameter is the name of the animation ended.</para>
        /// </summary>
        public event Action<string> OnAnimationEnd;
        /// <summary>
        /// <para>The current speed of this animation.</para>
        /// <para>Default: <c>1.0</c>, Minimal: <c>0.0</c></para>
        /// </summary>
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = float.Max(0f, value);
            }
        }
        /// <summary>
        /// Creates an animation manager.
        /// </summary>
        public AnimationPlayer()
        {
            _animations = new Dictionary<string, Animation>();
        }
        /// <summary>
        /// Update the current animation.
        /// </summary>
        public void Update()
        {
            if (!IsPlaying || CurrentAnimation == null) return;

            CurrentAnimation.Update(AnimationPlayerCore.DeltaTime * Speed);

            if (CurrentAnimation.IsFinished())
                IsPlaying = false;
        }
        /// <summary>
        /// <para>Add an animation to this manager.</para>
        /// <para>If the animation was already added, this operation will be ignored 
        /// and a warning will be printed.</para>
        /// </summary>
        /// <param name="animation">Animation to be added.</param>
        public void Add(Animation animation)
        {
            if (_animations.ContainsKey(animation.Name))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Add), "This animation was already added!"));
                return;
            }

            _animations.Add(animation.Name, animation);
            if (CurrentAnimation == null)
                CurrentAnimation = animation;
        }
        /// <summary>
        /// <para>Removes any animation with this <c>name</c>.</para>
        /// <para>If there is no animation with this <c>name</c>, 
        /// this operation will be ignored and a warning will be printed.</para>
        /// </summary>
        /// <param name="name">Name of the animation to be removed.</param>
        public void Remove(string name)
        {
            if (!_animations.ContainsKey(name))
            {
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Remove), $"There is no animation named as \"{name}\""));
                return;
            }
            _animations.Remove(name);
        }
        /// <summary>
        /// <para>Play the animation with this <c>name</c>.</para>
        /// <para>If the animation is already being played, nothing will happen.</para>
        /// <para>If there is no animation with this <c>name</c>, 
        /// this operation will be ignored and a warning will be printed.</para>
        /// </summary>
        /// <param name="name">Name of the animation to be played.</param>
        /// <param name="backward">If this animation should play backwards.</param>
        public void Play(string name, bool backward = false)
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

            CurrentAnimation.SetPlayDirection(!backward);

            IsPlaying = true;
            CurrentAnimation.Enter();
        }
        /// <summary>
        /// <para>Pause the current animation.</para>
        /// <para>If this manager doesn't have any animation, this operation will be ignored.</para>
        /// </summary>
        public void Pause()
        {
            if (CurrentAnimation != null)
                if (IsPlaying)
                    IsPlaying = false;
        }
        /// <summary>
        /// <para>Reset the state of the current animation (and everything internally, including keyframes).</para>
        /// <para>If this manager doesn't have any animation, this operation will be ignored.</para>
        /// </summary>
        public void Reset()
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Enter();
                CurrentAnimation.Exit();
            }
        }
        /// <summary>
        /// <para>Resume the current animation if it is paused.</para>
        /// <para>If this manager doesn't have any animation, this operation will be ignored.</para>
        /// </summary>
        public void Resume()
        {
            if (CurrentAnimation != null)
                if (!IsPlaying)
                    IsPlaying = true;
        }
    }
}