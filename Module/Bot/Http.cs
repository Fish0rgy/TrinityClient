using System.Collections.Specialized;
using System.Net;

namespace Area51.Module.Bot
{
    class Http
    {
        public Http()
        {
        }

        public static byte[] Post(string uri, NameValueCollection input)
        {
            byte[] data;
            using (WebClient webClient = new WebClient())
            {
                data = webClient.UploadValues(uri, input);
            }
            return data;
        }
    }
}
