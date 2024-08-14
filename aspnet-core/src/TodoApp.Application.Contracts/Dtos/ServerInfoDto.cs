using System;

namespace TodoApp
{
    public class ServerInfoDto
    {
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
        public string DotNetVersion { get; set; }
        
        // Thông tin về Thread Pool
        public int MinWorkerThreads { get; set; }
        public int MaxWorkerThreads { get; set; }
        public int AvailableWorkerThreads { get; set; }
        public int MinCompletionPortThreads { get; set; }
        public int MaxCompletionPortThreads { get; set; }
        public int AvailableCompletionPortThreads { get; set; }
    }
}
