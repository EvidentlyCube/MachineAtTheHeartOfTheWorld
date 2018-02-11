using System;
using System.Linq.Expressions;
using IrregularMachine.Core;

namespace IrregularMachine
{
//#if WINDOWS || LINUX // This conditional compilation is commented out because right now not all constants are correctly defined
// and we can't add our own in a simple fashion. I'll figure it out later
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Logger.Init("Main ran");
            try {
                using (var game = new GameCore()) {
                    Logger.Init("GameCore finished instantiation");
                    game.Run();
                }
            }
            catch (Exception e) {
                Logger.Error("Exception occured");
                Logger.Error(e.Message);
                Logger.Error(e.StackTrace);
            }
        }
    }

//#endif
}