using System;
using System.Linq;
using System.Web;
using log4net;

namespace FirstDemo.Custom.Utils
{
    public class CommonUtils
    {
        public static string ExtractYoutubeVideoId(string url)
        {
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);

            var videoId = string.Empty;

            if (query.AllKeys.Contains("v"))
            {
                videoId = query["v"];
            }
            else
            {
                videoId = uri.Segments.Last();
            }
            return videoId;
        }
    }
}