using System.Text.RegularExpressions;

namespace SisOp_TP1;

public class Util
{
    public static PCB CarregarProcesso(Programa programaLido)
    {
        var pcb = new PCB();
        var linhas = File.ReadAllText($"Input/{programaLido.Fonte}");
        var code = Regex.Split(Regex.Match(linhas, @"\.code((?:.|\n)*)\.endcode").Groups[1].Value, "\n")
            .Where(x => !string.IsNullOrWhiteSpace(x));
        var data = Regex.Split(Regex.Match(linhas, @"\.data((?:.|\n)*)\.enddata").Groups[1].Value, "\n")
            .Where(x => !string.IsNullOrWhiteSpace(x));

        foreach (var s in data)
        {
            var strSeparada = s.Trim().Split();
            pcb.Dados.Add(strSeparada[0], Convert.ToInt32(strSeparada[1]));
        }

        var linhaCode = 0;
        foreach (var s in code)
        {
            var matchLabel = Regex.Match(s, @"^\s*(\w*):");
            if (matchLabel.Success)
            {
                pcb.Labels.Add(matchLabel.Groups[1].Value, linhaCode);
            }
            else
            {
                var strSeparada = s.Trim().Split();

                var instrucao = new Instrucao
                {
                    Mnemonico = Enum.Parse<Mnemonico>(strSeparada[0], true)
                };
                switch (instrucao.Mnemonico)
                {
                    case Mnemonico.ADD:
                    case Mnemonico.SUB:
                    case Mnemonico.MULT:
                    case Mnemonico.DIV:
                    case Mnemonico.LOAD:
                        if (strSeparada[1][0] == '#')
                        {
                            instrucao.ModoEnderecamento = ModoEnderecamento.Imediato;
                            instrucao.Valor = Convert.ToInt32(strSeparada[1][1..]);
                        }
                        else
                        {
                            instrucao.ModoEnderecamento = ModoEnderecamento.Direto;
                            instrucao.Endereco = strSeparada[1];
                        }

                        break;
                    case Mnemonico.STORE:
                    case Mnemonico.BRANY:
                    case Mnemonico.BRPOS:
                    case Mnemonico.BRZERO:
                    case Mnemonico.BRNEG:
                        instrucao.Endereco = strSeparada[1];
                        break;
                    case Mnemonico.SYSCALL:
                        instrucao.Valor = Convert.ToInt32(strSeparada[1]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                pcb.Codigo.Add(instrucao);
                linhaCode++;
            }
        }

        return pcb;
    }
}