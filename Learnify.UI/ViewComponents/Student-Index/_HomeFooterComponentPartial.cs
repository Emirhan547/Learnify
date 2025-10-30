using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Student_Index
{
    public class _HomeFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}