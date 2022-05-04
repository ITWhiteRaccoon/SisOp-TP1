namespace SisOp_TP1;

public class ProcessoPrioridade : IComparable<ProcessoPrioridade>
{
    public int InstanteCarga { get; }
    public PCB Pcb { get; }
    public Prioridade Prioridade { get; set; }

    public ProcessoPrioridade(Programa programaLido)
    {
        InstanteCarga = programaLido.InstanteCarga;
        Pcb = Util.CarregarProcesso(programaLido);
    }
    
    public int CompareTo(ProcessoPrioridade? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Prioridade.CompareTo(other.Prioridade);
    }
}