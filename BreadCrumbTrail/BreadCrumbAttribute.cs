using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BreadCrumbTrail
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class BreadcrumbAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Clear all crumbs before this crumb.
        /// </summary>
        public bool Clear { get; set; }

        /// <summary>
        /// Set the label for this crumb.
        /// </summary>
        public string Label { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string sessionID = filterContext.HttpContext.Session.SessionID;

            if (filterContext.IsChildAction)
                return;

            if (filterContext.HttpContext.Request.HttpMethod != "GET")
                return;

            // Handle the bread crumb registration for the session
            if (!BreadCrumbTrails.IsRegistered(sessionID))
            {
                BreadCrumbTrails.RegisterSession(filterContext.RequestContext.HttpContext.Session);
            }

            string url = HttpContext.Current.Request.Url.ToString();


            // Clear the trail for the session if flagged
            if (Clear)
            {
                BreadCrumbTrails.GetTrail(sessionID).Clear();
            }

            // Get the current trail for this session
            var trail = BreadCrumbTrails.GetTrail(sessionID);

            if (trail.CrumbExistsName(Label))
            {
                // Select the crumb
                trail.SelectCrumbByName(Label);

                // Update the crumbs url
                trail.GetCrumbFromName(Label).URL = url;
            }
            else if (trail.CrumbExistsURL(url))
            {
                // Select the crumb
                trail.SelectCrumbByName(trail.GetCrumbFromURL(url).Label);
            }
            else
            {
                // Add the new crumb
                BreadCrumbTrails.GetTrail(sessionID).AddCrumb(url, Label);
            }
        }
    }
}
