using GM.ECSLibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GM.ECSLibrary.Systems
{
    public class MouseEventSystem : SystemBase
    {
        public MouseEventSystem()
        {
            _RequiredComponents.AddRange(new[] { typeof(PositionComponent), typeof(SpriteComponent), typeof(MouseEventComponent) });
        }

        public override UpdateStage SystemUpdateStage { get => UpdateStage.PreUpdate; }

        protected override void OnUpdate(Entity updatingEntity)
        {
            MouseState currentMouseState = ManagerCatalog.CurrentMouseState;

            PositionComponent entityPosition = updatingEntity.GetComponent<PositionComponent>();
            SpriteComponent entitySprite = updatingEntity.GetComponent<SpriteComponent>();
            MouseEventComponent eventComponent = updatingEntity.GetComponent<MouseEventComponent>();

            Rectangle boundingRectangle = new Rectangle((int)entityPosition.UpperLeft.X, (int)entityPosition.UpperLeft.Y,
                                                        entitySprite.Texture.Width, entitySprite.Texture.Height);

            if (boundingRectangle.Contains(currentMouseState.Position))
            {
                // Left button
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (eventComponent.WasLeftClick)
                    {
                        eventComponent.LeftClickContinue(updatingEntity);
                    }
                    else
                    {
                        eventComponent.WasLeftClick = true;

                        eventComponent.LeftClickStart(updatingEntity);
                    }
                }
                else if (eventComponent.WasLeftClick)
                {
                    eventComponent.WasLeftClick = false;

                    eventComponent.LeftClickEnd(updatingEntity);
                }

                // Right button
                if (currentMouseState.RightButton == ButtonState.Pressed)
                {
                    if (eventComponent.WasRightClick)
                    {
                        eventComponent.RightClickContinue(updatingEntity);
                    }
                    else
                    {
                        eventComponent.WasRightClick = true;

                        eventComponent.RightClickStart(updatingEntity);
                    }
                }
                else if (eventComponent.WasRightClick)
                {
                    eventComponent.WasRightClick = false;

                    eventComponent.RightClickEnd(updatingEntity);
                }
            }
            else
            {
                eventComponent.WasLeftClick = false;
                eventComponent.WasRightClick = false;
            }
        }
    }
}
