# Bread Crumb Trails #

A simplistic approach to bread crumbs in ASP.NET MVC.

## Setup ##


Place:

```C#
@Html.Raw(BreadCrumbTrails.Display(Session.SessionID))
```

Within the _Layout.cshtml.



## Example Bread Crumb Trail ##

General usage.

```C#
[BreadCrumbAttribute(Label = "Index")]
public ActionResult Index()
{
    return View();
}
```

Clear the current bread crumb trail.

```C#
[BreadCrumbAttribute(Clear = true, Label = "Help")]
public ActionResult Contact()
{
    ViewBag.Message = "Your contact page.";

    return View();
}
```

Dynamically set the label.

```C#
[BreadCrumbAttribute(Label = "About")]
public ActionResult About()
{
    ViewBag.Message = "Your application description page.";
    Breadcrumbs.Breadcrumbs.GetTrail(Session.SessionID).SetDynamicLabel("Hello, World");

    return View();
}
```