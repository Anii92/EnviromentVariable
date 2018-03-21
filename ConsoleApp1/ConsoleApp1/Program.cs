using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnviromentVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            //Crea una variable de entorno desde PowerShell
            PowerShell(@"../../CreateEnviromentVariable.ps1");
            string value = GetEnviromentVariable("TestVariable");
            Console.WriteLine("El valor de la variable de entorno TestVariable es " + value);
            Console.WriteLine("Pulsa enter para cerrar el programa");
            Console.ReadLine();

            //Crea una variable de entorno desde c#
            //string value = GetEnviromentVariable("Test1");
            //if (string.IsNullOrEmpty(value))
            //{
            //    CreateEnviromentVariable("Test1", "Value1");
            //    value = GetEnviromentVariable("Test1");
            //}
        }

        private static void PowerShell(String scriptFile)
        {
            InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
            initialSessionState.ApartmentState = ApartmentState.STA;
            initialSessionState.ThreadOptions = PSThreadOptions.UseCurrentThread;

            //RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();

            Runspace runspace = RunspaceFactory.CreateRunspace(initialSessionState);
            runspace.Open();

            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);

            Pipeline pipeline = runspace.CreatePipeline();

            //Here's how you add a new script with arguments
            Command myCommand = new Command(scriptFile);

            pipeline.Commands.Add(myCommand);

            // Execute PowerShell script
            var results = pipeline.Invoke();
        }

        private static void CreateEnviromentVariable(string name, string value)
        {
            Environment.SetEnvironmentVariable("Test1", "Value1");
        }

        private static string GetEnviromentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
