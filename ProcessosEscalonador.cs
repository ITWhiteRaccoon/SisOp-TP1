using System.Collections;
using System.Text;

namespace SisOp_TP1;

public class ProcessosEscalonador
{
    public ICollection Prontos;
    public ICollection Executando;
    public ICollection Bloqueados;
    public ICollection Finalizados;

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Prontos[{string.Join(',', Prontos)}]");
        stringBuilder.AppendLine($"Executando[{string.Join(',', Executando)}]");
        stringBuilder.AppendLine($"Bloqueados[{string.Join(',', Bloqueados)}]");
        stringBuilder.AppendLine($"Finalizados[{string.Join(',', Finalizados)}]");
        return stringBuilder.ToString();
    }
}