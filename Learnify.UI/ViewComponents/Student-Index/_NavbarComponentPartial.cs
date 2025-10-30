using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Student_Index
{
    public class _NavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}