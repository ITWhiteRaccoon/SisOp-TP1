namespace SisOp_TP1;

public enum Syscall
{
    Exit = 0,
    Print = 1,
    Read = 2
}

public enum ModoEnderecamento
{
    Imediato,
    Direto
}

public enum Escalonamento
{
    SemPreempcao,
    ComPreempcao,
    RoundRobin
}

public enum Prioridade
{
    Alta = 0,
    Media = 1,
    Baixa = 2
}

public enum Mnemonico
{
    ADD,
    SUB,
    MULT,
    DIV,
    LOAD,
    STORE,
    BRANY,
    BRPOS,
    BRZERO,
    BRNEG,
    SYSCALL
}
