using GM.ECSLibrary.Components;

namespace GM.ECSLibrary.Systems
{
    public class TextDrawingSystem : SystemBase
    {
        public override UpdateStage SystemUpdateStage => UpdateStage.PostDraw;

        public TextDrawingSystem()
        {
            _RequiredComponents.AddRange(new[] { typeof(PositionComponent), typeof(TextComponent) });
        }

        protected override void OnUpdate(Entity updatingEntity)
        {
            PositionComponent position = updatingEntity.GetComponent<PositionComponent>();
            TextComponent text = updatingEntity.GetComponent<TextComponent>();

            ManagerCatalog.SharedSpriteBatch.DrawString(ManagerCatalog.DefaultFont, text.Text, position.Position + text.Offset, text.Color);
        }
    }
}
