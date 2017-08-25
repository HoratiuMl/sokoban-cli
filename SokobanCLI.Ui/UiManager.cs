﻿using System.Collections.Generic;
using System.Linq;

using SokobanCLI.Ui.UiElements;

namespace SokobanCLI.Ui
{
    /// <summary>
    /// GUI manager.
    /// </summary>
    public class UiManager
    {
        static volatile UiManager instance;
        static object syncRoot = new object();

        /// <summary>
        /// Gets or sets the GUI elements.
        /// </summary>
        /// <value>The GUI elements.</value>
        public List<UiElement> UiElements { get; set; }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static UiManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UiManager();
                        }
                    }
                }

                return instance;
            }
        }

        public UiManager()
        {
           UiElements = new List<UiElement>();
        }


        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
           UiElements.ToList().ForEach(w => w.LoadContent());
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent()
        {
           UiElements.ForEach(w => w.UnloadContent());
           UiElements.Clear();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public virtual void Update(float gameTime)
        {
            UiElements.RemoveAll(e => e.IsDisposed);
            UiElements.Where(e => e.Enabled).ToList().ForEach(e => e.Update(gameTime));
        }

        /// <summary>
        /// Draws the content.
        /// </summary>
        public virtual void Draw()
        {
            UiElements.Where(e => e.Visible).ToList().ForEach(e => e.Draw());
        }

        /// <summary>
        /// Focuses the input on the element with the specified identifier.
        /// </summary>
        /// <param name="id">Element identifier.</param>
        public void FocusElement(string id)
        {
           UiElements.ForEach(e => e.InputFocus = false);
           UiElements.FirstOrDefault(e => e.Id == id).InputFocus = true;
        }

        /// <summary>
        /// Focuses the input on the specified element.
        /// </summary>
        /// <param name="element">Element.</param>
        public void FocusElement(UiElement element)
        {
            FocusElement(element.Id);
        }
    }
}
