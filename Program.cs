using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SisOp_TP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("Indique o arquivo de configuração na pasta Input");
                Console.WriteLine("Exemplo de chamada: Escalonador config.yaml");
            }

            TextReader file = new StreamReader($"Input/{args[0]}");
            Deserializer? deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            var config = deserializer.Deserialize<Escalonador>(file);
            IniciarPrograma(config);
        }

        private static void IniciarPrograma(Escalonador escalonador)
        {
            foreach (Processo processo in escalonador.Processos)
            {
                Console.WriteLine(processo);
            }
        }
    }
}
