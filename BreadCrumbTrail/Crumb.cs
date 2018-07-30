using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCrumbTrail
{
    public sealed class Crumb
    {
        /// <summary>
        /// Display label from the crumb.
        /// </summary>
        public string Label;

        /// <summary>
        /// URL the crumb is pointing to.
        /// </summary>
        public string URL;

        /// <summary>
        /// Whether or not the crumb is selected or not.
        /// </summary>
        public bool Selected;

        /// <summary>
        /// Create a new breadcrumb.
        /// </summary>
        /// <param name="label">Display label for the crumb.</param>
        /// <param name="url">URL for the crumb to point to.</param>
        /// <param name="selected">Whether or not the crumb is selected.</param>
        public Crumb(string label, string url, bool selected = true)
        {
            Label = label;
            URL = url;
            Selected = selected;
        }
    }
}
