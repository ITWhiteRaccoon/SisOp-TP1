using System.Text;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class EscalonadorRoundRobin
{
    private int _tempo;
    private int _tempoIdle;
    private ProcessoQuantum? _executando;
    private Processador _processador;
    private Queue<ProcessoQuantum> _prontos;
    private List<ProcessoQuantum> _bloqueados;
    private List<ProcessoQuantum> _finalizados;

    public EscalonadorRoundRobin(IEnumerable<Programa> programasLidos)
    {
        _processador = new Processador();
        _prontos = new Queue<ProcessoQuantum>();
        _bloqueados = new List<ProcessoQuantum>();
        _finalizados = new List<ProcessoQuantum>();
        foreach (var programa in programasLidos)
        {
            var processo = new ProcessoQuantum(programa);
            if (processo.InstanteCarga > 0)
            {
                _bloqueados.Add(processo);
            }
            else
            {
                _prontos.Enqueue(processo);
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
                    _executando = _prontos.Dequeue();
                    _executando.TempoProcessando = 0;
                    
                    LerContextoPcb();
                }
                else //Não tem pronto
                {
                    _tempo++;
                    _tempoIdle++;
                    continue; //Não executou ninguem
                }
                
            }

            if (_executando.Quantum == _executando.TempoProcessando)
            {
                GravarContextoPcb();
                _prontos.Enqueue(_executando);
                _executando = null;
                continue;
            }
            
            _processador.ExecutarInstrucao(_executando.Pcb, out var finalizado, out var bloquear);
            SomarTempoEspera();
            _tempo++;
            _executando.TempoProcessando++;

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
        var processosParaDesbloquear = new List<ProcessoQuantum>();
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
            _prontos.Enqueue(processo);
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