using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace AistrikesV_Client
{
    public class AirstrikesV_Client : BaseScript
    {
        public AirstrikesV_Client()
        {
            Tick += OnTick;

            RegisterCommand("airstrikes", new Action<int, List<object>, string>((source, arguments, raw) =>{
                
                if (arguments[0] == "light" || arguments[0]=="medium" || arguments[0]=="heavy")
                {
                    AirstrikeHandler(GetEntityCoords(PlayerPedId(),true).X, GetEntityCoords(PlayerPedId(), true).Y, GetEntityCoords(PlayerPedId(), true).Z,"light");
                }
            }), false);
        }
        private async Task AirstrikeHandler(float x, float y, float z, string attacktype)
        {
            var wanted_model = "S_M_Y_MARINE_01";
            var modelhash = GetHashKey(wanted_model);
            var hash = GetHashKey("lazer");

            RequestModel(Convert.ToUInt32(hash));
            RequestModel(Convert.ToUInt32(modelhash));
            while (!HasModelLoaded(Convert.ToUInt32(hash)) && !HasModelLoaded(Convert.ToUInt32(hash)))
            {
                await Delay(0);
            }

            var aircraft = CreateVehicle(Convert.ToUInt32(hash), 4510.431f, 330.208f, 519.128f, 0.0f, true, true);
            var pilot = CreatePedInsideVehicle(aircraft, 1, Convert.ToUInt32(modelhash), -1, true, true);
            var blip = AddBlipForEntity(pilot);
            TaskVehicleDriveToCoord(pilot, aircraft, x, y, z, 1500.0f * 3.6f, 500, Convert.ToUInt32(hash), 16777216, 1.0f, 1.0f);

        }
        private async Task OnTick()
        {
            await Delay(0);
        }
    }
}
