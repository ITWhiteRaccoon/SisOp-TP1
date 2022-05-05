using System.Text;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class ProcessoPrioridade : IComparable<ProcessoPrioridade>
{
    public int Pid { get; }
    public int InstanteCarga { get; }
    public int? InstanteDesbloquear { get; set; }
    public Pcb Pcb { get; }
    public Prioridade Prioridade { get; set; }
    public int TempoCriacao { get; set; }
    public int TempoEspera { get; set; }
    public int TempoProcessando { get; set; }

    public ProcessoPrioridade(Programa programaLido)
    {
        Pid = GeradorPid.GerarProximo();
        TempoCriacao = 0;
        TempoEspera = 0;
        TempoProcessando = 0;
        InstanteDesbloquear = null;
        InstanteCarga = programaLido.InstanteCarga;
        Prioridade = programaLido.Prioridade ?? Prioridade.Baixa;
        Pcb = Util.CarregarProcesso(programaLido);
    }

    public int CompareTo(ProcessoPrioridade? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Prioridade.CompareTo(other.Prioridade);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"Pid:{Pid}");
        sb.Append($"InstanteCarga:{InstanteCarga},InstanteDesbloquear{InstanteDesbloquear},Prioridade{Prioridade}");
        sb.Append($",TurnaroundTime:{TempoCriacao},WaitingTime:{TempoEspera},ProcessingTime:{TempoProcessando}");
        sb.Append($",PCB:{Pcb}");
        return sb.ToString();
    }
}