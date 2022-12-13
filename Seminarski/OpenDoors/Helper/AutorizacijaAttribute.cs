using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenDoors.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool vlasnik, bool krajnjiKorisnik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _vlasnik;
        private readonly bool _krajnjiKorisnik;


        public MyAuthorizeImpl(bool vlasnik, bool krajnjiKorisnik)
        {
            _vlasnik = vlasnik;
            _krajnjiKorisnik = krajnjiKorisnik;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.GetLoginInfo().isLogiran)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            KretanjePoSistemu.Save(filterContext.HttpContext);

            if (filterContext.HttpContext.GetLoginInfo().isLogiran)
            {
                return;//ok - ima pravo pristupa
            }


            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }
    }
}

