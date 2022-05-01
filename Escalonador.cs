namespace SisOp_TP1;

public class Escalonador
{
    public List<Processo> Processos { get; set; }
    public Escalonamento PoliticaEscalonamento { get; set; }

    public Escalonador()
    {
        Processos = new List<Processo>();
    }
}
