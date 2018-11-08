using GM.ECSLibrary.Components;
using System;
using System.Collections.Generic;

namespace GM.ECSLibrary
{
    /// <summary>
    /// Main class for storing and managing components for use by systems.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Unique string identifier for this entity
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Private dictionary of components, with the component <see cref="Type"/> as the keys.
        /// </summary>
        private Dictionary<Type, ComponentBase> Components { get; set; }

        // TODO: When I add a remove component method, I will have to remember to clear this list every time it is called.
        // TODO: Do I really want to remove components ever though? I think entities should never lose their components
        public HashSet<Type> VerifiedSystems { get; set; }

        public HashSet<Type> UnverifiedSystems { get; set; }

        // These are the only systems allowed to run on the entity (note that component checking is still done)
        public HashSet<Type> WhitelistSystems { get; set; }

        // These are the systems that are not allowed run on the entity whether they have the required components or not
        public HashSet<Type> BlacklistSystems { get; set; }

        /// <summary>
        /// Default constructor, initializes the <see cref="Components"/> dictionary.
        /// </summary>
        public Entity()
        {
            Id = Guid.NewGuid().ToString();

            Components = new Dictionary<Type, ComponentBase>();

            VerifiedSystems = new HashSet<Type>();
            UnverifiedSystems = new HashSet<Type>();
            WhitelistSystems = new HashSet<Type>();
            BlacklistSystems = new HashSet<Type>();
        }

        /// <summary>
        /// Add a component to the entity's component dictionary with the <see cref="Type"/> of the component as the key.
        /// </summary>
        /// <param name="component">The component to add to the dictionary.</param>
        /// <exception cref="ArgumentException">Thrown when a component is added of the same <see cref="Type"/> as a component alread in the dictionary.</exception>
        public void AddComponent(ComponentBase component)
        {
            // TODO: Don't add if there is already another of the same component. Throw an error, or just don't do it, I don't know.
            Components.Add(component.GetType(), component);
            UnverifiedSystems.Clear();
        }

        /// <summary>
        /// Get a certain component from the entity.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the component to get from the entity. Must be a subclass of <see cref="ComponentBase"/></typeparam>
        /// <returns>The requested component, or null if no component of that <see cref="Type"/> exists.</returns>
        public T GetComponent<T>() where T : ComponentBase
        {
            if (!Components.ContainsKey(typeof(T))) return null;

            return (T)Components[typeof(T)];
        }

        // TODO: Think about removing this method. Users should probably not be given full access to the component list, AddComponent and GetComponent should be enough.
        // TODO: So, make this use the same kind of thing systembase does, with a read only dict and all that.
        /// <summary>
        /// Get the list of all components in the entity.
        /// </summary>
        /// <returns>The dictionary of components in the entity.</returns>
        public Dictionary<Type, ComponentBase> GetComponents()
        {
            return Components;
        }
    }
}