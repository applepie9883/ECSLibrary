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

        /// <summary>
        /// Default constructor, initializes the <see cref="Components"/> dictionary.
        /// </summary>
        public Entity()
        {
            Id = Guid.NewGuid().ToString();

            Components = new Dictionary<Type, ComponentBase>();
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
        }

        /// <summary>
        /// Get the list of all components in the entity.
        /// </summary>
        /// <returns>The dictionary of components in the entity.</returns>
        public Dictionary<Type, ComponentBase> GetComponents()
        {
            return Components;
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
    }
}