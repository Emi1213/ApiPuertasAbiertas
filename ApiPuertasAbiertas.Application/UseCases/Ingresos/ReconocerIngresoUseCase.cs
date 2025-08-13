using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Domain.Repositories;

namespace ApiPuertasAbiertas.Application.UseCases.Ingresos;

public class ReconocerIngresoUseCase
{
  private readonly IIngresoRepository _ingresoRepository;
  private readonly IClock _clock;

  public ReconocerIngresoUseCase(IIngresoRepository ingresoRepository, IClock clock)
  {
    _ingresoRepository = ingresoRepository;
    _clock = clock;
  }

  public async Task ExecuteAsync(int ingresoId, int usuarioId)
  {
    var ok = await _ingresoRepository.ReconocerAsync(ingresoId, usuarioId, _clock.Now);
    if (!ok) throw new InvalidOperationException("No existe el ingreso o ya fue reconocido.");
  }

  public async Task UndoAsync(int ingresoId)
  {
    var ok = await _ingresoRepository.QuitarReconocimientoAsync(ingresoId);
    if (!ok) throw new InvalidOperationException("No existe el ingreso o no estaba reconocido.");
  }
}