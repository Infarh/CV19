using System;
using System.Net;
using System.Threading.Tasks;

namespace CV19.Web
{
    public class WebServer
    {
        public event EventHandler<RequestReceiverEventArgs> RequestReceived;

        //private TcpListener _Listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));
        private HttpListener _Listener;
        private readonly int _Port;
        private bool _Enabled;
        private readonly object _SyncRoot = new object();

        public int Port => _Port;

        public bool Enabled { get => _Enabled; set { if (value) Start(); else Stop(); } }

        public WebServer(int Port) => _Port = Port;

        public void Start()
        {
            if (_Enabled) return;
            lock (_SyncRoot)
            {
                if (_Enabled) return;

                _Listener = new HttpListener();
                _Listener.Prefixes.Add($"http://*:{_Port}/"); // netsh http add urlacl url=http://*:8080/ user=user_name
                _Listener.Prefixes.Add($"http://+:{_Port}/");
                _Enabled = true;
                ListenAsync();
            }
        }

        public void Stop()
        {
            if (!_Enabled) return;
            lock (_SyncRoot)
            {
                if (!_Enabled) return;

                _Listener = null;
                _Enabled = false;
            }
        }

        private async void ListenAsync()
        {
            var listener = _Listener;

            listener.Start();

            HttpListenerContext context = null;
            while (_Enabled)
            {
                var get_context_task = listener.GetContextAsync();
                if (context != null)
                    ProcessRequestAsync(context);
                context = await get_context_task.ConfigureAwait(false);
            }

            listener.Stop();
        }

        private async void ProcessRequestAsync(HttpListenerContext context)
        {
            await Task.Run(() => RequestReceived?.Invoke(this, new RequestReceiverEventArgs(context)));
        }
    }

    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context) => Context = context;
    }
}
