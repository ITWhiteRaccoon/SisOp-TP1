using System.Runtime.InteropServices.ComTypes;

namespace SisOp_TP1;

public class EscalonadorRoundRobin
{
    private int _tempo;
    private Queue<ProcessoQuantum> Prontos;
    private Queue<ProcessoQuantum> Executando;
    private Queue<ProcessoQuantum> Bloqueados;
    private Queue<ProcessoQuantum> Finalizados;

    public EscalonadorRoundRobin(IEnumerable<Programa> programasLidos)
    {
        Prontos = new Queue<ProcessoQuantum>();
        Executando = new Queue<ProcessoQuantum>();
        Bloqueados = new Queue<ProcessoQuantum>();
        Finalizados = new Queue<ProcessoQuantum>();
        foreach (var programa in programasLidos)
        {
            var processo = new ProcessoQuantum(programa);
            if (processo.InstanteCarga > 0)
            {
                Bloqueados.Enqueue(processo);
            }
            else
            {
                Prontos.Enqueue(processo);
            }
        }
    }

    public void Iniciar(List<Programa> programasLidos)
    {
        while (true)
        {
        }
    }
}