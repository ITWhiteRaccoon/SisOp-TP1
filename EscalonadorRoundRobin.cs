using System.Runtime.InteropServices.ComTypes;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class EscalonadorRoundRobin
{
    private int _tempo;
    private Queue<ProcessoQuantum> _prontos;
    private Queue<ProcessoQuantum> _executando;
    private Queue<ProcessoQuantum> _bloqueados;
    private Queue<ProcessoQuantum> _finalizados;
    private Processador _processador;

    public EscalonadorRoundRobin(IEnumerable<Programa> programasLidos)
    {
        _processador = new Processador();
        _prontos = new Queue<ProcessoQuantum>();
        _executando = new Queue<ProcessoQuantum>();
        _bloqueados = new Queue<ProcessoQuantum>();
        _finalizados = new Queue<ProcessoQuantum>();
        foreach (var programa in programasLidos)
        {
            var processo = new ProcessoQuantum(programa);
            if (processo.InstanteCarga > 0)
            {
                _bloqueados.Enqueue(processo);
            }
            else
            {
                _prontos.Enqueue(processo);
            }
        }
    }

    public void Iniciar()
    {
        while (true) { }
    }
}
