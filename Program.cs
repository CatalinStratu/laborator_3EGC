using System;


namespace Proiect
{
    class Program

    {   [STAThread]
        static void Main(string[] args)
        {
            Window3D ex =new  Window3D();

            //activam fereasta
            ex.Run(30.0,0.0);

        }
    }
}
