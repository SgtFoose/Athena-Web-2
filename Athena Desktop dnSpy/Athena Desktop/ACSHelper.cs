using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Ink;
using Athena.Tools.Sockets;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000007 RID: 7
	public class ACSHelper
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000011 RID: 17 RVA: 0x00002140 File Offset: 0x00000340
		// (remove) Token: 0x06000012 RID: 18 RVA: 0x00002178 File Offset: 0x00000378
		public event ACSHelper.OnErrorEventHandler OnError;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000013 RID: 19 RVA: 0x000021B0 File Offset: 0x000003B0
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x000021E8 File Offset: 0x000003E8
		public event ACSHelper.OnMessageEventHandler OnMessage;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000015 RID: 21 RVA: 0x00002220 File Offset: 0x00000420
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x00002258 File Offset: 0x00000458
		public event ACSHelper.OnAuthenticatedEventHandler OnAuthenticated;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000017 RID: 23 RVA: 0x00002290 File Offset: 0x00000490
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x000022C8 File Offset: 0x000004C8
		public event ACSHelper.OnConnectedEventHandler OnConnected;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000019 RID: 25 RVA: 0x00002300 File Offset: 0x00000500
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x00002338 File Offset: 0x00000538
		public event ACSHelper.OnDisconnectedEventHandler OnDisconnected;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002370 File Offset: 0x00000570
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x000023A8 File Offset: 0x000005A8
		public event ACSHelper.FileReceivedMapEventHandler FileReceivedMap;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600001D RID: 29 RVA: 0x000023E0 File Offset: 0x000005E0
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x00002418 File Offset: 0x00000618
		public event ACSHelper.MapCreatedEventHandler MapCreated;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600001F RID: 31 RVA: 0x00002450 File Offset: 0x00000650
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x00002488 File Offset: 0x00000688
		public event ACSHelper.MapDeletedEventHandler MapDeleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000021 RID: 33 RVA: 0x000024C0 File Offset: 0x000006C0
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x000024F8 File Offset: 0x000006F8
		public event ACSHelper.MapUpdatedEventHandler MapUpdated;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000023 RID: 35 RVA: 0x00002530 File Offset: 0x00000730
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x00002568 File Offset: 0x00000768
		public event ACSHelper.PlayerCreatedEventHandler PlayerCreated;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000025 RID: 37 RVA: 0x000025A0 File Offset: 0x000007A0
		// (remove) Token: 0x06000026 RID: 38 RVA: 0x000025D8 File Offset: 0x000007D8
		public event ACSHelper.PlayerDeletedEventHandler PlayerDeleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000027 RID: 39 RVA: 0x00002610 File Offset: 0x00000810
		// (remove) Token: 0x06000028 RID: 40 RVA: 0x00002648 File Offset: 0x00000848
		public event ACSHelper.RoomCreatedEventHandler RoomCreated;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000029 RID: 41 RVA: 0x00002680 File Offset: 0x00000880
		// (remove) Token: 0x0600002A RID: 42 RVA: 0x000026B8 File Offset: 0x000008B8
		public event ACSHelper.RoomDeletedEventHandler RoomDeleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600002B RID: 43 RVA: 0x000026F0 File Offset: 0x000008F0
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x00002728 File Offset: 0x00000928
		public event ACSHelper.RoomUpdatedEventHandler RoomUpdated;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600002D RID: 45 RVA: 0x00002760 File Offset: 0x00000960
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x00002798 File Offset: 0x00000998
		public event ACSHelper.RoomParticipantCreatedEventHandler RoomParticipantCreated;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600002F RID: 47 RVA: 0x000027D0 File Offset: 0x000009D0
		// (remove) Token: 0x06000030 RID: 48 RVA: 0x00002808 File Offset: 0x00000A08
		public event ACSHelper.RoomParticipantDeletedEventHandler RoomParticipantDeleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000031 RID: 49 RVA: 0x00002840 File Offset: 0x00000A40
		// (remove) Token: 0x06000032 RID: 50 RVA: 0x00002878 File Offset: 0x00000A78
		public event ACSHelper.RoomPermissionCreatedEventHandler RoomPermissionCreated;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000033 RID: 51 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x000028E8 File Offset: 0x00000AE8
		public event ACSHelper.RoomPermissionDeletedEventHandler RoomPermissionDeleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000035 RID: 53 RVA: 0x00002920 File Offset: 0x00000B20
		// (remove) Token: 0x06000036 RID: 54 RVA: 0x00002958 File Offset: 0x00000B58
		public event ACSHelper.SessionCreatedEventHandler SessionCreated;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000037 RID: 55 RVA: 0x00002990 File Offset: 0x00000B90
		// (remove) Token: 0x06000038 RID: 56 RVA: 0x000029C8 File Offset: 0x00000BC8
		public event ACSHelper.SessionDeletedEventHandler SessionDeleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000039 RID: 57 RVA: 0x00002A00 File Offset: 0x00000C00
		// (remove) Token: 0x0600003A RID: 58 RVA: 0x00002A38 File Offset: 0x00000C38
		public event ACSHelper.SessionUpdatedEventHandler SessionUpdated;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600003B RID: 59 RVA: 0x00002A70 File Offset: 0x00000C70
		// (remove) Token: 0x0600003C RID: 60 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public event ACSHelper.SessionValidatedEventHandler SessionValidated;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600003D RID: 61 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x00002B18 File Offset: 0x00000D18
		public event ACSHelper.StrokeCreatedEventHandler StrokeCreated;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600003F RID: 63 RVA: 0x00002B50 File Offset: 0x00000D50
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002B88 File Offset: 0x00000D88
		public event ACSHelper.StrokeDeletedEventHandler StrokeDeleted;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002BBD File Offset: 0x00000DBD
		public object Connecting
		{
			get
			{
				return this.SocketClient.IsConnecting;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public object Connected
		{
			get
			{
				object result;
				if (this.SocketClient == null)
				{
					result = false;
				}
				else
				{
					result = this.SocketClient.IsConnected;
				}
				return result;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C00 File Offset: 0x00000E00
		public ACSHelper()
		{
			this.SessionGUID = Guid.Empty;
			this.ServerAddress = string.Empty;
			this.ServerPort = -1;
			this.LastContact = DateTime.MinValue;
			this.LastValidated = DateTime.MinValue;
			this.Callsign = "none";
			this.Player = "none";
			this.SteamID = "none";
			this.WorldName = "none";
			this.WorldDisplayName = "none";
			this.SocketClient = new ClientAsync();
			this.InQueueMRE = new ManualResetEvent(false);
			this.InQueueFiles = new ConcurrentQueue<List<byte[]>>();
			this.InQueueString = new ConcurrentQueue<string[]>();
			this.DictionaryOfMaps = new Dictionary<string, MapDictionaryItem>();
			this.DictionaryOfRooms = new Dictionary<Guid, RoomDictionaryItem>();
			this.DictionaryOfSessions = new Dictionary<Guid, SessionDictionaryItem>();
			this.CreateInQueuesThread();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002CD4 File Offset: 0x00000ED4
		private void SubmitSyncRequests()
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "session<ath_sep>validate"
			});
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "room<ath_sep>request"
			});
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "session<ath_sep>request"
			});
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "map<ath_sep>request"
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D80 File Offset: 0x00000F80
		private void ClearDictionaries()
		{
			if (this.DictionaryOfRooms.Count != 0)
			{
				object dictionaryOfRooms = this.DictionaryOfRooms;
				lock (dictionaryOfRooms)
				{
					List<Guid> list = this.DictionaryOfRooms.Keys.ToList<Guid>();
					try
					{
						foreach (Guid guid in list)
						{
							this.UpdateRoomData(new string[]
							{
								"room",
								"delete",
								guid.ToString()
							});
						}
					}
					finally
					{
						List<Guid>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			if (this.DictionaryOfSessions.Count != 0)
			{
				object dictionaryOfSessions = this.DictionaryOfSessions;
				lock (dictionaryOfSessions)
				{
					List<Guid> list2 = this.DictionaryOfSessions.Keys.ToList<Guid>();
					try
					{
						foreach (Guid guid2 in list2)
						{
							this.UpdateSessionData(new string[]
							{
								"room",
								"delete",
								guid2.ToString()
							});
						}
					}
					finally
					{
						List<Guid>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EF0 File Offset: 0x000010F0
		public void SubmitFileGetMapRequest(string World)
		{
			ResponsePackage responsePackage = new ResponsePackage();
			responsePackage.Type = ResponseType.Text;
			responsePackage.Content = "fileget<ath_sep>map<ath_sep>" + World;
			this.SocketClient.OutQueue.Enqueue(responsePackage);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F2C File Offset: 0x0000112C
		public void SubmitFilePutMapRequest(string World, string FileName)
		{
			ResponsePackage responsePackage = new ResponsePackage();
			responsePackage.Type = ResponseType.File;
			responsePackage.FileType = "map";
			responsePackage.FileName = FileName;
			responsePackage.FileDisplayName = World;
			this.SocketClient.OutQueue.Enqueue(responsePackage);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F70 File Offset: 0x00001170
		public void SubmitCreateMapRequest(string Name, string World, string Size)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"map",
					"create",
					Name,
					World,
					Size
				})
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002FD0 File Offset: 0x000011D0
		public void SubmitCreateRoomRequest(Guid RoomGUID, string Action, string Security, string SideRequirement, string Name, string Server, string Mission, string WorldName, string WorldDisplayName)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"room",
					"create",
					RoomGUID.ToString(),
					this.SessionGUID.ToString(),
					Action,
					Security,
					SideRequirement,
					Name,
					Server,
					Mission,
					WorldName,
					WorldDisplayName
				})
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003070 File Offset: 0x00001270
		public void SubmitUpdateRoomRequest(Guid RoomGUID, Guid OwnerGUID, string Action, string Security, string SideRequirement, string Name, string Server, string Mission, string WorldName, string WorldDisplayName)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"room",
					"update",
					RoomGUID.ToString(),
					OwnerGUID.ToString(),
					Action,
					Security,
					SideRequirement,
					Name,
					Server,
					Mission,
					WorldName,
					WorldDisplayName
				})
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000310C File Offset: 0x0000130C
		public void SubmitJoinRoomRequest(Guid RoomGUID)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"roomparticipant",
					"create",
					RoomGUID.ToString()
				})
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003170 File Offset: 0x00001370
		public void SubmitLeaveRoomRequest(Guid RoomGUID)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"roomparticipant",
					"delete",
					RoomGUID.ToString(),
					this.SessionGUID.ToString()
				})
			});
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031E8 File Offset: 0x000013E8
		public void SubmitSessionUpdateRequest(string Callsign, string Player, string SteamID, string WorldName, string WorldDisplayName)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"session",
					"update",
					Callsign,
					Player,
					SteamID,
					WorldName,
					WorldDisplayName
				})
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003250 File Offset: 0x00001450
		public void SubmitStrokesRequest(Guid RoomGUID)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = string.Join("<ath_sep>", new string[]
				{
					"stroke",
					"request",
					RoomGUID.ToString()
				})
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000032B4 File Offset: 0x000014B4
		public void SubmitStrokeCreationCommand(string RoomGUID, Guid StrokeGUID, ref Stroke Stroke)
		{
			string content = string.Empty;
			try
			{
				StrokeCollection strokeCollection = new StrokeCollection();
				strokeCollection.Add(Stroke);
				MemoryStream memoryStream = new MemoryStream();
				strokeCollection.Save(memoryStream);
				memoryStream.Position = 0L;
				string text = BitConverter.ToString(memoryStream.ToArray()).Replace("-", "");
				if (!string.IsNullOrEmpty(text))
				{
					content = string.Concat(new string[]
					{
						"stroke<ath_sep>create<ath_sep>",
						RoomGUID,
						"<ath_sep>",
						StrokeGUID.ToString(),
						"<ath_sep>",
						text
					});
				}
				memoryStream.Close();
				memoryStream.Dispose();
				this.SocketClient.OutQueue.Enqueue(new ResponsePackage
				{
					Type = ResponseType.Text,
					Content = content
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003398 File Offset: 0x00001598
		public void SubmitStrokeDeletionCommand(string RoomGUID, string StrokeGUID)
		{
			this.SocketClient.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "stroke<ath_sep>delete<ath_sep>" + RoomGUID + "<ath_sep>" + StrokeGUID
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033D0 File Offset: 0x000015D0
		public void Connect(string Callsign, string WorldName, string WorldDisplayName, string Address, int Port, string Password)
		{
			this.Callsign = Callsign;
			this.WorldName = WorldName;
			this.WorldDisplayName = WorldDisplayName;
			this.ServerAddress = Address;
			this.ServerPort = Port;
			this.LastContact = DateTime.MinValue;
			this.LastValidated = DateTime.MinValue;
			MySettings.Default.ACSCallsign = Callsign;
			MySettings.Default.ACSServerAddress = Address;
			MySettings.Default.ACSServerPort = Port;
			MySettings.Default.Save();
			this.ClearDictionaries();
			try
			{
				this.SocketClient.OnErrorOccurred += this.HandleSocketError;
				this.SocketClient.OnMessageGenerated += this.HandleSocketMessage;
				this.SocketClient.OnConnected += this.HandleConnected;
				this.SocketClient.OnDisconnected += this.HandleDisconnected;
				this.SocketClient.OnReceivedData += this.HandleReceivedData;
				ResponsePackage responsePackage = new ResponsePackage();
				responsePackage.Type = ResponseType.Text;
				responsePackage.Content = string.Concat(new string[]
				{
					"session<ath_sep>create<ath_sep>",
					Callsign,
					"<ath_sep>",
					this.Player,
					"<ath_sep>",
					this.SteamID,
					"<ath_sep>",
					WorldName,
					"<ath_sep>",
					WorldDisplayName,
					"<ath_sep>",
					Password
				});
				responsePackage.Callback = new ResponseCallback(this.AuthCallback);
				this.SocketClient.StartConnecting(this.ServerAddress, this.ServerPort, responsePackage);
			}
			catch (Exception ex)
			{
				ACSHelper.OnErrorEventHandler onErrorEvent = this.OnErrorEvent;
				if (onErrorEvent != null)
				{
					onErrorEvent("Connect", ex.Message);
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000035A4 File Offset: 0x000017A4
		public void AuthCallback(byte[] Data)
		{
			try
			{
				if (Data == null)
				{
					ACSHelper.OnAuthenticatedEventHandler onAuthenticatedEvent = this.OnAuthenticatedEvent;
					if (onAuthenticatedEvent != null)
					{
						onAuthenticatedEvent(false, "No data received");
					}
				}
				else
				{
					List<byte[]> list = this.SocketClient.ByteArraySplit(Data);
					if (list == null)
					{
						ACSHelper.OnAuthenticatedEventHandler onAuthenticatedEvent2 = this.OnAuthenticatedEvent;
						if (onAuthenticatedEvent2 != null)
						{
							onAuthenticatedEvent2(false, "Failed to parse auth response");
						}
					}
					else
					{
						string[] array = this.SocketClient.ByteListToStringArray(list);
						if (array == null)
						{
							ACSHelper.OnAuthenticatedEventHandler onAuthenticatedEvent3 = this.OnAuthenticatedEvent;
							if (onAuthenticatedEvent3 != null)
							{
								onAuthenticatedEvent3(false, "Failed to convert auth response");
							}
						}
						else if (array.Count<string>() == 0)
						{
							ACSHelper.OnAuthenticatedEventHandler onAuthenticatedEvent4 = this.OnAuthenticatedEvent;
							if (onAuthenticatedEvent4 != null)
							{
								onAuthenticatedEvent4(false, "Auth response format is invalid");
							}
						}
						else if (array[0].Equals("success", StringComparison.InvariantCultureIgnoreCase))
						{
							ACSHelper.OnAuthenticatedEventHandler onAuthenticatedEvent5 = this.OnAuthenticatedEvent;
							if (onAuthenticatedEvent5 != null)
							{
								onAuthenticatedEvent5(true, "Success");
							}
						}
						else
						{
							ACSHelper.OnAuthenticatedEventHandler onAuthenticatedEvent6 = this.OnAuthenticatedEvent;
							if (onAuthenticatedEvent6 != null)
							{
								onAuthenticatedEvent6(false, array[1]);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000036AC File Offset: 0x000018AC
		public void Disconnect()
		{
			try
			{
				this.SocketClient.StopConnecting();
				this.SocketClient.OnErrorOccurred -= this.HandleSocketError;
				this.SocketClient.OnMessageGenerated -= this.HandleSocketMessage;
				this.SocketClient.OnConnected -= this.HandleConnected;
				this.SocketClient.OnDisconnected -= this.HandleDisconnected;
				this.SocketClient.OnReceivedData -= this.HandleReceivedData;
			}
			catch (Exception ex)
			{
				ACSHelper.OnErrorEventHandler onErrorEvent = this.OnErrorEvent;
				if (onErrorEvent != null)
				{
					onErrorEvent("Disconnect", ex.Message);
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003774 File Offset: 0x00001974
		public string[] ByteListToStringArray(ref List<byte[]> ByteList)
		{
			List<string> list = new List<string>();
			try
			{
				foreach (byte[] bytes in ByteList)
				{
					list.Add(Encoding.UTF8.GetString(bytes));
				}
			}
			finally
			{
				List<byte[]>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			return list.ToArray();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000037DC File Offset: 0x000019DC
		private void HandleSocketError(string Source, string Message)
		{
			ACSHelper.OnErrorEventHandler onErrorEvent = this.OnErrorEvent;
			if (onErrorEvent != null)
			{
				onErrorEvent("HandleSocketError", Source + ": " + Message);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000380C File Offset: 0x00001A0C
		private void HandleSocketMessage(string Source, string Message)
		{
			ACSHelper.OnMessageEventHandler onMessageEvent = this.OnMessageEvent;
			if (onMessageEvent != null)
			{
				onMessageEvent("HandleSocketMessage", Source + ": " + Message);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000383C File Offset: 0x00001A3C
		private void HandleConnected(bool Success, string Message)
		{
			if (Success)
			{
				this.SubmitSyncRequests();
			}
			ACSHelper.OnConnectedEventHandler onConnectedEvent = this.OnConnectedEvent;
			if (onConnectedEvent != null)
			{
				onConnectedEvent(Success, Message);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003864 File Offset: 0x00001A64
		private void HandleDisconnected()
		{
			ACSHelper.OnDisconnectedEventHandler onDisconnectedEvent = this.OnDisconnectedEvent;
			if (onDisconnectedEvent != null)
			{
				onDisconnectedEvent();
			}
			if (this.DictionaryOfRooms.Count != 0)
			{
				try
				{
					foreach (RoomDictionaryItem roomDictionaryItem in this.DictionaryOfRooms.Select((ACSHelper._Closure$__.$I163-0 == null) ? (ACSHelper._Closure$__.$I163-0 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : ACSHelper._Closure$__.$I163-0).ToList<RoomDictionaryItem>())
					{
						List<Guid> list = roomDictionaryItem.ParticipantDictionary.Select((ACSHelper._Closure$__.$I163-1 == null) ? (ACSHelper._Closure$__.$I163-1 = ((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Value.Participant.SessionGUID)) : ACSHelper._Closure$__.$I163-1).ToList<Guid>();
						if (list != null && list.Count != 0)
						{
							try
							{
								foreach (Guid guid in list)
								{
									this.UpdateRoomParticipantData(new string[]
									{
										"roomparticipant",
										"delete",
										roomDictionaryItem.Room.RoomGUID.ToString(),
										guid.ToString()
									});
								}
							}
							finally
							{
								List<Guid>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
						}
						this.UpdateRoomData(new string[]
						{
							"room",
							"delete",
							roomDictionaryItem.Room.RoomGUID.ToString()
						});
					}
				}
				finally
				{
					List<RoomDictionaryItem>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			if (this.DictionaryOfSessions.Count != 0)
			{
				try
				{
					foreach (SessionDictionaryItem sessionDictionaryItem in this.DictionaryOfSessions.Select((ACSHelper._Closure$__.$I163-2 == null) ? (ACSHelper._Closure$__.$I163-2 = ((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Value)) : ACSHelper._Closure$__.$I163-2).ToList<SessionDictionaryItem>())
					{
						this.UpdateSessionData(new string[]
						{
							"session",
							"delete",
							sessionDictionaryItem.Session.SessionGUID.ToString()
						});
					}
				}
				finally
				{
					List<SessionDictionaryItem>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
			}
			this.SessionGUID = Guid.Empty;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003AE4 File Offset: 0x00001CE4
		private void HandleReceivedData(string Command, List<byte[]> Data)
		{
			try
			{
				this.LastContact = DateTime.Now;
				string left = Command.ToLower();
				if (Operators.CompareString(left, "file", false) != 0)
				{
					if (Operators.CompareString(left, "error", false) != 0)
					{
						this.InQueueString.Enqueue(this.ByteListToStringArray(ref Data));
					}
				}
				else
				{
					this.InQueueFiles.Enqueue(Data);
				}
				this.InQueueMRE.Set();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003B70 File Offset: 0x00001D70
		private void CreateInQueuesThread()
		{
			if (this.InQueuesThread == null || (this.InQueuesThread != null && this.InQueuesThread.IsAlive))
			{
				this.InQueuesThread = new Thread(new ThreadStart(this.WatchInQueues));
				this.InQueuesThread.IsBackground = true;
				this.InQueuesThread.Start();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private void WatchInQueues()
		{
			List<byte[]> list = null;
			string[] array = null;
			for (;;)
			{
				this.InQueueMRE.WaitOne();
				try
				{
					if (this.InQueueFiles.TryDequeue(out list))
					{
						if (list.Count > 1)
						{
							this.UpdateFileData(list);
						}
					}
					else if (this.InQueueString.TryDequeue(out array))
					{
						if (array.Length > 1)
						{
							string text = array[0].ToLower();
							uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
							if (num <= 1585286271U)
							{
								if (num != 748274432U)
								{
									if (num != 939340410U)
									{
										if (num == 1585286271U)
										{
											if (Operators.CompareString(text, "roomparticipant", false) == 0)
											{
												this.UpdateRoomParticipantData(array);
											}
										}
									}
									else if (Operators.CompareString(text, "room", false) == 0)
									{
										this.UpdateRoomData(array);
									}
								}
								else if (Operators.CompareString(text, "player", false) == 0)
								{
									this.UpdatePlayerData(array);
								}
							}
							else if (num <= 3277802743U)
							{
								if (num != 3072721717U)
								{
									if (num == 3277802743U)
									{
										if (Operators.CompareString(text, "session", false) == 0)
										{
											this.UpdateSessionData(array);
										}
									}
								}
								else if (Operators.CompareString(text, "stroke", false) == 0)
								{
									this.UpdateStrokeData(array);
								}
							}
							else if (num != 3383765181U)
							{
								if (num == 3751997361U)
								{
									if (Operators.CompareString(text, "map", false) == 0)
									{
										this.UpdateMapData(array);
									}
								}
							}
							else if (Operators.CompareString(text, "roompermission", false) == 0)
							{
								this.UpdateRoomPermissionData(array);
							}
						}
					}
					else
					{
						string empty = string.Empty;
					}
				}
				catch (Exception ex)
				{
				}
				list = null;
				array = null;
				if (this.InQueueFiles.IsEmpty & this.InQueueString.IsEmpty)
				{
					this.InQueueMRE.Reset();
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003DAC File Offset: 0x00001FAC
		public void UpdateFileData(List<byte[]> Data)
		{
			string left = Encoding.UTF8.GetString(Data[1]).ToLower();
			if (Operators.CompareString(left, "map", false) == 0)
			{
				ACSHelper.FileReceivedMapEventHandler fileReceivedMapEvent = this.FileReceivedMapEvent;
				if (fileReceivedMapEvent != null)
				{
					fileReceivedMapEvent(Encoding.UTF8.GetString(Data[2]), Encoding.UTF8.GetString(Data[3]), Data[4]);
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003E18 File Offset: 0x00002018
		public void UpdateMapData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) != 0)
					{
						if (Operators.CompareString(left, "update", false) == 0)
						{
							MapDictionaryItem value = this.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, Data[3], false) == 0).Value;
							if (value != null)
							{
								value.Map.Name = Data[2];
								value.Map.World = Data[3];
								value.Map.WorldSize = Data[4];
								value.Map.FileSize = Data[5];
								ACSHelper.MapUpdatedEventHandler mapUpdatedEvent = this.MapUpdatedEvent;
								if (mapUpdatedEvent != null)
								{
									mapUpdatedEvent(value.Map.World);
								}
							}
						}
					}
					else if (this.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, Data[2], false) == 0).Value != null)
					{
						ACSHelper.MapDeletedEventHandler mapDeletedEvent = this.MapDeletedEvent;
						if (mapDeletedEvent != null)
						{
							mapDeletedEvent(Data[2]);
						}
						this.DictionaryOfMaps.Remove(Data[2]);
					}
				}
				else if (!this.DictionaryOfMaps.ContainsKey(Data[3]))
				{
					MapDictionaryItem mapDictionaryItem = new MapDictionaryItem();
					mapDictionaryItem.Map = new Map
					{
						Name = Data[2],
						World = Data[3],
						WorldSize = Data[4],
						FileSize = Data[5]
					};
					this.DictionaryOfMaps.Add(mapDictionaryItem.Map.World, mapDictionaryItem);
					ACSHelper.MapCreatedEventHandler mapCreatedEvent = this.MapCreatedEvent;
					if (mapCreatedEvent != null)
					{
						mapCreatedEvent(mapDictionaryItem.Map.World);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004028 File Offset: 0x00002228
		public void UpdatePlayerData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) == 0)
					{
						Guid RoomGUID = Guid.Empty;
						string text = Data[3];
						ACSHelper.PlayerDeletedEventHandler playerDeletedEvent = this.PlayerDeletedEvent;
						if (playerDeletedEvent != null)
						{
							playerDeletedEvent(RoomGUID, text);
						}
						if (Guid.TryParse(Data[2], out RoomGUID))
						{
							RoomDictionaryItem value = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
							if (value != null)
							{
								value.PlayerDictionary.Remove(text);
							}
						}
					}
				}
				else
				{
					Guid RoomGUID = Guid.Empty;
					string text2 = Data[3];
					string callsign = Data[4];
					string side = Data[5];
					if (Guid.TryParse(Data[2], out RoomGUID))
					{
						RoomDictionaryItem value2 = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
						if (value2 != null && !value2.PlayerDictionary.ContainsKey(text2))
						{
							value2.PlayerDictionary.Add(text2, new PlayerDictionaryItem
							{
								Player = new Player
								{
									RoomGUID = RoomGUID,
									SteamID = text2,
									Callsign = callsign,
									Side = side
								}
							});
						}
					}
					ACSHelper.PlayerCreatedEventHandler playerCreatedEvent = this.PlayerCreatedEvent;
					if (playerCreatedEvent != null)
					{
						playerCreatedEvent(RoomGUID, text2);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000041C8 File Offset: 0x000023C8
		public void UpdateRoomData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) != 0)
					{
						if (Operators.CompareString(left, "update", false) == 0)
						{
							Guid RoomGUID = Guid.Empty;
							Guid empty = Guid.Empty;
							string action = Data[4];
							string security = Data[5];
							string side = Data[6];
							string name = Data[7];
							string server = Data[8];
							string mission = Data[9];
							string worldName = Data[10];
							string worldDisplayName = Data[11];
							if (Guid.TryParse(Data[2], out RoomGUID) & Guid.TryParse(Data[3], out empty))
							{
								RoomDictionaryItem value = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
								if (value != null)
								{
									value.Room.Action = action;
									value.Room.Security = security;
									value.Room.OwnerSessionGUID = empty;
									value.Room.Side = side;
									value.Room.Name = name;
									value.Room.Server = server;
									value.Room.Mission = mission;
									value.Room.WorldName = worldName;
									value.Room.WorldDisplayName = worldDisplayName;
									ACSHelper.RoomUpdatedEventHandler roomUpdatedEvent = this.RoomUpdatedEvent;
									if (roomUpdatedEvent != null)
									{
										roomUpdatedEvent(RoomGUID);
									}
								}
							}
						}
					}
					else
					{
						Guid RoomGUID = Guid.Empty;
						if (Guid.TryParse(Data[2], out RoomGUID) && this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value != null)
						{
							ACSHelper.RoomDeletedEventHandler roomDeletedEvent = this.RoomDeletedEvent;
							if (roomDeletedEvent != null)
							{
								roomDeletedEvent(RoomGUID);
							}
							this.DictionaryOfRooms.Remove(RoomGUID);
						}
					}
				}
				else
				{
					Guid empty2 = Guid.Empty;
					Guid empty3 = Guid.Empty;
					string action2 = Data[4];
					string security2 = Data[5];
					string side2 = Data[6];
					string name2 = Data[7];
					string server2 = Data[8];
					string mission2 = Data[9];
					string worldName2 = Data[10];
					string worldDisplayName2 = Data[11];
					if ((Guid.TryParse(Data[2], out empty2) & Guid.TryParse(Data[3], out empty3)) && !this.DictionaryOfRooms.ContainsKey(empty2))
					{
						RoomDictionaryItem roomDictionaryItem = new RoomDictionaryItem();
						roomDictionaryItem.Room = new Room
						{
							RoomGUID = empty2,
							Action = action2,
							Security = security2,
							OwnerSessionGUID = empty3,
							Side = side2,
							Name = name2,
							Server = server2,
							Mission = mission2,
							WorldName = worldName2,
							WorldDisplayName = worldDisplayName2
						};
						this.DictionaryOfRooms.Add(empty2, roomDictionaryItem);
						if (!empty3.Equals(this.SessionGUID))
						{
							this.SocketClient.OutQueue.Enqueue(new ResponsePackage
							{
								Type = ResponseType.Text,
								Content = "roomparticipant<ath_sep>request<ath_sep>" + empty2.ToString()
							});
						}
						ACSHelper.RoomCreatedEventHandler roomCreatedEvent = this.RoomCreatedEvent;
						if (roomCreatedEvent != null)
						{
							roomCreatedEvent(empty2);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004514 File Offset: 0x00002714
		public void UpdateRoomParticipantData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) == 0)
					{
						Guid RoomGUID = Guid.Empty;
						Guid SessionGUID = Guid.Empty;
						if (Guid.TryParse(Data[2], out RoomGUID) & Guid.TryParse(Data[3], out SessionGUID))
						{
							RoomDictionaryItem value = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
							if (value != null && value.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == SessionGUID).Value != null)
							{
								ACSHelper.RoomParticipantDeletedEventHandler roomParticipantDeletedEvent = this.RoomParticipantDeletedEvent;
								if (roomParticipantDeletedEvent != null)
								{
									roomParticipantDeletedEvent(RoomGUID, SessionGUID);
								}
								value.ParticipantDictionary.Remove(SessionGUID);
							}
						}
					}
				}
				else
				{
					Guid RoomGUID = Guid.Empty;
					Guid empty = Guid.Empty;
					if (Guid.TryParse(Data[2], out RoomGUID) & Guid.TryParse(Data[3], out empty))
					{
						RoomDictionaryItem value2 = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
						if (value2 != null && !value2.ParticipantDictionary.ContainsKey(empty))
						{
							RoomParticipantDictionaryItem roomParticipantDictionaryItem = new RoomParticipantDictionaryItem();
							roomParticipantDictionaryItem.Participant = new RoomParticipant
							{
								RoomGUID = RoomGUID,
								SessionGUID = empty
							};
							value2.ParticipantDictionary.Add(empty, roomParticipantDictionaryItem);
							if (empty.Equals(this.SessionGUID))
							{
								this.SocketClient.OutQueue.Enqueue(new ResponsePackage
								{
									Type = ResponseType.Text,
									Content = "roompermission<ath_sep>request<ath_sep>" + RoomGUID.ToString()
								});
								this.SocketClient.OutQueue.Enqueue(new ResponsePackage
								{
									Type = ResponseType.Text,
									Content = "player<ath_sep>request<ath_sep>" + RoomGUID.ToString()
								});
								this.SocketClient.OutQueue.Enqueue(new ResponsePackage
								{
									Type = ResponseType.Text,
									Content = "stroke<ath_sep>request<ath_sep>" + RoomGUID.ToString()
								});
							}
							ACSHelper.RoomParticipantCreatedEventHandler roomParticipantCreatedEvent = this.RoomParticipantCreatedEvent;
							if (roomParticipantCreatedEvent != null)
							{
								roomParticipantCreatedEvent(RoomGUID, empty);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000047C8 File Offset: 0x000029C8
		public void UpdateRoomPermissionData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) == 0)
					{
						Guid RoomGUID = Guid.Empty;
						Guid empty = Guid.Empty;
						if (Guid.TryParse(Data[2], out RoomGUID) & Guid.TryParse(Data[3], out empty))
						{
							RoomDictionaryItem value = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
							if (value != null)
							{
								ACSHelper.RoomPermissionDeletedEventHandler roomPermissionDeletedEvent = this.RoomPermissionDeletedEvent;
								if (roomPermissionDeletedEvent != null)
								{
									roomPermissionDeletedEvent(RoomGUID, empty);
								}
								value.PermissionDictionary.Remove(empty);
							}
						}
					}
				}
				else
				{
					Guid RoomGUID = Guid.Empty;
					Guid empty2 = Guid.Empty;
					if (Guid.TryParse(Data[2], out RoomGUID) & Guid.TryParse(Data[3], out empty2))
					{
						RoomDictionaryItem value2 = this.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
						if (value2 != null && !value2.PermissionDictionary.ContainsKey(empty2))
						{
							value2.PermissionDictionary.Add(empty2, new RoomPermissionDictionaryItem
							{
								Permission = new RoomPermission
								{
									RoomGUID = RoomGUID,
									SessionGUID = empty2
								}
							});
							ACSHelper.RoomPermissionCreatedEventHandler roomPermissionCreatedEvent = this.RoomPermissionCreatedEvent;
							if (roomPermissionCreatedEvent != null)
							{
								roomPermissionCreatedEvent(RoomGUID, empty2);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004970 File Offset: 0x00002B70
		public void UpdateSessionData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) != 0)
					{
						if (Operators.CompareString(left, "update", false) != 0)
						{
							if (Operators.CompareString(left, "validate", false) == 0)
							{
								this.Callsign = Data[3];
								this.Player = Data[4];
								this.SteamID = Data[5];
								this.WorldName = Data[6];
								this.WorldDisplayName = Data[7];
								if (Guid.TryParse(Data[2], out this.SessionGUID))
								{
									this.LastValidated = DateTime.Now;
									ACSHelper.SessionValidatedEventHandler sessionValidatedEvent = this.SessionValidatedEvent;
									if (sessionValidatedEvent != null)
									{
										sessionValidatedEvent(this.SessionGUID);
									}
								}
							}
						}
						else
						{
							Guid SessionGUID = Guid.Empty;
							string callsign = Data[3];
							string player = Data[4];
							string steamID = Data[5];
							string worldName = Data[6];
							string worldDisplayName = Data[7];
							if (Guid.TryParse(Data[2], out SessionGUID))
							{
								SessionDictionaryItem value = this.DictionaryOfSessions.SingleOrDefault((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Key == SessionGUID).Value;
								if (value != null)
								{
									value.Session.Callsign = callsign;
									value.Session.Player = player;
									value.Session.SteamID = steamID;
									value.Session.WorldName = worldName;
									value.Session.WorldDisplayName = worldDisplayName;
									ACSHelper.SessionUpdatedEventHandler sessionUpdatedEvent = this.SessionUpdatedEvent;
									if (sessionUpdatedEvent != null)
									{
										sessionUpdatedEvent(SessionGUID);
									}
								}
							}
						}
					}
					else
					{
						Guid SessionGUID = Guid.Empty;
						if (Guid.TryParse(Data[2], out SessionGUID) && this.DictionaryOfSessions.SingleOrDefault((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Key == SessionGUID).Value != null)
						{
							ACSHelper.SessionDeletedEventHandler sessionDeletedEvent = this.SessionDeletedEvent;
							if (sessionDeletedEvent != null)
							{
								sessionDeletedEvent(SessionGUID);
							}
							this.DictionaryOfSessions.Remove(SessionGUID);
						}
					}
				}
				else
				{
					Guid empty = Guid.Empty;
					string callsign2 = Data[3];
					string player2 = Data[4];
					string steamID2 = Data[5];
					string worldName2 = Data[6];
					string worldDisplayName2 = Data[7];
					if (Guid.TryParse(Data[2], out empty) && !this.DictionaryOfSessions.ContainsKey(empty))
					{
						SessionDictionaryItem sessionDictionaryItem = new SessionDictionaryItem();
						sessionDictionaryItem.Session = new Session
						{
							SessionGUID = empty,
							Callsign = callsign2,
							Player = player2,
							SteamID = steamID2,
							WorldName = worldName2,
							WorldDisplayName = worldDisplayName2
						};
						this.DictionaryOfSessions.Add(empty, sessionDictionaryItem);
						ACSHelper.SessionCreatedEventHandler sessionCreatedEvent = this.SessionCreatedEvent;
						if (sessionCreatedEvent != null)
						{
							sessionCreatedEvent(empty);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004C48 File Offset: 0x00002E48
		public void UpdateStrokeData(string[] Data)
		{
			try
			{
				string left = Data[1];
				if (Operators.CompareString(left, "create", false) != 0)
				{
					if (Operators.CompareString(left, "delete", false) == 0)
					{
						Guid empty = Guid.Empty;
						Guid empty2 = Guid.Empty;
						Guid empty3 = Guid.Empty;
						if (((Guid.TryParse(Data[2], out empty) & Guid.TryParse(Data[3], out empty2)) && !empty2.Equals(Guid.Empty)) & Guid.TryParse(Data[4], out empty3))
						{
							ACSHelper.StrokeDeletedEventHandler strokeDeletedEvent = this.StrokeDeletedEvent;
							if (strokeDeletedEvent != null)
							{
								strokeDeletedEvent(empty, empty2, empty3);
							}
						}
					}
				}
				else
				{
					Guid empty4 = Guid.Empty;
					Guid empty5 = Guid.Empty;
					Guid empty6 = Guid.Empty;
					string data = Data[5];
					if (Guid.TryParse(Data[2], out empty4) & Guid.TryParse(Data[3], out empty5) & Guid.TryParse(Data[4], out empty6))
					{
						ACSHelper.StrokeCreatedEventHandler strokeCreatedEvent = this.StrokeCreatedEvent;
						if (strokeCreatedEvent != null)
						{
							strokeCreatedEvent(empty4, empty5, Common.DeserializeStrokeCollection(data));
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0400001F RID: 31
		public Guid SessionGUID;

		// Token: 0x04000020 RID: 32
		public string ServerAddress;

		// Token: 0x04000021 RID: 33
		public int ServerPort;

		// Token: 0x04000022 RID: 34
		public DateTime LastContact;

		// Token: 0x04000023 RID: 35
		public DateTime LastValidated;

		// Token: 0x04000024 RID: 36
		public string Callsign;

		// Token: 0x04000025 RID: 37
		public string Player;

		// Token: 0x04000026 RID: 38
		public string SteamID;

		// Token: 0x04000027 RID: 39
		public string WorldName;

		// Token: 0x04000028 RID: 40
		public string WorldDisplayName;

		// Token: 0x04000029 RID: 41
		private ClientAsync SocketClient;

		// Token: 0x0400002A RID: 42
		private Thread InQueuesThread;

		// Token: 0x0400002B RID: 43
		private ManualResetEvent InQueueMRE;

		// Token: 0x0400002C RID: 44
		private ConcurrentQueue<List<byte[]>> InQueueFiles;

		// Token: 0x0400002D RID: 45
		private ConcurrentQueue<string[]> InQueueString;

		// Token: 0x0400002E RID: 46
		public Dictionary<string, MapDictionaryItem> DictionaryOfMaps;

		// Token: 0x0400002F RID: 47
		public Dictionary<Guid, RoomDictionaryItem> DictionaryOfRooms;

		// Token: 0x04000030 RID: 48
		public Dictionary<Guid, SessionDictionaryItem> DictionaryOfSessions;

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x060006C2 RID: 1730
		public delegate void OnErrorEventHandler(string Source, string Message);

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x060006C6 RID: 1734
		public delegate void OnMessageEventHandler(string Source, string Message);

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x060006CA RID: 1738
		public delegate void OnAuthenticatedEventHandler(bool Success, string Message);

		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x060006CE RID: 1742
		public delegate void OnConnectedEventHandler(bool Success, string Message);

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x060006D2 RID: 1746
		public delegate void OnDisconnectedEventHandler();

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x060006D6 RID: 1750
		public delegate void FileReceivedMapEventHandler(string DisplayName, string FileName, byte[] Content);

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x060006DA RID: 1754
		public delegate void MapCreatedEventHandler(string World);

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060006DE RID: 1758
		public delegate void MapDeletedEventHandler(string World);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x060006E2 RID: 1762
		public delegate void MapUpdatedEventHandler(string World);

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x060006E6 RID: 1766
		public delegate void PlayerCreatedEventHandler(Guid RoomGUID, string SteamID);

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x060006EA RID: 1770
		public delegate void PlayerDeletedEventHandler(Guid RoomGUID, string SteamID);

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x060006EE RID: 1774
		public delegate void RoomCreatedEventHandler(Guid RoomGUID);

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x060006F2 RID: 1778
		public delegate void RoomDeletedEventHandler(Guid RoomGUID);

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x060006F6 RID: 1782
		public delegate void RoomUpdatedEventHandler(Guid RoomGUID);

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x060006FA RID: 1786
		public delegate void RoomParticipantCreatedEventHandler(Guid RoomGUID, Guid SessionGUID);

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x060006FE RID: 1790
		public delegate void RoomParticipantDeletedEventHandler(Guid RoomGUID, Guid SessionGUID);

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x06000702 RID: 1794
		public delegate void RoomPermissionCreatedEventHandler(Guid RoomGUID, Guid SessionGUID);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x06000706 RID: 1798
		public delegate void RoomPermissionDeletedEventHandler(Guid RoomGUID, Guid SessionGUID);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x0600070A RID: 1802
		public delegate void SessionCreatedEventHandler(Guid SessionGUID);

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x0600070E RID: 1806
		public delegate void SessionDeletedEventHandler(Guid SessionGUID);

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x06000712 RID: 1810
		public delegate void SessionUpdatedEventHandler(Guid SessionGUID);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x06000716 RID: 1814
		public delegate void SessionValidatedEventHandler(Guid SessionGUID);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x0600071A RID: 1818
		public delegate void StrokeCreatedEventHandler(Guid RoomGUID, Guid StrokeGUID, StrokeCollection StrokeCollection);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x0600071E RID: 1822
		public delegate void StrokeDeletedEventHandler(Guid RoomGUID, Guid StrokeGUID, Guid DeleterGUID);
	}
}
