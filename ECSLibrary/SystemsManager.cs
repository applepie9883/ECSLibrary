using GM.ECSLibrary.Systems;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GM.ECSLibrary
{
    /// <summary>
    /// Class to manage a collection of <see cref="SystemBase"/> instances.
    /// </summary>
    public class SystemsManager
    {
        public Catalog ManagerCatalog { get; protected set; }

        private Dictionary<Type, SystemBase> Systems { get; set; }
        
        public SystemsManager(GraphicsDevice gDevice, SpriteBatch spriteBatch)
        {
            ManagerCatalog = new Catalog();
            ManagerCatalog.SharedGraphicsDevice = gDevice;
            ManagerCatalog.SharedSpriteBatch = spriteBatch;

            Systems = new Dictionary<Type, SystemBase>();
        }

        public void Update(ICollection<Entity> entityCollection)
        {
            ManagerCatalog.CurrentKeyboardState = Keyboard.GetState();
            ManagerCatalog.CurrentMouseState = Mouse.GetState();

            if (ManagerCatalog.OldKeyboardState != null)
            {
                ManagerCatalog.OldKeyboardState = ManagerCatalog.CurrentKeyboardState;
            }

            if (ManagerCatalog.OldMouseState != null)
            {
                ManagerCatalog.OldMouseState = ManagerCatalog.OldMouseState;
            }

            if (entityCollection.Count > 0)
            {
                for (UpdateStage currentStage = UpdateStage.PreUpdate; currentStage <= UpdateStage.PostUpdate; currentStage++)
                {
                    List<SystemBase> compatibleSystems = Systems.Values.Where(i => i.SystemUpdateStage == currentStage).ToList();

                    if (compatibleSystems.Count > 0)
                    {
                        foreach (SystemBase entitySystem in compatibleSystems)
                        {
                            foreach (Entity currentEntity in entityCollection)
                            {
                                entitySystem.Update(currentEntity);
                            }
                        }
                    }
                }
            }

            ManagerCatalog.OldKeyboardState = ManagerCatalog.CurrentKeyboardState;
            ManagerCatalog.OldMouseState = ManagerCatalog.CurrentMouseState;
        }

        public void Draw(ICollection<Entity> entityCollection)
        {
            if (entityCollection.Count <= 0) return;

            for (UpdateStage currentStage = UpdateStage.PreDraw; currentStage <= UpdateStage.PostDraw; currentStage++)
            {
                List<SystemBase> compatibleSystems = Systems.Values.Where(i => i.SystemUpdateStage == currentStage).ToList();

                if (compatibleSystems.Count > 0)
                {
                    ManagerCatalog.SharedSpriteBatch.Begin(SpriteSortMode.FrontToBack);

                    foreach (SystemBase entitySystem in compatibleSystems)
                    {
                        foreach (Entity currentEntity in entityCollection)
                        {
                            entitySystem.Update(currentEntity);
                        }
                    }

                    ManagerCatalog.SharedSpriteBatch.End();
                }
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
            // TODO: Should I return null, or let it throw an error?
            if (!Systems.ContainsKey(typeof(T))) return null;

            return (T)Systems[typeof(T)];
        }
    }
}
