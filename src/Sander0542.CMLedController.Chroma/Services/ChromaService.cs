using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChromaBroadcast;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Chroma.Services
{
    public class ChromaService : BackgroundService
    {
        // private static readonly Guid ChromeBroadcastGuid = Guid.Empty;
        // private static readonly Guid ChromeBroadcastGuid = Guid.Parse("560e8823-636a-40a4-900f-209a512a5be5");
        private static readonly Guid ChromeBroadcastGuid = Guid.Parse("33d8f7b0-fb49-4705-b343-ae536033afc3");
        // private static readonly Guid ChromeBroadcastGuid = Guid.Parse("def05dce-1662-4d9a-a312-a31028651915");

        private readonly ILogger<ChromaService> _logger;
        private readonly ILedControllerProvider _ledControllerProvider;

        private IEnumerable<ILedControllerDevice> _ledControllerDevices;

        public ChromaService(ILogger<ChromaService> logger, ILedControllerProvider ledControllerProvider)
        {
            _logger = logger;
            _ledControllerProvider = ledControllerProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var result = RzChromaBroadcastAPI.Init(ChromeBroadcastGuid);

            if (result == RzResult.Success)
            {
                RzChromaBroadcastAPI.RegisterEventNotification(OnChromaBroadcastEvent);
                _logger.LogInformation("Razer Chroma Broadcast API successfully initialized");
            }
            else
            {
                _logger.LogError($"Could not initialize Razer Chroma Broadcast API ({result})");
                return;
            }

            _ledControllerDevices = await _ledControllerProvider.GetControllersAsync(stoppingToken);

            if (!_ledControllerDevices.Any())
            {
                _logger.LogWarning("There are not RGB Led Controllers installed");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unregistering Razer Chroma Broadcast API");
            RzChromaBroadcastAPI.UnRegisterEventNotification();
            RzChromaBroadcastAPI.UnInit();

            await base.StopAsync(cancellationToken);
        }

        private RzResult OnChromaBroadcastEvent(RzChromaBroadcastType type, RzChromaBroadcastStatus? status, RzChromaBroadcastEffect? effect)
        {
            switch (type)
            {
                case RzChromaBroadcastType.BroadcastEffect:
                    if (effect.HasValue)
                    {
                        foreach (var device in _ledControllerDevices)
                        {
                            device.SetMultipleColorAsync(effect.Value.ChromaLink1, effect.Value.ChromaLink2, effect.Value.ChromaLink3, effect.Value.ChromaLink4).Start();
                        }
                    }
                    break;
                case RzChromaBroadcastType.BroadcastStatus:
                    switch (status)
                    {
                        case RzChromaBroadcastStatus.Live:
                            _logger.LogInformation("Razer Chroma Broadcast API is live");
                            break;
                        case RzChromaBroadcastStatus.NotLive:
                            _logger.LogInformation("Razer Chroma Broadcast API is not live");
                            break;
                    }
                    break;
            }

            return RzResult.Success;
        }
    }
}
