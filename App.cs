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
        }
    }
}