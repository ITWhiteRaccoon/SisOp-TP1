namespace SisOp_TP1;

public enum Escalonamento
{
    SemPreempcao,
    ComPreempcao,
    RoundRobin
}

public enum Prioridade
{
    Alta=0,
    Media=1,
    Baixa=2
}

public enum Instrucoes
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