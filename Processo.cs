namespace SisOp_TP1;

public class Processo
{
    public string Fonte { get; set; }
    public int InstanteCarga { get; set; }
    public Prioridade? Prioridade { get; set; }
    public int? Quantum { get; set; }

    public Processo()
    {
        Fonte = "";
    }
}
