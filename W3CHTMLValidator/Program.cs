using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace W3CHTMLValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = @"http://lunchbreakcoding.com/";
            string[] errors = ValidatePage(address);

            Console.WriteLine("Found " + errors.Length + " HTML errors in the page, " + address);
            foreach (string error in errors)
            {
                Console.WriteLine("\r\n***ERROR** " + error);
            }

            Console.ReadLine();
        }

        static string[] ValidatePage(string address)
        {

            HtmlDocument hDoc = new HtmlDocument();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://validator.w3.org/check?uri=" + address);
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.57 Safari/537.36";
            StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream());

            hDoc.LoadHtml(sr.ReadToEnd());

            string[] errors = hDoc.DocumentNode.Descendants("li").Where(li => li.GetAttributeValue("class", "") == "msg_err").Select(li => li.InnerText.Trim()).ToArray();

            return errors;
        }
    }
}
