using System.Text;

namespace SisOp_TP1;

public class Pcb
{
    public Dictionary<string, int> Dados { get; }
    public Dictionary<string, int> Labels { get; }
    public List<Instrucao> Instrucoes { get; }
    public int Acc { get; set; }
    public int Pc { get; set; }

    public Pcb()
    {
        Dados = new Dictionary<string, int>();
        Instrucoes = new List<Instrucao>();
        Labels = new Dictionary<string, int>();
        Acc = 0;
        Pc = 0;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"({string.Join(' ', Dados)})");
        return sb.ToString();
    }
}
