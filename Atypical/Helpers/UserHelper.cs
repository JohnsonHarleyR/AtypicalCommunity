using System.Web;
using System.IO;

namespace Atypical.Web.Helpers
{
    public static class UserHelper
    {
        // Returns true if successful
        public static bool SaveImage(HttpPostedFileBase file)
        {

            if (file != null)
            {
                string path = Path.Combine(System.Web.HttpContext.Current
                    .Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                return true;
            }
            return false;
        }
    }
}