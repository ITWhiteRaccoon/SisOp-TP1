using System.Reflection.Metadata;

namespace SisOp_TP1;

public class EscalonadorSemPreempcao
{
    private int _tempo;
    private SortedSet<ProcessoPrioridade> Prontos;
    private SortedSet<ProcessoPrioridade> Executando;
    private SortedSet<ProcessoPrioridade> Bloqueados;
    private SortedSet<ProcessoPrioridade> Finalizados;

    public EscalonadorSemPreempcao(IEnumerable<Programa> programasLidos)
    {
        Prontos = new SortedSet<ProcessoPrioridade>();
        Executando = new SortedSet<ProcessoPrioridade>();
        Bloqueados = new SortedSet<ProcessoPrioridade>();
        Finalizados = new SortedSet<ProcessoPrioridade>();
        foreach (var programa in programasLidos)
        {
            var processo = new ProcessoPrioridade(programa);
            if (processo.InstanteCarga > 0)
            {
                Bloqueados.Add(processo);
            }
            else
            {
                Prontos.Add(processo);
            }
        }
    }

    public void Iniciar(List<Programa> programasLidos)
    {
    }
}