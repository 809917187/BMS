namespace BMS.MQTT {
    public class TimedBackgroundService : BackgroundService {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {

            /*//while (!stoppingToken.IsCancellationRequested) {
            MQTTHelperClickHouse.SaveMqttPeriodDataToDB(1);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            MQTTHelperClickHouse.SaveMqttPeriodDataToDB(2);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            MQTTHelperClickHouse.SaveMqttPeriodDataToDB(3);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            MQTTHelperClickHouse.SaveMqttPeriodDataToDB(4);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);*/
            //}
        }

        private Task DoWorkAsync() {
            // 在这里实现你的任务逻辑
            Console.WriteLine("执行任务...");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken stoppingToken) {
            Console.WriteLine("服务已停止");
            return base.StopAsync(stoppingToken);
        }
    }
}
