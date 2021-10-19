using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using K_Box_project.Model;
using K_Box_project.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Box_project.Pages
{
    [BindProperties]
    public class BokaNuModel : PageModel
    {
        [Required(ErrorMessage = "Var god och fyll i förnamn!")]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Var god och fyll i efternamn!")]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Var god och fyll i ditt telefon nummer")]
        [MaxLength(10, ErrorMessage = "Telefonnumer måste vara exakt 10 siffra"), MinLength(10, ErrorMessage = "Telefonnumer måste vara exakt 10 siffra")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Var god och fyll i ditt telefon nummer")]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Stad { get; set; }

        [Required(ErrorMessage = "Var god och välja ett datum!")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Var god och välja start tid!")]
        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Var god och välja slut tid!")]
        [DataType(DataType.Time)]
        public DateTime End { get; set; }

        [Required]
        public string Rum { get; set; }

        [Required]
        public int People { get; set; }

        public string Message { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Epost { get; set; }

        public IList<BookInfo> informations { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DateTime time = new DateTime(2021, 02, 12, 03, 00, 00);
                var totalttime = End > Start ? End - Start : End.AddDays(1) - Start;
                if (totalttime.Hours <= 3)
                {
                    if (End < Start)
                    {
                        if (End.Hour < 00)
                        {

                        }
                        else
                        {
                            informations = new List<BookInfo>()
                                                {
                                                new BookInfo() { type = "firstname", text = $"{Firstname}" },
                                                new BookInfo() { type = "lastname", text = $"{Lastname}" },
                                                new BookInfo() { type = "epost", text = $"{Epost}" },
                                                new BookInfo() { type = "stad", text = $"{Stad}" },
                                                new BookInfo() { type = "mobile", text = $"{Mobile}" },
                                                new BookInfo() { type = "date", datetime = Date.Value },
                                                new BookInfo() { type = "timesstart", datetime = Start },
                                                new BookInfo() { type = "timeend", datetime = End },
                                                new BookInfo() { type = "rum", text = $"{Rum}" },
                                                new BookInfo() { type = "message", text = $"{Message}" },
                                                new BookInfo() { type = "people", text = $"{People.ToString()}" },
                                                };
                            HttpContext.Session.Set("informationlist", informations);
                            return RedirectToPage("/Preview");
                        }
                    }
                    else if(End > Start)
                    {
                        informations = new List<BookInfo>()
                                                {
                                                new BookInfo() { type = "firstname", text = $"{Firstname}" },
                                                new BookInfo() { type = "lastname", text = $"{Lastname}" },
                                                new BookInfo() { type = "epost", text = $"{Epost}" },
                                                new BookInfo() { type = "stad", text = $"{Stad}" },
                                                new BookInfo() { type = "mobile", text = $"{Mobile}" },
                                                new BookInfo() { type = "date", datetime = Date.Value },
                                                new BookInfo() { type = "timesstart", datetime = Start },
                                                new BookInfo() { type = "timeend", datetime = End },
                                                new BookInfo() { type = "rum", text = $"{Rum}" },
                                                new BookInfo() { type = "message", text = $"{Message}" },
                                                new BookInfo() { type = "people", text = $"{People.ToString()}" },
                                                };
                        HttpContext.Session.Set("informationlist", informations);
                        return RedirectToPage("/Preview");
                    }
                }
                else
                {
                    if (End < Start)
                    {
                        if (End < Start && (End.Hour > 00 && End.Hour < 03))
                        {
                            ModelState.AddModelError("Invalid", $"Ogiltig tid! Slut tiden måste vara senare än {Start.ToString("t")}");
                            return Page();
                        }
                        else
                        {
                            ModelState.AddModelError("Overtime", "Du får max 3 timmar på bokningen");
                            return Page();
                        }
                    }
                    else
                    {
                        if (End < Start && (End.Hour < 00 && End.Hour > 03))
                        {
                            ModelState.AddModelError("Overtime", "Du får max 3 timmar på bokningen");
                            return Page();
                        }
                        ModelState.AddModelError("Invalid", $"Ogiltig tid! Slut tiden måste vara senare än {Start.ToString("t")}");
                        return Page();
                    }                    
                }
            }
            return Page();
        }
    }
}
//informations = new List<BookInfo>()
//                    {
//                    new BookInfo() { type = "firstname", text = $"{Firstname}" },
//                    new BookInfo() { type = "lastname", text = $"{Lastname}" },
//                    new BookInfo() { type = "epost", text = $"{Epost}" },
//                    new BookInfo() { type = "stad", text = $"{Stad}" },
//                    new BookInfo() { type = "mobile", text = $"{Mobile}" },
//                    new BookInfo() { type = "date", datetime = Date.Value },
//                    new BookInfo() { type = "timesstart", datetime = Start },
//                    new BookInfo() { type = "timeend", datetime = End },
//                    new BookInfo() { type = "rum", text = $"{Rum}" },
//                    new BookInfo() { type = "message", text = $"{Message}" },
//                    new BookInfo() { type = "people", text = $"{People.ToString()}" },
//                    };
//HttpContext.Session.Set("informationlist", informations);
//return RedirectToPage("/Preview");
