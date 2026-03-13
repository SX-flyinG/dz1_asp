using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using homework_project.DTOs;
using homework_project.Models;
using homework_project.Repository;

namespace homework_project.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository repo, IMapper mapper)
    {
        _repo   = repo;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Отримати всіх користувачів", Tags = new[] { "Users" })]
    [SwaggerResponse(200, "Список користувачів", typeof(IEnumerable<UserDto>))]
    public ActionResult<IEnumerable<UserDto>> GetAll()
    {
        var users = _repo.GetAll();
        var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Отримати користувача за ID", Tags = new[] { "Users" })]
    [SwaggerResponse(200, "Користувач знайдений", typeof(UserDto))]
    [SwaggerResponse(404, "Користувача не знайдено")]
    public ActionResult<UserDto> GetById(int id)
    {
        var user = _repo.GetById(id);
        if (user is null)
            return NotFound(new { error = $"Користувача з ID {id} не знайдено" });

        var dto = _mapper.Map<UserDto>(user);
        return Ok(dto);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Створити користувача", Tags = new[] { "Users" })]
    [SwaggerResponse(201, "Користувача створено", typeof(UserDto))]
    [SwaggerResponse(400, "Невалідні дані або email вже існує")]
    public ActionResult<UserDto> Create([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_repo.EmailExists(dto.Email))
            return BadRequest(new { error = $"Email '{dto.Email}' вже використовується" });

        var user    = _mapper.Map<User>(dto);
        var created = _repo.Add(user);

        var resultDto = _mapper.Map<UserDto>(created);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, resultDto);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Оновити користувача", Tags = new[] { "Users" })]
    [SwaggerResponse(200, "Користувача оновлено", typeof(UserDto))]
    [SwaggerResponse(400, "Невалідні дані або email вже зайнятий")]
    [SwaggerResponse(404, "Користувача не знайдено")]
    public ActionResult<UserDto> Update(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_repo.EmailExists(dto.Email, excludeId: id))
            return BadRequest(new { error = $"Email '{dto.Email}' вже використовується іншим користувачем" });

        var updatedUser = _mapper.Map<User>(dto);
        var result      = _repo.Update(id, updatedUser);

        if (result is null)
            return NotFound(new { error = $"Користувача з ID {id} не знайдено" });

        var resultDto = _mapper.Map<UserDto>(result);
        return Ok(resultDto);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Видалити користувача", Tags = new[] { "Users" })]
    [SwaggerResponse(204, "Користувача видалено")]
    [SwaggerResponse(404, "Користувача не знайдено")]
    public IActionResult Delete(int id)
    {
        var deleted = _repo.Delete(id);
        if (!deleted)
            return NotFound(new { error = $"Користувача з ID {id} не знайдено" });

        return NoContent();
    }
}
