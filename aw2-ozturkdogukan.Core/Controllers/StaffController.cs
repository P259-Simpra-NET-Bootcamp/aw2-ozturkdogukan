using aw2_ozturkdogukan.Core.Mapper;
using aw2_ozturkdogukan.Data.FluentValidation;
using aw2_ozturkdogukan.Data.Models;
using aw2_ozturkdogukan.DataAccess.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace aw2_ozturkdogukan.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public StaffController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // Firstname ve city alanlarına göre filtreleme yapar.
        [HttpGet("FilterApi")]
        public ActionResult<IEnumerable<Staff>> Filter([FromQuery] string firstName, [FromQuery] string city)
        {

            var query = unitOfWork.GetRepository<Staff>().GetAll();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = unitOfWork.GetRepository<Staff>().Where(s => s.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = unitOfWork.GetRepository<Staff>().Where(s => s.City == city);
            }

            var staff = query.ToList();

            return Ok(staff);
        }
        // Verilen id parametresine göre kaydı çeker.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(unitOfWork.GetRepository<Staff>().Get(x => x.Id.Equals(id)));
        }
        // Bütün verileri çeker.
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(unitOfWork.GetRepository<Staff>().GetAll().ToList());
        }
        // Staff nesnesini Email unique olacak şekilde veri tabanına ekler.
        [HttpPost("Post")]
        public IActionResult Post([FromBody] Staff staff)
        {
            StaffValidator validator = new StaffValidator();
            var result = validator.Validate(staff);
            if (result.IsValid)
            {
                var uniqueRecord = unitOfWork.GetRepository<Staff>().Get(x => x.Email.Equals(staff.Email));
                if (uniqueRecord != null)
                    return StatusCode(409);
                unitOfWork.GetRepository<Staff>().Add(staff);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return Ok("Staff Added.");
                }
                return StatusCode(500);
            }
            return BadRequest(result.Errors.Select(x => x.ErrorMessage));
        }

        // Staff nesnesini düzenler.
        [HttpPut("Put")]
        public IActionResult Put([FromBody] Staff staff)
        {
            StaffValidator validator = new StaffValidator();

            var result = validator.Validate(staff);
            if (result.IsValid)
            {
                var uniqueRecord = unitOfWork.GetRepository<Staff>().Get(x => x.Email.Equals(staff.Email));
                if (uniqueRecord == null)
                    return NotFound();
                ObjectMapper.Map(uniqueRecord, staff);
                unitOfWork.GetRepository<Staff>().Update(uniqueRecord);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return Ok("Staff Updated.");
                }
                return StatusCode(500);
            }
            return BadRequest(result.Errors.Select(x => x.ErrorMessage));

        }
        // Staff nesnesini siler.
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            unitOfWork.GetRepository<Staff>().DeleteById(id);
            unitOfWork.SaveChanges();
        }
    }
}
