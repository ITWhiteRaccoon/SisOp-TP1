using System.Text.RegularExpressions;

namespace SisOp_TP1;

public class Escalonador
{
    public List<Processo> ProcessosLidos { get; set; }
    public List<Processo> Pronto { get; }
    public List<Processo> Executando { get; }
    public List<Processo> Bloqueado { get; }
    public List<Processo> Finalizado { get; }
    public Escalonamento PoliticaEscalonamento { get; set; }
    public Processador Processador { get; }
    public int Timer { get; set; }

    public Escalonador()
    {
        ProcessosLidos = new List<Processo>();
        Pronto = new List<Processo>();
        Executando = new List<Processo>();
        Bloqueado = new List<Processo>();
        Finalizado = new List<Processo>();
        Processador = new Processador();
    }

    public void Iniciar() { }
}
