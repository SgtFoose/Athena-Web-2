using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Athena.Objects.v2;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000036 RID: 54
	[StandardModule]
	public sealed class Common
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00012527 File Offset: 0x00010727
		public static string RemoveNonAlphaNumChars(string Value)
		{
			string empty = string.Empty;
			return Regex.Replace(Value, "[^a-zA-Z0-9_]", "_");
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012540 File Offset: 0x00010740
		public static double convert_DIR_ARMA(double DIR_ARMA)
		{
			double num = 0.0;
			try
			{
				if (DIR_ARMA != 0.0)
				{
					num = Math.Round(DIR_ARMA, 2);
					if (num > 360.0)
					{
						num -= num * Math.Floor(num / 360.0);
					}
					if (num < 0.0)
					{
						num = Math.Abs(num);
						num -= num * Math.Floor(num / 360.0);
						num = 360.0 - num;
					}
				}
			}
			catch (Exception ex)
			{
				num = 0.0;
			}
			return num;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000125EC File Offset: 0x000107EC
		public static void ConvertSize(string ARMA_SizeX, string ARMA_SizeY, ref int Width, ref int Height)
		{
			Width = 0;
			Height = 0;
			double num = -1.0;
			double num2 = -1.0;
			try
			{
				num = Convert.ToDouble(ARMA_SizeX, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				num = -1.0;
			}
			try
			{
				num2 = Convert.ToDouble(ARMA_SizeY, CultureInfo.InvariantCulture);
			}
			catch (Exception ex2)
			{
				num2 = -1.0;
			}
			if (num != -1.0 & num2 != -1.0)
			{
				Width = Convert.ToInt32(num, CultureInfo.InvariantCulture);
				Height = Convert.ToInt32(num2, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000126C4 File Offset: 0x000108C4
		public static void ConvertGridToPos(ref Map Map, string Grid, ref double PosX, ref double PosY)
		{
			PosX = -1.0;
			PosY = -1.0;
			if (Map != null && Map.WorldSize >= 0 && Grid.Trim().Length == 6)
			{
				string value = Grid.Substring(0, 3);
				string value2 = Grid.Substring(3, 3);
				int num = -1;
				int num2 = -1;
				try
				{
					num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					num = -1;
				}
				try
				{
					num2 = Convert.ToInt32(value2, CultureInfo.InvariantCulture);
				}
				catch (Exception ex2)
				{
					num2 = -1;
				}
				double num3 = (double)Map.OffsetX;
				double num4 = (double)(checked(Map.WorldSize - Map.OffsetY));
				bool flag = Conversions.ToBoolean(Interaction.IIf(Map.StepY > 0, true, false));
				if (Conversions.ToBoolean(Interaction.IIf(Map.StepX > 0, true, false)))
				{
					PosX = num3 + (double)(checked(num * 100));
					if (PosX > (double)Map.WorldSize)
					{
						PosX = num3 - (double)(checked((1000 - num) * 100));
						if (PosX < -99.0)
						{
							PosX = -1.0;
						}
					}
					if (PosX != -1.0)
					{
						PosX += 50.0;
					}
				}
				else
				{
					PosX = num3 - (double)(checked(num * 100));
					if (PosX < -99.0)
					{
						PosX = num3 + (double)(checked((1000 - num) * 100));
						if (PosX > (double)Map.WorldSize)
						{
							PosX = -1.0;
						}
					}
					if (PosX != -1.0)
					{
						PosX -= 50.0;
					}
				}
				if (flag)
				{
					PosY = num4 - (double)(checked(num2 * 100));
					if (PosY < -99.0)
					{
						PosY = num4 + (double)(checked((1000 - num2) * 100));
						if (PosY > (double)Map.WorldSize)
						{
							PosY = -1.0;
						}
					}
					if (PosY != -1.0)
					{
						PosY -= 50.0;
						return;
					}
				}
				else
				{
					PosY = num4 + (double)(checked(num2 * 100));
					if (PosY > (double)Map.WorldSize)
					{
						PosY = num4 - (double)(checked((1000 - num2) * 100));
						if (PosY < -99.0)
						{
							PosY = -1.0;
						}
					}
					if (PosY != -1.0)
					{
						PosY += 50.0;
					}
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00012944 File Offset: 0x00010B44
		public static void ConvertPointToPos(int WorldSize, Point Target, ref double PosX, ref double PosY)
		{
			if (WorldSize >= 0)
			{
				try
				{
					PosX = Conversions.ToDouble(Target.X.ToString());
					PosY = (double)WorldSize - Target.Y;
				}
				catch (Exception ex)
				{
					PosX = -1.0;
					PosY = -1.0;
				}
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000129B0 File Offset: 0x00010BB0
		public static void ConvertPos(int WorldSize, string ARMA_PosX, string ARMA_PosY, ref double Canvas_X, ref double Canvas_Y)
		{
			Canvas_X = 0.0;
			Canvas_Y = 0.0;
			double num = -1.0;
			double num2 = -1.0;
			try
			{
				if (!string.IsNullOrEmpty(ARMA_PosX))
				{
					num = Convert.ToDouble(ARMA_PosX, CultureInfo.InvariantCulture);
				}
			}
			catch (Exception ex)
			{
				num = -1.0;
				Canvas_X = -1.0;
			}
			try
			{
				if (!string.IsNullOrEmpty(ARMA_PosY))
				{
					num2 = Convert.ToDouble(ARMA_PosY, CultureInfo.InvariantCulture);
				}
			}
			catch (Exception ex2)
			{
				num2 = -1.0;
				Canvas_Y = -1.0;
			}
			if (num != -1.0 & num2 != -1.0)
			{
				Canvas_X = num;
				Canvas_Y = (double)WorldSize - num2;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00012AA4 File Offset: 0x00010CA4
		public static void ConvertPosToGrid(ref Map Map, string ARMA_PosX, string ARMA_PosY, ref string GridPos)
		{
			GridPos = string.Empty;
			if (Map != null && Map.WorldSize >= 0)
			{
				double num = -1.0;
				double num2 = -1.0;
				try
				{
					num = Convert.ToDouble(ARMA_PosX, CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					num = -1.0;
				}
				try
				{
					num2 = Convert.ToDouble(ARMA_PosY, CultureInfo.InvariantCulture);
				}
				catch (Exception ex2)
				{
					num2 = -1.0;
				}
				if (num != -1.0 & num2 != -1.0)
				{
					double num3 = (double)Map.OffsetX;
					double num4 = (double)(checked(Map.WorldSize - Map.OffsetY));
					bool flag = Conversions.ToBoolean(Interaction.IIf(Map.StepY > 0, true, false));
					bool flag2 = Conversions.ToBoolean(Interaction.IIf(Map.StepX > 0, true, false));
					double num5 = num3 - num;
					double num6 = num4 - num2;
					double num7 = Math.Floor(Math.Abs(num5) / 100.0);
					double num8 = Math.Floor(Math.Abs(num6) / 100.0);
					if (flag2)
					{
						if (num5 > 0.0)
						{
							num7 = 999.0 - Math.Floor(Math.Abs(num5) / 100.0);
						}
					}
					else if (num5 < 0.0)
					{
						num7 = 999.0 - Math.Floor(Math.Abs(num5) / 100.0);
					}
					if (flag)
					{
						if (num6 < 0.0)
						{
							num8 = 999.0 - Math.Floor(Math.Abs(num6) / 100.0);
						}
					}
					else if (num6 > 0.0)
					{
						num8 = 999.0 - Math.Floor(Math.Abs(num6) / 100.0);
					}
					GridPos = num7.ToString().PadLeft(3, '0') + num8.ToString().PadLeft(3, '0');
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00012CF0 File Offset: 0x00010EF0
		public static void ConvertMousePosToGrid(ref Map Map, double MouseX, double MouseY, ref string GridPos)
		{
			GridPos = string.Empty;
			if (Map.WorldSize >= 0 && (MouseX != -1.0 & MouseY != -1.0))
			{
				double num = (double)Map.OffsetX;
				double num2 = (double)(checked(Map.WorldSize - Map.OffsetY));
				bool flag = Conversions.ToBoolean(Interaction.IIf(Map.StepY > 0, true, false));
				bool flag2 = Conversions.ToBoolean(Interaction.IIf(Map.StepX > 0, true, false));
				double num3 = num - MouseX;
				double num4 = num2 - ((double)Map.WorldSize - MouseY);
				double num5 = Math.Floor(Math.Abs(num3) / 100.0);
				double num6 = Math.Floor(Math.Abs(num4) / 100.0);
				if (flag2)
				{
					if (num3 > 0.0)
					{
						num5 = 999.0 - Math.Floor(Math.Abs(num3) / 100.0);
					}
				}
				else if (num3 < 0.0)
				{
					num5 = 999.0 - Math.Floor(Math.Abs(num3) / 100.0);
				}
				if (flag)
				{
					if (num4 < 0.0)
					{
						num6 = 999.0 - Math.Floor(Math.Abs(num4) / 100.0);
					}
				}
				else if (num4 > 0.0)
				{
					num6 = 999.0 - Math.Floor(Math.Abs(num4) / 100.0);
				}
				GridPos = num5.ToString().PadLeft(3, '0') + num6.ToString().PadLeft(3, '0');
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00012EB0 File Offset: 0x000110B0
		public static Point ConvertPosToPoint(int WorldSize, ref string PosX, ref string PosY)
		{
			double x = 0.0;
			double y = 0.0;
			Common.ConvertPos(WorldSize, PosX, PosY, ref x, ref y);
			return new Point(x, y);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012EE8 File Offset: 0x000110E8
		public static Point ConvertMouseToCanvas(ref Point MousePos, ref ScrollViewer ScrollView, ref double ZoomFactor)
		{
			Point result = MousePos;
			result.X = (ScrollView.HorizontalOffset + result.X) / ZoomFactor;
			result.Y = (ScrollView.VerticalOffset + result.Y) / ZoomFactor;
			return result;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012F30 File Offset: 0x00011130
		public static string CreateUnitString(ref Unit Unit, bool IncludeRank, bool IsLeader)
		{
			string text = string.Empty;
			if (Unit != null)
			{
				if (IncludeRank)
				{
					switch (Unit.RankID)
					{
					case Enums.Rank.PRIVATE:
						text = " (Pvt)";
						break;
					case Enums.Rank.CORPORAL:
						text = " (Cpl)";
						break;
					case Enums.Rank.SERGEANT:
						text = " (Sgt)";
						break;
					case Enums.Rank.LIEUTENANT:
						text = " (Lt)";
						break;
					case Enums.Rank.CAPTAIN:
						text = " (Cpt)";
						break;
					case Enums.Rank.MAJOR:
						text = " (Mjr)";
						break;
					case Enums.Rank.COLONEL:
						text = " (Col)";
						break;
					default:
						text = " (Pvt)";
						break;
					}
				}
				if (string.IsNullOrEmpty(Unit.SteamID))
				{
					text = string.Concat(new string[]
					{
						text,
						" ",
						Unit.DisplayName,
						Common.CreateWeaponsString(Unit.Weapon1, Unit.Weapon2, Unit.Weapon1IsGL),
						Common.CreateClassString(ref Unit)
					});
				}
				else
				{
					text = string.Concat(new string[]
					{
						text,
						" ",
						Unit.Player,
						Common.CreateWeaponsString(Unit.Weapon1, Unit.Weapon2, Unit.Weapon1IsGL),
						Common.CreateClassString(ref Unit)
					});
				}
				if (IsLeader)
				{
					text += " *";
				}
			}
			return text;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001306C File Offset: 0x0001126C
		public static string CreateClassString(ref Unit Unit)
		{
			string result = string.Empty;
			if (Unit != null)
			{
				if (Unit.Type.Trim().ToLower().Contains("medic") | Operators.CompareString(Unit.HasMedikit, "1", false) == 0)
				{
					result = " (Medic)";
				}
				if (Unit.Type.Trim().ToLower().Contains("crew"))
				{
					result = " (Crew)";
				}
				if (Unit.Type.Trim().ToLower().Contains("pilot"))
				{
					result = " (Pilot)";
				}
				if (Unit.Type.Trim().ToLower().Contains("engineer"))
				{
					result = " (Engineer)";
				}
			}
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001312C File Offset: 0x0001132C
		public static string CreateWeaponsString(string Weapon1, string Weapon2, string Weapon1IsGL)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(Weapon1) | !string.IsNullOrEmpty(Weapon2))
			{
				if (string.IsNullOrEmpty(Weapon2))
				{
					string text = Common.CreateWeaponString(Weapon1, Weapon1IsGL);
					if (!string.IsNullOrEmpty(text))
					{
						result = " [" + text + "]";
					}
				}
				else
				{
					string text2 = Common.CreateWeaponString(Weapon1, Weapon1IsGL);
					string text3 = Common.CreateWeaponString(Weapon2, "0");
					if (!string.IsNullOrEmpty(text2) & !string.IsNullOrEmpty(text3))
					{
						if (string.IsNullOrEmpty(text2))
						{
							result = " [" + text2 + "]";
						}
						else if (string.IsNullOrEmpty(text3))
						{
							result = " [" + text2 + "]";
						}
						else
						{
							result = string.Concat(new string[]
							{
								" [",
								text2,
								"/",
								text3,
								"]"
							});
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00013214 File Offset: 0x00011414
		public static string CreateWeaponString(string Weapon, string WeaponIsGL)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(Weapon))
			{
				string text2 = Weapon.ToLower();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 2759233313U)
				{
					if (num <= 234279893U)
					{
						if (num != 79721040U)
						{
							if (num == 234279893U)
							{
								if (Operators.CompareString(text2, "shotgun", false) == 0)
								{
									text = "SG";
								}
							}
						}
						else if (Operators.CompareString(text2, "sniperrifle", false) == 0)
						{
							text = "SN";
						}
					}
					else if (num != 664985372U)
					{
						if (num != 884715675U)
						{
							if (num == 2759233313U)
							{
								if (Operators.CompareString(text2, "rocketlauncher", false) == 0)
								{
									text = "AT";
								}
							}
						}
						else if (Operators.CompareString(text2, "missilelauncher", false) == 0)
						{
							text = "AT";
						}
					}
					else if (Operators.CompareString(text2, "cannon", false) != 0)
					{
					}
				}
				else if (num <= 3813049233U)
				{
					if (num != 3226444333U)
					{
						if (num != 3777931474U)
						{
							if (num == 3813049233U)
							{
								if (Operators.CompareString(text2, "launcher", false) != 0)
								{
								}
							}
						}
						else if (Operators.CompareString(text2, "assaultrifle", false) == 0)
						{
							text = "R";
						}
					}
					else if (Operators.CompareString(text2, "bomblauncher", false) != 0)
					{
					}
				}
				else if (num != 3887481340U)
				{
					if (num != 4227936217U)
					{
						if (num == 4239060780U)
						{
							if (Operators.CompareString(text2, "submachinegun", false) == 0)
							{
								text = "SMG";
							}
						}
					}
					else if (Operators.CompareString(text2, "grenadelauncher", false) == 0)
					{
						text = "GL";
					}
				}
				else if (Operators.CompareString(text2, "machinegun", false) == 0)
				{
					text = "AR";
				}
			}
			if (!string.IsNullOrEmpty(WeaponIsGL) && Operators.CompareString(WeaponIsGL.Trim(), "1", false) == 0)
			{
				text += "GL";
			}
			return text;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00013410 File Offset: 0x00011610
		public static string SerializeStrokeCollection(ref StrokeCollection Collection)
		{
			string result = string.Empty;
			try
			{
				if (Collection != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					Collection.Save(memoryStream);
					memoryStream.Position = 0L;
					result = BitConverter.ToString(memoryStream.ToArray()).Replace("-", "");
					memoryStream.Close();
					memoryStream.Dispose();
				}
			}
			catch (Exception ex)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00013490 File Offset: 0x00011690
		public static StrokeCollection DeserializeStrokeCollection(string Data)
		{
			StrokeCollection result = null;
			try
			{
				if (!string.IsNullOrEmpty(Data))
				{
					SoapHexBinary soapHexBinary = SoapHexBinary.Parse(Data);
					if (soapHexBinary != null)
					{
						MemoryStream memoryStream = new MemoryStream(soapHexBinary.Value);
						if (memoryStream != null)
						{
							result = new StrokeCollection(memoryStream);
						}
					}
				}
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000134F0 File Offset: 0x000116F0
		public static MemoryStream create_memory_stream(string object_str)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				byte[] bytes = new UTF8Encoding().GetBytes(object_str);
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Position = 0L;
			}
			catch (Exception ex)
			{
				memoryStream = null;
			}
			return memoryStream;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013548 File Offset: 0x00011748
		public static StreamReader create_reader(string object_str)
		{
			StreamReader result = null;
			MemoryStream stream = Common.create_memory_stream(object_str);
			try
			{
				result = new StreamReader(stream);
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000169 RID: 361
		public static Dictionary<string, DictionaryOfGroupItem> DictionaryOfGroups = new Dictionary<string, DictionaryOfGroupItem>();

		// Token: 0x0400016A RID: 362
		public static Dictionary<string, DictionaryOfInkItem> DictionaryOfInk = new Dictionary<string, DictionaryOfInkItem>();

		// Token: 0x0400016B RID: 363
		public static Dictionary<string, DictionaryOfMarkerItem> DictionaryOfMarkers = new Dictionary<string, DictionaryOfMarkerItem>();

		// Token: 0x0400016C RID: 364
		public static Dictionary<string, DictionaryOfUnitItem> DictionaryOfUnits = new Dictionary<string, DictionaryOfUnitItem>();

		// Token: 0x0400016D RID: 365
		public static Dictionary<string, DictionaryOfVehicleItem> DictionaryOfVehicles = new Dictionary<string, DictionaryOfVehicleItem>();

		// Token: 0x0400016E RID: 366
		public static Color COLOR_WEST = Color.FromArgb(180, 78, 118, 204);

		// Token: 0x0400016F RID: 367
		public static Color COLOR_EAST = Color.FromArgb(180, 204, 78, 78);

		// Token: 0x04000170 RID: 368
		public static Color COLOR_GREEN = Color.FromArgb(180, 0, 128, 0);

		// Token: 0x04000171 RID: 369
		public static Color COLOR_CIV = Color.FromArgb(180, 178, 0, byte.MaxValue);
	}
}
