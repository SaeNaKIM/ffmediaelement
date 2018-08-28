﻿namespace Unosquare.FFME.Commands
{
    using Shared;

    /// <summary>
    /// The Pause Command Implementation
    /// </summary>
    /// <seealso cref="CommandBase" />
    internal sealed class PauseCommand : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PauseCommand"/> class.
        /// </summary>
        /// <param name="mediaCore">The media core.</param>
        public PauseCommand(MediaEngine mediaCore)
            : base(mediaCore)
        {
            CommandType = CommandType.Pause;
        }

        /// <inheritdoc />
        public override CommandType CommandType { get; }

        /// <inheritdoc />
        protected override void PerformActions()
        {
            var m = MediaCore;
            if (m.State.CanPause == false)
                return;

            m.Clock.Pause();

            foreach (var renderer in m.Renderers.Values)
                renderer.Pause();

            m.ChangePosition(m.SnapPositionToBlockPosition(m.WallClock));
            m.State.UpdateMediaState(PlaybackStatus.Pause);
        }
    }
}
