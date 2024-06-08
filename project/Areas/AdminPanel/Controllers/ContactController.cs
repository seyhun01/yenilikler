using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ContactController : Controller
    {
        private readonly ShoppingDbContext _context;

        public ContactController(ShoppingDbContext context)
        {
            _context = context;

        }

        public IActionResult Index(int page = 0)
        {
            List<Contact> contact = _context.Contacts.Skip(page * 5).Take(4).ToList();
            PaginationVM<Contact> pagination = new PaginationVM<Contact>
            {

                Items = contact,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Contacts.Count() / 4)

            };
            return View(pagination);
        }
        public IActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {

            if (!ModelState.IsValid) return View();

            if (contact == null)
            {

                return View();

            }


            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == 0 || id < 1)
            {
                return BadRequest();

            }
            Contact contact = await _context.Contacts.FirstOrDefaultAsync(n => n.İd == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }





        [HttpPost]
        public async Task<IActionResult> Update(Contact contact)
        {
            if (ModelState.IsValid) return View();

            Contact old = await _context.Contacts.FirstOrDefaultAsync(o => o.İd == contact.İd);
            if (old == null)
            {
                return NotFound();
            }


            if (contact == null)
            {
                BadRequest();
                return View();
            }

            old.PhoneNumber = contact.PhoneNumber;
            old.Address = contact.Address;
            old.Email = contact.Email;
            old.MapAddress = contact.MapAddress;
            old.OpenTime = contact.OpenTime;
            old.Country = contact.Country;



            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Contact contact = await _context.Contacts.FirstOrDefaultAsync(n => n.İd == id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
