using BlockSort.GameLogic;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("id", "playerName", "maxLevel", "curLevel")]
	public class ES3UserType_PlayerInfoSO : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerInfoSO() : base(typeof(PlayerInfoSO)){ Instance = this; priority = 1; }


		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (PlayerInfoSO)obj;
			
			writer.WritePrivateField("id", instance);
			writer.WritePrivateField("playerName", instance);
			writer.WritePrivateField("maxLevel", instance);
			writer.WritePrivateField("curLevel", instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerInfoSO)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "id":
					instance = (PlayerInfoSO)reader.SetPrivateField("id", reader.Read<System.String>(), instance);
					break;
					case "playerName":
					instance = (PlayerInfoSO)reader.SetPrivateField("playerName", reader.Read<System.String>(), instance);
					break;
					case "maxLevel":
					instance = (PlayerInfoSO)reader.SetPrivateField("maxLevel", reader.Read<System.Int32>(), instance);
					break;
					case "curLevel":
					instance = (PlayerInfoSO)reader.SetPrivateField("curLevel", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerInfoSOArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerInfoSOArray() : base(typeof(PlayerInfoSO[]), ES3UserType_PlayerInfoSO.Instance)
		{
			Instance = this;
		}
	}
}