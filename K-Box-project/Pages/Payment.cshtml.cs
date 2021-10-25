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
        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Rum { get; set; }
        public int People { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public decimal Totaltprice { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ReaPrice { get; set; }
        public string Epost { get; set; }
        public string Rabatt { get; set; }
        public bool Student { get; set; }


        public void OnGet()
        {
            List<BookInfo> rum = new List<BookInfo>();
            rum.Add(new BookInfo { type = "Superior", text = "350", reaprice = 900, ImagesUrl = "assets/img/Superior.jpg" });
            rum.Add(new BookInfo { type = "Premier", text = "200", reaprice = 550, ImagesUrl = "assets/img/premier.JPG" });
            rum.Add(new BookInfo { type = "VIP", text = "550", reaprice = 1500, ImagesUrl = "assets/img/vip.jpg" });
            rum.Add(new BookInfo { type = "Deluxe", text = "450", reaprice = 1200, ImagesUrl = "assets/img/Deluxe.jpg" });

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
            Image = rum.First(i => i.type == Rum).ImagesUrl;
            People = int.Parse(preview.First(p => p.type == "people").text);
            Epost = preview.First(e => e.type == "epost").text;
            Student = preview.First(e => e.type == "Student").student;

            Price = decimal.Parse(rum.First(r => r.type == $"{Rum}").text);
            ReaPrice = decimal.Parse(rum.First(r => r.type == $"{Rum}").text);

            decimal service = 49;
            decimal Sale = 0.15m;
            int PromotionMonth = 10;

            var totalttime = TimeEnd > TimeStart ? TimeEnd - TimeStart : TimeEnd.AddDays(1) - TimeStart;
            ViewData["Totalttime"] = totalttime;

            if (totalttime.Hours < 03)
            {
                decimal price = int.Parse(rum.First(p => p.type == $"{Rum}").text);
                if (Student == true)
                {
                    if (Date.Month == PromotionMonth && Date.Year == 2021)
                    {
                        Totaltprice = ((totalttime.Hours * price) * (1 - Sale * 2)) + service;
                    }
                    else if (Date.Month != PromotionMonth && Date.Year == 2021)
                    {
                        Totaltprice = ((totalttime.Hours * price) * (1 - Sale)) + service;
                    }
                }
                else if (Student == false)
                {
                    if (Date.Month != PromotionMonth && Date.Year == 2021)
                    {
                        decimal totalprice = (totalttime.Hours * price) + service;
                    }
                    else if (Date.Month == PromotionMonth && Date.Year == 2021)
                    {
                        decimal totalprice = ((totalttime.Hours * price) * (1 - Sale * 2))+ service;
                    }
                }
            }
            else if (totalttime.Hours == 03)
            {
                ViewData["Reapris"] = $"Rea pris på 3 timmar: {rum.First(p => p.type == $"{Rum}").reaprice} kr";

                if (Student == true)
                {
                    if (Date.Month == 10 && Date.Year == 2021)
                    {
                        Totaltprice = ((totalttime.Hours * ReaPrice) * (1 - Sale * 2)) + service;
                    }
                    else if (Date.Month != 10 && Date.Year == 2021)
                    {
                        Totaltprice = ((totalttime.Hours * ReaPrice) * (1 - Sale)) + service;
                    }
                }
                else if (Student == false)
                {
                    if (Date.Month != PromotionMonth && Date.Year == 2021)
                    {
                        Totaltprice = (totalttime.Hours * ReaPrice) + service;
                    }
                    else if (Date.Month == PromotionMonth && Date.Year == 2021)
                    {
                        Totaltprice = ((totalttime.Hours * ReaPrice) * (1 - Sale)) + service;
                    }
                }
                //else if (Student == true)
                //{
                //    if (Date.Month != PromotionMonth && Date.Year == 2021)
                //    {
                //        Totaltprice = ((totalttime.Hours * ReaPrice) * (1 - Sale)) + service;
                //    }
                //    else if (Date.Month == PromotionMonth && Date.Year == 2021)
                //    {
                //        Totaltprice = ((totalttime.Hours * ReaPrice) * (1 - Sale * 2)) + service;
                //    }
                //}
            }
        }
    }
}
