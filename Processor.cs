namespace SisOp_TP1;

public class Processor
{
    public int Acc { get; }
    public int Pc { get; }

    public Processor(int acc, int pc)
    {
        this.Acc = acc;
        this.Pc = pc;
    }
}
