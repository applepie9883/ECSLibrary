namespace GM.ECSLibrary.Systems
{
    /// <summary>
    /// Enum used by systems to tell the system manager at what stage to update them.
    /// </summary>
    public enum UpdateStage
    {
        /// <summary>
        /// Update just before the main Update stage. Most user input is processed in the PreUpdate stage. This is the first update in a cycle.
        /// </summary>
        PreUpdate,

        /// <summary>
        /// Main Update stage.
        /// </summary>
        Update,

        /// <summary>
        /// Update just after the main Update stage, and before the PreDraw stage.
        /// </summary>
        PostUpdate,

        /// <summary>
        /// Update just before the main Draw stage, and after the PostUpdate stage.
        /// </summary>
        PreDraw,

        /// <summary>
        /// Main Draw stage.
        /// </summary>
        Draw,

        /// <summary>
        /// Update just after the main Draw stage. This is the last update in a cycle.
        /// </summary>
        PostDraw
    }
}