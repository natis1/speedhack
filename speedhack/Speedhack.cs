using System.Globalization;
using System.IO;
using UnityEngine;

namespace speedhack
{
    public class Speedhack : Modding.Mod
    {
        private float targetSpeed = 1.0f;
        public override void Initialize()
        {
            this._modName = "Speedhack";
            this._modVersion = "1.0 - Speed set to: ";

            string path = "Mods/speedhack.cfg";
            if (File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("Speed") && lines.Length > i + 1)
                    {
                        targetSpeed = float.Parse(lines[i + 1], CultureInfo.InvariantCulture.NumberFormat);
                        this._modVersion += targetSpeed.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Speed");
                    sw.WriteLine("1.00");
                    this._modVersion = "1.0 - Edit speedhack.cfg to change your speed";
                }
            }
            
            GameObject go = new GameObject("Speedhacker", typeof(speedchanger));
            go.GetComponent<speedchanger>().targetSpeed = targetSpeed;
            Object.DontDestroyOnLoad(go);

        }

        // By loading earlier it is more likely to get overwritten by other mods.
        public override int LoadPriority()
        {
            return 1000;
        }
    }
}