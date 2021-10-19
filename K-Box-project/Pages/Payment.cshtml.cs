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
    public class PaymentModel : PageModel
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
        public int Totaltprice { get; set; }
        public int PricePerHour { get; set; }
        public string Epost { get; set; }


        public void OnGet()
        {
            List<BookInfo> images = new List<BookInfo>();
            images.Add(new BookInfo { type = "Superior", text = "assets/img/Superior.jpg" });
            images.Add(new BookInfo { type = "Premier", text = "assets/img/premier.JPG" });
            images.Add(new BookInfo { type = "VIP", text = "assets/img/vip.jpg" });
            images.Add(new BookInfo { type = "Deluxe", text = "assets/img/Deluxe.jpg" });

            List<BookInfo> rumprices = new List<BookInfo>();
            rumprices.Add(new BookInfo { type = "Superior", text = "150", reaprice = 400 });
            rumprices.Add(new BookInfo { type = "Premier", text = "200", reaprice = 550 });
            rumprices.Add(new BookInfo { type = "VIP", text = "380", reaprice = 1090 });
            rumprices.Add(new BookInfo { type = "Deluxe", text = "330", reaprice = 950 });

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
            Epost = preview.First(e => e.type == "epost").text;

            var totalttime = TimeEnd > TimeStart ? TimeEnd - TimeStart : TimeEnd.AddDays(1) - TimeStart;
            ViewData["Totalttime"] = totalttime;

            PricePerHour = int.Parse(rumprices.First(r => r.type == $"{Rum}").text);
            if (totalttime.Hours < 03)
            {
                int price = int.Parse(rumprices.First(p => p.type == $"{Rum}").text);
                Totaltprice = totalttime.Hours * price + 49;
            }
            else if (totalttime.Hours == 03)
            {
                ViewData["Reapris"] = $"Rea pris på 3 timmar: {rumprices.First(p => p.type == $"{Rum}").reaprice} kr";
                Totaltprice = rumprices.First(p => p.type == $"{Rum}").reaprice + 49;
            }
            switch (Rum)
            {
                case "Superior":

                    break;
                case "Premier":
                    break;
                case "VIP":
                    break;
                case "Deluxe":
                    break;
            }
        }
    }
}
