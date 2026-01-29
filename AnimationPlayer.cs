using System;
using System.Collections.Generic;

namespace LyiarOwl.AnimationPlayer
{
    /// <summary>
    /// Manages and controls animations.
    /// </summary>
    public class AnimationPlayer
    {

        private float _speed = 1f;
        private Dictionary<string, Animation> _animations;

        /// <summary>
        /// The animation currently being played.
        /// </summary>
        public Animation CurrentAnimation { get; private set; }
        /// <summary>
        /// Indicates whether an animation is currently playing.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// <para>Called when the current animation ends.</para>
        /// <para>The <c>string</c> parameter is the name of the animation that ended.</para>
        /// </summary>
        public event Action<string> OnAnimationEnd;
        /// <summary>
        /// <para>Playback speed of the current animation.</para>
        /// <para>Default: <c>1.0</c>, Minimum: <c>0.0</c></para>
        /// </summary>
        public float Speed
        {
            get => _speed;
            set
            {
                if (float.IsNaN(value) || float.IsInfinity(value))
                {
#if DEBUG
                    Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Speed), "The value being set is NaN or Infinity. Speed is being set to 0.0!"));
#endif                    
                    _speed = 0f;
                }
                _speed = AnimationPlayerCore.Max(0f, value);
            }
        }
        /// <summary>
        /// Creates a new animation manager.
        /// </summary>
        public AnimationPlayer()
        {
            _animations = new Dictionary<string, Animation>();
        }

        /// <summary>
        /// Updates the current animation.
        /// </summary>
        public void Update()
        {
            if (!IsPlaying || CurrentAnimation == null) return;

            CurrentAnimation.Update(AnimationPlayerCore.DeltaTime * Speed);

            if (CurrentAnimation.IsFinished())
                IsPlaying = false;
        }
        /// <summary>
        /// <para>Adds an animation to this manager.</para>
        /// <para>If the animation was already added, this operation will be ignored
        /// and a warning will be printed.</para>
        /// </summary>
        /// <param name="animation">The animation to be added.</param>
        public void Add(Animation animation)
        {
            if (_animations.ContainsKey(animation.Name))
            {
#if DEBUG
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Add), $"The \"{animation.Name}\" animation was already added!"));
#endif
                return;
            }

            _animations.Add(animation.Name, animation);
            if (CurrentAnimation == null)
                CurrentAnimation = animation;
        }
        /// <summary>
        /// <para>Removes the animation with the specified <c>name</c>.</para>
        /// <para>If no animation with this <c>name</c> exists,
        /// this operation will be ignored and a warning will be printed.</para>
        /// </summary>
        /// <param name="name">Name of the animation to be removed.</param>
        public void Remove(string name)
        {
            if (!_animations.ContainsKey(name))
            {
#if DEBUG
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Remove), $"There is no animation named as \"{name}\""));
#endif
                return;
            }
            _animations.Remove(name);
        }
        /// <summary>
        /// <para>Plays the animation with the specified <c>name</c>.</para>
        /// <para>If the animation is already playing, nothing will happen.</para>
        /// <para>If no animation with this <c>name</c> exists,
        /// this operation will be ignored and a warning will be printed.</para>
        /// </summary>
        /// <param name="name">Name of the animation to be played.</param>
        /// <param name="backward">Whether the animation should play backwards.</param>
        public void Play(string name, bool backward = false)
        {
            if (CurrentAnimation != null && CurrentAnimation.Name == name && IsPlaying)
                return;

            if (!_animations.TryGetValue(name, out Animation anim))
            {
#if DEBUG
                Console.WriteLine(AnimationPlayerCore.CreateWarningMessage<AnimationPlayer>(nameof(Play), $"there is no animation named as \"{name}\""));
#endif
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
        /// <para>Pauses the current animation.</para>
        /// <para>Use <seealso cref="Resume"/> to continue the animation.</para>
        /// <para>If this manager has no animation, this operation will be ignored.</para>
        /// </summary>
        public void Pause()
        {
            if (CurrentAnimation != null)
                if (IsPlaying)
                    IsPlaying = false;
        }
        /// <summary>
        /// <para>Resets the state of the current animation, including all keyframes.</para>
        /// <para>If this manager has no animation, this operation will be ignored.</para>
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
        /// <para>Resumes the current animation if it is paused.</para>
        /// <para>If this manager has no animation, this operation will be ignored.</para>
        /// </summary>
        public void Resume()
        {
            if (CurrentAnimation != null)
                if (!IsPlaying)
                    IsPlaying = true;
        }

        /// <summary>
        /// <para>Stops the current animation and resets its state.</para>
        /// <para>Use <seealso cref="Pause"/> if you want to pause the animation instead.</para>
        /// <para>If this manager has no animation, this operation will be ignored.</para>
        /// </summary>
        public void Stop()
        {
            if (CurrentAnimation != null)
            {
                Pause();
                Reset();
            }
        }
    }
}
