using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Admin_Index
{
    public class _AdminSidebarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
