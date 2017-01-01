using System.Threading.Tasks;

namespace ProgrammingGame.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mainServerFlow = new MainServerFlow();
            mainServerFlow.Run();
        }
    }
}
