using GM.ECSLibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GM.ECSLibrary.Systems
{
    public class MouseOperationSystem : SystemBase
    {
        public MouseOperationSystem()
        {
            _RequiredComponents.AddRange(new[] { typeof(PositionComponent), typeof(SpriteComponent), typeof(MouseOperationComponent) });
        }

        public override UpdateStage SystemUpdateStage { get => UpdateStage.PreUpdate; }

        protected override void OnUpdate(Entity updatingEntity)
        {
            MouseState currentMouseState = ManagerCatalog.CurrentMouseState;

            PositionComponent entityPosition = updatingEntity.GetComponent<PositionComponent>();
            SpriteComponent entitySprite = updatingEntity.GetComponent<SpriteComponent>();
            MouseOperationComponent mouseComponent = updatingEntity.GetComponent<MouseOperationComponent>();

            Rectangle boundingRectangle = new Rectangle((int)entityPosition.UpperLeft.X, (int)entityPosition.UpperLeft.Y,
                                                        entitySprite.Texture.Width, entitySprite.Texture.Height);

            if (boundingRectangle.Contains(currentMouseState.Position))
            {
                if (mouseComponent.WasHover)
                {
                    HoverContinue(updatingEntity);
                }
                else
                {
                    mouseComponent.WasHover = true;

                    HoverStart(updatingEntity);
                }

                // Left button
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (mouseComponent.WasLeftClick)
                    {
                        LeftClickContinue(updatingEntity);
                    }
                    else // if old mouse state left button isn't pressed? Currently if the mouse is clicked when it is dragged onto the button, it will initiate a click start!
                    {
                        mouseComponent.WasLeftClick = true;

                        LeftClickStart(updatingEntity);
                    }
                }
                else if (mouseComponent.WasLeftClick)
                {
                    mouseComponent.WasLeftClick = false;

                    LeftClickEnd(updatingEntity);
                }

                // Right button
                if (currentMouseState.RightButton == ButtonState.Pressed)
                {
                    if (mouseComponent.WasRightClick)
                    {
                        RightClickContinue(updatingEntity);
                    }
                    else
                    {
                        mouseComponent.WasRightClick = true;

                        RightClickStart(updatingEntity);
                    }
                }
                else if (mouseComponent.WasRightClick)
                {
                    mouseComponent.WasRightClick = false;

                    RightClickEnd(updatingEntity);
                }
            }
            else
            {
                if (mouseComponent.WasHover)
                {
                    HoverEnd(updatingEntity);
                }

                mouseComponent.WasHover = false;
                mouseComponent.WasLeftClick = false;
                mouseComponent.WasRightClick = false;
            }
        }

        protected virtual void HoverStart(Entity updatingEntity) { }

        protected virtual void HoverContinue(Entity updatingEntity) { }

        protected virtual void HoverEnd(Entity updatingEntity) { }

        protected virtual void LeftClickStart(Entity updatingEntity) { }

        protected virtual void LeftClickContinue(Entity updatingEntity) { }

        protected virtual void LeftClickEnd(Entity updatingEntity) { }

        protected virtual void RightClickStart(Entity updatingEntity) { }

        protected virtual void RightClickContinue(Entity updatingEntity) { }

        protected virtual void RightClickEnd(Entity updatingEntity) { }
    }
}
