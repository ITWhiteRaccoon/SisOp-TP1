using System.Text;
using System.Xml;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class ProcessoQuantum : IComparable<ProcessoQuantum>
{
    public int InstanteCarga { get; }
    public int? InstanteDesbloquear { get; set; }
    public Pcb Pcb { get; }
    public int Quantum { get; set; }
    public int TempoCriacao { get; }
    public int TempoEspera { get; }
    public int TempoProcessando { get; }

    public ProcessoQuantum(Programa programaLido)
    {
        TempoCriacao = 0;
        TempoEspera = 0;
        TempoProcessando = 0;
        InstanteDesbloquear = null;
        InstanteCarga = programaLido.InstanteCarga;
        Quantum = programaLido.Quantum ?? -1;
        Pcb = Util.CarregarProcesso(programaLido);
    }

    public int CompareTo(ProcessoQuantum? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Quantum.CompareTo(other.Quantum);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"InstanteCarga:{InstanteCarga},InstanteDesbloquear{InstanteDesbloquear},Quantum{Quantum}");
        sb.Append($",TurnaroundTime:{TempoCriacao},WaitingTime:{TempoEspera},ProcessingTime:{TempoProcessando}");
        sb.Append($",PCB:{Pcb}");
        return sb.ToString();
    }
}
