using System.Text;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class EscalonadorComPreempcao
{
    private int _tempoIdle;
    private int _tempo;
    private readonly SortedSet<ProcessoPrioridade> _prontos;
    private readonly SortedSet<ProcessoPrioridade> _bloqueados;
    private readonly SortedSet<ProcessoPrioridade> _finalizados;
    private ProcessoPrioridade? _executando;
    private readonly Processador _processador;

    public EscalonadorComPreempcao(IEnumerable<Programa> programasLidos)
    {
        _processador = new Processador();
        _prontos = new SortedSet<ProcessoPrioridade>();
        _bloqueados = new SortedSet<ProcessoPrioridade>();
        _finalizados = new SortedSet<ProcessoPrioridade>();
        foreach (var programa in programasLidos)
        {
            var processo = new ProcessoPrioridade(programa);
            if (processo.InstanteCarga > 0)
            {
                _bloqueados.Add(processo);
            }
            else
            {
                _prontos.Add(processo);
            }
        }
    }

    public void Iniciar()
    {
        _tempo = 0;
        _tempoIdle = 0;
        while (_prontos.Count > 0 || _bloqueados.Count > 0 || _executando != null)
        {
            DesbloquearProcessos();
            if (_executando == null) //Não executando
            {
                if (_prontos.Count >= 1) //Tem pronto
                {
                    _executando = _prontos.Min;
                    _prontos.Remove(_executando);

                    LerContextoPcb();
                }
                else //Não tem pronto
                {
                    _tempo++;
                    _tempoIdle++;
                    continue; //Não executou ninguem
                }
            }

            if (_prontos.Count >= 1)
            {
                var processoPrioritario = _prontos.Min;
                if (processoPrioritario?.Prioridade > _executando.Prioridade)
                {
                    GravarContextoPcb();

                    _prontos.Add(_executando);
                    _executando = processoPrioritario;
                    _prontos.Remove(processoPrioritario);

                    LerContextoPcb();
                }
            }

            SomarTempoEspera();

            _processador.ExecutarInstrucao(_executando.Pcb, out var finalizado, out var bloquear);
            _executando.TempoProcessando++;
            _tempo++;

            if (finalizado)
            {
                _executando.TempoCriacao = _tempo - _executando.InstanteCarga;
                _finalizados.Add(_executando);
                _executando = null;
            }
            else if (bloquear != null)
            {
                _executando.InstanteDesbloquear = _tempo + bloquear;
                GravarContextoPcb();
                _bloqueados.Add(_executando);
                _executando = null;
            }
        }
    }

    private void GravarContextoPcb()
    {
        _executando.Pcb.Acc = _processador.Acc;
        _executando.Pcb.Pc = _processador.Pc;
    }

    private void LerContextoPcb()
    {
        _processador.Acc = _executando.Pcb.Acc;
        _processador.Pc = _executando.Pcb.Pc;
    }

    private void DesbloquearProcessos()
    {
        var processosParaDesbloquear = new List<ProcessoPrioridade>();
        foreach (var processoBloqueado in _bloqueados)
        {
            if (processoBloqueado.InstanteCarga == _tempo ||
                (processoBloqueado.InstanteDesbloquear != null && processoBloqueado.InstanteDesbloquear == _tempo))
            {
                processosParaDesbloquear.Add(processoBloqueado);
            }
        }

        foreach (var processo in processosParaDesbloquear)
        {
            _prontos.Add(processo);
            _bloqueados.Remove(processo);
        }
    }

    private void SomarTempoEspera()
    {
        foreach (var processoPronto in _prontos)
        {
            processoPronto.TempoEspera++;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Tempo:{_tempo},TempoIdle:{_tempoIdle}");
        sb.AppendLine($"Processos finalizados:");
        foreach (var processo in _finalizados)
        {
            sb.AppendLine(processo.ToString());
        }

        return sb.ToString();
    }
}
