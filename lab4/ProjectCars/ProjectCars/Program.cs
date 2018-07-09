using System;

namespace ProjectCars
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ProjectCars game = new ProjectCars())
            {
                game.Run();
            }
        }
    }
#endif
}

