using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCrumbTrail
{
    public sealed class Trail
    {
        // List of crumbs.
        private List<Crumb> CrumbList;

        /// <summary>
        /// Number of crumbs in the trail.
        /// </summary>
        public uint Count
        {
            get
            {
                return (uint)CrumbList.Count;
            }
        }

        /// <summary>
        /// Create a new trail for crumbs.
        /// </summary>
        public Trail()
        {
            CrumbList = new List<Crumb>();
        }

        /// <summary>
        /// Dynamically set the label of the current crumb.
        /// </summary>
        /// <param name="label">New label for the current crumb.</param>
        public void SetDynamicLabel(string label)
        {
            CrumbList[CrumbList.Count - 1].Label = label;
        }

        /// <summary>
        /// Add a new crumb.
        /// </summary>
        /// <param name="url">Crumb url.</param>
        /// <param name="label">Label of crumb to display.</param>
        public void AddCrumb(string url, string label)
        {
            CrumbList.Add(new Crumb(label, url));
        }

        /// <summary>
        /// Get a crumb by it's url.
        /// </summary>
        /// <param name="url">Url of crumb.</param>
        /// <returns>Crumb instance or null.</returns>
        public Crumb GetCrumbFromURL(string url)
        {
            return CrumbList.FirstOrDefault(x => x.URL == url);
        }

        /// <summary>
        /// Get a crumb from its name.
        /// </summary>
        /// <param name="name">Name of crumb.</param>
        /// <returns>Crumb instance or null.</returns>
        public Crumb GetCrumbFromName(string name)
        {
            return CrumbList.FirstOrDefault(x => x.Label == name);
        }

        /// <summary>
        /// Check if a crumb exists based on a url.
        /// </summary>
        /// <param name="url">Url of crumb to check.</param>
        /// <returns>True of crumb exists.</returns>
        public bool CrumbExistsURL(string url)
        {
            return CrumbList.FirstOrDefault(x => x.URL == url) != null;
        }

        /// <summary>
        /// Check if a crumb exists based on a name.
        /// </summary>
        /// <param name="name">Name of crumb to check.</param>
        /// <returns>True of crumb exists.</returns>
        public bool CrumbExistsName(string name)
        {
            return CrumbList.FirstOrDefault(x => x.Label == name) != null;
        }

        /// <summary>
        /// Select a crumb and remove all other crumbs ahead of this one.
        /// </summary>
        /// <param name="name">Name of crumb to select.</param>
        public void SelectCrumbByName(string name)
        {
            int crumbIndex = 0;

            // Get the crumb index
            for (int i = 0; i < CrumbList.Count; i++)
            {
                // Deselect crumbs
                CrumbList[i].Selected = false;

                if (CrumbList[i].Label == name)
                {
                    // Select this crumb
                    CrumbList[i].Selected = true;
                    crumbIndex = i;
                    break;
                }
            }

            // Remove all crumb up untill this point
            for (int i = CrumbList.Count - 1; i > crumbIndex; i--)
            {
                CrumbList.RemoveAt(i);
            }
        }

        /// <summary>
        /// Get all crumbs.
        /// </summary>
        /// <returns></returns>
        public List<Crumb> GetCrumbs()
        {
            return CrumbList;
        }

        /// <summary>
        /// Clear the bread crumbs.
        /// </summary>
        public void Clear()
        {
            CrumbList.Clear();
        }
    }
}
