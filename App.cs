using System.Drawing;
using SisOp_TP1.Config;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SisOp_TP1
{
    public class App
    {
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("Indique o arquivo de configuração na pasta Input");
                Console.WriteLine("Exemplo de chamada: Escalonador config.yaml");
            }

            TextReader file = new StreamReader($"Input/{args[0]}");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            var config = deserializer.Deserialize<ArquivoConfiguracao>(file);
            switch (config.PoliticaEscalonamento)
            {
                case Escalonamento.ComPreempcao:
                {
                    var esc = new EscalonadorComPreempcao(config.Programas);
                    esc.Iniciar();
                    Console.WriteLine(esc.ToString());
                    break;
                }
                case Escalonamento.SemPreempcao:
                {
                    var esc = new EscalonadorSemPreempcao(config.Programas);
                    esc.Iniciar();
                    break;
                }
                case Escalonamento.RoundRobin:
                {
                    var esc = new EscalonadorRoundRobin(config.Programas);
                    esc.Iniciar();
                    break;
                }
            }
        }
    }
}
