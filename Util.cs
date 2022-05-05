using System.Text.RegularExpressions;
using SisOp_TP1.Config;

namespace SisOp_TP1;

public class Util
{
    public static Pcb CarregarProcesso(Programa programaLido)
    {
        var pcb = new Pcb();
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
                    case Mnemonico.Add:
                    case Mnemonico.Sub:
                    case Mnemonico.Mult:
                    case Mnemonico.Div:
                    case Mnemonico.Load:
                    {
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
                    }
                    case Mnemonico.Store:
                    case Mnemonico.BrAny:
                    case Mnemonico.BrPos:
                    case Mnemonico.BrZero:
                    case Mnemonico.BrNeg:
                    {
                        instrucao.Endereco = strSeparada[1];
                        break;
                    }
                    case Mnemonico.Syscall:
                    {
                        instrucao.Valor = Convert.ToInt32(strSeparada[1]);
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                pcb.Instrucoes.Add(instrucao);
                linhaCode++;
            }
        }

        return pcb;
    }
}
