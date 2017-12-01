using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore
{
    public static class CartHelper
    {
        public const string cartID = "cartID";
        public static Guid? GetCartID(this Controller controller)
        {
            if (controller.Request.Cookies.AllKeys.Contains(cartID))
            {
                if (Guid.TryParse(controller.Request.Cookies[cartID].Value, out Guid result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}