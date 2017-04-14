using GM.ECSLibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace GM.ECSLibrary.Systems
{
    /// <summary>
    /// System that uses position and a sprite to draw entities. Requires <see cref="PositionComponent"/> and <see cref="SpriteComponent"/>.
    /// </summary>
    public class DrawingSystem : SystemBase
    {
        /// <inheritdoc />
        public override UpdateStage SystemUpdateStage
        {
            get
            {
                return UpdateStage.Draw;
            }
        }

        /// <summary>
        /// Default constructor, adds <see cref="PositionComponent"/> and <see cref="SpriteComponent"/> to the <see cref="SystemBase._RequiredComponents"/> list.
        /// </summary>
        public DrawingSystem()
        {
            _RequiredComponents.AddRange(new[] { typeof(PositionComponent), typeof(SpriteComponent) });
        }

        /// <summary>
        /// Updates the given entity, drawing it according to it's current position and sprite. See <see cref="SystemBase.OnUpdate(Entity)"/> for the base method that this overrides.
        /// </summary>
        /// <param name="updatingEntity">The entity to draw.</param>
        protected override void OnUpdate(Entity updatingEntity)
        {
            // TODO: If this proves to be too costly to call each update, try overriding OnSetCatalog() to set a SpriteBatch field, and use that instead.
            SpriteBatch spriteBatch = ManagerCatalog.SharedSpriteBatch;

            PositionComponent entityUpperLeft = updatingEntity.GetComponent<PositionComponent>();
            SpriteComponent entitySprite = updatingEntity.GetComponent<SpriteComponent>();

            spriteBatch.Draw(entitySprite.Texture, entityUpperLeft.UpperLeft, null, entitySprite.SpriteColor * entitySprite.Transparency,
                                   -MathHelper.ToRadians((float)entitySprite.RotationAngle), entitySprite.Origin / entitySprite.Scale, entitySprite.Scale,
                                   SpriteEffects.None, entitySprite.LayerDepth);
        }
    }
}
