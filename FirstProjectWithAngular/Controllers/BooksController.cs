using FirstProjectWithAngularCore.Entities.Books;
using FirstProjectWithAngularCore.IService.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProjectWithAngular.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly IBooksService BooksService;
        public BooksController(IBooksService _BooksService)
        {
            BooksService = _BooksService;
        }
        public async Task<IActionResult> Read()
        {
            try
            {
                var Books = await BooksService.Read();
                if (Books == null)
                {
                    return NotFound();
                }
                return Ok(Books);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        public async Task<IActionResult> Create([FromForm] BookModel obj, IFormFile PhotoPath)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var RespondID = await BooksService.Create(obj, PhotoPath);
                    if (RespondID > 0)
                        return Ok(RespondID);
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return BadRequest();
        }

        public async Task<IActionResult> Delete(int BookID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var RespondID = await BooksService.Delete(BookID);
                    if (RespondID > 0)
                        return Ok(RespondID);
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
