using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace BMS.MQTT {
    public class MqttSubscribeService : BackgroundService {
        private IMqttClient _mqttClient;
        private MqttClientOptions _options;
        /*private string _topic = "test/topic";
        private string _host = "localhost";
        private int _port = 1883;*/
        private string _topic = "bluesun/bms/period/#";
        private string _host = "47.120.14.45";
        private int _port = 3011;
        private string username = "Bluesun";
        private string password = "Bluesun007";

        [Obsolete]
        public MqttSubscribeService(ILogger<MqttSubscribeService> logger) {
            /*var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(_host, _port) // 改成你的MQTT服务器地址和端口
                .WithCredentials(username, password)
                .Build();

            _mqttClient.ConnectedAsync += async e => {
                Console.WriteLine("MQTT 已连接");
                await _mqttClient.SubscribeAsync(_topic);
                Console.WriteLine("已订阅主题 " + _topic);
            };

            _mqttClient.DisconnectedAsync += async e => {
                Console.WriteLine("MQTT 已断开，准备重连...");
                // 可以考虑重连逻辑
            };

            _mqttClient.ApplicationMessageReceivedAsync += e => {
                var topic = e.ApplicationMessage.Topic;
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                MQTTHelperClickHouse.SaveMqttPeriodDataToDB(payload);
                return Task.CompletedTask;
            };*/
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            /*while (!stoppingToken.IsCancellationRequested) {
                if (!_mqttClient.IsConnected) {
                    try {
                        await _mqttClient.ConnectAsync(_options, stoppingToken);
                    } catch (Exception ex) {
                        Console.WriteLine($"MQTT连接失败: {ex.Message}");
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }

            if (_mqttClient.IsConnected) {
                await _mqttClient.DisconnectAsync();
            }*/
        }

        public override async Task StopAsync(CancellationToken cancellationToken) {
            /*if (_mqttClient.IsConnected) {
                await _mqttClient.DisconnectAsync();
            }
            await base.StopAsync(cancellationToken);*/
        }
    }
}
