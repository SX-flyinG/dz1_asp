using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using homework_project.Models;
using homework_project.DTOs;

namespace homework_project.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUserRepository  _userRepo;
    private readonly IMapper          _mapper;

    public OrdersController(
        IOrderRepository orderRepo,
        IUserRepository  userRepo,
        IMapper          mapper)
    {
        _orderRepo = orderRepo;
        _userRepo  = userRepo;
        _mapper    = mapper;
    }

    // ─── GET ALL ─────────────────────────────────────────────────────────────
    // GET api/orders
    [HttpGet]
    public ActionResult<IEnumerable<OrderDto>> GetAll()
    {
        var orders = _orderRepo.GetAll();
        var dtos   = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return Ok(dtos);
    }

    // ─── GET BY USER ─────────────────────────────────────────────────────────
    // GET api/orders/user/1
    [HttpGet("user/{userId:int}")]
    public ActionResult<IEnumerable<OrderDto>> GetByUser(int userId)
    {
        // Перевіряємо чи існує користувач
        var user = _userRepo.GetById(userId);
        if (user is null)
            return NotFound(new { error = $"Користувача з ID {userId} не знайдено" });

        var orders = _orderRepo.GetByUserId(userId);
        var dtos   = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return Ok(dtos);
    }

    // ─── GET BY ID ────────────────────────────────────────────────────────────
    // GET api/orders/1
    [HttpGet("{id:int}")]
    public ActionResult<OrderDto> GetById(int id)
    {
        var order = _orderRepo.GetById(id);
        if (order is null)
            return NotFound(new { error = $"Замовлення з ID {id} не знайдено" });

        var dto = _mapper.Map<OrderDto>(order);
        return Ok(dto);
    }

    // ─── CREATE ───────────────────────────────────────────────────────────────
    // POST api/orders
    [HttpPost]
    public ActionResult<OrderDto> Create([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Перевіряємо чи існує користувач
        var user = _userRepo.GetById(dto.UserId);
        if (user is null)
            return NotFound(new { error = $"Користувача з ID {dto.UserId} не знайдено" });

        // AutoMapper: CreateOrderDto → Order
        var order   = _mapper.Map<Order>(dto);
        var created = _orderRepo.Add(order);

        // AutoMapper: Order → OrderDto
        var resultDto = _mapper.Map<OrderDto>(created);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, resultDto);
    }

    // ─── UPDATE ───────────────────────────────────────────────────────────────
    // PUT api/orders/1
    [HttpPut("{id:int}")]
    public ActionResult<OrderDto> Update(int id, [FromBody] UpdateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // AutoMapper: UpdateOrderDto → Order
        var updatedOrder = _mapper.Map<Order>(dto);
        var result       = _orderRepo.Update(id, updatedOrder);

        if (result is null)
            return NotFound(new { error = $"Замовлення з ID {id} не знайдено" });

        // AutoMapper: Order → OrderDto
        var resultDto = _mapper.Map<OrderDto>(result);
        return Ok(resultDto);
    }

    // ─── DELETE ───────────────────────────────────────────────────────────────
    // DELETE api/orders/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _orderRepo.Delete(id);
        if (!deleted)
            return NotFound(new { error = $"Замовлення з ID {id} не знайдено" });

        return NoContent();
    }
}
