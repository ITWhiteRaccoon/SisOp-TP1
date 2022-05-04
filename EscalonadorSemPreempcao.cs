using System.Reflection.Metadata;

namespace SisOp_TP1;

public class EscalonadorSemPreempcao
{
    private int _tempo;
    private SortedSet<ProcessoPrioridade> _prontos;
    private SortedSet<ProcessoPrioridade> _executando;
    private SortedSet<ProcessoPrioridade> _bloqueados;
    private SortedSet<ProcessoPrioridade> _finalizados;
    private Processador _processador;

    public EscalonadorSemPreempcao(IEnumerable<Programa> programasLidos)
    {
        _processador = new Processador();
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
        _tempo = 0;
        while (true)
        {
            if (_executando.Count<=0)
            {
                
            }

            _tempo++;
        }
    }
}