using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Web;

namespace Water.Client
{
    internal class Connection
    {
        internal static Connection connection;

        public static Connection Instance => connection;

        public Connection(GrpcChannel channel)
        {
            Channel = channel;
            Client = new CharService.CharServiceClient(channel);
        }
        private readonly HashSet<uint> listenId = new HashSet<uint>();
        private readonly object locker = new object();

        public GrpcChannel Channel { get; }

        public CharService.CharServiceClient Client { get; }

        public void ShowStart(RequestRequest request)
        {
            var startTime = request.StartTime.ToDateTime().ToLocalTime();
            var url = $"id={request.ResultId}&type=1";
            new ToastContentBuilder()
                .AddText($"Request by {request.Name}")
                .AddText(request.WithText)
                .AddText($"Start At {startTime:HH:MM:SS} end at {startTime.Add(request.Deadline.ToTimeSpan()):HH:MM:SS}")
                .AddButton("Y", ToastActivationType.Foreground, $"{url}&res=y")
                .AddButton("N", ToastActivationType.Foreground, $"{url}&res=n")
                .SetToastDuration(ToastDuration.Long)
                .SetToastScenario(ToastScenario.Alarm)
                .Show();
        }
        public void ShowAccept(AcceptOrRefuseRequest request)
        {
            new ToastContentBuilder()
                .AddText($"Session Id {request.ResultId}")
                .AddText($"{request.Name} was accepted")
                .SetToastDuration(ToastDuration.Long)
                .SetToastScenario(ToastScenario.Alarm)
                .Show();
            lock (locker)
            {
                listenId.Add(request.ResultId);
            }
        }
        public void ShowRefuse(AcceptOrRefuseRequest request)
        {
            new ToastContentBuilder()
                .AddText($"Session Id {request.ResultId}")
                .AddText($"{request.Name} was refused")
                .SetToastDuration(ToastDuration.Long)
                .SetToastScenario(ToastScenario.Alarm)
                .Show();
        }
        public void ShowCancel(CancelRequest request)
        {
            new ToastContentBuilder()
                .AddText($"Session Id {request.ResultId} was canceled")
                .SetToastDuration(ToastDuration.Long)
                .SetToastScenario(ToastScenario.Alarm)
                .Show();
            lock (locker)
            {
                listenId.Remove(request.ResultId);
            }
        }
        public void ShowEndOfSession(EndRequestData request)
        {
            new ToastContentBuilder()
                .AddText($"Session Id {request.Request.ResultId} was ended")
                .AddText($"Has {string.Join(",", request.Joined)}")
                .SetToastDuration(ToastDuration.Long)
                .SetToastScenario(ToastScenario.Alarm)
                .Show();

            lock (locker)
            {
                listenId.Remove(request.Request.ResultId);
            }
        }

        public async Task StartMsgAsync()
        {
            while (true)
            {
                try
                {

                    var msgRep = Client.Msg(new EmptyRequest());
                    var stream = msgRep.ResponseStream;
                    while (await stream.MoveNext(CancellationToken.None))
                    {
                        switch (stream.Current.Type)
                        {
                            case MsgType.Start:
                                {
                                    var rep = RequestRequest.Parser.ParseFrom(stream.Current.Body);
                                    connection.listenId.Add(rep.ResultId);
                                    if (rep.Name != Program.name)
                                    {
                                        ShowStart(rep);
                                    }
                                }
                                break;
                            case MsgType.Cancel:
                                {
                                    var rep = CancelRequest.Parser.ParseFrom(stream.Current.Body);
                                    if (rep.Name != Program.name && connection.listenId.Contains(rep.ResultId))
                                    {
                                        ShowCancel(rep);
                                    }
                                }
                                break;
                            case MsgType.Accept:
                                {
                                    var rep = AcceptOrRefuseRequest.Parser.ParseFrom(stream.Current.Body);
                                    if (rep.Name != Program.name && connection.listenId.Contains(rep.ResultId))
                                    {
                                        ShowAccept(rep);
                                    }
                                }
                                break;
                            case MsgType.Refuse:
                                {
                                    var rep = AcceptOrRefuseRequest.Parser.ParseFrom(stream.Current.Body);
                                    if (rep.Name != Program.name && connection.listenId.Contains(rep.ResultId))
                                    {
                                        ShowRefuse(rep);
                                    }
                                }
                                break;
                            case MsgType.EndOfSession:
                                {
                                    var rep = EndRequestData.Parser.ParseFrom(stream.Current.Body);
                                    if (connection.listenId.Contains(rep.Request.ResultId))
                                    {
                                        ShowEndOfSession(rep);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await Task.Delay(1000);
                }
            }
        }
    }
    internal class Program
    {
        public static string name;

        static void Main(string[] args)
        {
            Console.WriteLine("What's your name");
            name = Console.ReadLine()!;
            var grpc = GrpcChannel.ForAddress("http://192.168.2.86:5678");
            Connection.connection = new Connection(grpc);
            _ = Connection.Instance.StartMsgAsync();
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
            while (true)
            {
                try
                {
                    Console.WriteLine("""
                    [q键] 立即邀约上厕所
                    """);
                    var k = Console.ReadKey();
                    switch (k.Key)
                    {
                        case ConsoleKey.Q:
                            {
                                Connection.Instance.Client.Start(new RequestRequest
                                {
                                    Name = name,
                                    StartTime = Timestamp.FromDateTime(DateTime.UtcNow),
                                    WithText = "上厕所吗",
                                    Deadline = Duration.FromTimeSpan(TimeSpan.FromSeconds(10))
                                });
                            }
                            break;
                        case ConsoleKey.A:
                            {
                                Console.Write("附加信息:");
                                var msg = Console.ReadLine();
                                Console.Write("开始时间(不填就是立刻):");
                                var timeStr = Console.ReadLine();
                                var time = DateTime.Now;
                                if (!string.IsNullOrEmpty(timeStr))
                                {
                                    time = DateTime.Parse(timeStr);
                                }
                                Console.Write("持续时间(HH:mm:ss):");
                                var deal = TimeSpan.Parse(Console.ReadLine());
                                Connection.Instance.Client.Start(new RequestRequest
                                {
                                    Name = name,
                                    StartTime = Timestamp.FromDateTime(time.ToUniversalTime()),
                                    WithText = msg,
                                    Deadline = Duration.FromTimeSpan(deal)
                                });
                            }
                            break;
                        case ConsoleKey.G:
                            {
                                Console.Write("会话Id:");
                                var msg = Console.ReadLine();
                                var res = Connection.Instance.Client.GetSession(new GetSessionRequest
                                {
                                    ResultId = uint.Parse(msg)
                                });
                                Console.WriteLine(@$"""
                                    Id:         {res.Id}
                                    Owner:      {res.Owner}
                                    WithText:   {res.WithText}
                                    State:      {res.State}
                                    Joined:     {string.Join(",", res.Joined)}
                                    """);
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            var type = HttpUtility.ParseQueryString(e.Argument);
            var val = type.Get("type");
            switch (val)
            {
                case "1":
                    {
                        var res = type.Get("res");
                        var id = uint.Parse(type.Get("id")!);
                        if (res == "y")
                        {
                            Connection.Instance.Client.Accept(new AcceptOrRefuseRequest
                            {
                                Name = name,
                                ResultId = id
                            });
                        }
                        else
                        {
                            Connection.Instance.Client.Refuse(new AcceptOrRefuseRequest
                            {
                                Name = name,
                                ResultId = id
                            });
                        }
                    }
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }
    }
}
