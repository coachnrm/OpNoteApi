using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpNoteApi.Data;
using OpNoteApi.Dtos;
using OpNoteApi.Models;

namespace OpNoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpNotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OpNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OpNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpNote>>> GetAll()
        {
            return await _context.OpNotes.ToListAsync();
        }

        // GET: api/OpNotes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OpNote>> GetById(int id)
        {
            var note = await _context.OpNotes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return note;
        }

        // POST: api/addopnote
        [HttpPost("addopnote")]
        public async Task<ActionResult<OpNote>> Create(OpNote note)
        {
            _context.OpNotes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
        }

        // GET: api/getpictures/{an}
        [HttpGet("getpictures/{an}")]
        public async Task<ActionResult<IEnumerable<OpPicture>>> GetPicturesByAn(string an)
        {
            try
            {
                var pictures = await _context.OpPictures
                    .Where(p => p.An == an)
                    .Select(p => new OpPicture
                    {
                        Id = p.Id,
                        Hn = p.Hn,
                        An = p.An,
                        OpType = p.OpType,
                        Content = p.Content  // Convert byte[] to Base64 string
                    })
                    .ToListAsync();

                if (pictures == null)
                {
                    return NotFound(new { Message = "No pictures found for the given AN." });
                }

                return Ok(pictures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        // POST: api/addpicture
        [HttpPost("addpicture")]
        // [Consumes("multipart/form-data")]
        public async Task<ActionResult<OpPicture>> CreatePicture(IFormFile? formFile, [FromForm] OpPictureDto note)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                byte[]? imageBytes = null;

                if (formFile != null && formFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }
                }

                // Save the image to the database (even if null)
                var opPicture = new OpPicture
                {
                    Hn = note.Hn,
                    An = note.An,
                    OpType = note.OpType,
                    Content = imageBytes
                };

                // Save opPicture to database
                _context.OpPictures.Add(opPicture);
                await _context.SaveChangesAsync();

                response.ResponseCode = 200;
                response.Result = "Picture uploaded successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 500;
                response.Errormessage = ex.Message;
                return StatusCode(500, response);
            }
        }

        // POST: api/addpicture
        [HttpPost("addpicture2")]
        // [Consumes("multipart/form-data")]
        public async Task<ActionResult<OpPicture>> CreatePicture2([FromForm] OpPicture2Dto note)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                byte[]? imageBytes = null;

                if (note.File != null && note.File.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await note.File.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }
                }

                // Save the image to the database (even if null)
                var opPicture = new OpPicture
                {
                    Hn = note.Hn,
                    An = note.An,
                    OpType = note.OpType,
                    Content = imageBytes
                };

                // Save opPicture to database
                _context.OpPictures.Add(opPicture);
                await _context.SaveChangesAsync();

                response.ResponseCode = 200;
                response.Result = "Picture uploaded successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 500;
                response.Errormessage = ex.Message;
                return StatusCode(500, response);
            }
        }


        // PUT: api/OpNotes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OpNote note)
        {
            if (id != note.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpNoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/OpNotes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.OpNotes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.OpNotes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OpNoteExists(int id)
        {
            return _context.OpNotes.Any(e => e.Id == id);
        }
    }
}
