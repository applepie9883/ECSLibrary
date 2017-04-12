﻿using System;
using GM.ECSLibrary.Components;

namespace GM.ECSLibrary.Systems
{
    /// <summary>
    /// System that uses position and velocity to move entites. Requires <see cref="PositionComponent"/> and <see cref="VelocityComponent"/>.
    /// </summary>
    public class VelocitySystem : SystemBase
    {
        /// <inheritdoc />
        public override UpdateStage SystemUpdateStage
        {
            get
            {
                return UpdateStage.Update;
            }
        }

        /// <summary>
        /// Default constructor, adds <see cref="PositionComponent"/> and <see cref="VelocityComponent"/> to the <see cref="SystemBase._RequiredComponents"/> list.
        /// </summary>
        public VelocitySystem()
        {
            _RequiredComponents.AddRange(new[] { typeof(PositionComponent), typeof(VelocityComponent) });
        }

        /// <summary>
        /// Updates the given entity, moving it according to it's current position and velocity. See <see cref="SystemBase.OnUpdate(Entity)"/> for the base method that this overrides.
        /// </summary>
        /// <param name="updatingEntity">The entity to move.</param>
        protected override void OnUpdate(Entity updatingEntity)
        {
            updatingEntity.GetComponent<PositionComponent>().Position += updatingEntity.GetComponent<VelocityComponent>().Velocity;
        }
    }
}
