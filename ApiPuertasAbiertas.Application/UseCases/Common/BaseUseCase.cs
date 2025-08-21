using AutoMapper;
using ApiPuertasAbiertas.Domain.Repositories;

namespace ApiPuertasAbiertas.Application.UseCases.Common;

public abstract class BaseUseCase<TEntity, TDto, TCreateDto, TUpdateDto, TRepository>
  where TEntity : class
  where TDto : class
  where TCreateDto : class
  where TUpdateDto : class
  where TRepository : class
{
  protected readonly TRepository Repository;
  protected readonly IMapper Mapper;

  protected BaseUseCase(TRepository repository, IMapper mapper)
  {
    Repository = repository;
    Mapper = mapper;
  }

  public virtual async Task<List<TDto>> ObtenerTodosAsync()
  {
    var entities = await GetAllEntitiesAsync();
    return Mapper.Map<List<TDto>>(entities);
  }

  public virtual async Task<TDto?> ObtenerPorIdAsync(int id)
  {
    var entity = await GetEntityByIdAsync(id);
    return entity == null ? null : Mapper.Map<TDto>(entity);
  }

  public virtual async Task<TDto> CrearAsync(TCreateDto createDto)
  {
    var entity = Mapper.Map<TEntity>(createDto);
    await CreateEntityAsync(entity);
    return Mapper.Map<TDto>(entity);
  }

  public virtual async Task ActualizarAsync(TUpdateDto updateDto)
  {
    var entity = Mapper.Map<TEntity>(updateDto);
    await UpdateEntityAsync(entity);
  }

  public virtual async Task EliminarAsync(int id)
  {
    await DeleteEntityAsync(id);
  }
  protected abstract Task<List<TEntity>> GetAllEntitiesAsync();
  protected abstract Task<TEntity?> GetEntityByIdAsync(int id);
  protected abstract Task CreateEntityAsync(TEntity entity);
  protected abstract Task UpdateEntityAsync(TEntity entity);
  protected abstract Task DeleteEntityAsync(int id);
}
