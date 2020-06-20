using System;
using System.IO;
using System.Threading;
using CV19.Services.Interfaces;
using CV19.Web;

namespace CV19.Services
{
    internal class HttpListenerWebSeverer : IWebServerService
    {
        private readonly WebServer _Server = new WebServer(8080);

        public bool Enabled { get => _Server.Enabled; set => _Server.Enabled = value; }

        public void Start() => _Server.Start();

        public void Stop() => _Server.Stop();

        public HttpListenerWebSeverer() => _Server.RequestReceived += OnRequestReceived;

        private static void OnRequestReceived(object Sender, RequestReceiverEventArgs E)
        {
            Thread.Sleep(3000);
            using var writer = new StreamWriter(E.Context.Response.OutputStream);
            writer.WriteLine("CV-19 Application - " + DateTime.Now);
        }
    }
}
