using System.Xml;

namespace SisOp_TP1;

public class ProcessoQuantum : IComparable<ProcessoQuantum>
{
    public int InstanteCarga { get; }
    public Pcb Pcb { get; }
    public int Quantum { get; set; }

    public ProcessoQuantum(Programa programaLido)
    {
        InstanteCarga = programaLido.InstanteCarga;
        Pcb = Util.CarregarProcesso(programaLido);
    }

    public int CompareTo(ProcessoQuantum? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Quantum.CompareTo(other.Quantum);
    }
}