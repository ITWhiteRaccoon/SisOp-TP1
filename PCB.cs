namespace SisOp_TP1;

public class PCB
{
    public Dictionary<string, int> Dados { get; }
    public Dictionary<string, int> Labels { get; }
    public List<Instrucao> Codigo { get; }
    public int Acc { get; set; }
    public int Pc { get; set; }

    public PCB()
    {
        Dados = new Dictionary<string, int>();
        Codigo = new List<Instrucao>();
        Labels = new Dictionary<string, int>();
    }
}