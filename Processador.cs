namespace SisOp_TP1;

public class Processador
{
    public int Acc { get; set; }
    public int Pc { get; set; }

    public Processador()
    {
        Acc = 0;
        Pc = 0;
    }

    public void ExecutarInstrucao(Pcb pcb, out bool finalizado, out int? bloquear)
    {
        finalizado = false;
        bloquear = null;

        if (Pc >= pcb.Instrucoes.Count)
        {
            throw new IndexOutOfRangeException("sla o Fabiano n deu syscall dps eu arrumo");
        }

        var instrucao = pcb.Instrucoes[Pc];
        switch (instrucao.Mnemonico)
        {
            case Mnemonico.Add:
            {
                var valorAdd = instrucao.ModoEnderecamento switch
                {
                    ModoEnderecamento.Direto => pcb.Dados[instrucao.Endereco],
                    ModoEnderecamento.Imediato => instrucao.Valor ?? 0,
                    _ => 0
                };
                Acc += valorAdd;
                Pc++;
                break;
            }
            case Mnemonico.Sub:
            {
                var valorSub = instrucao.ModoEnderecamento switch
                {
                    ModoEnderecamento.Direto => pcb.Dados[instrucao.Endereco],
                    ModoEnderecamento.Imediato => instrucao.Valor ?? 0,
                    _ => 0
                };
                Acc -= valorSub;
                Pc++;
                break;
            }
            case Mnemonico.Mult:
            {
                var valorMult = instrucao.ModoEnderecamento switch
                {
                    ModoEnderecamento.Direto => pcb.Dados[instrucao.Endereco],
                    ModoEnderecamento.Imediato => instrucao.Valor ?? 0,
                    _ => 0
                };
                Acc *= valorMult;
                Pc++;
                break;
            }
            case Mnemonico.Div:
            {
                var valorDiv = instrucao.ModoEnderecamento switch
                {
                    ModoEnderecamento.Direto => pcb.Dados[instrucao.Endereco],
                    ModoEnderecamento.Imediato => instrucao.Valor ?? 0,
                    _ => 0
                };
                Acc /= valorDiv;
                Pc++;
                break;
            }
            case Mnemonico.Load:
            {
                var valorCarregar = instrucao.ModoEnderecamento switch
                {
                    ModoEnderecamento.Direto => pcb.Dados[instrucao.Endereco],
                    ModoEnderecamento.Imediato => instrucao.Valor ?? 0,
                    _ => 0
                };
                Acc = valorCarregar;
                Pc++;
                break;
            }
            case Mnemonico.Store:
            {
                pcb.Dados[instrucao.Endereco] = Acc;
                Pc++;
                break;
            }
            case Mnemonico.BrAny:
            {
                Pc = pcb.Labels[instrucao.Endereco];
                break;
            }
            case Mnemonico.BrPos:
            {
                if (Acc > 0)
                {
                    Pc = pcb.Labels[instrucao.Endereco];
                }
                else
                {
                    Pc++;
                }

                break;
            }
            case Mnemonico.BrZero:
            {
                if (Acc == 0)
                {
                    Pc = pcb.Labels[instrucao.Endereco];
                }
                else
                {
                    Pc++;
                }

                break;
            }
            case Mnemonico.BrNeg:
            {
                if (Acc < 0)
                {
                    Pc = pcb.Labels[instrucao.Endereco];
                }
                else
                {
                    Pc++;
                }

                break;
            }
            case Mnemonico.Syscall:
            {
                switch (instrucao.Valor)
                {
                    case 0:
                    {
                        finalizado = true;
                        break;
                    }
                    case 1:
                    {
                        Console.WriteLine(Acc);
                        bloquear = new Random().Next(10, 20);
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("Informe um número:");
                        Acc = Convert.ToInt32(Console.ReadLine());
                        bloquear = new Random().Next(10, 20);
                        break;
                    }
                }

                Pc++;
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
