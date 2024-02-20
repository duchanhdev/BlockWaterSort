using BlockSort.GameLogic;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("maxTube", "numTube", "tubes")]
	public class ES3UserType_GameStatus : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_GameStatus() : base(typeof(GameStatus)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (GameStatus)obj;
			
			writer.WriteProperty("maxTube", GameStatus.maxTube, ES3Type_int.Instance);
			writer.WritePrivateField("numTube", instance);
			writer.WritePrivateField("tubes", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (GameStatus)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "maxTube":
						GameStatus.maxTube = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "numTube":
					instance = (GameStatus)reader.SetPrivateField("numTube", reader.Read<System.Int32>(), instance);
					break;
					case "tubes":
					instance = (GameStatus)reader.SetPrivateField("tubes", reader.Read<System.Collections.Generic.List<Tube>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new GameStatus();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_GameStatusArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_GameStatusArray() : base(typeof(GameStatus[]), ES3UserType_GameStatus.Instance)
		{
			Instance = this;
		}
	}
}