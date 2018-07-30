using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace BreadCrumbTrail
{
    public static class BreadCrumbTrails
    {
        /// <summary>
        /// Session cache - used to store session property for bread crumb trail.
        /// </summary>
        private static Dictionary<string, HttpSessionStateBase> SessionDict = new Dictionary<string, HttpSessionStateBase>();

        /// <summary>
        /// Default bread crumb session identifier.
        /// </summary>
        public static string BreadcrumbIdentifier = "BreadTrail";

        /// <summary>
        /// Register a new session.
        /// </summary>
        /// <param name="session"></param>
        public static void RegisterSession(HttpSessionStateBase session)
        {
            // Add session if it's unique
            if (!SessionDict.ContainsKey(session.SessionID))
            {
                // Register session and create new trail
                session[BreadcrumbIdentifier] = new Trail();
                SessionDict.Add(session.SessionID, session);
            }
            else
            {
                // Create a new bread crumb trail if the session property doesn't exist
                if (session[BreadcrumbIdentifier] == null)
                {
                    session[BreadcrumbIdentifier] = new Trail();
                }
            }

            // Get expired sessions
            var expiredSessions = SessionDict.Where(x => x.Value[BreadcrumbIdentifier] == null);

            // Clear expired sessions from the session dict
            for (int i = expiredSessions.Count(); i > 0; i--)
            {
                SessionDict.Remove(expiredSessions.ElementAt(i).Key);
            }
        }

        /// <summary>
        /// Get if a session has been registered.
        /// </summary>
        /// <param name="sessionID">Target Session ID to check.</param>
        /// <returns>True if session is registered.</returns>
        public static bool IsRegistered(string sessionID)
        {
            return SessionDict.ContainsKey(sessionID);
        }

        /// <summary>
        /// Get a trail for the session.
        /// </summary>
        /// <param name="sessionID">Target Session ID</param>
        /// <returns>Get the HTML trail for the session.</returns>
        public static Trail GetTrail(string sessionID)
        {
            return (Trail)SessionDict[sessionID][BreadcrumbIdentifier];
        }


        /// <summary>
        /// Render the session crumbs.
        /// </summary>
        /// <param name="sessionID">Session ID.</param>
        /// <param name="css">Custom CSS for breadcrumbs.</param>
        /// <returns>HTML</returns>
        public static string Display(string sessionID, string css = "breadcrumbs")
        {

            var trail = GetTrail(sessionID);

            if (trail.Count == 0)
                return "<!-- BreadCrumbs stack is empty -->";

            StringBuilder sb = new StringBuilder();
            sb.Append("<ol class=\"");
            sb.Append(css);
            sb.Append("\">");

            uint crumbCount = trail.Count;
            uint crumbIndex = 0;

            foreach (var crumb in trail.GetCrumbs())
            {
                crumbIndex++;

                string label = crumb.Label;

                if (crumbIndex >= crumbCount)
                {
                    sb.Append("<li class='active'>" + label + "</li>");
                }
                else
                {
                    sb.Append("<li><a href=\"" + crumb.URL + "\">" + label + "</a></li>");
                }

            };

            sb.Append("</ol>");
            return sb.ToString();

        }
    }
}
