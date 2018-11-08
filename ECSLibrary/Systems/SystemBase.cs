using GM.ECSLibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GM.ECSLibrary.Systems
{
    /// <summary>
    /// The abstract base class that every system must inherit from. It is reccomended to use a <see cref="SystemsManager"/> for all systems.
    /// </summary>
    public abstract class SystemBase
    {
        // TODO: Rename ManagerCatalog? If users don't want to use a SystemsManager, the name wouldn't make any sense.
        protected Catalog ManagerCatalog { get; set; }

        /// <summary>
        /// Indicates when in the update / draw cycle this system should be updated.
        /// </summary>
        public abstract UpdateStage SystemUpdateStage { get; }

        /// <summary>
        /// Protected writable version of RequiredComponents.
        /// </summary>
        protected List<Type> _RequiredComponents { get; set; }

        /// <summary>
        /// Read only collection containing the <see cref="Type"/> of the components required by the system for an entity to update.
        /// </summary>
        /// <value>
        /// RequiredComponents reflects changes made to the private field <see cref="_RequiredComponents"/>
        /// </value>
        public IReadOnlyCollection<Type> RequiredComponents { get; }

        /// <summary>
        /// Indicates whether or not the system should populate entities it is run on with the required components. Defaults to false. 
        /// </summary>
        public bool AutoCreateComponents { get; set; }

        /// <summary>
        /// Default constructor, initializes <see cref="RequiredComponents"/> and <see cref="AutoCreateComponents"/>.
        /// </summary>
        public SystemBase()
        {
            _RequiredComponents = new List<Type>();
            RequiredComponents = _RequiredComponents.AsReadOnly();

            AutoCreateComponents = false;
        }

        public void SetCatalog(Catalog catalog)
        {
            ManagerCatalog = catalog;

            OnSetCatalog();
        }

        // TODO: This method may be completely useless. If I don't do anything with it for a little while longer, consider removing it.
        protected virtual void OnSetCatalog()
        {

        }

        /// <summary>
        /// Update method for the system. Verifies that the entity can be updated by the system, then updates.
        /// </summary>
        /// <param name="updatingEntity">The entity for the system to run on.</param>
        public void Update(Entity updatingEntity)
        {
            // If the entity doesn't have the required components for the system to run
            if (!HasRequiredComponents(updatingEntity))
            {
                if (AutoCreateComponents)
                {
                    CreateRequiredComponents(updatingEntity);
                }
                else
                {
                    return;
                }
            }

            OnUpdate(updatingEntity);
        }

        /// <summary>
        /// Update method for the system. Calls <see cref="Update(Entity)"/> for every entity in the collection.
        /// </summary>
        /// <param name="updatingEntities">The entities for the system to run on.</param>
        public void Update(ICollection<Entity> updatingEntities)
        {
            foreach (Entity currentEntity in updatingEntities)
            {
                Update(currentEntity);
            }
        }

        /// <summary>
        /// Called after the entity is verified to have all of the correct components by the public <see cref="Update(Entity)"/> method
        /// </summary>
        /// <param name="updatingEntity">The entity for the system to run on.</param>
        protected abstract void OnUpdate(Entity updatingEntity);

        /// <summary>
        /// Checks whether the given entity has all of the component types required by the system to run.
        /// </summary>
        /// <param name="checkingEntity">The entity to check.</param>
        /// <returns>true if the entity has the required components, false otherwise.</returns>
        protected bool HasRequiredComponents(Entity checkingEntity)
        {
            if (checkingEntity.BlacklistSystems.Contains(GetType()) || (checkingEntity.WhitelistSystems.Count > 0 && !checkingEntity.WhitelistSystems.Contains(GetType())))
            {
                return false;
            }

            // If the entity has already been verified to have the correct components for this system, return true.
            if (checkingEntity.UnverifiedSystems.Contains(GetType()))
            {
                return false;
            }
            else if (checkingEntity.VerifiedSystems.Contains(GetType()))
            {
                return true;
            }
            else
            {
                // If the system isn't in either list, check all components of the entity to determine if it has the required components.

                Dictionary<Type, ComponentBase> entityComponents = checkingEntity.GetComponents();

                foreach (Type componentType in RequiredComponents)
                {
                    // If the entity doesn't contain any one of the components, return false.
                    if (!entityComponents.ContainsKey(componentType))
                    {
                        checkingEntity.UnverifiedSystems.Add(GetType());
                        return false;
                    }
                }

                // All components passed, so return true.
                checkingEntity.VerifiedSystems.Add(GetType());
                return true;
            }
        }

        /// <summary>
        /// Creates any missing components in the entity that the system requires to run.
        /// </summary>
        /// <param name="creatingEntity">The entity to create components in.</param>
        public void CreateRequiredComponents(Entity creatingEntity)
        {
            Dictionary<Type, ComponentBase> entityComponents = creatingEntity.GetComponents();

            foreach (Type componentType in RequiredComponents)
            {
                // Make sure we aren't adding a component that the entity already has
                if (!entityComponents.ContainsKey(componentType))
                {
                    ComponentBase component = (ComponentBase)Activator.CreateInstance(componentType);
                    creatingEntity.AddComponent(component);
                }
            }
        }
    }
}
