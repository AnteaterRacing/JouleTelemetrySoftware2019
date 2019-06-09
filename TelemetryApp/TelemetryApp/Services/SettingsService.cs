using System;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using TelemetryApp.Models;

namespace TelemetryApp.Services
{
    public static class SettingsService
    {
        private const string SettingsFilename = "TelemetryAppSettings.json";
        private static readonly StorageFolder SettingsFolder = ApplicationData.Current.LocalFolder;

        public static async Task<Settings> LoadSettings()
        {
            try
            {
                var sf = await SettingsFolder.GetFileAsync(SettingsFilename);
                if (sf == null) return null;

                var content = await FileIO.ReadTextAsync(sf);
                return JsonConvert.DeserializeObject<Settings>(content);
            }
            catch
            {
                return null;
            }
        }

        public static async Task<bool> SaveSettings(Settings data)
        {
            try
            {
                var file = await SettingsFolder.CreateFileAsync(SettingsFilename,
                    CreationCollisionOption.ReplaceExisting);
                var content = JsonConvert.SerializeObject(data);
                await FileIO.WriteTextAsync(file, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}