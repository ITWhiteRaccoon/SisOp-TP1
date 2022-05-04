namespace SisOp_TP1;

public sealed class GeradorPid
{
    private int _proximoPid;
    private static GeradorPid? _instancia;

    private GeradorPid()
    {
        _proximoPid = 0;
    }

    public static int GerarProximo()
    {
        _instancia ??= new GeradorPid();

        var estePid = _instancia._proximoPid;
        _instancia._proximoPid++;
        return estePid;
    }
}