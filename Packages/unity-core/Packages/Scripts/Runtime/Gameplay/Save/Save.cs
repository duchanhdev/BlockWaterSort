using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save
{
    /// <summary>
    ///     This class contains all the variables that will be serialized and saved to a file.<br />
    ///     Can be considered as a save file structure or format.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Save
    {
        [JsonProperty]
        [SerializeField]
        private Setting.Setting _setting;

        public Save(Setting.Setting setting)
        {
            _setting = setting;
        }

        public Setting.Setting Setting
        {
            get => _setting;
            set => _setting = value;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void LoadFromJson(in string json)
        {
            var save = JsonConvert.DeserializeObject<Save>(json);
            if (save == null)
            {
                return;
            }

            _setting = save._setting;
        }
    }
}