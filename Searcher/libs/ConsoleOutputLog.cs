//This library is found and referred from: https://gist.github.com/mikey-t/7999228
//I have only modified the filepath to my desired location.

using System.IO;
using System;
using static System.DateTime;
using static Utils.FileUtils;
using static Utils.StringUtils;

public class Output {
    private readonly string  LogDirPath = GetChildDir("logs");

    private static Output _outputSingleton;
    private static Output OutputSingleton
    {
        get
        {
            if (_outputSingleton == null)
            {
                _outputSingleton = new Output();
            }
            return _outputSingleton;
        }
    }

    public StreamWriter SW { get; set; }

    public Output()
    {
        EnsureLogDirectoryExists();
        InstantiateStreamWriter();
    }

    ~Output()
    {
        if (SW != null)
        {
            try
            {
                SW.Dispose();
            }
            catch(ObjectDisposedException){} // object already disposed - ignore exception
        }
    }

    public static void WriteLine(string str)
    {
        Console.WriteLine(str);
        OutputSingleton.SW.WriteLine(str);
    }

    public static void Write(string str)
    {
        Console.Write(str);
        OutputSingleton.SW.Write(str);
    }

    private void InstantiateStreamWriter()
    {
        string filePath = $"{GetChildDir("logs")}\\log_{ParseDateTimeToString(Now)}.yaml";
        try
        {   
            SW = new StreamWriter(filePath);
            SW.AutoFlush = true;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new ApplicationException(string.Format("Access denied. Could not instantiate StreamWriter using path: {0}.", filePath), ex);
        }
    }

    private void EnsureLogDirectoryExists()
    {
        if (!Directory.Exists(LogDirPath))
        {
            try
            {
                Directory.CreateDirectory(LogDirPath);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new ApplicationException(string.Format("Access denied. Could not create log directory using path: {0}.", LogDirPath), ex);
            }
        }
    }
}