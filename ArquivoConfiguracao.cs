using System.Text.RegularExpressions;

namespace SisOp_TP1;

public class ArquivoConfiguracao
{
    public List<Programa> Processos { get; set; }
    public Escalonamento PoliticaEscalonamento { get; set; }
    public Processador Processador { get; }
    public int Timer { get; set; }

    public ArquivoConfiguracao()
    {
        Processos = new List<Programa>();
        Processador = new Processador();
    }
}