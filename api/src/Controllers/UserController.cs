using Microsoft.AspNetCore.Mvc;
using api.src.DTOs;
using api.src.Interfaces;
using api.src.Mappers;

namespace api.src.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IUserRepository _userRepository;

        public ProductController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var exists = await _userRepository.ExistsByRut(createUserDto.Rut);
            if (exists)
            {
                return Conflict("El codigo ya esta registrado.");
            } 
            else 
            {
                var userModel = createUserDto.ToUserFromUserDto();
                await _userRepository.Post(userModel);
                return CreatedAtAction(nameof(CreateUser), new { Rut = userModel.Rut }, userModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? gender)
        {
            var validGenders = new List<string> {"masculino", "femenino", "otro", "prefiero no decirlo"};

            if (!string.IsNullOrEmpty(gender) && !validGenders.Contains(gender.ToLower()))
            {
                return BadRequest("Filtro de busqueda no valido");
            }

            var users = await _userRepository.GetAll(gender);   
            var userDto = users.Select(p => p.ToUserDto());
            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string rut)
        {
            var exists = await _userRepository.ExistsByRut(rut);
            if (exists)
            {
                await _userRepository.DeleteUser(rut);
                return Ok("Usuario eliminado");
            } 
            else
            {
                return NotFound("Rut no valido");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string rut, [FromBody]UpdateUserRequestDto updateUserRequestDto)
        {
            // Verificar si el cuerpo del request es válido (validación automática de ASP.NET Core)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 si alguna validación en el DTO falla
            }

            // Verificar si el producto existe
            var exists = await _userRepository.ExistsByRut(rut);
            if (!exists)
            {
                return NotFound("Usuario no encontrado"); // Retorna 404 si no encuentra el producto
            }

            // Actualizar el producto
            var userModel = await _userRepository.Put(rut, updateUserRequestDto);

            // Devolver el producto actualizado como respuesta
            return Ok(userModel.ToUserDto());
        }
        
    }
}