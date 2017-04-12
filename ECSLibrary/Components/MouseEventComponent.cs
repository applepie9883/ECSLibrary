using System;

namespace GM.ECSLibrary.Components
{
    public class MouseEventComponent : ComponentBase
    {
        public event EntityEventHandler OnLeftClickStart;

        public event EntityEventHandler OnLeftClickContinue;

        public event EntityEventHandler OnLeftClickEnd;

        public event EntityEventHandler OnRightClickStart;

        public event EntityEventHandler OnRightClickContinue;

        public event EntityEventHandler OnRightClickEnd;

        public bool WasLeftClick { get; set; }

        public bool WasRightClick { get; set; }

        public MouseEventComponent()
        {
            WasLeftClick = false;
            WasRightClick = false;
        }

        public void LeftClickStart(Entity parent)
        {
            if (OnLeftClickStart != null)
            {
                OnLeftClickStart(parent);
            }
        }

        public void LeftClickContinue(Entity parent)
        {
            if (OnLeftClickContinue != null)
            {
                OnLeftClickContinue(parent);
            }
        }

        public void LeftClickEnd(Entity parent)
        {
            if (OnLeftClickEnd != null)
            {
                OnLeftClickEnd(parent);
            }
        }

        public void RightClickStart(Entity parent)
        {
            if (OnRightClickStart != null)
            {
                OnRightClickStart(parent);
            }
        }

        public void RightClickContinue(Entity parent)
        {
            if (OnRightClickContinue != null)
            {
                OnRightClickContinue(parent);
            }
        }

        public void RightClickEnd(Entity parent)
        {
            if (OnRightClickEnd != null)
            {
                OnRightClickEnd(parent);
            }
        }
    }
}
