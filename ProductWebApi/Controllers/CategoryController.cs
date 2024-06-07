using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Models;
using ProductWebApi.Repository.IRepository;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var objCategoryList = (await _unitOfWork.Category.GetAllAsync()).ToList();
            return Ok(objCategoryList);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var categoryFromDb = await _unitOfWork.Category.GetAsync(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return Ok(categoryFromDb);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] Category obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.Category.AddAsync(obj);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetCategory", new { id = obj.Id }, obj);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category obj)
        {
            if (obj == null || obj.Id != id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.Category.UpdateAsync(obj);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryFromDb = await _unitOfWork.Category.GetAsync(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            await _unitOfWork.Category.RemoveAsync(categoryFromDb);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
