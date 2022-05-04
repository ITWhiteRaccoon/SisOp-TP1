namespace SisOp_TP1;

public class EscalonadorComPreempcao
{
    private int _tempo;
    private SortedSet<ProcessoPrioridade> _prontos;
    private SortedSet<ProcessoPrioridade> _executando;
    private SortedSet<ProcessoPrioridade> _bloqueados;
    private SortedSet<ProcessoPrioridade> _finalizados;

    public EscalonadorComPreempcao(IEnumerable<Programa> programasLidos)
    {
        _prontos = new SortedSet<ProcessoPrioridade>();
        _executando = new SortedSet<ProcessoPrioridade>();
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

    public void Iniciar(List<Programa> programasLidos)
    {
    }
}