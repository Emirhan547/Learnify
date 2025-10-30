using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Student_Index
{
    public class _HomeCarouselComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
