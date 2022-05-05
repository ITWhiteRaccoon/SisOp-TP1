using System.Text;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class ProcessoQuantum : IComparable<ProcessoQuantum>
{
    public int Pid { get; }
    public int InstanteCarga { get; }
    public int? InstanteDesbloquear { get; set; }
    public Pcb Pcb { get; }
    public int Quantum { get; set; }
    public int TempoCriacao { get; set; }
    public int TempoEspera { get; set; }
    public int TempoProcessando { get; set; }

    public ProcessoQuantum(Programa programaLido)
    {
        Pid = GeradorPid.GerarProximo();
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
        sb.Append($"Pid:{Pid},");
        sb.Append($"InstanteCarga:{InstanteCarga},Quantum:{Quantum},TurnaroundTime:{TempoCriacao}");
        sb.Append($",WaitingTime:{TempoEspera},ProcessingTime:{TempoProcessando},PCB:{Pcb}");
        return sb.ToString();
    }
}
