using System.IO;
using System.Linq;
using System.Timers;
using System.Collections.Generic;

using DSharpPlus.Entities;

using Serilog;

using Newtonsoft.Json;

using static Tofu.Util.ResourceManager;

namespace Tofu.Misc
{
    public class UserData
    {
        public static List<User> users;
        private static string jsonFile;

        public static void Init()
        {
            jsonFile = GetResourcePath("userdata", Util.ResourceType.JsonData);

            // Load/create data
            if(File.Exists(jsonFile))
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(jsonFile));
            else {
                users = new List<User>();
                File.WriteAllText(jsonFile, JsonConvert.SerializeObject(users, Formatting.Indented));
            }

            // Autosave
            Timer t = new Timer(300000);
            t.Elapsed += (e, s) => { 
                SaveData();
            };
            t.AutoReset = true;
            t.Start();

            Log.Information("User data system initialized");
        }

        public static void SaveData()
        {
            File.WriteAllText(jsonFile, JsonConvert.SerializeObject(users, Formatting.Indented));
        }

        public static User GetOrCreateUser(DiscordUser user)
        {
            // This probably could and should be improved but oh well, it works.
            if(users.FirstOrDefault(x => x.id == user.Id) != null) {
                return users.FirstOrDefault(x => x.id == user.Id);
            }
            
            // Possible trouble point; adding to a list but returning original value
            // Remove these comments if no issues arise
            User newUser = new User() { id = user.Id, username = user.Username };
            users.Add(newUser);
            Log.Information($"Created data entry for user: {user.Username}#{user.Discriminator} ({user.Id})");
            return newUser;
        }
    }

    public class User
    {
        public ulong id { get; set; }
        public string username { get; set; }

        // Leveling
        public bool levelMessages { get; set; } = true;
        public float xp { get; set; } = 0;
        public float totalxp { get; set; } = 0;
        public int level { get; set; } = 1;

        // Trivia
        public int correctTrivia { get; set; } = 0;
        public int totalTrivia { get; set; } = 0;
        public int triviaScore { get; set; } = 0;

        // Messages
        public bool optedOutOfMessages { get; set; } = false;
        public List<string> messages { get; set; } = new List<string>();
    }
}