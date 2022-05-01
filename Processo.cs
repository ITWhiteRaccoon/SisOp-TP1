namespace SisOp_TP1;

public class Processo
{
    public string Fonte { get; set; }
    public int InstanteCarga { get; set; }
    public Prioridade? Prioridade { get; set; }
    public int? Quantum { get; set; }
    public PCB Pcb { get; set; }

    public Processo()
    {
        Fonte = "";
        Pcb = new PCB();
    }

    public override string ToString()
    {
        var str = $"Fonte: {Fonte}, InstanteCarga: {InstanteCarga}";
        if (Quantum != null)
        {
            str += $", Quantum: {Quantum}";
        }

        if (Prioridade != null)
        {
            str += $", Prioridade: {Prioridade}";
        }

        return str;
    }
}
