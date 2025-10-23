using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Admin_Index
{
    public class _AdminNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
