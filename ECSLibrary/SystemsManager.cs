using GM.ECSLibrary.Systems;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GM.ECSLibrary
{
    /// <summary>
    /// Class to manage a collection of <see cref="SystemBase"/> instances.
    /// </summary>
    public class SystemsManager
    {
        public Catalog ManagerCatalog { get; protected set; }

        private Dictionary<Type, SystemBase> Systems { get; set; }
        
        public SystemsManager()
        {
            ManagerCatalog = new Catalog();
            Systems = new Dictionary<Type, SystemBase>();
        }

        public void Update(ICollection<Entity> entityCollection)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();

            ManagerCatalog.SetEntry("CurrentKeyboardState", currentKeyboardState);
            ManagerCatalog.SetEntry("CurrentMouseState", currentMouseState);

            if (!ManagerCatalog.HasEntry("OldKeyboardState"))
            {
                ManagerCatalog.SetEntry("OldKeyboardState", currentKeyboardState);
            }

            if (!ManagerCatalog.HasEntry("OldMouseState"))
            {
                ManagerCatalog.SetEntry("OldMouseState", currentMouseState);
            }

            if (entityCollection.Count > 0)
            {
                for (UpdateStage currentStage = UpdateStage.PreUpdate; currentStage <= UpdateStage.PostUpdate; currentStage++)
                {
                    foreach (SystemBase entitySystem in Systems.Values)
                    {
                        if (entitySystem.SystemUpdateStage == currentStage)
                        {
                            foreach (Entity currentEntity in entityCollection)
                            {
                                entitySystem.Update(currentEntity);
                            }
                        }
                    }
                }
            }

            ManagerCatalog.SetEntry("OldKeyboardState", currentKeyboardState);
            ManagerCatalog.SetEntry("OldMouseState", currentMouseState);
        }

        public void Draw(ICollection<Entity> entityCollection)
        {
            if (entityCollection.Count <= 0) return;

            for (UpdateStage currentStage = UpdateStage.PreDraw; currentStage <= UpdateStage.PostDraw; currentStage++)
            {
                ManagerCatalog.GetEntry<Microsoft.Xna.Framework.Graphics.SpriteBatch>("SpriteBatch").Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.FrontToBack);

                foreach (SystemBase entitySystem in Systems.Values)
                {
                    if (entitySystem.SystemUpdateStage == currentStage)
                    {
                        foreach (Entity currentEntity in entityCollection)
                        {
                            entitySystem.Update(currentEntity);
                        }
                    }
                }

                ManagerCatalog.GetEntry<Microsoft.Xna.Framework.Graphics.SpriteBatch>("SpriteBatch").End();
            }
        }

        /// <summary>
        /// Gives every entity in the collection all of the components required for every system in the manager to run.
        /// </summary>
        /// <param name="entityCollection">The collection of entities to create components in.</param>
        public void CreateRequiredComponents(ICollection<Entity> entityCollection)
        {
            if (entityCollection.Count > 0)
            {
                foreach (SystemBase entitySystem in Systems.Values)
                {
                    foreach (Entity currentEntity in entityCollection)
                    {
                        entitySystem.CreateRequiredComponents(currentEntity);
                    }
                }
            }
        }

        public void AddSystem(SystemBase system)
        {
            system.SetCatalog(ManagerCatalog);
            Systems.Add(system.GetType(), system);
        }

        public Dictionary<Type, SystemBase> GetSystems()
        {
            return Systems;
        }

        public T GetSystem<T>() where T : SystemBase
        {
            if (!Systems.ContainsKey(typeof(T))) return null;

            return (T)Systems[typeof(T)];
        }
    }
}
