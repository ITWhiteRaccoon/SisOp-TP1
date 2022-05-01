namespace SisOp_TP1;

public class Instrucao
{
    public Mnemonico Mnemonico { get; set; }
    public int? Valor { get; set; }
    public string Endereco { get; set; }
    public ModoEnderecamento? ModoEnderecamento { get; set; }
}
