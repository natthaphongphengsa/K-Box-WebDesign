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
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Var god och välja start tid!")]
        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Var god och välja slut tid!")]
        [DataType(DataType.Time)]
        public DateTime End { get; set; }

        [Required(ErrorMessage = "Var god och välja ett rum!")]
        public string Rum { get; set; }

        [Required(ErrorMessage = "Var god och välja antal personer!")]
        public int People { get; set; }
        public string Message { get; set; }
        [Required(ErrorMessage = "Var god och fyll i ditt e postadress")]
        [DataType(DataType.EmailAddress)]
        public string Epost { get; set; }

        public bool Student { get; set; }

        public IList<BookInfo> informations { get; set; }
        public void OnGet()
        {
            Date = DateTime.Now;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Add check for that the persone-count is allowd with the room
                TimeSpan offset = Date - Start.Date;
                DateTime openingTime = DateTime.Today.Add(new TimeSpan(hours: 18, minutes: 0, seconds: 0).Add(offset));
                DateTime closingTime = DateTime.Today.Add(new TimeSpan(hours: 3, minutes: 0, seconds: 0).Add(offset));
                Start = Start.Add(offset);
                End = End.Add(offset);
                if (End < Start) End = End.AddDays(1);
                if (Start < DateTime.Now)
                {
                    //Error message on past date
                    ModelState.AddModelError("Invalid", $"Det har gått en natt! var vänlig byt ut datumet!");
                    return Page();
                }
                if (Start == End)
                {
                    ModelState.AddModelError("Invalid", $"Ogiltig tid! Slut tiden kan inte vara samma som start tiden");
                    return Page();
                }
                if ((End - Start).Hours > 3)
                {
                    string time = Start.AddHours(3) > closingTime && Start.AddHours(3) < openingTime ? closingTime.ToString("t") : Start.AddHours(3).ToString("t");
                    ModelState.AddModelError("Overtime", $"Ogiltig tid! Slut tiden måste vara senast {time}");
                    return Page();
                }
                informations = new List<BookInfo>()
                    {
                    new BookInfo() { type = "firstname", text = $"{Firstname}" },
                    new BookInfo() { type = "lastname", text = $"{Lastname}" },
                    new BookInfo() { type = "epost", text = $"{Epost}" },
                    new BookInfo() { type = "stad", text = $"{Stad}" },
                    new BookInfo() { type = "mobile", text = $"{Mobile}" },
                    new BookInfo() { type = "date", datetime = Date },
                    new BookInfo() { type = "timesstart", datetime = Start },
                    new BookInfo() { type = "timeend", datetime = End },
                    new BookInfo() { type = "rum", text = $"{Rum}" },
                    new BookInfo() { type = "message", text = $"{Message}" },
                    new BookInfo() { type = "people", text = $"{People.ToString()}" },
                    new BookInfo() { type = "discount", text = "G36FUH78" },
                    new BookInfo() { type = "Student", text = $"{Student.ToString()}"},
                    };
                HttpContext.Session.Set("informationlist", informations);
                return RedirectToPage("/Preview");
            }
            return Page();
        }
    }
}
