// See https://aka.ms/new-console-template for more information
using WmiProcessTracker;


using WmiClient wmiClient = new WmiClient();
wmiClient.ProcessCreation += ProcessCreation;
wmiClient.ProcessDeletion += ProcessDeletion;


wmiClient.Start();
Console.ReadLine();



void ProcessCreation(Win32_Process win32_Process)
{
    Console.WriteLine($"ProcessCreation: {win32_Process.ExecutablePath} {win32_Process.ProcessId}");
}

void ProcessDeletion(Win32_Process win32_Process)
{
    Console.WriteLine($"ProcessDeletion: {win32_Process.ExecutablePath} {win32_Process.ProcessId}");
}