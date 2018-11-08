using GM.ECSLibrary.Components;
using Microsoft.Xna.Framework;

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

        public bool OverCompinsate { get; set; }

        public bool UnderCompinsate { get; set; }

        /// <summary>
        /// Default constructor, adds <see cref="PositionComponent"/> and <see cref="VelocityComponent"/> to the <see cref="SystemBase._RequiredComponents"/> list.
        /// </summary>
        public VelocitySystem()
        {
            _RequiredComponents.AddRange(new[] { typeof(PositionComponent), typeof(VelocityComponent) });

            OverCompinsate = false;
            UnderCompinsate = false;
        }

        /// <summary>
        /// Updates the given entity, moving it according to it's current position and velocity. See <see cref="SystemBase.OnUpdate(Entity)"/> for the base method that this overrides.
        /// </summary>
        /// <param name="updatingEntity">The entity to move.</param>
        protected override void OnUpdate(Entity updatingEntity)
        {
            VelocityComponent entityVelocity = updatingEntity.GetComponent<VelocityComponent>();
            PositionComponent entityPosition = updatingEntity.GetComponent<PositionComponent>();
            double elapsedMilliseconds = ManagerCatalog.CurrentGameTime.ElapsedGameTime.TotalMilliseconds;

            if ((elapsedMilliseconds < entityVelocity.MovementMilliseconds && UnderCompinsate) ||
                (elapsedMilliseconds > entityVelocity.MovementMilliseconds && OverCompinsate))
            {
                entityPosition.Position += Vector2.Multiply(entityVelocity.Velocity, (float)(elapsedMilliseconds / entityVelocity.MovementMilliseconds));
            }
            else
            {
                entityPosition.Position += entityVelocity.Velocity;
            }
        }
    }
}
