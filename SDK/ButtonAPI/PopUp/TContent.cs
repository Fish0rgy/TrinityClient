using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.SDK.ButtonAPI.PopUp
{
    internal class TContent
    {
        public static List<TContent> toastcontent = new List<TContent>(){new TContent{
           Title = "Trinity", Content = "Finished Creating Buttons For Client!"}};
        public string Title { get; set; }
        public string Content { get; set; }
        public TContent CopyWithNewContent(string Content)
        {
            TContent newToastContent = (TContent)this.MemberwiseClone();
            newToastContent.Content = Content;
            return newToastContent;
        }
    }
}
 