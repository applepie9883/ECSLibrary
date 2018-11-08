namespace GM.ECSLibrary.Components
{
    public class MouseOperationComponent : ComponentBase
    {
        public bool WasHover { get; set; }

        public bool WasLeftClick { get; set; }

        public bool WasRightClick { get; set; }

        public MouseOperationComponent()
        {
            WasHover = false;
            WasLeftClick = false;
            WasRightClick = false;
        }
    }
}
