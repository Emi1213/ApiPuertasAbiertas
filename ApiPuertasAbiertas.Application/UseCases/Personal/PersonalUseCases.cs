namespace ApiPuertasAbiertas.Application.UseCases.Personal
{
  using ApiPuertasAbiertas.Application.DTOs.Personal;
  using ApiPuertasAbiertas.Domain.Repositories;
  using AutoMapper;

  public class PersonalUseCases
  {
    private readonly IPersonalRepository _personalRepository;
    private readonly IMapper _mapper;

    public PersonalUseCases(IPersonalRepository personalRepository, IMapper mapper)
    {
      _personalRepository = personalRepository;
      _mapper = mapper;
    }

    public async Task<List<PersonalDto>> ObtenerTodosAsync()
    {
      var personalList = await _personalRepository.ObtenerTodosAsync();
      return _mapper.Map<List<PersonalDto>>(personalList);
    }
    public async Task<PersonalDto?> ObtenerPorIdAsync(int id)
    {
      var personal = await _personalRepository.ObtenerPorIdAsync(id);
      return personal == null ? null : _mapper.Map<PersonalDto>(personal);
    }

    public async Task CrearAsync(CrearPersonalDto crearPersonalDto)
    {
      var personal = _mapper.Map<Domain.Entities.Personal>(crearPersonalDto);
      await _personalRepository.CrearAsync(personal);
    }

    public async Task ActualizarAsync(ActualizarPersonalDto actualizarPersonalDto)
    {
      var existingPersonal = await _personalRepository.ObtenerPorIdAsync(actualizarPersonalDto.Id);
      if (existingPersonal == null)
      {
        throw new KeyNotFoundException("Personal no encontrado");
      }
      var personal = _mapper.Map<Domain.Entities.Personal>(actualizarPersonalDto);
      await _personalRepository.ActualizarAsync(personal);
    }

    public async Task EliminarAsync(int id)
    {
      var personal = await _personalRepository.ObtenerPorIdAsync(id);
      if (personal == null)
      {
        throw new KeyNotFoundException("Personal no encontrado");
      }
      await _personalRepository.EliminarAsync(id);
    }
  }
}