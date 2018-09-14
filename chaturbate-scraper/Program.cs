using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace chaturbate_scraper
{
    class Program
    {

        private static Int32 startPage;
        private static Int32 finishPage;
        private static String genderSelected;
        private static String url;
        private static IEnumerable<object> liness;

        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("Welcome to zer0's chaturbate email scraper - written in c#");
            Console.WriteLine("==========================================================");

            //select gender
            Console.WriteLine("Which gender? (m, f, t, c)");
            Console.Write(">");
            genderSelected = Console.ReadLine();
            //select start and finish page
            Console.WriteLine("Which page would you like to start at?");
            Console.Write(">");
            startPage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Which page would you like to end at?");
            Console.Write(">");
            finishPage = Convert.ToInt32(Console.ReadLine());
            //call the scraper function
            RunScraper();




        }
        
        static void RunScraper()
        {
            Console.WriteLine("[{0}] Extracting url's from each page....", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
            for (int n = 0; n < finishPage; n++)
            {
                if (genderSelected == "m")
                {
                    url = @"https://chaturbate.com/male-cams?page=" + n.ToString();
                }
                if (genderSelected == "f")
                {
                    url = @"https://chaturbate.com/female-cams?page=" + n.ToString();
                }
                if (genderSelected == "t")
                {
                    url = @"https://chaturbate.com/trans-cams?page=" + n.ToString();
                }
                if (genderSelected == "c")
                {
                    url = @"https://chaturbate.com/couple-cams?page=" + n.ToString();
                }

                HtmlAgilityPack.HtmlWeb hw = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = hw.Load(url);
                List<string> htmls = new List<string>();
                foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (hrefValue.Contains("/") && hrefValue.Contains("attachment"))
                        htmls.Add(hrefValue);
                    if (
                        !hrefValue.Contains("/tag/")
                        && !hrefValue.Equals("/")
                        && !hrefValue.Equals("#")
                        && !hrefValue.Contains("/accounts/register/")
                        && !hrefValue.Contains("/tube/")
                        && !hrefValue.Contains("https")
                        && !hrefValue.Contains("/tipping/free_tokens/")
                        && !hrefValue.Contains("/auth/login/")
                        && !hrefValue.Contains("/female-cams/")
                        && !hrefValue.Contains("/male-cams/")
                        && !hrefValue.Contains("/trans-cams/")
                        && !hrefValue.Contains("/supporter/upgrade/")
                        && !hrefValue.Contains("/tags/")
                        && !hrefValue.Contains("/auth/password_reset/")
                        && !hrefValue.Contains("/couple-cams/")
                        && !hrefValue.Contains("/?page=")
                        && !hrefValue.Contains("http://")
                        && !hrefValue.Contains("/teen-cams/")
                        && !hrefValue.Contains("/18to21-cams/")
                        && !hrefValue.Contains("/20to30-cams/")
                        && !hrefValue.Contains("/30to50-cams/")
                        && !hrefValue.Contains("/mature-cams/")
                        && !hrefValue.Contains("/north-american-cams/")
                        && !hrefValue.Contains("/other-region-cams/")
                        && !hrefValue.Contains("/euro-russian-cams/")
                        && !hrefValue.Contains("/asian-cams/")
                        && !hrefValue.Contains("/south-american-cams/")
                        && !hrefValue.Contains("/exhibitionist-cams/")
                        && !hrefValue.Contains("/hd-cams/")
                        && !hrefValue.Contains("/spy-on-cams/")
                        && !hrefValue.Contains("/new-cams/")
                        && !hrefValue.Contains("/6-tokens-per-minute-private-cams/female/")
                        && !hrefValue.Contains("/12-tokens-per-minute-private-cams/female/")
                        && !hrefValue.Contains("/18-tokens-per-minute-private-cams/female/")
                        && !hrefValue.Contains("/30-tokens-per-minute-private-cams/female/")
                        && !hrefValue.Contains("/60-tokens-per-minute-private-cams/female/")
                        && !hrefValue.Contains("/90-tokens-per-minute-private-cams/female/")
                        && !hrefValue.Contains("/terms/")
                        && !hrefValue.Contains("/privacy/")
                        && !hrefValue.Contains("/security/")
                        && !hrefValue.Contains("/law_enforcement/")
                        && !hrefValue.Contains("/billingsupport/")
                        && !hrefValue.Contains("/security/privacy/deactivate/")
                        && !hrefValue.Contains("/apps/")
                        && !hrefValue.Contains("/contest/details/")
                        && !hrefValue.Contains("/affiliates/")
                        && !hrefValue.Contains("/affiliates/")
                        && !hrefValue.Contains("/sitemap/")
                        )
                    {
                        if (genderSelected == "m")
                        {
                            File.AppendAllText("males.txt", Environment.NewLine + hrefValue);
                        }
                        if (genderSelected == "f")
                        {
                            File.AppendAllText("females.txt", Environment.NewLine + hrefValue);
                        }
                        if (genderSelected == "t")
                        {
                            File.AppendAllText("trans.txt", Environment.NewLine + hrefValue);
                        }
                        if (genderSelected == "c")
                        {
                            File.AppendAllText("couples.txt", Environment.NewLine + hrefValue);
                        }

                       
                    }
                }

            }


            // remove and duplicate lines
            Console.WriteLine("[{0}] Removing duplicate lines...", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
            if (genderSelected == "m")
            {
                string[] lines = File.ReadAllLines("males.txt");
                File.WriteAllLines("males.txt", lines.Distinct().ToArray());
            }
            if (genderSelected == "f")
            {
                string[] lines = File.ReadAllLines("females.txt");
                File.WriteAllLines("females.txt", lines.Distinct().ToArray());
            }
            if (genderSelected == "t")
            {
                string[] lines = File.ReadAllLines("trans.txt");
                File.WriteAllLines("trans.txt", lines.Distinct().ToArray());
            }
            if (genderSelected == "c")
            {
                string[] lines = File.ReadAllLines("couples.txt");
                File.WriteAllLines("couples.txt", lines.Distinct().ToArray());
            }


            // start searching for emails on all profiles added to the list
            Console.WriteLine("[{0}] Searching each page for emails...", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));

 
            if (genderSelected == "m")
            {
                liness = File.ReadAllLines("males.txt");
            }
            if (genderSelected == "f")
            {
                liness = File.ReadAllLines("female.txt");
            }
            if (genderSelected == "t")
            {
                liness = File.ReadAllLines("trans.txt");
            }
            if (genderSelected == "c")
            {
                liness = File.ReadAllLines("couples.txt");
            }

            
            foreach (var line in liness)
            {
                String url = @"http://chaturbate.com" + line;


                WebClient client = new WebClient();
                string page = client.DownloadString(url);

                Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                //find items that matches with our pattern
                MatchCollection emailMatches = emailRegex.Matches(page);

                StringBuilder sb = new StringBuilder();

                foreach (Match emailMatch in emailMatches)
                {
                    if (!emailMatch.Value.Contains("highwebmedia.com") && !emailMatch.Value.Contains("chaturbate.com"))
                    {
                        sb.AppendLine(emailMatch.Value);
                        Console.WriteLine("[{0}] Email Found! - {1} @ {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"), emailMatch.Value, line);
                    }
                }
                //store to file
                if (genderSelected == "m")
                {
                    File.AppendAllText("male-emails.txt", sb.ToString());
                }
                if (genderSelected == "f")
                {
                    File.AppendAllText("female-emails.txt", sb.ToString());
                }
                if (genderSelected == "t")
                {
                    File.AppendAllText("trans-emails.txt", sb.ToString());
                }
                if (genderSelected == "c")
                {
                    File.AppendAllText("couple-emails.txt", sb.ToString());
                }
               
            }



        }
    }
}
