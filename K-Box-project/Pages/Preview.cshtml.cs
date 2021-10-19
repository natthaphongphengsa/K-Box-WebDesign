using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K_Box_project.Model;
using K_Box_project.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Box_project.Pages
{
    public class PreviewModel : PageModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobil { get; set; }
        public string Stad { get; set; }
        public DateTime? Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Rum { get; set; }
        public int People { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }

        public void OnGet()
        {
            List<BookInfo> images = new List<BookInfo>();
            images.Add(new BookInfo { type = "Superior", text = "assets/img/Superior.jpg" });
            images.Add(new BookInfo { type = "Premier", text = "assets/img/premier.JPG" });
            images.Add(new BookInfo { type = "VIP", text = "assets/img/vip.jpg" });
            images.Add(new BookInfo { type = "Deluxe", text = "assets/img/Deluxe.jpg" });

            var preview = HttpContext.Session.Get<List<BookInfo>>("informationlist").ToList();
            Firstname = preview.First(u => u.type == "firstname").text;
            Lastname = preview.First(u => u.type == "lastname").text;
            Mobil = preview.First(u => u.type == "mobile").text;
            Stad = preview.First(u => u.type == "stad").text;
            Date = preview.First(u => u.type == "date").datetime;
            TimeStart = preview.First(u => u.type == "timesstart").datetime;
            TimeEnd = preview.First(u => u.type == "timeend").datetime;
            Rum = preview.First(u => u.type == "rum").text;
            Message = preview.First(u => u.type == "message").text;
            Image = images.First(i => i.type == Rum).text;
            People = int.Parse(preview.First(p => p.type == "people").text);

            //DateTime customtime = new DateTime(2021, 05, 25, 00, 00, 00);
            //DateTime beforemidnight = new DateTime(2021, 05, 25, 23, 00, 00);
            //TimeSpan totalttime;
            //if (TimeEnd > customtime)
            //{
            //    //totalttime = beforemidnight - TimeStart;
            //    totalttime = Convert.ToInt32(beforemidnight.Hour) - TimeStart.Hour;
            //}
            //else
            //{
            ViewData["Totalttime"] = TimeEnd - TimeStart;
            //}
        }
    }
}
