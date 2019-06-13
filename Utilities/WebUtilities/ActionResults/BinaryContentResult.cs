using System.Web;
using System.Web.Mvc;

namespace AIT.WebUtilities.ActionResults
{
    public class BinaryContentResult : ActionResult
    {
        private string _contentType;
        private byte[] _contentBytes;

        public BinaryContentResult(byte[] contentBytes, string contentType)
        {
            _contentBytes = contentBytes;
            _contentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = _contentType;

            context.HttpContext.Response.BinaryWrite(_contentBytes);
            context.HttpContext.Response.End();
        }
    }
}
