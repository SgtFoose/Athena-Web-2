using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Athena.JSON;
using Athena.Objects.v2;
using Athena.Tools;
using Athena.Tools.Sockets;
using Athena.Utilities;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace Athena.App.W7
{
	// Token: 0x02000038 RID: 56
	[DesignerGenerated]
	public partial class MainWindow : Window
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0001358C File Offset: 0x0001178C
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00013594 File Offset: 0x00011794
		private virtual ACSHelper MyACSHelper
		{
			[CompilerGenerated]
			get
			{
				return this._MyACSHelper;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ACSHelper.OnAuthenticatedEventHandler obj = new ACSHelper.OnAuthenticatedEventHandler(this.HandleACSOnAuthenticated);
				ACSHelper.OnConnectedEventHandler obj2 = new ACSHelper.OnConnectedEventHandler(this.HandleACSOnConnected);
				ACSHelper.OnDisconnectedEventHandler obj3 = new ACSHelper.OnDisconnectedEventHandler(this.HandleACSOnDisconnected);
				ACSHelper.SessionValidatedEventHandler obj4 = delegate(Guid a0)
				{
					this.HandleACSOnValidated();
				};
				ACSHelper.FileReceivedMapEventHandler obj5 = new ACSHelper.FileReceivedMapEventHandler(this.HandleFileGetMap);
				ACSHelper.MapCreatedEventHandler obj6 = new ACSHelper.MapCreatedEventHandler(this.HandleMapCreated);
				ACSHelper.MapDeletedEventHandler obj7 = new ACSHelper.MapDeletedEventHandler(this.HandleMapDeleted);
				ACSHelper.MapUpdatedEventHandler obj8 = new ACSHelper.MapUpdatedEventHandler(this.HandleMapUpdated);
				ACSHelper.RoomCreatedEventHandler obj9 = new ACSHelper.RoomCreatedEventHandler(this.HandleRoomCreated);
				ACSHelper.RoomDeletedEventHandler obj10 = new ACSHelper.RoomDeletedEventHandler(this.HandleRoomDeleted);
				ACSHelper.RoomUpdatedEventHandler obj11 = new ACSHelper.RoomUpdatedEventHandler(this.HandleRoomUpdated);
				ACSHelper.PlayerCreatedEventHandler obj12 = new ACSHelper.PlayerCreatedEventHandler(this.HandlePlayerCreated);
				ACSHelper.PlayerDeletedEventHandler obj13 = new ACSHelper.PlayerDeletedEventHandler(this.HandlePlayerDeleted);
				ACSHelper.RoomParticipantCreatedEventHandler obj14 = new ACSHelper.RoomParticipantCreatedEventHandler(this.HandleRoomParticipantCreated);
				ACSHelper.RoomParticipantDeletedEventHandler obj15 = new ACSHelper.RoomParticipantDeletedEventHandler(this.HandleRoomParticipantDeleted);
				ACSHelper.RoomPermissionCreatedEventHandler obj16 = new ACSHelper.RoomPermissionCreatedEventHandler(this.HandleRoomPermissionCreated);
				ACSHelper.RoomPermissionDeletedEventHandler obj17 = new ACSHelper.RoomPermissionDeletedEventHandler(this.HandleRoomPermissionDeleted);
				ACSHelper.SessionCreatedEventHandler obj18 = new ACSHelper.SessionCreatedEventHandler(this.HandleSessionCreated);
				ACSHelper.SessionDeletedEventHandler obj19 = new ACSHelper.SessionDeletedEventHandler(this.HandleSessionDeleted);
				ACSHelper.SessionUpdatedEventHandler obj20 = new ACSHelper.SessionUpdatedEventHandler(this.HandleSessionUpdated);
				ACSHelper.SessionValidatedEventHandler obj21 = new ACSHelper.SessionValidatedEventHandler(this.HandleSssionValidated);
				ACSHelper.StrokeCreatedEventHandler obj22 = new ACSHelper.StrokeCreatedEventHandler(this.HandleStrokeCreated);
				ACSHelper.StrokeDeletedEventHandler obj23 = new ACSHelper.StrokeDeletedEventHandler(this.HandleStrokeDeleted);
				ACSHelper myACSHelper = this._MyACSHelper;
				if (myACSHelper != null)
				{
					myACSHelper.OnAuthenticated -= obj;
					myACSHelper.OnConnected -= obj2;
					myACSHelper.OnDisconnected -= obj3;
					myACSHelper.SessionValidated -= obj4;
					myACSHelper.FileReceivedMap -= obj5;
					myACSHelper.MapCreated -= obj6;
					myACSHelper.MapDeleted -= obj7;
					myACSHelper.MapUpdated -= obj8;
					myACSHelper.RoomCreated -= obj9;
					myACSHelper.RoomDeleted -= obj10;
					myACSHelper.RoomUpdated -= obj11;
					myACSHelper.PlayerCreated -= obj12;
					myACSHelper.PlayerDeleted -= obj13;
					myACSHelper.RoomParticipantCreated -= obj14;
					myACSHelper.RoomParticipantDeleted -= obj15;
					myACSHelper.RoomPermissionCreated -= obj16;
					myACSHelper.RoomPermissionDeleted -= obj17;
					myACSHelper.SessionCreated -= obj18;
					myACSHelper.SessionDeleted -= obj19;
					myACSHelper.SessionUpdated -= obj20;
					myACSHelper.SessionValidated -= obj21;
					myACSHelper.StrokeCreated -= obj22;
					myACSHelper.StrokeDeleted -= obj23;
				}
				this._MyACSHelper = value;
				myACSHelper = this._MyACSHelper;
				if (myACSHelper != null)
				{
					myACSHelper.OnAuthenticated += obj;
					myACSHelper.OnConnected += obj2;
					myACSHelper.OnDisconnected += obj3;
					myACSHelper.SessionValidated += obj4;
					myACSHelper.FileReceivedMap += obj5;
					myACSHelper.MapCreated += obj6;
					myACSHelper.MapDeleted += obj7;
					myACSHelper.MapUpdated += obj8;
					myACSHelper.RoomCreated += obj9;
					myACSHelper.RoomDeleted += obj10;
					myACSHelper.RoomUpdated += obj11;
					myACSHelper.PlayerCreated += obj12;
					myACSHelper.PlayerDeleted += obj13;
					myACSHelper.RoomParticipantCreated += obj14;
					myACSHelper.RoomParticipantDeleted += obj15;
					myACSHelper.RoomPermissionCreated += obj16;
					myACSHelper.RoomPermissionDeleted += obj17;
					myACSHelper.SessionCreated += obj18;
					myACSHelper.SessionDeleted += obj19;
					myACSHelper.SessionUpdated += obj20;
					myACSHelper.SessionValidated += obj21;
					myACSHelper.StrokeCreated += obj22;
					myACSHelper.StrokeDeleted += obj23;
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0001389A File Offset: 0x00011A9A
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000138A2 File Offset: 0x00011AA2
		private virtual MapHelper myMapHelper { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000138AB File Offset: 0x00011AAB
		// (set) Token: 0x06000368 RID: 872 RVA: 0x000138B3 File Offset: 0x00011AB3
		private virtual LowLevelKeyboardListener hkHost { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000138BC File Offset: 0x00011ABC
		// (set) Token: 0x0600036A RID: 874 RVA: 0x000138C4 File Offset: 0x00011AC4
		private virtual DispatcherTimer ZoomTimer
		{
			[CompilerGenerated]
			get
			{
				return this._ZoomTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ZoomTimer_Tick);
				DispatcherTimer zoomTimer = this._ZoomTimer;
				if (zoomTimer != null)
				{
					zoomTimer.Tick -= value2;
				}
				this._ZoomTimer = value;
				zoomTimer = this._ZoomTimer;
				if (zoomTimer != null)
				{
					zoomTimer.Tick += value2;
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00013907 File Offset: 0x00011B07
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00013910 File Offset: 0x00011B10
		private virtual System.Timers.Timer ArchiveTimer
		{
			[CompilerGenerated]
			get
			{
				return this._ArchiveTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ElapsedEventHandler value2 = new ElapsedEventHandler(this.ArchiveTimer_Elapsed);
				System.Timers.Timer archiveTimer = this._ArchiveTimer;
				if (archiveTimer != null)
				{
					archiveTimer.Elapsed -= value2;
				}
				this._ArchiveTimer = value;
				archiveTimer = this._ArchiveTimer;
				if (archiveTimer != null)
				{
					archiveTimer.Elapsed += value2;
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00013953 File Offset: 0x00011B53
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001395C File Offset: 0x00011B5C
		private virtual System.Timers.Timer MissionTimer
		{
			[CompilerGenerated]
			get
			{
				return this._MissionTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ElapsedEventHandler value2 = new ElapsedEventHandler(this.MissionTimer_Elapsed);
				System.Timers.Timer missionTimer = this._MissionTimer;
				if (missionTimer != null)
				{
					missionTimer.Elapsed -= value2;
				}
				this._MissionTimer = value;
				missionTimer = this._MissionTimer;
				if (missionTimer != null)
				{
					missionTimer.Elapsed += value2;
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001399F File Offset: 0x00011B9F
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000139A8 File Offset: 0x00011BA8
		private virtual System.Timers.Timer PlaybackTimer
		{
			[CompilerGenerated]
			get
			{
				return this._PlaybackTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ElapsedEventHandler value2 = new ElapsedEventHandler(this.PlaybackTimer_Elapsed);
				System.Timers.Timer playbackTimer = this._PlaybackTimer;
				if (playbackTimer != null)
				{
					playbackTimer.Elapsed -= value2;
				}
				this._PlaybackTimer = value;
				playbackTimer = this._PlaybackTimer;
				if (playbackTimer != null)
				{
					playbackTimer.Elapsed += value2;
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000139EB File Offset: 0x00011BEB
		// (set) Token: 0x06000372 RID: 882 RVA: 0x000139F3 File Offset: 0x00011BF3
		private virtual frmStatus FormStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000139FC File Offset: 0x00011BFC
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00013A04 File Offset: 0x00011C04
		private virtual HotKeys frmHotKeys { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00013A0D File Offset: 0x00011C0D
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00013A15 File Offset: 0x00011C15
		private virtual ClientAsync SocketClientRelay { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00013A1E File Offset: 0x00011C1E
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00013A28 File Offset: 0x00011C28
		private virtual ActiveTool Tool
		{
			[CompilerGenerated]
			get
			{
				return this._Tool;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ActiveTool.ToolChangedEventHandler obj = new ActiveTool.ToolChangedEventHandler(this.Tool_ToolChanged);
				ActiveTool tool = this._Tool;
				if (tool != null)
				{
					tool.ToolChanged -= obj;
				}
				this._Tool = value;
				tool = this._Tool;
				if (tool != null)
				{
					tool.ToolChanged += obj;
				}
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000379 RID: 889 RVA: 0x00013A6C File Offset: 0x00011C6C
		// (remove) Token: 0x0600037A RID: 890 RVA: 0x00013AA4 File Offset: 0x00011CA4
		public event MainWindow.User_Initiated_OnlineEventHandler User_Initiated_Online;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600037B RID: 891 RVA: 0x00013ADC File Offset: 0x00011CDC
		// (remove) Token: 0x0600037C RID: 892 RVA: 0x00013B14 File Offset: 0x00011D14
		public event MainWindow.User_Initiated_OfflineEventHandler User_Initiated_Offline;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600037D RID: 893 RVA: 0x00013B4C File Offset: 0x00011D4C
		// (remove) Token: 0x0600037E RID: 894 RVA: 0x00013B84 File Offset: 0x00011D84
		public event MainWindow.OnMapChangedEventHandler OnMapChanged;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600037F RID: 895 RVA: 0x00013BBC File Offset: 0x00011DBC
		// (remove) Token: 0x06000380 RID: 896 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public event MainWindow.OnUserInitiatedMapChangeEventHandler OnUserInitiatedMapChange;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000381 RID: 897 RVA: 0x00013C2C File Offset: 0x00011E2C
		// (remove) Token: 0x06000382 RID: 898 RVA: 0x00013C64 File Offset: 0x00011E64
		public event MainWindow.OnUserInitiatedMapRenderModeChangeEventHandler OnUserInitiatedMapRenderModeChange;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000383 RID: 899 RVA: 0x00013C9C File Offset: 0x00011E9C
		// (remove) Token: 0x06000384 RID: 900 RVA: 0x00013CD4 File Offset: 0x00011ED4
		public event MainWindow.Mission_StartedEventHandler Mission_Started;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000385 RID: 901 RVA: 0x00013D0C File Offset: 0x00011F0C
		// (remove) Token: 0x06000386 RID: 902 RVA: 0x00013D44 File Offset: 0x00011F44
		public event MainWindow.Mission_StoppedEventHandler Mission_Stopped;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000387 RID: 903 RVA: 0x00013D7C File Offset: 0x00011F7C
		// (remove) Token: 0x06000388 RID: 904 RVA: 0x00013DB4 File Offset: 0x00011FB4
		public event MainWindow.Archive_ElapsedEventHandler Archive_Elapsed;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000389 RID: 905 RVA: 0x00013DEC File Offset: 0x00011FEC
		// (remove) Token: 0x0600038A RID: 906 RVA: 0x00013E24 File Offset: 0x00012024
		public event MainWindow.User_Initiated_FollowChangeEventHandler User_Initiated_FollowChange;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600038B RID: 907 RVA: 0x00013E5C File Offset: 0x0001205C
		// (remove) Token: 0x0600038C RID: 908 RVA: 0x00013E94 File Offset: 0x00012094
		public event MainWindow.User_Initiated_RecordStartEventHandler User_Initiated_RecordStart;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600038D RID: 909 RVA: 0x00013ECC File Offset: 0x000120CC
		// (remove) Token: 0x0600038E RID: 910 RVA: 0x00013F04 File Offset: 0x00012104
		public event MainWindow.User_Initiated_RecordStopEventHandler User_Initiated_RecordStop;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600038F RID: 911 RVA: 0x00013F3C File Offset: 0x0001213C
		// (remove) Token: 0x06000390 RID: 912 RVA: 0x00013F74 File Offset: 0x00012174
		public event MainWindow.User_Initiated_PlaybackLoadEventHandler User_Initiated_PlaybackLoad;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000391 RID: 913 RVA: 0x00013FAC File Offset: 0x000121AC
		// (remove) Token: 0x06000392 RID: 914 RVA: 0x00013FE4 File Offset: 0x000121E4
		public event MainWindow.User_Initiated_PlaybackExitEventHandler User_Initiated_PlaybackExit;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000393 RID: 915 RVA: 0x0001401C File Offset: 0x0001221C
		// (remove) Token: 0x06000394 RID: 916 RVA: 0x00014054 File Offset: 0x00012254
		public event MainWindow.ScaleChangedEventHandler ScaleChanged;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000395 RID: 917 RVA: 0x0001408C File Offset: 0x0001228C
		// (remove) Token: 0x06000396 RID: 918 RVA: 0x000140C4 File Offset: 0x000122C4
		public event MainWindow.MapImportStartEventHandler MapImportStart;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000397 RID: 919 RVA: 0x000140FC File Offset: 0x000122FC
		// (remove) Token: 0x06000398 RID: 920 RVA: 0x00014134 File Offset: 0x00012334
		public event MainWindow.MapImportCompleteEventHandler MapImportComplete;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000399 RID: 921 RVA: 0x0001416C File Offset: 0x0001236C
		// (remove) Token: 0x0600039A RID: 922 RVA: 0x000141A4 File Offset: 0x000123A4
		public event MainWindow.MapImportFailEventHandler MapImportFail;

		// Token: 0x0600039B RID: 923 RVA: 0x000141DC File Offset: 0x000123DC
		public MainWindow()
		{
			base.Loaded += this.MainWindow_Loaded;
			base.Closing += this.MainWindow_Closing;
			this.User_Initiated_PlaybackLoad += this.PlaybackLoad;
			this.User_Initiated_PlaybackExit += this.PlaybackExit;
			this.OnOrbatLocateLocationSelected += this.HandleOrbatLocateLocationSelected;
			this.OnOrbatLocateAnchor += this.HandleOrbatLocateAnchor;
			this.OnOrbatCreateAnchor += this.HandleOrbatCreateAnchor;
			this.OnOrbatRemoveAnchor += this.HandleOrbatRemoveAnchor;
			this.OnOrbatTrackAnchor += this.HandleOrbatTrackAnchor;
			this.OnOrbatAnchorPosRequested += this.HandleOrbatClickAnchor;
			this.OnOrbatAnchorPosRetrieved += this.HandleOrbatAnchorClick;
			this.OnMapChanged += this.HandleACSMapChanged;
			this.OnOrbatFollowUnitSelected += this.HandleOrbatFollowUnitSelected;
			this.OnOrbatLocateUnitSelected += this.HandleOrbatLocateUnitSelected;
			this.OnOrbatTrackGroup += this.HandleOrbatTrackGroup;
			this.OnOrbatTrackUnit += this.HandleOrbatTrackUnit;
			base.PreviewKeyUp += this.MainWindow_PreviewKeyUp;
			base.PreviewMouseDoubleClick += this.MainWindow_PreviewMouseDoubleClick;
			this.User_Initiated_Online += this.GoOnline;
			this.User_Initiated_Offline += this.GoOffline;
			this.User_Initiated_FollowChange += this.ChangeFollow;
			this.User_Initiated_RecordStart += this.RecordStart;
			this.User_Initiated_RecordStop += this.RecordStop;
			this.OnUserInitiatedMapChange += this.LoadMap;
			this.OnUserInitiatedMapRenderModeChange += this.HandleSwitchRenderMode;
			this.MapImportFail += this.HandleMapImportFail;
			this.MapImportStart += this.HandleMapImportStart;
			this.MapImportComplete += this.HandleMapImportComplete;
			this.Mission_Started += this.OnMissionStarted;
			this.Mission_Stopped += this.OnMissionStopped;
			this.Archive_Elapsed += this.OnArchiveElapsed;
			this.MyACSHelper = new ACSHelper();
			this.myMapHelper = new MapHelper();
			this.PlaybackHelper = new FramesSet();
			this.Frames_Recording = new Frames();
			this.TrackingAnchor = null;
			this.TrackingAnchorLine = null;
			this.TrackingGroup = null;
			this.TrackingGroupLine = null;
			this.TrackingUnit = null;
			this.TrackingUnitLine = null;
			this.AthenaHotKeys = new List<AthenaHotKey>();
			this.LastUpdated_Units = DateTime.MinValue;
			this.LastUpdated_Groups = DateTime.MinValue;
			this.LastUpdated_Markers = DateTime.MinValue;
			this.LastUpdated_Vehicles = DateTime.MinValue;
			this.PlayerUnit = null;
			this.ZoomTimer = new DispatcherTimer();
			this.ArchiveTimer = new System.Timers.Timer();
			this.MissionTimer = new System.Timers.Timer();
			this.PlaybackTimer = new System.Timers.Timer();
			this.FormStatus = new frmStatus();
			this.SocketClientRelay = new ClientAsync();
			this.Comm_RelayClientGUID = Guid.Empty;
			this.Comm_LastUpdated = DateTime.Now;
			this.Comm_LastArchived = DateTime.Now;
			this.Comm_IsInMission = false;
			this.Status_Source = Enums.SourceStatus.Offline;
			this.Status_Relay = Enums.RelayStatus.Disconnecting;
			this.DIR_Athena = string.Empty;
			this.DIR_Data = string.Empty;
			this.DIR_Maps = string.Empty;
			this.DIR_Record = string.Empty;
			this.DIR_Record_Live = string.Empty;
			this.DIR_Record_Archives = string.Empty;
			this.DIR_Record_Playback = string.Empty;
			this.DIR_Save = string.Empty;
			this.Maps = new Dictionary<string, Map>();
			this.Map_Current = null;
			this.Map_Current_Anchors = new List<MapAnchor>();
			this.Map_Current_Locations = new List<MapLocation>();
			this.Map_Import = null;
			this.Map_Import_Data = null;
			this.Map_Import_Elevations = new List<ElevationCells>();
			this.Map_Import_FoliageTrees = new MapFoliage();
			this.Map_Import_Forests = new MapForests();
			this.Map_Import_Locations = new List<MapLocation>();
			this.Map_Import_Objects = new List<MapObject>();
			this.Map_Import_Roads = new List<MapRoads>();
			this.Map_Import_RoadSegments = new List<MapRoadSegment>();
			this.Map_Import_Trees = new List<MapBush>();
			this.Map_Import_InProgress = false;
			this.Map_Scroll_Follow = true;
			this.ZoomFactor = 1.0;
			this.ZoomFactorMax = 3.0;
			this.ZoomFactorMin = 0.1;
			this.ZoomStep = 0.1;
			this.ZoomLines = -1.0;
			this.ZoomLinesStep = -1;
			this.GroupMaximumZoomFactor = 2.2;
			this.IsMapReady = true;
			this.IsWaitingForUpdate = true;
			this.FollowedUnitNetID = string.Empty;
			this.Rec_IsRecording = false;
			this.Rec_LastFrameRecordedOn = DateTime.Now;
			this.Rec_RecordFrameIntervalSeconds = 1;
			this.Rec_ArchiveFile = string.Empty;
			this.Rec_FrameCurrent = 0;
			this.Rec_PlaybackSpeed = 1.0;
			this.Rec_PlaybackSpeedStep = 0.25;
			this.Rec_PlaybackSpeedMin = 0.25;
			this.Rec_PlaybackSpeedMax = 4.0;
			this.pointStart = default(Point);
			this.pointOffset = default(Point);
			this.CursorGridPos = new TextBlock();
			this.Tool = new ActiveTool
			{
				Current = Enums.ActiveToolEnum.None
			};
			this.Tool_Visible = false;
			this.InkCurrentMode = string.Empty;
			this.myLocationsMission = new List<MapLocation>();
			this.mySelectedAnchor = null;
			this.mySelectedLocation = null;
			this.ACS_IsConnected = false;
			this.ACS_IsAuthenticated = false;
			this.Pending_MapPublish_World = string.Empty;
			this.mySelectedBlock = null;
			this.InQueueMRE = new ManualResetEvent(false);
			this.FrameQueue = new ConcurrentQueue<string[]>();
			this.OtherQueue = new ConcurrentQueue<string[]>();
			this.CurrentMissionGUID = string.Empty;
			this.CurrentMissionSteamID = string.Empty;
			this.InitializeComponent();
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
			this.CreateFolderStructure();
			this.EnumerateMapFolders();
			this.UpdateMapMenu();
			this.LoadSettings();
			this.PrepChildObjects();
			this.CreateBackgroundThreads();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00014808 File Offset: 0x00012A08
		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.LoadHotkeys();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00014810 File Offset: 0x00012A10
		private void CreateBackgroundThreads()
		{
			this.CreateBackgroundThread(ref this.SocketRelayProcessThread, new ThreadStart(this.ProcessQueues));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001482A File Offset: 0x00012A2A
		private void CreateBackgroundThread(ref Thread TargetThread, ThreadStart StartProcedure)
		{
			if (TargetThread == null || (TargetThread != null && TargetThread.IsAlive))
			{
				TargetThread = new Thread(StartProcedure);
				TargetThread.IsBackground = true;
				TargetThread.Start();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00014854 File Offset: 0x00012A54
		private void CreateFolderStructure()
		{
			try
			{
				this.DIR_Athena = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Athena";
				if (!Directory.Exists(this.DIR_Athena))
				{
					Directory.CreateDirectory(this.DIR_Athena);
				}
				if (Directory.Exists(this.DIR_Athena))
				{
					this.DIR_Data = this.DIR_Athena + "\\Data";
					if (!Directory.Exists(this.DIR_Data))
					{
						Directory.CreateDirectory(this.DIR_Data);
					}
					this.DIR_Maps = this.DIR_Athena + "\\Maps";
					if (!Directory.Exists(this.DIR_Maps))
					{
						Directory.CreateDirectory(this.DIR_Maps);
					}
					this.DIR_Record = this.DIR_Athena + "\\Recordings";
					if (!Directory.Exists(this.DIR_Record))
					{
						Directory.CreateDirectory(this.DIR_Record);
					}
					if (Directory.Exists(this.DIR_Record))
					{
						this.DIR_Record_Live = this.DIR_Record + "\\Live";
						if (!Directory.Exists(this.DIR_Record_Live))
						{
							Directory.CreateDirectory(this.DIR_Record_Live);
						}
						this.DIR_Record_Archives = this.DIR_Record + "\\Archives";
						if (!Directory.Exists(this.DIR_Record_Archives))
						{
							Directory.CreateDirectory(this.DIR_Record_Archives);
						}
						this.DIR_Record_Playback = this.DIR_Record + "\\Playback";
						if (!Directory.Exists(this.DIR_Record_Playback))
						{
							Directory.CreateDirectory(this.DIR_Record_Playback);
						}
					}
					this.DIR_Save = this.DIR_Athena + "\\Saves";
					if (!Directory.Exists(this.DIR_Save))
					{
						Directory.CreateDirectory(this.DIR_Save);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00014A28 File Offset: 0x00012C28
		private void EnumerateMapFolders()
		{
			try
			{
				this.Maps.Clear();
				if (Directory.Exists(this.DIR_Maps))
				{
					List<string> list = Directory.EnumerateDirectories(this.DIR_Maps).ToList<string>();
					if (list != null && list.Count != 0)
					{
						try
						{
							foreach (string str in list)
							{
								if (File.Exists(str + "\\map.txt"))
								{
									Map map = this.ParseMapFile(str + "\\map.txt");
									if (map != null && !string.IsNullOrEmpty(map.WorldName.Trim()) && !this.Maps.ContainsKey(map.WorldName))
									{
										this.Maps.Add(map.WorldName, map);
									}
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00014B2C File Offset: 0x00012D2C
		private Map ParseMapFile(string File)
		{
			Map map = null;
			try
			{
				if (File.Exists(File))
				{
					string text = File.ReadAllText(File);
					if (text != null)
					{
						map = JsonConvert.DeserializeObject<Map>(text);
						if (map != null && Operators.CompareString(map.Folder, System.IO.Path.GetDirectoryName(File), false) != 0)
						{
							map.Folder = System.IO.Path.GetDirectoryName(File);
							try
							{
								File.WriteAllText(File, JsonConvert.SerializeObject(map));
							}
							catch (Exception ex)
							{
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				map = null;
			}
			return map;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00014BC4 File Offset: 0x00012DC4
		private void UpdateMapMenu()
		{
			try
			{
				this.menuMap.Items.Clear();
				if (this.Maps != null && this.Maps.Count != 0)
				{
					try
					{
						foreach (Map map in this.Maps.Values.OrderBy((MainWindow._Closure$__.$I208-0 == null) ? (MainWindow._Closure$__.$I208-0 = ((Map x) => x.DisplayName)) : MainWindow._Closure$__.$I208-0).ToList<Map>())
						{
							System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem();
							if (string.IsNullOrEmpty(map.DisplayName))
							{
								menuItem.Header = map.WorldName;
								menuItem.CommandParameter = map.WorldName;
							}
							else
							{
								menuItem.Header = map.DisplayName;
								menuItem.CommandParameter = map.WorldName;
							}
							menuItem.Click += this.handleMenuMap_Click;
							this.menuMap.Items.Add(menuItem);
						}
					}
					finally
					{
						List<Map>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00014CF4 File Offset: 0x00012EF4
		private void PrepChildObjects()
		{
			this.ZoomTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
			if (this.MapVC != null)
			{
				this.MapVC.UseTouchForInk = MySettings.Default.UseTouchForInk;
				this.MapVC.ScaleChanged += this.HandleMapVCScaleChange;
				if (this.MapVC.ScrollOwner == null)
				{
					this.MapVC.ScrollOwner = this.MapScroll;
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00014D6C File Offset: 0x00012F6C
		private void HideAll()
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.ClearDictionaries();
				this.RemoveMissionInfo();
				this.RemoveUserInk();
				this.ClearORBAT();
				this.ClearMissionAnchors();
				this.HideTrackingLines();
			});
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014D85 File Offset: 0x00012F85
		private void ExitAthena()
		{
			this.CleanUpBeforeClose();
			base.Close();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00014D93 File Offset: 0x00012F93
		private void MainWindow_Closing(object sender, CancelEventArgs e)
		{
			this.CleanUpBeforeClose();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00014D9C File Offset: 0x00012F9C
		private void CleanUpBeforeClose()
		{
			this.GoOffline();
			if (this.frmHotKeys != null)
			{
				this.frmHotKeys.AllowClose = true;
				this.frmHotKeys.Close();
			}
			if (this.hkHost != null)
			{
				this.hkHost.UnHookKeyboard();
				this.hkHost = null;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00014DE8 File Offset: 0x00012FE8
		private void ShowPleaseWait(string WaitText = "")
		{
			if (string.IsNullOrEmpty(WaitText))
			{
				this.txtWait.Text = "ATHENA IS BUSY WORKING";
			}
			else
			{
				this.txtWait.Text = WaitText.ToUpper();
			}
			this.brdWait.Visibility = Visibility.Visible;
			this.ForceUpdate();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014E27 File Offset: 0x00013027
		private void HidePleaseWait()
		{
			this.brdWait.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00014E38 File Offset: 0x00013038
		private void ForceUpdate()
		{
			try
			{
				DispatcherFrame dispatcherFrame = new DispatcherFrame();
				Dispatcher.CurrentDispatcher.BeginInvoke(new DispatcherOperationCallback(this.ExitFrame), DispatcherPriority.Background, new object[]
				{
					dispatcherFrame
				});
				Dispatcher.PushFrame(dispatcherFrame);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00014E94 File Offset: 0x00013094
		private object ExitFrame(object F)
		{
			((DispatcherFrame)F).Continue = false;
			return null;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00014EA4 File Offset: 0x000130A4
		private void ShowMessage(string Title, string Message)
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.txtMessageTitle.Text = Title;
				this.txtMessageValue.Text = Message;
				this.brdMessage.Visibility = Visibility.Visible;
			});
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014EE3 File Offset: 0x000130E3
		private void HandleMessageHide_Click(object sender, RoutedEventArgs e)
		{
			this.brdMessage.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00014EF1 File Offset: 0x000130F1
		private void ToggleFlyoutRight()
		{
			if (this.FlyoutRight.IsVisible)
			{
				this.FlyoutRight.Visibility = Visibility.Collapsed;
				this.MenuShowFlyoutRight.IsChecked = false;
				return;
			}
			this.FlyoutRight.Visibility = Visibility.Visible;
			this.MenuShowFlyoutRight.IsChecked = true;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00014F31 File Offset: 0x00013131
		private void ShowWelcomeGUI()
		{
			new frmAbout().ShowDialog();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014F40 File Offset: 0x00013140
		private void ShowHotKeysGUI()
		{
			if (this.frmHotKeys == null)
			{
				this.frmHotKeys = new HotKeys();
				this.frmHotKeys.HotkeyAdditionRequested += this.HandleHotKeyRequest;
				this.frmHotKeys.HotkeyRemovalRequested += this.HandleHotKeyRemoval;
			}
			this.frmHotKeys.AthenaHotKeys.AddRange(this.AthenaHotKeys);
			this.frmHotKeys.LoadHotKeys();
			try
			{
				if (this.hkHost == null)
				{
					this.CreateHotKeyHost();
				}
				if (this.hkHost == null)
				{
					System.Windows.MessageBox.Show("The HotKey system is unavailable.");
				}
				else
				{
					this.hkHost.UnHookKeyboard();
					this.frmHotKeys.ShowDialog();
					this.hkHost.HookKeyboard();
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("An error occurred while attempting to display the HotKey form.");
			}
			this.SaveHotKeys();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015028 File Offset: 0x00013228
		private void CreateHotKeyHost()
		{
			if (this.hkHost == null)
			{
				this.hkHost = new LowLevelKeyboardListener();
				this.hkHost.OnKeyPressed += this.HandleHotKeyPressed;
				this.hkHost.HookKeyboard();
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00015060 File Offset: 0x00013260
		private void HandleHotKeyRequest(string Action, string KeyString, Keys Key, bool Shift, bool Control, bool AltMenu)
		{
			if (this.CheckForDuplicateHotKey(Action, KeyString, Key, Shift, Control, AltMenu))
			{
				if (this.frmHotKeys != null)
				{
					this.frmHotKeys.ClearConflict(Action);
					return;
				}
			}
			else
			{
				this.RemoveExistingHotKeysForAction(Action);
				this.AthenaHotKeys.Add(new AthenaHotKey
				{
					Action = Action,
					HotKeyString = KeyString,
					HotKey = Key,
					Shift = Shift,
					Control = Control,
					AltMenu = AltMenu
				});
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000150D8 File Offset: 0x000132D8
		private void HandleHotKeyRemoval(string Action)
		{
			this.CreateHotKeyHost();
			checked
			{
				if (this.hkHost != null && this.AthenaHotKeys != null && this.AthenaHotKeys.Count != 0)
				{
					for (int i = this.AthenaHotKeys.Count - 1; i >= 0; i += -1)
					{
						if (Operators.CompareString(this.AthenaHotKeys[i].Action, Action, false) == 0)
						{
							this.AthenaHotKeys.RemoveAt(i);
						}
					}
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00015148 File Offset: 0x00013348
		private void HandleHotKeyPressed(object sender, KeyPressedArgs e)
		{
			if (this.AthenaHotKeys != null && this.AthenaHotKeys.Count != 0)
			{
				try
				{
					foreach (AthenaHotKey athenaHotKey in this.AthenaHotKeys)
					{
						if (athenaHotKey.HotKey.Equals(e.KeyPressed) & athenaHotKey.Shift == e.Shift & athenaHotKey.Control == e.Control & athenaHotKey.AltMenu == e.AltMenu)
						{
							string action = athenaHotKey.Action;
							uint num = <PrivateImplementationDetails>.ComputeStringHash(action);
							if (num <= 2067622972U)
							{
								if (num <= 710316662U)
								{
									if (num <= 283578168U)
									{
										if (num != 85755406U)
										{
											if (num != 283578168U)
											{
												break;
											}
											if (Operators.CompareString(action, "pandown", false) != 0)
											{
												break;
											}
											this.MapPan("down");
											break;
										}
										else
										{
											if (Operators.CompareString(action, "layergroups", false) != 0)
											{
												break;
											}
											ToggleButton chkLayer = this.chkLayer5;
											bool? isChecked = this.chkLayer5.IsChecked;
											chkLayer.IsChecked = ((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked);
											this.HandleLayerToggle(null, new RoutedEventArgs(null, this.chkLayer5));
											break;
										}
									}
									else if (num != 429101034U)
									{
										if (num != 710316662U)
										{
											break;
										}
										if (Operators.CompareString(action, "layerink", false) != 0)
										{
											break;
										}
										ToggleButton chkLayer2 = this.chkLayer2;
										bool? isChecked = this.chkLayer2.IsChecked;
										chkLayer2.IsChecked = ((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked);
										this.HandleLayerToggle(null, new RoutedEventArgs(null, this.chkLayer2));
										break;
									}
									else
									{
										if (Operators.CompareString(action, "layerlocations", false) != 0)
										{
											break;
										}
										ToggleButton chkLayer3 = this.chkLayer0;
										bool? isChecked = this.chkLayer0.IsChecked;
										chkLayer3.IsChecked = ((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked);
										this.HandleLayerToggle(null, new RoutedEventArgs(null, this.chkLayer0));
										break;
									}
								}
								else if (num <= 1697318111U)
								{
									if (num != 1191862662U)
									{
										if (num != 1697318111U)
										{
											break;
										}
										if (Operators.CompareString(action, "start", false) != 0)
										{
											break;
										}
										if (this.Status_Source == Enums.SourceStatus.Recording)
										{
											MainWindow.User_Initiated_OfflineEventHandler user_Initiated_OfflineEvent = this.User_Initiated_OfflineEvent;
											if (user_Initiated_OfflineEvent != null)
											{
												user_Initiated_OfflineEvent();
												break;
											}
											break;
										}
										else if (this.Status_Source != Enums.SourceStatus.Offline)
										{
											MainWindow.User_Initiated_OfflineEventHandler user_Initiated_OfflineEvent2 = this.User_Initiated_OfflineEvent;
											if (user_Initiated_OfflineEvent2 != null)
											{
												user_Initiated_OfflineEvent2();
												break;
											}
											break;
										}
										else
										{
											MainWindow.User_Initiated_OnlineEventHandler user_Initiated_OnlineEvent = this.User_Initiated_OnlineEvent;
											if (user_Initiated_OnlineEvent != null)
											{
												user_Initiated_OnlineEvent();
												break;
											}
											break;
										}
									}
									else
									{
										if (Operators.CompareString(action, "zoomout", false) != 0)
										{
											break;
										}
										this.ZoomOut(false);
										break;
									}
								}
								else if (num != 1909040957U)
								{
									if (num != 2067622972U)
									{
										break;
									}
									if (Operators.CompareString(action, "track", false) != 0)
									{
										break;
									}
									MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
									if (user_Initiated_FollowChangeEvent != null)
									{
										user_Initiated_FollowChangeEvent(!this.Map_Scroll_Follow);
										break;
									}
									break;
								}
								else
								{
									if (Operators.CompareString(action, "layermarkers", false) != 0)
									{
										break;
									}
									ToggleButton chkLayer4 = this.chkLayer3;
									bool? isChecked = this.chkLayer3.IsChecked;
									chkLayer4.IsChecked = ((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked);
									this.HandleLayerToggle(null, new RoutedEventArgs(null, this.chkLayer3));
									break;
								}
							}
							else if (num <= 3777329969U)
							{
								if (num <= 3472975342U)
								{
									if (num != 2281451638U)
									{
										if (num != 3472975342U)
										{
											break;
										}
										if (Operators.CompareString(action, "layergrid", false) != 0)
										{
											break;
										}
										ToggleButton chkLayer5 = this.chkLayer1;
										bool? isChecked = this.chkLayer1.IsChecked;
										chkLayer5.IsChecked = ((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked);
										this.HandleLayerToggle(null, new RoutedEventArgs(null, this.chkLayer1));
										break;
									}
									else
									{
										if (Operators.CompareString(action, "panright", false) != 0)
										{
											break;
										}
										this.MapPan("right");
										break;
									}
								}
								else if (num != 3652464425U)
								{
									if (num != 3777329969U)
									{
										break;
									}
									if (Operators.CompareString(action, "panup", false) != 0)
									{
										break;
									}
									this.MapPan("up");
									break;
								}
								else
								{
									if (Operators.CompareString(action, "layerunits", false) != 0)
									{
										break;
									}
									ToggleButton chkLayer6 = this.chkLayer4;
									bool? isChecked = this.chkLayer4.IsChecked;
									chkLayer6.IsChecked = ((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked);
									this.HandleLayerToggle(null, new RoutedEventArgs(null, this.chkLayer4));
									break;
								}
							}
							else if (num <= 3899525117U)
							{
								if (num != 3889436925U)
								{
									if (num != 3899525117U)
									{
										break;
									}
									if (Operators.CompareString(action, "panleft", false) != 0)
									{
										break;
									}
									this.MapPan("left");
									break;
								}
								else
								{
									if (Operators.CompareString(action, "zoomin", false) != 0)
									{
										break;
									}
									this.ZoomIn(false);
									break;
								}
							}
							else if (num != 3952099778U)
							{
								if (num != 4149321280U)
								{
									if (num != 4204126854U)
									{
										break;
									}
									if (Operators.CompareString(action, "unitnext", false) != 0)
									{
										break;
									}
									this.ChangeFollowedUnit(1);
									break;
								}
								else
								{
									if (Operators.CompareString(action, "recordstart", false) != 0)
									{
										break;
									}
									if (this.Rec_IsRecording)
									{
										MainWindow.User_Initiated_RecordStopEventHandler user_Initiated_RecordStopEvent = this.User_Initiated_RecordStopEvent;
										if (user_Initiated_RecordStopEvent != null)
										{
											user_Initiated_RecordStopEvent();
											break;
										}
										break;
									}
									else
									{
										MainWindow.User_Initiated_RecordStartEventHandler user_Initiated_RecordStartEvent = this.User_Initiated_RecordStartEvent;
										if (user_Initiated_RecordStartEvent != null)
										{
											user_Initiated_RecordStartEvent();
											break;
										}
										break;
									}
								}
							}
							else
							{
								if (Operators.CompareString(action, "unitprev", false) != 0)
								{
									break;
								}
								this.ChangeFollowedUnit(0);
								break;
							}
						}
					}
				}
				finally
				{
					List<AthenaHotKey>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000157A4 File Offset: 0x000139A4
		private bool CheckForDuplicateHotKey(string Action, string KeyString, Keys Key, bool Shift, bool Control, bool AltMenu)
		{
			bool result = false;
			if (this.AthenaHotKeys != null && this.AthenaHotKeys.Count != 0)
			{
				try
				{
					foreach (AthenaHotKey athenaHotKey in this.AthenaHotKeys)
					{
						if ((athenaHotKey.HotKey.Equals(Key) & athenaHotKey.Shift == Shift & athenaHotKey.Control == Control & athenaHotKey.AltMenu == AltMenu) && Operators.CompareString(athenaHotKey.Action, Action, false) != 0)
						{
							System.Windows.MessageBox.Show("The key combo " + KeyString + " is already assigned to the following action: " + athenaHotKey.Action);
							result = true;
						}
					}
				}
				finally
				{
					List<AthenaHotKey>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			return result;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015878 File Offset: 0x00013A78
		private void RemoveExistingHotKeysForAction(string Action)
		{
			checked
			{
				if (this.AthenaHotKeys != null && this.AthenaHotKeys.Count != 0)
				{
					for (int i = this.AthenaHotKeys.Count - 1; i <= 0; i++)
					{
						if (Operators.CompareString(this.AthenaHotKeys[i].Action, Action, false) == 0)
						{
							this.AthenaHotKeys.RemoveAt(i);
						}
					}
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000158D8 File Offset: 0x00013AD8
		private void LoadHotkeys()
		{
			if (MySettings.Default["HotKeys"] != null && !string.IsNullOrEmpty(MySettings.Default["HotKeys"].ToString()))
			{
				try
				{
					this.AthenaHotKeys = (List<AthenaHotKey>)JsonConvert.DeserializeObject(MySettings.Default["HotKeys"].ToString(), typeof(List<AthenaHotKey>));
				}
				catch (Exception ex)
				{
					MySettings.Default.HotKeys = string.Empty;
					this.LoadHotkeys();
				}
			}
			if (this.AthenaHotKeys != null && this.AthenaHotKeys.Count != 0)
			{
				this.CreateHotKeyHost();
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00015990 File Offset: 0x00013B90
		private void SaveHotKeys()
		{
			MySettings.Default.HotKeys = JsonConvert.SerializeObject(this.AthenaHotKeys);
			MySettings.Default.Save();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000159B4 File Offset: 0x00013BB4
		private void ShowLoadGUI()
		{
			if (this.Status_Source != Enums.SourceStatus.Offline)
			{
				System.Windows.MessageBox.Show("Unable to load file while in 'Start' mode.");
				return;
			}
			frmLoad frmLoad = new frmLoad();
			frmLoad.dlgOpenFile.InitialDirectory = this.DIR_Save;
			if (frmLoad.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string text = frmLoad.txtFileName.Text;
				bool @checked = frmLoad.chkLoadGameGroupsUnits.Checked;
				bool checked2 = frmLoad.chkLoadGameMarkers.Checked;
				bool checked3 = frmLoad.chkSaveInk.Checked;
				if (string.IsNullOrWhiteSpace(text))
				{
					System.Windows.MessageBox.Show("You must specify a file name");
					return;
				}
				if (!File.Exists(text))
				{
					System.Windows.MessageBox.Show("The specified file does not exist");
					return;
				}
				Data JSONData = new Data();
				JSONData.LoadAthenaFile(text);
				if (JSONData.AthenaFile == null)
				{
					System.Windows.MessageBox.Show("An error occurred during import. Unable to parse frames object.");
					return;
				}
				if (JSONData.AthenaFile.Frames == null || JSONData.AthenaFile.Frames.Children.Count == 0 || JSONData.AthenaFile.Frames.Mission == null)
				{
					System.Windows.MessageBox.Show("An error occurred during import. Unable to parse frames or mission object.");
					return;
				}
				if (string.IsNullOrEmpty(JSONData.AthenaFile.Frames.Mission.Map))
				{
					System.Windows.MessageBox.Show("An error occurred during import. Unable to determine map name.");
					return;
				}
				if (this.Maps == null || this.Maps.Count == 0)
				{
					System.Windows.MessageBox.Show("You have not imported any maps.");
					return;
				}
				Map value = this.Maps.SingleOrDefault((KeyValuePair<string, Map> x) => Operators.CompareString(x.Value.WorldName.Trim().ToLower(), JSONData.AthenaFile.Frames.Mission.Map.Trim().ToLower(), false) == 0).Value;
				if (value == null || !value.WorldName.Equals(JSONData.AthenaFile.Frames.Mission.Map, StringComparison.InvariantCultureIgnoreCase))
				{
					System.Windows.MessageBox.Show("Unable to locate map (" + JSONData.AthenaFile.Frames.Mission.Map + "). Does not appear to have been imported.");
					return;
				}
				this.CurrentFrames = JSONData.AthenaFile.Frames;
				this.LoadMap(value);
				this.Zoom(JSONData.AthenaFile.Zoom, false, true);
				this.MapScroll.ScrollToHorizontalOffset(JSONData.AthenaFile.OffsetX);
				this.MapScroll.ScrollToVerticalOffset(JSONData.AthenaFile.OffsetY);
				if (@checked)
				{
					this.UpdateDictionaryOfVehicles(ref JSONData.AthenaFile.Frames.Children[0].Vehicles);
					this.UpdateDictionaryOfUnits(ref JSONData.AthenaFile.Frames.Children[0].Units);
					this.UpdateDictionaryOfGroups(ref JSONData.AthenaFile.Frames.Children[0].Groups);
					this.UpdateVehicleLinks();
					this.UpdateUnitIcons();
					this.UpdateGroupIcons();
					this.UpdateVehicleIcons();
				}
				if (checked2)
				{
					this.UpdateDictionaryOfMarkers(ref JSONData.AthenaFile.Frames.Children[0].Markers);
				}
				if (checked3 && JSONData.AthenaFile.UserInk != null && JSONData.AthenaFile.UserInk.Length != 0)
				{
					this.MapVC.Ink.Strokes = new StrokeCollection(new MemoryStream(JSONData.AthenaFile.UserInk));
				}
				JSONData = null;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015D2C File Offset: 0x00013F2C
		private void ShowSaveGUI()
		{
			if (this.Map_Current == null)
			{
				System.Windows.MessageBox.Show("There is no data to save.");
				return;
			}
			frmSave frmSave = new frmSave();
			frmSave.dlgDestFile.InitialDirectory = this.DIR_Save;
			frmSave.txtFileName.Text = this.DIR_Save;
			if (frmSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string text = frmSave.txtFileName.Text;
				bool @checked = frmSave.chkIncludeMarkers.Checked;
				bool checked2 = frmSave.chkSaveGroupsUnits.Checked;
				bool checked3 = frmSave.chkSaveInk.Checked;
				if (string.IsNullOrWhiteSpace(text))
				{
					System.Windows.MessageBox.Show("You must specify a file name.");
					return;
				}
				if (Directory.Exists(text))
				{
					System.Windows.MessageBox.Show("You've specified a folder and not a file name.");
					return;
				}
				if (!text.Contains("\\") & !text.Contains("/"))
				{
					text = this.DIR_Save + "\\" + text;
				}
				if (!System.IO.Path.GetFileName(text).EndsWith(".athena"))
				{
					text += ".athena";
				}
				if (!checked2 & !@checked & !checked3)
				{
					System.Windows.MessageBox.Show("You have chosen not to save any data so no filed was created.");
					return;
				}
				AthenaFile athenaFile = new AthenaFile();
				athenaFile.Zoom = this.ZoomFactor;
				athenaFile.OffsetX = this.MapScroll.HorizontalOffset;
				athenaFile.OffsetY = this.MapScroll.VerticalOffset;
				athenaFile.Frames.Mission.Map = this.Map_Current.WorldName;
				Frames currentFrames = this.CurrentFrames;
				if (currentFrames != null && currentFrames.Mission != null)
				{
					athenaFile.Frames.Mission = currentFrames.Mission;
					athenaFile.Frames.MissionGUID = currentFrames.MissionGUID;
				}
				athenaFile.Frames.Children.Add(new Athena.Objects.v2.Frame());
				if ((checked2 || @checked) && currentFrames != null && currentFrames.Children != null && currentFrames.Children.Count == 1)
				{
					Athena.Objects.v2.Frame frame = currentFrames.Children[0];
					Athena.Objects.v2.Frame frame2 = athenaFile.Frames.Children[0];
					if (checked2)
					{
						if (frame.Groups != null)
						{
							frame2.Groups = frame.Groups;
						}
						if (frame.Units != null)
						{
							frame2.Units = frame.Units;
						}
						if (frame.Vehicles != null)
						{
							frame2.Vehicles = frame.Vehicles;
						}
					}
					if (@checked && frame.Markers != null)
					{
						frame2.Markers = frame.Markers;
					}
				}
				if (checked3)
				{
					try
					{
						MemoryStream memoryStream = new MemoryStream();
						this.MapVC.Ink.Strokes.Save(memoryStream);
						memoryStream.Position = 0L;
						athenaFile.UserInk = memoryStream.ToArray();
						memoryStream.Close();
						memoryStream.Dispose();
					}
					catch (Exception ex)
					{
					}
				}
				string text2 = new Data().SaveAthenaFile(athenaFile, text);
				if (string.IsNullOrEmpty(text2))
				{
					System.Windows.MessageBox.Show("File saved successfully!");
					return;
				}
				System.Windows.MessageBox.Show(text2);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00016034 File Offset: 0x00014234
		private void ShowSettingsGUI()
		{
			frmSettings frmSettings = new frmSettings();
			if (frmSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (string.IsNullOrEmpty(frmSettings.txtARMAIP.Text.Trim()))
				{
					frmSettings.txtARMAIP.Text = "localhost";
				}
				if (string.IsNullOrEmpty(frmSettings.txtARMAPort.Text.Trim()))
				{
					frmSettings.txtARMAPort.Text = "28800";
				}
				else
				{
					int num = -1;
					try
					{
						num = Convert.ToInt32(frmSettings.txtARMAPort.Text.Trim(), CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						num = -1;
					}
					if (num != -1)
					{
						if (num < 0 | num > 65536)
						{
							frmSettings.txtARMAPort.Text = "28800";
						}
						else
						{
							frmSettings.txtARMAPort.Text = num.ToString();
						}
					}
					else
					{
						frmSettings.txtARMAPort.Text = "28800";
					}
				}
				MySettings.Default.ARMAPCIP = frmSettings.txtARMAIP.Text;
				MySettings.Default.ARMAPCPort = Conversions.ToInteger(frmSettings.txtARMAPort.Text);
				MySettings.Default.UseTouchForInk = frmSettings.chkUseTouchForInk.Checked;
				MySettings.Default.Save();
				this.LoadSettings();
			}
			frmSettings = null;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00016184 File Offset: 0x00014384
		private void LoadSettings()
		{
			if (MySettings.Default["ARMAPCIP"] == null)
			{
				MySettings.Default.ARMAPCIP = "localhost";
			}
			if (MySettings.Default["ARMAPCPORT"] == null)
			{
				MySettings.Default.ARMAPCPort = 28800;
			}
			if (MySettings.Default["UseTouchForInk"] == null)
			{
				MySettings.Default.UseTouchForInk = true;
			}
			if (MySettings.Default["ACSCallsign"] == null)
			{
				MySettings.Default.ACSCallsign = string.Empty;
			}
			if (MySettings.Default["ACSServerAddress"] == null)
			{
				MySettings.Default.ACSServerAddress = string.Empty;
			}
			if (MySettings.Default["ACSServerPort"] == null)
			{
				MySettings.Default.ACSServerPort = 28804;
			}
			if (this.MapVC != null)
			{
				this.MapVC.UseTouchForInk = MySettings.Default.UseTouchForInk;
			}
			base.Dispatcher.Invoke(delegate()
			{
				this.txtACSCallsign.Text = MySettings.Default.ACSCallsign;
				this.txtACSServerAddress.Text = MySettings.Default.ACSServerAddress;
				this.txtACSServerPort.Text = MySettings.Default.ACSServerPort.ToString();
			});
			MySettings.Default.Save();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001628B File Offset: 0x0001448B
		private void ShowStatusWindow()
		{
			this.FormStatus.Show();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00016298 File Offset: 0x00014498
		private void ShowRecordingGUI()
		{
			frmLoadRecording frmLoadRecording = new frmLoadRecording();
			frmLoadRecording.dlgOpenFile.InitialDirectory = this.DIR_Record_Archives;
			if (frmLoadRecording.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(frmLoadRecording.txtFileName.Text) && File.Exists(frmLoadRecording.txtFileName.Text))
			{
				MainWindow.User_Initiated_PlaybackLoadEventHandler user_Initiated_PlaybackLoadEvent = this.User_Initiated_PlaybackLoadEvent;
				if (user_Initiated_PlaybackLoadEvent != null)
				{
					user_Initiated_PlaybackLoadEvent(frmLoadRecording.txtFileName.Text);
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00016308 File Offset: 0x00014508
		private void PlaybackLoad(string Archive)
		{
			this.ShowPleaseWait("Loading archive. Load time increases with archive size.");
			this.GoOffline();
			this.Status_Source = Enums.SourceStatus.Recording;
			this.Rec_ArchiveFile = Archive;
			this.Rec_FrameCurrent = 0;
			this.PlaybackHelper.Populate(this.Rec_ArchiveFile, this.DIR_Record_Playback);
			bool flag = false;
			if (this.PlaybackHelper.Frames.Mission != null && !string.IsNullOrEmpty(this.PlaybackHelper.Frames.Mission.Map))
			{
				flag = this.CheckHasMap(this.PlaybackHelper.Frames.Mission.Map);
			}
			if (flag)
			{
				this.CurrentMissionGUID = string.Empty;
				this.CurrentMissionSteamID = string.Empty;
				if (this.PlaybackHelper != null)
				{
					this.CurrentMissionGUID = this.PlaybackHelper.Frames.MissionGUID;
					this.CurrentMissionSteamID = this.PlaybackHelper.Frames.Mission.SteamID;
				}
				this.Mode_Online_Button.Content = "PLAY";
				this.Mode_Offline_Button.Content = "PAUSE";
				this.Playback_Recording.Visibility = Visibility.Collapsed;
				this.Playback_Play.Visibility = Visibility.Visible;
				this.MenuRecordingStart.IsEnabled = false;
				this.MenuRecordingStop.IsEnabled = false;
				this.PlaybackTotalFrames.Text = "Total Frames: " + this.PlaybackHelper.TotalFrames.ToString();
				this.PlaybackCurrentFrame.Text = "Current Frame: " + (checked(this.Rec_FrameCurrent + 1)).ToString();
				this.PlayFrame(false);
				this.EnableFrameControls();
			}
			else
			{
				MainWindow.User_Initiated_PlaybackExitEventHandler user_Initiated_PlaybackExitEvent = this.User_Initiated_PlaybackExitEvent;
				if (user_Initiated_PlaybackExitEvent != null)
				{
					user_Initiated_PlaybackExitEvent();
				}
				System.Windows.MessageBox.Show("You do not appear to have the map required to play this recording.");
			}
			this.HidePleaseWait();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000164C0 File Offset: 0x000146C0
		private void EnableFrameControls()
		{
			this.FrameFirst.IsEnabled = true;
			this.FramePrev.IsEnabled = true;
			this.FrameNext.IsEnabled = true;
			this.FrameLast.IsEnabled = true;
			this.Frame_Current.IsEnabled = true;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00016500 File Offset: 0x00014700
		private void PackageLiveFolder()
		{
			try
			{
				if (Directory.Exists(this.DIR_Record_Live))
				{
					List<string> list = Directory.EnumerateFiles(this.DIR_Record_Live).ToList<string>();
					if (list.Count != 0)
					{
						list.Sort();
						string destinationArchiveFileName = string.Empty;
						try
						{
							destinationArchiveFileName = this.DIR_Record_Archives + "\\" + list[0].Split(new char[]
							{
								'\\'
							}).Last<string>().Replace("json", "zip");
						}
						catch (Exception ex)
						{
						}
						try
						{
							ZipFile.CreateFromDirectory(this.DIR_Record_Live, destinationArchiveFileName);
						}
						catch (Exception ex2)
						{
						}
					}
				}
			}
			catch (Exception ex3)
			{
			}
			this.PlaybackHelper.DeleteFiles(this.DIR_Record_Live);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000165F8 File Offset: 0x000147F8
		private string GenerateLiveFileName()
		{
			string result = string.Empty;
			try
			{
				DateTime now = DateTime.Now;
				string str = string.Concat(new string[]
				{
					now.Year.ToString(),
					now.Month.ToString().PadLeft(2, '0'),
					now.Day.ToString().PadLeft(2, '0'),
					now.Hour.ToString().PadLeft(2, '0'),
					now.Minute.ToString().PadLeft(2, '0'),
					now.Second.ToString().PadLeft(2, '0')
				});
				result = this.DIR_Record_Live + "\\" + str + ".json";
			}
			catch (Exception ex)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000166F0 File Offset: 0x000148F0
		private void PlaybackExit()
		{
			this.Status_Source = Enums.SourceStatus.Offline;
			this.Rec_FrameCurrent = 0;
			this.Mode_Online_Button.Content = "START";
			this.Mode_Offline_Button.Content = "STOP";
			this.Playback_Recording.Visibility = Visibility.Visible;
			this.Playback_Play.Visibility = Visibility.Collapsed;
			this.MenuRecordingStart.IsEnabled = true;
			this.MenuRecordingStop.IsEnabled = false;
			this.GoOffline();
			this.UpdateFrameControls(0);
			this.PlaybackHelper.DeleteFiles(this.DIR_Record_Playback);
			this.StopPlaybackCycle();
			this.DisableFrameControls();
			this.CurrentMissionGUID = string.Empty;
			this.CurrentMissionSteamID = string.Empty;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001679C File Offset: 0x0001499C
		private void UpdateFrameControls(int CurrentFrame)
		{
			try
			{
				if (this.Status_Source == Enums.SourceStatus.Recording)
				{
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackCurrentFrame.Text = "Current Frame: " + CurrentFrame.ToString();
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackSpeed.Text = "Playback Speed: " + this.Rec_PlaybackSpeed.ToString();
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.Frame_Current.Text = CurrentFrame.ToString();
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackCurrentFrame.Visibility = Visibility.Visible;
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackSpeed.Visibility = Visibility.Visible;
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackTotalFrames.Visibility = Visibility.Visible;
					}), new object[0]);
				}
				else
				{
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackCurrentFrame.Visibility = Visibility.Collapsed;
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackSpeed.Visibility = Visibility.Collapsed;
					}), new object[0]);
					System.Windows.Application.Current.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.PlaybackTotalFrames.Visibility = Visibility.Collapsed;
					}), new object[0]);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001692C File Offset: 0x00014B2C
		private void DisableFrameControls()
		{
			this.FrameFirst.IsEnabled = false;
			this.FramePrev.IsEnabled = false;
			this.FrameNext.IsEnabled = false;
			this.FrameLast.IsEnabled = false;
			this.Frame_Current.IsEnabled = false;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001696C File Offset: 0x00014B6C
		private void GotoBIS()
		{
			try
			{
				Process.Start("http://forums.bistudio.com/showthread.php?181928-Athena");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000169A4 File Offset: 0x00014BA4
		private void GotoAthena()
		{
			Process.Start("http://www.athenamod.com");
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000169B4 File Offset: 0x00014BB4
		private void ChangeInkMode(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;
				if (button != null && button.Tag != null)
				{
					if (Operators.ConditionalCompareObjectEqual(this.InkCurrentMode, button.Tag.ToString(), false))
					{
						this.InkCurrentMode = string.Empty;
						this.Tool.Current = Enums.ActiveToolEnum.None;
					}
					else
					{
						this.InkCurrentMode = button.Tag.ToString();
						this.Tool.Current = Enums.ActiveToolEnum.Ink;
					}
					this.btnInkModeDraw.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
					this.btnInkModeLight.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
					this.btnInkModeErase.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
					this.btnInkModeSelect.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
					object inkCurrentMode = this.InkCurrentMode;
					if (Operators.ConditionalCompareObjectEqual(inkCurrentMode, string.Empty, false))
					{
						this.InkLayerActive.IsEnabled = false;
						this.InkLayerActive.IsHitTestVisible = false;
					}
					else if (Operators.ConditionalCompareObjectEqual(inkCurrentMode, "0", false))
					{
						this.btnInkModeDraw.Background = new SolidColorBrush(Colors.LightSlateGray);
						this.InkLayerActive.EditingMode = InkCanvasEditingMode.Ink;
						this.InkLayerActive.DefaultDrawingAttributes.IsHighlighter = false;
						this.InkLayerActive.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
						this.InkLayerActive.IsEnabled = true;
						this.InkLayerActive.IsHitTestVisible = true;
					}
					else if (Operators.ConditionalCompareObjectEqual(inkCurrentMode, "1", false))
					{
						this.btnInkModeErase.Background = new SolidColorBrush(Colors.LightSlateGray);
						this.InkLayerActive.EditingMode = InkCanvasEditingMode.EraseByStroke;
						this.InkLayerActive.DefaultDrawingAttributes.IsHighlighter = false;
						this.InkLayerActive.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
						this.InkLayerActive.IsEnabled = true;
						this.InkLayerActive.IsHitTestVisible = true;
					}
					else if (Operators.ConditionalCompareObjectEqual(inkCurrentMode, "2", false))
					{
						this.btnInkModeSelect.Background = new SolidColorBrush(Colors.LightSlateGray);
						this.InkLayerActive.EditingMode = InkCanvasEditingMode.Select;
						this.InkLayerActive.DefaultDrawingAttributes.IsHighlighter = false;
						this.InkLayerActive.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
						this.InkLayerActive.IsEnabled = true;
						this.InkLayerActive.IsHitTestVisible = true;
					}
					else if (Operators.ConditionalCompareObjectEqual(inkCurrentMode, "3", false))
					{
						this.btnInkModeLight.Background = new SolidColorBrush(Colors.LightSlateGray);
						this.InkLayerActive.EditingMode = InkCanvasEditingMode.Ink;
						this.InkLayerActive.DefaultDrawingAttributes.IsHighlighter = true;
						this.InkLayerActive.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
						this.InkLayerActive.IsEnabled = true;
						this.InkLayerActive.IsHitTestVisible = true;
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00016CD0 File Offset: 0x00014ED0
		private void ChangeInkColor(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;
				if (button != null && button.Tag != null)
				{
					string text = button.Tag.ToString();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 856466825U)
					{
						if (num != 806133968U)
						{
							if (num != 822911587U)
							{
								if (num == 856466825U)
								{
									if (Operators.CompareString(text, "6", false) == 0)
									{
										this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Purple;
									}
								}
							}
							else if (Operators.CompareString(text, "4", false) == 0)
							{
								this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Green;
							}
						}
						else if (Operators.CompareString(text, "5", false) == 0)
						{
							this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Orange;
						}
					}
					else if (num <= 890022063U)
					{
						if (num != 873244444U)
						{
							if (num == 890022063U)
							{
								if (Operators.CompareString(text, "0", false) == 0)
								{
									this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Black;
								}
							}
						}
						else if (Operators.CompareString(text, "1", false) == 0)
						{
							this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Red;
						}
					}
					else if (num != 906799682U)
					{
						if (num == 923577301U)
						{
							if (Operators.CompareString(text, "2", false) == 0)
							{
								this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Yellow;
							}
						}
					}
					else if (Operators.CompareString(text, "3", false) == 0)
					{
						this.InkLayerActive.DefaultDrawingAttributes.Color = Colors.Blue;
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00016ED8 File Offset: 0x000150D8
		private void ChangeInkSize(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				if (this.InkLayerActive != null)
				{
					this.InkLayerActive.DefaultDrawingAttributes.Height = e.NewValue;
					this.InkLayerActive.DefaultDrawingAttributes.Width = e.NewValue;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00016F3C File Offset: 0x0001513C
		private void RemoveUserInk()
		{
			this.MapVC.Ink.Strokes.Clear();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00016F54 File Offset: 0x00015154
		private void SetActivateInkLayer(object sender, RoutedEventArgs e)
		{
			if (this.Map_Current != null)
			{
				try
				{
					if (e.Source != null && e.Source.GetType() == typeof(System.Windows.Controls.Button))
					{
						System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;
						if (button != null && button.Tag != null)
						{
							if (Operators.CompareString(button.Tag.ToString(), "2", false) == 0)
							{
								base.Dispatcher.Invoke(delegate()
								{
									this.lblActiveLayerName.Text = "Active: Personal Canvas";
								});
								this.pnlLayer2.Background = new SolidColorBrush(Colors.SlateGray);
								this.txtLayer2.Foreground = new SolidColorBrush(Colors.White);
								this.txtLayer2.FontSize = 14.0;
								this.txtLayer2.FontWeight = FontWeights.SemiBold;
								this.MapVC.Ink.IsEnabled = this.InkLayerActive.IsEnabled;
								this.MapVC.Ink.IsHitTestVisible = this.InkLayerActive.IsHitTestVisible;
								this.MapVC.Ink.EditingMode = this.InkLayerActive.EditingMode;
								this.MapVC.Ink.DefaultDrawingAttributes.IsHighlighter = this.InkLayerActive.DefaultDrawingAttributes.IsHighlighter;
								this.MapVC.Ink.DefaultDrawingAttributes.StylusTip = this.InkLayerActive.DefaultDrawingAttributes.StylusTip;
								this.MapVC.Ink.DefaultDrawingAttributes.Color = this.InkLayerActive.DefaultDrawingAttributes.Color;
								this.MapVC.Ink.DefaultDrawingAttributes.Height = this.InkLayerActive.DefaultDrawingAttributes.Height;
								this.MapVC.Ink.DefaultDrawingAttributes.Width = this.InkLayerActive.DefaultDrawingAttributes.Width;
								this.InkLayerActive = this.MapVC.Ink;
								List<RoomDictionaryItem> list = (from x in this.MyACSHelper.DictionaryOfRooms
								where x.Value.ParticipantDictionary.ContainsKey(this.MyACSHelper.SessionGUID)
								select x).Select((MainWindow._Closure$__.$I252-2 == null) ? (MainWindow._Closure$__.$I252-2 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I252-2).ToList<RoomDictionaryItem>();
								if (list == null)
								{
									goto IL_613;
								}
								try
								{
									foreach (RoomDictionaryItem roomDictionaryItem in list)
									{
										RoomParticipantDictionaryItem value = roomDictionaryItem.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
										if (value != null)
										{
											value.LayerControl.Update(false);
											value.LayerControl.LayerCanvas.IsEnabled = false;
											value.LayerControl.LayerCanvas.IsHitTestVisible = false;
										}
									}
									goto IL_613;
								}
								finally
								{
									List<RoomDictionaryItem>.Enumerator enumerator;
									((IDisposable)enumerator).Dispose();
								}
							}
							MainWindow._Closure$__252-0 CS$<>8__locals1 = new MainWindow._Closure$__252-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							CS$<>8__locals1.$VB$Local_RoomGUID = Guid.Empty;
							if (Guid.TryParse(button.Tag.ToString(), out CS$<>8__locals1.$VB$Local_RoomGUID))
							{
								MainWindow._Closure$__252-1 CS$<>8__locals2 = new MainWindow._Closure$__252-1(CS$<>8__locals2);
								CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
								CS$<>8__locals2.$VB$Local_RoomItemActive = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RoomGUID).Value;
								if (CS$<>8__locals2.$VB$Local_RoomItemActive != null)
								{
									RoomParticipantDictionaryItem value2 = CS$<>8__locals2.$VB$Local_RoomItemActive.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
									if (value2 != null)
									{
										base.Dispatcher.Invoke(delegate()
										{
											CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Me.lblActiveLayerName.Text = "Active: " + CS$<>8__locals2.$VB$Local_RoomItemActive.Room.Name;
										});
										value2.LayerControl.Update(true);
										value2.LayerControl.LayerCanvas.IsEnabled = this.InkLayerActive.IsEnabled;
										value2.LayerControl.LayerCanvas.IsHitTestVisible = this.InkLayerActive.IsHitTestVisible;
										value2.LayerControl.LayerCanvas.EditingMode = this.InkLayerActive.EditingMode;
										value2.LayerControl.LayerCanvas.DefaultDrawingAttributes.IsHighlighter = this.InkLayerActive.DefaultDrawingAttributes.IsHighlighter;
										value2.LayerControl.LayerCanvas.DefaultDrawingAttributes.StylusTip = this.InkLayerActive.DefaultDrawingAttributes.StylusTip;
										value2.LayerControl.LayerCanvas.DefaultDrawingAttributes.Color = this.InkLayerActive.DefaultDrawingAttributes.Color;
										value2.LayerControl.LayerCanvas.DefaultDrawingAttributes.Height = this.InkLayerActive.DefaultDrawingAttributes.Height;
										value2.LayerControl.LayerCanvas.DefaultDrawingAttributes.Width = this.InkLayerActive.DefaultDrawingAttributes.Width;
										this.InkLayerActive = value2.LayerControl.LayerCanvas;
									}
								}
								List<RoomDictionaryItem> list2 = (from x in this.MyACSHelper.DictionaryOfRooms
								where x.Value.ParticipantDictionary.ContainsKey(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Me.MyACSHelper.SessionGUID) & !(x.Key == CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RoomGUID)
								select x).Select((MainWindow._Closure$__.$I252-8 == null) ? (MainWindow._Closure$__.$I252-8 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I252-8).ToList<RoomDictionaryItem>();
								if (list2 != null)
								{
									try
									{
										foreach (RoomDictionaryItem roomDictionaryItem2 in list2)
										{
											RoomParticipantDictionaryItem value3 = roomDictionaryItem2.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
											if (value3 != null)
											{
												value3.LayerControl.Update(false);
												value3.LayerControl.LayerCanvas.IsEnabled = false;
												value3.LayerControl.LayerCanvas.IsHitTestVisible = false;
											}
										}
									}
									finally
									{
										List<RoomDictionaryItem>.Enumerator enumerator2;
										((IDisposable)enumerator2).Dispose();
									}
								}
								this.pnlLayer2.Background = new SolidColorBrush(Colors.White);
								this.chkLayer2.IsEnabled = true;
								this.txtLayer2.Foreground = new SolidColorBrush(Colors.Black);
								this.txtLayer2.FontSize = 12.0;
								this.txtLayer2.FontWeight = FontWeights.Normal;
								this.MapVC.Ink.IsEnabled = false;
								this.MapVC.Ink.IsHitTestVisible = false;
							}
						}
					}
					IL_613:;
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000175D4 File Offset: 0x000157D4
		private void HandleOnInkCanvasStyusDown(object sender, StylusDownEventArgs e)
		{
			if (!MySettings.Default.UseTouchForInk && e.StylusDevice != null && e.StylusDevice.TabletDevice.Type == TabletDeviceType.Touch)
			{
				e.Handled = true;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00017604 File Offset: 0x00015804
		private void HandleOnInkCanvasMosueDown(object sender, MouseButtonEventArgs e)
		{
			if (!MySettings.Default.UseTouchForInk && e.StylusDevice != null && e.StylusDevice.TabletDevice.Type == TabletDeviceType.Touch)
			{
				e.Handled = true;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00017634 File Offset: 0x00015834
		private void HandleStrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
		{
			if (!(this.Status_Source == Enums.SourceStatus.Offline | this.Status_Source == Enums.SourceStatus.Recording))
			{
				this.LoadSettings();
				if (e.Added != null)
				{
					try
					{
						try
						{
							foreach (Stroke stroke in e.Added)
							{
								bool flag = false;
								try
								{
									List<Guid> list = stroke.GetPropertyDataIds().ToList<Guid>();
									if (list != null && list.Count != 0)
									{
										try
										{
											foreach (Guid propertyDataId in list)
											{
												if (Operators.CompareString(stroke.GetPropertyData(propertyDataId).ToString().ToLower(), "creatorguid", false) == 0)
												{
													flag = true;
													break;
												}
											}
										}
										finally
										{
											List<Guid>.Enumerator enumerator2;
											((IDisposable)enumerator2).Dispose();
										}
									}
								}
								catch (Exception ex)
								{
									flag = false;
								}
								if (!flag)
								{
									Guid guid = Guid.NewGuid();
									stroke.AddPropertyData(this.Comm_RelayClientGUID, "creatorguid");
									stroke.AddPropertyData(guid, "strokeguid");
									string text = this.CreateStrokeCreationCommand(guid, ref stroke);
									if (!string.IsNullOrEmpty(text))
									{
										this.SocketClientRelay.OutQueue.Enqueue(new ResponsePackage
										{
											Type = ResponseType.Text,
											Content = text
										});
									}
								}
							}
						}
						finally
						{
							IEnumerator<Stroke> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					catch (Exception ex2)
					{
					}
				}
				if (e.Removed != null)
				{
					try
					{
						try
						{
							foreach (Stroke stroke2 in e.Removed)
							{
								bool flag2 = false;
								Guid guid2 = Guid.Empty;
								try
								{
									List<Guid> list2 = stroke2.GetPropertyDataIds().ToList<Guid>();
									if (list2 != null && list2.Count != 0)
									{
										try
										{
											foreach (Guid guid3 in list2)
											{
												string left = stroke2.GetPropertyData(guid3).ToString().ToLower();
												if (Operators.CompareString(left, "strokeguid", false) != 0)
												{
													if (Operators.CompareString(left, "deleterguid", false) == 0)
													{
														flag2 = true;
													}
												}
												else
												{
													guid2 = guid3;
												}
											}
										}
										finally
										{
											List<Guid>.Enumerator enumerator4;
											((IDisposable)enumerator4).Dispose();
										}
									}
								}
								catch (Exception ex3)
								{
									flag2 = false;
								}
								if (!flag2 & !guid2.Equals(Guid.Empty))
								{
									string text2 = "strokedelete<ath_sep>" + guid2.ToString();
									if (!string.IsNullOrEmpty(text2))
									{
										this.SocketClientRelay.OutQueue.Enqueue(new ResponsePackage
										{
											Type = ResponseType.Text,
											Content = text2
										});
									}
								}
							}
						}
						finally
						{
							IEnumerator<Stroke> enumerator3;
							if (enumerator3 != null)
							{
								enumerator3.Dispose();
							}
						}
					}
					catch (Exception ex4)
					{
					}
				}
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000179A4 File Offset: 0x00015BA4
		private string CreateStrokeCreationCommand(Guid StrokeGUID, ref Stroke Stroke)
		{
			string result = string.Empty;
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
					result = "strokecreate<ath_sep>" + StrokeGUID.ToString() + "<ath_sep>" + text;
				}
				memoryStream.Close();
				memoryStream.Dispose();
			}
			catch (Exception ex)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060003D1 RID: 977 RVA: 0x00017A4C File Offset: 0x00015C4C
		// (remove) Token: 0x060003D2 RID: 978 RVA: 0x00017A84 File Offset: 0x00015C84
		public event MainWindow.OnOrbatLocateLocationSelectedEventHandler OnOrbatLocateLocationSelected;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060003D3 RID: 979 RVA: 0x00017ABC File Offset: 0x00015CBC
		// (remove) Token: 0x060003D4 RID: 980 RVA: 0x00017AF4 File Offset: 0x00015CF4
		public event MainWindow.OnOrbatAnchorPosRequestedEventHandler OnOrbatAnchorPosRequested;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060003D5 RID: 981 RVA: 0x00017B2C File Offset: 0x00015D2C
		// (remove) Token: 0x060003D6 RID: 982 RVA: 0x00017B64 File Offset: 0x00015D64
		public event MainWindow.OnOrbatAnchorPosRetrievedEventHandler OnOrbatAnchorPosRetrieved;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060003D7 RID: 983 RVA: 0x00017B9C File Offset: 0x00015D9C
		// (remove) Token: 0x060003D8 RID: 984 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public event MainWindow.OnOrbatLocateAnchorEventHandler OnOrbatLocateAnchor;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060003D9 RID: 985 RVA: 0x00017C0C File Offset: 0x00015E0C
		// (remove) Token: 0x060003DA RID: 986 RVA: 0x00017C44 File Offset: 0x00015E44
		public event MainWindow.OnOrbatCreateAnchorEventHandler OnOrbatCreateAnchor;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060003DB RID: 987 RVA: 0x00017C7C File Offset: 0x00015E7C
		// (remove) Token: 0x060003DC RID: 988 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public event MainWindow.OnOrbatRemoveAnchorEventHandler OnOrbatRemoveAnchor;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060003DD RID: 989 RVA: 0x00017CEC File Offset: 0x00015EEC
		// (remove) Token: 0x060003DE RID: 990 RVA: 0x00017D24 File Offset: 0x00015F24
		public event MainWindow.OnOrbatTrackAnchorEventHandler OnOrbatTrackAnchor;

		// Token: 0x060003DF RID: 991 RVA: 0x00017D5C File Offset: 0x00015F5C
		private void HandleOrbatLocateLocationSelected(MapLocation SelectedLocation)
		{
			MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
			if (user_Initiated_FollowChangeEvent != null)
			{
				user_Initiated_FollowChangeEvent(false);
			}
			Common.ConvertPos(this.Map_Current.WorldSize, SelectedLocation.PosX, SelectedLocation.PosY, ref SelectedLocation.CanvasX, ref SelectedLocation.CanvasY);
			this.MapVC.SetHorizontalOffset(SelectedLocation.CanvasX * this.ZoomFactor - this.MapScroll.ViewportWidth / 2.0);
			this.MapVC.SetVerticalOffset(SelectedLocation.CanvasY * this.ZoomFactor - this.MapScroll.ViewportHeight / 2.0);
			this.MapVC.StartUpdate();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00017E0C File Offset: 0x0001600C
		private void HandleOrbatLocateAnchor(MapAnchor SelectedAnchor)
		{
			MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
			if (user_Initiated_FollowChangeEvent != null)
			{
				user_Initiated_FollowChangeEvent(false);
			}
			if (SelectedAnchor.CanvasX == -1.0)
			{
				Common.ConvertPos(this.Map_Current.WorldSize, Conversions.ToString(SelectedAnchor.PosX), Conversions.ToString(SelectedAnchor.PosY), ref SelectedAnchor.CanvasX, ref SelectedAnchor.CanvasY);
			}
			this.MapVC.SetHorizontalOffset(SelectedAnchor.CanvasX * this.ZoomFactor - this.MapScroll.ViewportWidth / 2.0);
			this.MapVC.SetVerticalOffset(SelectedAnchor.CanvasY * this.ZoomFactor - this.MapScroll.ViewportHeight / 2.0);
			this.MapVC.StartUpdate();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00017ED4 File Offset: 0x000160D4
		private void HandleOrbatCreateAnchor(MapAnchor NewAnchor)
		{
			if (this.Map_Current != null && NewAnchor != null && (NewAnchor.PosX != -1.0 & NewAnchor.PosY != -1.0))
			{
				if (NewAnchor.CanvasX == -1.0)
				{
					Common.ConvertPos(this.Map_Current.WorldSize, NewAnchor.PosX.ToString(), NewAnchor.PosY.ToString(), ref NewAnchor.CanvasX, ref NewAnchor.CanvasY);
				}
				this.GenerateAnchorShape(ref NewAnchor);
				if (NewAnchor.Permanent)
				{
					List<MapAnchor> list = new List<MapAnchor>();
					this.ParseAnchorsFiles(ref this.Map_Current, ref list);
					list.Add(NewAnchor);
					try
					{
						string text = JsonConvert.SerializeObject(list);
						if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.Map_Current.Folder))
						{
							File.WriteAllText(this.Map_Current.Folder + "\\Anchors.txt", text);
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00017FEC File Offset: 0x000161EC
		private void HandleOrbatRemoveAnchor(MapAnchor Anchor)
		{
			checked
			{
				if (Anchor != null && this.MapVC.Anchors.Children.Count != 0)
				{
					if (this.TrackingAnchor != null && (Operators.CompareString(this.TrackingAnchor.Name, Anchor.Name, false) == 0 & Operators.CompareString(this.TrackingAnchor.Grid, Anchor.Grid, false) == 0 & this.TrackingAnchor.PosX == Anchor.PosX & this.TrackingAnchor.PosY == Anchor.PosY))
					{
						this.TrackingAnchor = null;
						this.TrackingAnchorLine.Visibility = Visibility.Collapsed;
					}
					for (int i = this.MapVC.Anchors.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.MapVC.Anchors.Children[i].GetType() == typeof(anchor))
						{
							anchor anchor = this.MapVC.Anchors.Children[i] as anchor;
							if (anchor != null && (Operators.CompareString(anchor.MapAnchor.Name, Anchor.Name, false) == 0 & Operators.CompareString(anchor.MapAnchor.Grid, Anchor.Grid, false) == 0 & anchor.MapAnchor.PosX == Anchor.PosX & anchor.MapAnchor.PosY == Anchor.PosY))
							{
								this.MapVC.Anchors.Children.RemoveAt(i);
							}
						}
					}
					if (Anchor.Permanent)
					{
						List<MapAnchor> list = new List<MapAnchor>();
						this.ParseAnchorsFiles(ref this.Map_Current, ref list);
						if (list != null && list.Count != 0)
						{
							for (int j = list.Count - 1; j >= 0; j += -1)
							{
								if (Operators.CompareString(list[j].Name, Anchor.Name, false) == 0 & Operators.CompareString(list[j].Grid, Anchor.Grid, false) == 0 & list[j].PosX == Anchor.PosX & list[j].PosY == Anchor.PosY)
								{
									list.RemoveAt(j);
								}
							}
							try
							{
								string text = JsonConvert.SerializeObject(list);
								if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.Map_Current.Folder))
								{
									File.WriteAllText(this.Map_Current.Folder + "\\Anchors.txt", text);
								}
							}
							catch (Exception ex)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00018290 File Offset: 0x00016490
		private void HandleOrbatTrackAnchor(MapAnchor Anchor)
		{
			if (this.TrackingAnchor == null)
			{
				this.TrackingAnchor = Anchor;
			}
			else if (Operators.CompareString(this.TrackingAnchor.Name, Anchor.Name, false) == 0 & Operators.CompareString(this.TrackingAnchor.Grid, Anchor.Grid, false) == 0 & this.TrackingAnchor.PosX == Anchor.PosX & this.TrackingAnchor.PosY == Anchor.PosY)
			{
				this.TrackingAnchor = null;
				this.TrackingAnchorLine.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.TrackingAnchor = Anchor;
			}
			if (this.TrackingAnchor != null && this.Map_Current != null)
			{
				Unit followedUnit = this.GetFollowedUnit();
				if (followedUnit != null)
				{
					if (this.TrackingAnchor.CanvasX == -1.0)
					{
						Common.ConvertPos(this.Map_Current.WorldSize, Conversions.ToString(this.TrackingAnchor.PosX), Conversions.ToString(this.TrackingAnchor.PosY), ref this.TrackingAnchor.CanvasX, ref this.TrackingAnchor.CanvasY);
					}
					if (followedUnit.CanvasX == -1.0)
					{
						Common.ConvertPos(this.Map_Current.WorldSize, followedUnit.PosX, followedUnit.PosY, ref followedUnit.CanvasX, ref followedUnit.CanvasY);
					}
					Point point = new Point(followedUnit.CanvasX, followedUnit.CanvasY);
					Point point2 = new Point(this.TrackingAnchor.CanvasX, this.TrackingAnchor.CanvasY);
					Color red = Colors.Red;
					TextBlock lblTrackingAnchor = this.lblTrackingAnchor;
					this.UpdateTrackingLine(ref this.TrackingAnchorLine, ref point, ref point2, ref red, ref lblTrackingAnchor);
					this.lblTrackingAnchor = lblTrackingAnchor;
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001843B File Offset: 0x0001663B
		private void HandleOrbatClickAnchor()
		{
			this.Tool.Current = Enums.ActiveToolEnum.MapAnchor;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001844C File Offset: 0x0001664C
		private void HandleLayerToggle(object sender, RoutedEventArgs e)
		{
			if (e.Source != null)
			{
				System.Windows.Controls.CheckBox checkBox = e.Source as System.Windows.Controls.CheckBox;
				if (checkBox != null && checkBox.Tag != null)
				{
					string text = checkBox.Tag.ToString();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 856466825U)
					{
						if (num <= 822911587U)
						{
							if (num != 806133968U)
							{
								if (num == 822911587U)
								{
									if (Operators.CompareString(text, "4", false) == 0)
									{
										UIElement units = this.MapVC.Units;
										bool? isChecked = checkBox.IsChecked;
										units.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
										goto IL_45C;
									}
								}
							}
							else if (Operators.CompareString(text, "5", false) == 0)
							{
								UIElement groups = this.MapVC.Groups;
								bool? isChecked = checkBox.IsChecked;
								groups.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
								goto IL_45C;
							}
						}
						else if (num != 839689206U)
						{
							if (num == 856466825U)
							{
								if (Operators.CompareString(text, "6", false) == 0)
								{
									UIElement anchors = this.MapVC.Anchors;
									bool? isChecked = checkBox.IsChecked;
									anchors.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
									goto IL_45C;
								}
							}
						}
						else if (Operators.CompareString(text, "7", false) == 0)
						{
							UIElement vehicles = this.MapVC.Vehicles;
							bool? isChecked = checkBox.IsChecked;
							vehicles.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
							goto IL_45C;
						}
					}
					else if (num <= 890022063U)
					{
						if (num != 873244444U)
						{
							if (num == 890022063U)
							{
								if (Operators.CompareString(text, "0", false) == 0)
								{
									UIElement locations = this.MapVC.Locations;
									bool? isChecked = checkBox.IsChecked;
									locations.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
									goto IL_45C;
								}
							}
						}
						else if (Operators.CompareString(text, "1", false) == 0)
						{
							UIElement lines = this.MapVC.Lines;
							bool? isChecked = checkBox.IsChecked;
							lines.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
							goto IL_45C;
						}
					}
					else if (num != 906799682U)
					{
						if (num == 923577301U)
						{
							if (Operators.CompareString(text, "2", false) == 0)
							{
								UIElement ink = this.MapVC.Ink;
								bool? isChecked = checkBox.IsChecked;
								ink.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
								goto IL_45C;
							}
						}
					}
					else if (Operators.CompareString(text, "3", false) == 0)
					{
						UIElement markers = this.MapVC.Markers;
						bool? isChecked = checkBox.IsChecked;
						markers.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
						goto IL_45C;
					}
					Guid RoomGUID = Guid.Empty;
					if (Guid.TryParse(checkBox.Tag.ToString(), out RoomGUID))
					{
						RoomDictionaryItem value = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
						if (value != null)
						{
							RoomParticipantDictionaryItem value2 = value.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
							if (value2 != null && value2.LayerControl != null && value2.LayerControl.LayerCanvas != null)
							{
								UIElement layerCanvas = value2.LayerControl.LayerCanvas;
								bool? isChecked = checkBox.IsChecked;
								layerCanvas.Visibility = ((-((((isChecked != null) ? new bool?(!isChecked.GetValueOrDefault()) : isChecked).Value > false) ? Visibility.Hidden : Visibility.Visible)) ? Visibility.Hidden : Visibility.Visible);
							}
						}
					}
				}
				IL_45C:;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000188B8 File Offset: 0x00016AB8
		public void ClearLocations()
		{
			this.pnlLocationOptions.Visibility = Visibility.Collapsed;
			if (this.pnlLocationOptions.Parent != null)
			{
				(this.pnlLocationOptions.Parent as StackPanel).Children.Remove(this.pnlLocationOptions);
			}
			this.pnlMap.Children.Add(this.pnlLocationOptions);
			checked
			{
				if (this.pnlMap.Children != null && this.pnlMap.Children.Count != 0)
				{
					for (int i = this.pnlMap.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.pnlMap.Children[i].GetType() == typeof(StackPanel))
						{
							bool flag = false;
							StackPanel stackPanel = this.pnlMap.Children[i] as StackPanel;
							if (stackPanel != null && stackPanel.Children.Count != 0 && stackPanel.Children[0].GetType() == typeof(System.Windows.Controls.Label))
							{
								flag = true;
								System.Windows.Controls.Label label = stackPanel.Children[0] as System.Windows.Controls.Label;
								if (label != null)
								{
									label.GotFocus -= this.HandleLocationSelection;
									label.MouseDown -= new MouseButtonEventHandler(this.HandleLocationClick);
								}
							}
							if (flag)
							{
								this.pnlMap.Children.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00018A24 File Offset: 0x00016C24
		public void DisplayLocationsInOrbat()
		{
			this.ClearLocations();
			this.Map_Current_Locations = this.SortLocations(this.Map_Current_Locations);
			if (this.Map_Current_Locations != null && this.Map_Current_Locations.Count != 0)
			{
				try
				{
					foreach (MapLocation mapLocation in this.Map_Current_Locations)
					{
						this.CreateLocationLabel(ref mapLocation);
					}
				}
				finally
				{
					List<MapLocation>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00018AA8 File Offset: 0x00016CA8
		public void ClearMissionLocations()
		{
			this.myLocationsMission = new List<MapLocation>();
			if (this.mySelectedLocation != null && this.mySelectedLocation.Tag != null && this.mySelectedLocation.Tag.GetType() == typeof(MapLocation))
			{
				MapLocation mapLocation = this.mySelectedLocation.Tag as MapLocation;
				if (mapLocation != null && mapLocation.Scope == 1)
				{
					this.pnlLocationOptions.Visibility = Visibility.Collapsed;
					if (this.pnlLocationOptions.Parent != null)
					{
						(this.pnlLocationOptions.Parent as StackPanel).Children.Remove(this.pnlLocationOptions);
					}
					this.pnlMap.Children.Add(this.pnlLocationOptions);
				}
			}
			checked
			{
				if (this.pnlMap.Children != null && this.pnlMap.Children.Count != 0)
				{
					for (int i = this.pnlMap.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.pnlMap.Children[i].GetType() == typeof(StackPanel))
						{
							bool flag = false;
							bool flag2 = false;
							StackPanel stackPanel = this.pnlMap.Children[i] as StackPanel;
							if (stackPanel != null && stackPanel.Children.Count != 0 && stackPanel.Children[0].GetType() == typeof(System.Windows.Controls.Label))
							{
								flag2 = true;
								System.Windows.Controls.Label label = stackPanel.Children[0] as System.Windows.Controls.Label;
								if (label != null && label.Tag != null && label.Tag.GetType() == typeof(MapLocation))
								{
									MapLocation mapLocation2 = label.Tag as MapLocation;
									if (mapLocation2 != null && mapLocation2.Scope == 1)
									{
										flag = true;
										label.GotFocus -= this.HandleLocationSelection;
										label.MouseDown -= new MouseButtonEventHandler(this.HandleLocationClick);
									}
								}
							}
							if (flag && flag2)
							{
								this.pnlMap.Children.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00018CD8 File Offset: 0x00016ED8
		public void DisplayMissionLocations(List<MapLocation> Locations)
		{
			this.myLocationsMission = this.SortLocations(Locations);
			if (this.myLocationsMission != null && this.myLocationsMission.Count != 0)
			{
				try
				{
					foreach (MapLocation mapLocation in this.myLocationsMission)
					{
						if (!this.UpdateLocationLabel(ref mapLocation))
						{
							this.CreateLocationLabel(ref mapLocation);
						}
					}
				}
				finally
				{
					List<MapLocation>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			this.RemoveLocationsStale(ref this.myLocationsMission);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00018D64 File Offset: 0x00016F64
		private bool UpdateLocationLabel(ref MapLocation Location)
		{
			bool result = false;
			checked
			{
				if (this.pnlMap.Children != null && this.pnlMap.Children.Count != 0)
				{
					for (int i = this.pnlMap.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.pnlMap.Children[i].GetType() == typeof(StackPanel))
						{
							StackPanel stackPanel = this.pnlMap.Children[i] as StackPanel;
							if (stackPanel != null && stackPanel.Children.Count != 0 && stackPanel.Children[0].GetType() == typeof(System.Windows.Controls.Label))
							{
								System.Windows.Controls.Label label = stackPanel.Children[0] as System.Windows.Controls.Label;
								if (label != null && label.Tag != null && label.Tag.GetType() == typeof(MapLocation))
								{
									MapLocation mapLocation = label.Tag as MapLocation;
									if (mapLocation != null && mapLocation.Scope == 1 && Operators.CompareString(mapLocation.ID, Location.ID, false) == 0)
									{
										label.Content = Location.Text;
										label.Tag = Location;
										if (stackPanel.Children.Count == 3)
										{
											TextBlock textBlock = stackPanel.Children[1] as TextBlock;
											if (textBlock != null)
											{
												string empty = string.Empty;
												Common.ConvertPosToGrid(ref this.Map_Current, Location.PosX, Location.PosY, ref empty);
												textBlock.Text = "GRID: " + empty;
											}
											TextBlock textBlock2 = stackPanel.Children[2] as TextBlock;
											if (textBlock2 != null)
											{
												textBlock2.Text = "X:" + Location.PosX + ", Y:" + Location.PosY;
											}
										}
										result = true;
										break;
									}
								}
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00018F6C File Offset: 0x0001716C
		private void CreateLocationLabel(ref MapLocation Location)
		{
			if (Location != null)
			{
				StackPanel stackPanel = new StackPanel();
				stackPanel.Focusable = false;
				stackPanel.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				System.Windows.Controls.Label label = new System.Windows.Controls.Label();
				label.FontSize = 14.0;
				label.FontWeight = FontWeights.Bold;
				label.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
				label.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
				label.Content = Location.Text.Trim();
				label.Tag = Location;
				label.IsHitTestVisible = true;
				label.IsTabStop = true;
				label.Focusable = true;
				label.Cursor = System.Windows.Input.Cursors.Hand;
				label.GotFocus += this.HandleLocationSelection;
				label.MouseDown += new MouseButtonEventHandler(this.HandleLocationClick);
				stackPanel.Children.Add(label);
				string empty = string.Empty;
				Common.ConvertPosToGrid(ref this.Map_Current, Location.PosX, Location.PosY, ref empty);
				TextBlock textBlock = new TextBlock();
				textBlock.FontSize = 12.0;
				textBlock.Text = "GRID: " + empty;
				textBlock.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				stackPanel.Children.Add(textBlock);
				TextBlock textBlock2 = new TextBlock();
				textBlock2.FontSize = 12.0;
				textBlock2.Text = "X:" + Location.PosX + ", Y:" + Location.PosY;
				textBlock2.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				stackPanel.Children.Add(textBlock2);
				this.pnlMap.Children.Add(stackPanel);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000191BC File Offset: 0x000173BC
		private void RemoveLocationsStale(ref List<MapLocation> Locations)
		{
			checked
			{
				if ((this.pnlMap.Children != null & Locations != null) && this.pnlMap.Children.Count != 0)
				{
					for (int i = this.pnlMap.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.pnlMap.Children[i].GetType() == typeof(StackPanel))
						{
							StackPanel stackPanel = this.pnlMap.Children[i] as StackPanel;
							if (stackPanel != null && stackPanel.Children.Count != 0 && stackPanel.Children[0].GetType() == typeof(System.Windows.Controls.Label))
							{
								System.Windows.Controls.Label label = stackPanel.Children[0] as System.Windows.Controls.Label;
								if (label != null && label.Tag != null && label.Tag.GetType() == typeof(MapLocation))
								{
									MapLocation mapLocation = label.Tag as MapLocation;
									if (mapLocation != null && mapLocation.Scope == 1)
									{
										bool flag = false;
										try
										{
											foreach (MapLocation mapLocation2 in Locations)
											{
												if (Operators.CompareString(mapLocation.ID, mapLocation2.ID, false) == 0)
												{
													flag = true;
													break;
												}
											}
										}
										finally
										{
											List<MapLocation>.Enumerator enumerator;
											((IDisposable)enumerator).Dispose();
										}
										if (!flag)
										{
											label.GotFocus -= this.HandleLocationSelection;
											label.MouseDown -= new MouseButtonEventHandler(this.HandleLocationClick);
											this.pnlMap.Children.RemoveAt(i);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00019390 File Offset: 0x00017590
		private List<MapLocation> SortLocations(List<MapLocation> Locations)
		{
			List<MapLocation> result = new List<MapLocation>();
			if (Locations != null && Locations.Count != 0)
			{
				try
				{
					foreach (MapLocation mapLocation in Locations)
					{
						if (string.IsNullOrEmpty(mapLocation.Text))
						{
							mapLocation.Text = mapLocation.Type;
						}
					}
				}
				finally
				{
					List<MapLocation>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				result = Locations.OrderBy((MainWindow._Closure$__.$I309-0 == null) ? (MainWindow._Closure$__.$I309-0 = ((MapLocation x) => x.Text)) : MainWindow._Closure$__.$I309-0).ToList<MapLocation>();
			}
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00019434 File Offset: 0x00017634
		private void HandleLocationClick(object Sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Label label = Sender as System.Windows.Controls.Label;
			if (label != null)
			{
				label.Focus();
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00019454 File Offset: 0x00017654
		private void HandleLocationSelection(object sender, RoutedEventArgs e)
		{
			this.mySelectedLocation = (sender as System.Windows.Controls.Label);
			if (this.mySelectedLocation != null)
			{
				if ((this.pnlLocationOptions.Parent != null & this.mySelectedLocation.Parent != null) && this.pnlLocationOptions.Parent.Equals(this.mySelectedLocation.Parent))
				{
					if (this.pnlLocationOptions.Visibility == Visibility.Collapsed)
					{
						this.pnlLocationOptions.Visibility = Visibility.Visible;
						return;
					}
					this.pnlLocationOptions.Visibility = Visibility.Collapsed;
					return;
				}
				else
				{
					if (this.pnlLocationOptions.Parent != null)
					{
						(this.pnlLocationOptions.Parent as StackPanel).Children.Remove(this.pnlLocationOptions);
					}
					if (this.mySelectedLocation.Parent != null)
					{
						StackPanel stackPanel = this.mySelectedLocation.Parent as StackPanel;
						if (stackPanel != null)
						{
							stackPanel.Children.Add(this.pnlLocationOptions);
						}
					}
					this.pnlLocationOptions.Visibility = Visibility.Visible;
					this.pnlLocationOptions.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00019580 File Offset: 0x00017780
		private void DoLocationLocate()
		{
			if (this.mySelectedLocation != null && this.mySelectedLocation.Tag != null)
			{
				MapLocation mapLocation = this.mySelectedLocation.Tag as MapLocation;
				if (mapLocation != null)
				{
					MainWindow.OnOrbatLocateLocationSelectedEventHandler onOrbatLocateLocationSelectedEvent = this.OnOrbatLocateLocationSelectedEvent;
					if (onOrbatLocateLocationSelectedEvent != null)
					{
						onOrbatLocateLocationSelectedEvent(mapLocation);
					}
				}
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000195C8 File Offset: 0x000177C8
		public void ClearAllAnchors()
		{
			this.pnlAnchorOptions.Visibility = Visibility.Collapsed;
			if (this.pnlAnchorOptions.Parent != null)
			{
				(this.pnlAnchorOptions.Parent as StackPanel).Children.Remove(this.pnlAnchorOptions);
			}
			this.pnlAnchor.Children.Add(this.pnlAnchorOptions);
			checked
			{
				if (this.pnlAnchor.Children != null && this.pnlAnchor.Children.Count != 0)
				{
					for (int i = this.pnlAnchor.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.pnlAnchor.Children[i].GetType() == typeof(StackPanel))
						{
							bool flag = false;
							StackPanel stackPanel = this.pnlAnchor.Children[i] as StackPanel;
							if (stackPanel != null && stackPanel.Children.Count != 0 && stackPanel.Children[0].GetType() == typeof(System.Windows.Controls.Label))
							{
								flag = true;
								System.Windows.Controls.Label label = stackPanel.Children[0] as System.Windows.Controls.Label;
								if (label != null)
								{
									label.GotFocus -= this.HandleAnchorSelection;
									label.MouseDown -= new MouseButtonEventHandler(this.HandleAnchorClick);
								}
							}
							if (flag)
							{
								this.pnlAnchor.Children.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00019734 File Offset: 0x00017934
		public void ClearMissionAnchors()
		{
			this.pnlAnchorOptions.Visibility = Visibility.Collapsed;
			if (this.pnlAnchorOptions.Parent != null)
			{
				(this.pnlAnchorOptions.Parent as StackPanel).Children.Remove(this.pnlAnchorOptions);
			}
			this.pnlAnchor.Children.Add(this.pnlAnchorOptions);
			checked
			{
				if (this.pnlAnchor.Children != null && this.pnlAnchor.Children.Count != 0)
				{
					for (int i = this.pnlAnchor.Children.Count - 1; i >= 0; i += -1)
					{
						if (this.pnlAnchor.Children[i].GetType() == typeof(StackPanel))
						{
							bool flag = false;
							bool flag2 = false;
							StackPanel stackPanel = this.pnlAnchor.Children[i] as StackPanel;
							if (stackPanel != null && stackPanel.Children.Count != 0 && stackPanel.Children[0].GetType() == typeof(System.Windows.Controls.Label))
							{
								flag = true;
								flag2 = false;
								if (stackPanel.Children.Count != 0)
								{
									System.Windows.Controls.Label label = stackPanel.Children[0] as System.Windows.Controls.Label;
									if (label != null)
									{
										if (label.Tag != null && label.Tag.GetType() == typeof(MapAnchor))
										{
											MapAnchor mapAnchor = label.Tag as MapAnchor;
											if (mapAnchor != null && !mapAnchor.Permanent)
											{
												flag2 = true;
												MainWindow.OnOrbatRemoveAnchorEventHandler onOrbatRemoveAnchorEvent = this.OnOrbatRemoveAnchorEvent;
												if (onOrbatRemoveAnchorEvent != null)
												{
													onOrbatRemoveAnchorEvent(mapAnchor);
												}
												for (int j = this.Map_Current_Anchors.Count - 1; j >= 0; j += -1)
												{
													if (Operators.CompareString(this.Map_Current_Anchors[j].Name, mapAnchor.Name, false) == 0 & Operators.CompareString(this.Map_Current_Anchors[j].Grid, mapAnchor.Grid, false) == 0 & this.Map_Current_Anchors[j].PosX == mapAnchor.PosX & this.Map_Current_Anchors[j].PosY == mapAnchor.PosY)
													{
														this.Map_Current_Anchors.RemoveAt(j);
													}
												}
											}
										}
										if (flag2)
										{
											label.GotFocus -= this.HandleAnchorSelection;
											label.MouseDown -= new MouseButtonEventHandler(this.HandleAnchorClick);
										}
									}
								}
							}
							if (flag && flag2)
							{
								this.pnlAnchor.Children.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000199E0 File Offset: 0x00017BE0
		public void DisplayAnchorsInOrbat()
		{
			this.ClearAllAnchors();
			this.Map_Current_Anchors = this.SortAnchors(this.Map_Current_Anchors);
			if (this.Map_Current_Anchors != null && this.Map_Current_Anchors.Count != 0)
			{
				try
				{
					foreach (MapAnchor mapAnchor in this.Map_Current_Anchors)
					{
						this.CreateAnchorLabel(ref mapAnchor);
					}
				}
				finally
				{
					List<MapAnchor>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00019A64 File Offset: 0x00017C64
		private void CreateAnchorLabel(ref MapAnchor Anchor)
		{
			if (Anchor != null)
			{
				StackPanel stackPanel = new StackPanel();
				stackPanel.Focusable = false;
				stackPanel.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				System.Windows.Controls.Label label = new System.Windows.Controls.Label();
				label.FontSize = 14.0;
				label.FontWeight = FontWeights.Bold;
				label.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
				label.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
				label.Content = Anchor.Name.Trim();
				label.Tag = Anchor;
				label.IsHitTestVisible = true;
				label.IsTabStop = true;
				label.Focusable = true;
				label.Cursor = System.Windows.Input.Cursors.Hand;
				label.GotFocus += this.HandleAnchorSelection;
				label.MouseDown += new MouseButtonEventHandler(this.HandleAnchorClick);
				stackPanel.Children.Add(label);
				TextBlock textBlock = new TextBlock();
				textBlock.FontSize = 12.0;
				textBlock.Text = "GRID: " + Anchor.Grid;
				textBlock.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				stackPanel.Children.Add(textBlock);
				TextBlock textBlock2 = new TextBlock();
				textBlock2.FontSize = 12.0;
				textBlock2.Text = "X:" + Anchor.PosX.ToString() + ", Y:" + Anchor.PosY.ToString();
				textBlock2.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				stackPanel.Children.Add(textBlock2);
				this.pnlAnchor.Children.Add(stackPanel);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00019C9C File Offset: 0x00017E9C
		private List<MapAnchor> SortAnchors(List<MapAnchor> Anchors)
		{
			List<MapAnchor> result = new List<MapAnchor>();
			if (Anchors != null && Anchors.Count != 0)
			{
				result = Anchors.OrderBy((MainWindow._Closure$__.$I317-0 == null) ? (MainWindow._Closure$__.$I317-0 = ((MapAnchor x) => x.Name)) : MainWindow._Closure$__.$I317-0).ToList<MapAnchor>();
			}
			return result;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00019CEC File Offset: 0x00017EEC
		private void HandleAnchorClick(object Sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Label label = Sender as System.Windows.Controls.Label;
			if (label != null)
			{
				label.Focus();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00019D0C File Offset: 0x00017F0C
		private void HandleAnchorSelection(object sender, RoutedEventArgs e)
		{
			this.mySelectedAnchor = (sender as System.Windows.Controls.Label);
			if (this.mySelectedAnchor != null)
			{
				if ((this.pnlAnchorOptions.Parent != null & this.mySelectedAnchor.Parent != null) && this.pnlAnchorOptions.Parent.Equals(this.mySelectedAnchor.Parent))
				{
					if (this.pnlAnchorOptions.Visibility == Visibility.Collapsed)
					{
						this.pnlAnchorOptions.Visibility = Visibility.Visible;
						return;
					}
					this.pnlAnchorOptions.Visibility = Visibility.Collapsed;
					return;
				}
				else
				{
					if (this.pnlAnchorOptions.Parent != null)
					{
						(this.pnlAnchorOptions.Parent as StackPanel).Children.Remove(this.pnlAnchorOptions);
					}
					if (this.mySelectedAnchor.Parent != null)
					{
						StackPanel stackPanel = this.mySelectedAnchor.Parent as StackPanel;
						if (stackPanel != null)
						{
							stackPanel.Children.Add(this.pnlAnchorOptions);
						}
					}
					this.pnlAnchorOptions.Visibility = Visibility.Visible;
					this.pnlAnchorOptions.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				}
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00019E38 File Offset: 0x00018038
		private void DoAnchorLocate()
		{
			if (this.mySelectedAnchor != null && this.mySelectedAnchor.Tag != null)
			{
				MapAnchor mapAnchor = this.mySelectedAnchor.Tag as MapAnchor;
				if (mapAnchor != null)
				{
					MainWindow.OnOrbatLocateAnchorEventHandler onOrbatLocateAnchorEvent = this.OnOrbatLocateAnchorEvent;
					if (onOrbatLocateAnchorEvent != null)
					{
						onOrbatLocateAnchorEvent(mapAnchor);
					}
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00019E80 File Offset: 0x00018080
		private void DoAnchorRemove()
		{
			if (this.mySelectedAnchor != null && this.mySelectedAnchor.Tag != null)
			{
				MapAnchor mapAnchor = this.mySelectedAnchor.Tag as MapAnchor;
				if (mapAnchor != null)
				{
					MainWindow.OnOrbatRemoveAnchorEventHandler onOrbatRemoveAnchorEvent = this.OnOrbatRemoveAnchorEvent;
					if (onOrbatRemoveAnchorEvent != null)
					{
						onOrbatRemoveAnchorEvent(mapAnchor);
					}
				}
			}
			StackPanel stackPanel = this.mySelectedAnchor.Parent as StackPanel;
			if (stackPanel != null)
			{
				this.pnlAnchor.Children.Remove(stackPanel);
			}
			this.mySelectedAnchor = null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00019EF8 File Offset: 0x000180F8
		private void DoAnchorAdd()
		{
			if (this.Map_Current != null && this.Map_Current.WorldSize >= 0)
			{
				MapAnchor mapAnchor = new MapAnchor();
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				if (string.IsNullOrEmpty(this.txtAnchorName.Text))
				{
					this.lblAnchorName.Foreground = new SolidColorBrush(Colors.Red);
					flag = false;
				}
				else
				{
					mapAnchor.Name = this.txtAnchorName.Text.Trim();
				}
				if (string.IsNullOrEmpty(this.txtAnchorGrid.Text) & string.IsNullOrEmpty(this.txtAnchorPosX.Text) & string.IsNullOrEmpty(this.txtAnchorPosY.Text))
				{
					this.lblAnchorGrid.Foreground = new SolidColorBrush(Colors.Red);
					this.lblAnchorPosX.Foreground = new SolidColorBrush(Colors.Red);
					this.lblAnchorPosY.Foreground = new SolidColorBrush(Colors.Red);
					flag = false;
				}
				else
				{
					if (!string.IsNullOrEmpty(this.txtAnchorPosX.Text.Trim()) & !string.IsNullOrEmpty(this.txtAnchorPosY.Text.Trim()))
					{
						flag2 = true;
						try
						{
							mapAnchor.PosX = Convert.ToDouble(this.txtAnchorPosX.Text.Trim(), CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							mapAnchor.PosX = -1.0;
						}
						try
						{
							mapAnchor.PosY = Convert.ToDouble(this.txtAnchorPosY.Text.Trim(), CultureInfo.InvariantCulture);
						}
						catch (Exception ex2)
						{
							mapAnchor.PosY = -1.0;
						}
						if (mapAnchor.PosX == -1.0 | mapAnchor.PosY == -1.0)
						{
							mapAnchor.PosX = -1.0;
							mapAnchor.PosY = -1.0;
						}
						else if (mapAnchor.PosX >= 0.0 & mapAnchor.PosX <= (double)this.Map_Current.WorldSize & mapAnchor.PosY >= 0.0 & mapAnchor.PosY <= (double)this.Map_Current.WorldSize)
						{
							flag3 = true;
							Common.ConvertPos(this.Map_Current.WorldSize, mapAnchor.PosX.ToString(), mapAnchor.PosY.ToString(), ref mapAnchor.CanvasX, ref mapAnchor.CanvasY);
							Common.ConvertPosToGrid(ref this.Map_Current, mapAnchor.PosX.ToString(), mapAnchor.PosY.ToString(), ref mapAnchor.Grid);
						}
						else
						{
							this.lblAnchorPosX.Foreground = new SolidColorBrush(Colors.Red);
							this.lblAnchorPosY.Foreground = new SolidColorBrush(Colors.Red);
							flag = false;
						}
					}
					if (!flag2 & !flag3)
					{
						if (this.txtAnchorGrid.Text.Trim().Length == 6)
						{
							int num = -1;
							try
							{
								num = Convert.ToInt32(this.txtAnchorGrid.Text, CultureInfo.InvariantCulture);
							}
							catch (Exception ex3)
							{
								this.lblAnchorGrid.Foreground = new SolidColorBrush(Colors.Red);
								flag = false;
							}
							if (num == -1)
							{
								this.lblAnchorGrid.Foreground = new SolidColorBrush(Colors.Red);
								flag = false;
							}
							else
							{
								mapAnchor.Grid = this.txtAnchorGrid.Text.Trim();
								Common.ConvertGridToPos(ref this.Map_Current, this.txtAnchorGrid.Text.Trim(), ref mapAnchor.PosX, ref mapAnchor.PosY);
								if (mapAnchor.PosX == -1.0 | mapAnchor.PosY == -1.0)
								{
									this.lblAnchorGrid.Foreground = new SolidColorBrush(Colors.Red);
									flag = false;
								}
							}
						}
						else
						{
							this.lblAnchorGrid.Foreground = new SolidColorBrush(Colors.Red);
							flag = false;
						}
					}
				}
				if (flag)
				{
					this.txtAnchorName.Text = string.Empty;
					this.txtAnchorGrid.Text = string.Empty;
					this.txtAnchorPosX.Text = string.Empty;
					this.txtAnchorPosY.Text = string.Empty;
					this.lblAnchorName.Foreground = new SolidColorBrush(Colors.Black);
					this.lblAnchorGrid.Foreground = new SolidColorBrush(Colors.Black);
					this.lblAnchorPosX.Foreground = new SolidColorBrush(Colors.Black);
					this.lblAnchorPosY.Foreground = new SolidColorBrush(Colors.Black);
					mapAnchor.Permanent = this.chkPermanent.IsChecked.Value;
					this.CreateAnchorLabel(ref mapAnchor);
					MainWindow.OnOrbatCreateAnchorEventHandler onOrbatCreateAnchorEvent = this.OnOrbatCreateAnchorEvent;
					if (onOrbatCreateAnchorEvent != null)
					{
						onOrbatCreateAnchorEvent(mapAnchor);
					}
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001A3E4 File Offset: 0x000185E4
		private void DoAnchorTrack()
		{
			if (this.mySelectedAnchor != null && this.mySelectedAnchor.Tag != null)
			{
				MapAnchor mapAnchor = this.mySelectedAnchor.Tag as MapAnchor;
				if (mapAnchor != null)
				{
					MainWindow.OnOrbatTrackAnchorEventHandler onOrbatTrackAnchorEvent = this.OnOrbatTrackAnchorEvent;
					if (onOrbatTrackAnchorEvent != null)
					{
						onOrbatTrackAnchorEvent(mapAnchor);
					}
				}
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001A42C File Offset: 0x0001862C
		private void GetAnchorClick()
		{
			if (this.Map_Current != null)
			{
				this.btnORBATAnchorGet.IsEnabled = false;
				this.btnORBATAnchorGet.Content = "Click Map";
				this.btnORBATAnchroSave.IsEnabled = false;
				MainWindow.OnOrbatAnchorPosRequestedEventHandler onOrbatAnchorPosRequestedEvent = this.OnOrbatAnchorPosRequestedEvent;
				if (onOrbatAnchorPosRequestedEvent != null)
				{
					onOrbatAnchorPosRequestedEvent();
				}
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001A47C File Offset: 0x0001867C
		public void HandleOrbatAnchorClick(Point Target)
		{
			this.btnORBATAnchorGet.IsEnabled = true;
			this.btnORBATAnchorGet.Content = "Use Map";
			this.btnORBATAnchroSave.IsEnabled = true;
			if (this.Map_Current != null && this.Map_Current.WorldSize > 0)
			{
				double value = 0.0;
				double value2 = 0.0;
				Common.ConvertPointToPos(this.Map_Current.WorldSize, Target, ref value, ref value2);
				this.txtAnchorPosX.Text = Math.Round(value, 2).ToString();
				this.txtAnchorPosY.Text = Math.Round(value2, 2).ToString();
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001A524 File Offset: 0x00018724
		private void ACSConnect()
		{
			this.ACSConnectButton.IsEnabled = false;
			this.ACS_IsConnected = false;
			this.ACS_IsAuthenticated = false;
			if (string.IsNullOrEmpty(this.txtACSCallsign.Text.Trim()))
			{
				this.UpdateServerConnectStatus("Callsign required");
				return;
			}
			if (string.IsNullOrEmpty(this.txtACSServerAddress.Text.Trim()))
			{
				this.UpdateServerConnectStatus("Address required");
				return;
			}
			int num = -1;
			if (!int.TryParse(this.txtACSServerPort.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out num))
			{
				this.UpdateServerConnectStatus("Port is not valid");
				return;
			}
			if (num <= 0 & num > 65535)
			{
				this.UpdateServerConnectStatus("Port is not valid");
				return;
			}
			string worldName = "none";
			string worldDisplayName = "none";
			if (this.Map_Current != null)
			{
				worldName = this.Map_Current.WorldName;
				worldDisplayName = this.Map_Current.DisplayName;
			}
			this.UpdateServerConnectStatus("Connecting.");
			this.MyACSHelper.Connect(this.txtACSCallsign.Text.Trim(), worldName, worldDisplayName, this.txtACSServerAddress.Text.Trim(), num, this.txtACSPassword.Password);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001A648 File Offset: 0x00018848
		private void ACSDisconnect()
		{
			this.ACSDisconnectButton.IsEnabled = false;
			this.ACS_IsConnected = false;
			this.ACS_IsAuthenticated = false;
			this.MyACSHelper.Disconnect();
			this.CleanupACSData();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001A675 File Offset: 0x00018875
		private void ACSShowRoomCreate()
		{
			this.txtACSRoomCreate_Name.Text = string.Empty;
			this.brdRoomCreate.Visibility = Visibility.Visible;
			this.txtACSRoomCreate_Name.Focus();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001A6A0 File Offset: 0x000188A0
		private void AdjustVisibility_FlyoutACS_Connect(bool IsVisible)
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.FlyoutACS_Connect.Visibility = (Visibility)Conversions.ToByte(Interaction.IIf(IsVisible, Visibility.Visible, Visibility.Collapsed));
			});
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001A6D8 File Offset: 0x000188D8
		private void AdjustVisibility_FlyoutACS_Connected(bool IsVisible)
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.FlyoutACS_Connected.Visibility = (Visibility)Conversions.ToByte(Interaction.IIf(IsVisible, Visibility.Visible, Visibility.Collapsed));
			});
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001A710 File Offset: 0x00018910
		public void UpdateServerConnectStatus(string Status)
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.txtACSConnectStatus.Text = Status;
			});
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001A748 File Offset: 0x00018948
		private void CleanupACSData()
		{
			try
			{
				if (this.MyACSHelper.DictionaryOfMaps != null && this.MyACSHelper.DictionaryOfMaps.Count != 0)
				{
					try
					{
						foreach (MapDictionaryItem mapDictionaryItem in this.MyACSHelper.DictionaryOfMaps.Values)
						{
							this.HandleMapDeleted(mapDictionaryItem.Map.World);
						}
					}
					finally
					{
						Dictionary<string, MapDictionaryItem>.ValueCollection.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
			}
			try
			{
				if (this.MyACSHelper.DictionaryOfRooms != null && this.MyACSHelper.DictionaryOfRooms.Count != 0)
				{
					try
					{
						foreach (RoomDictionaryItem roomDictionaryItem in this.MyACSHelper.DictionaryOfRooms.Values)
						{
							if (roomDictionaryItem.PlayerDictionary != null && roomDictionaryItem.PlayerDictionary.Count != 0)
							{
								try
								{
									foreach (PlayerDictionaryItem playerDictionaryItem in roomDictionaryItem.PlayerDictionary.Values)
									{
										this.HandlePlayerDeleted(playerDictionaryItem.Player.RoomGUID, playerDictionaryItem.Player.SteamID);
									}
								}
								finally
								{
									Dictionary<string, PlayerDictionaryItem>.ValueCollection.Enumerator enumerator3;
									((IDisposable)enumerator3).Dispose();
								}
							}
							if (roomDictionaryItem.ParticipantDictionary != null && roomDictionaryItem.ParticipantDictionary.Count != 0)
							{
								try
								{
									foreach (RoomParticipantDictionaryItem roomParticipantDictionaryItem in roomDictionaryItem.ParticipantDictionary.Values)
									{
										this.HandleRoomParticipantDeleted(roomParticipantDictionaryItem.Participant.RoomGUID, roomParticipantDictionaryItem.Participant.SessionGUID);
									}
								}
								finally
								{
									Dictionary<Guid, RoomParticipantDictionaryItem>.ValueCollection.Enumerator enumerator4;
									((IDisposable)enumerator4).Dispose();
								}
							}
							if (roomDictionaryItem.PermissionDictionary != null && roomDictionaryItem.PermissionDictionary.Count != 0)
							{
								try
								{
									foreach (RoomPermissionDictionaryItem roomPermissionDictionaryItem in roomDictionaryItem.PermissionDictionary.Values)
									{
										this.HandleRoomPermissionDeleted(roomPermissionDictionaryItem.Permission.RoomGUID, roomPermissionDictionaryItem.Permission.SessionGUID);
									}
								}
								finally
								{
									Dictionary<Guid, RoomPermissionDictionaryItem>.ValueCollection.Enumerator enumerator5;
									((IDisposable)enumerator5).Dispose();
								}
							}
							this.HandleRoomDeleted(roomDictionaryItem.Room.RoomGUID);
						}
					}
					finally
					{
						Dictionary<Guid, RoomDictionaryItem>.ValueCollection.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
			}
			catch (Exception ex2)
			{
			}
			try
			{
				if (this.MyACSHelper.DictionaryOfSessions != null && this.MyACSHelper.DictionaryOfSessions.Count != 0)
				{
					try
					{
						foreach (SessionDictionaryItem sessionDictionaryItem in this.MyACSHelper.DictionaryOfSessions.Values)
						{
							this.HandleSessionDeleted(sessionDictionaryItem.Session.SessionGUID);
						}
					}
					finally
					{
						Dictionary<Guid, SessionDictionaryItem>.ValueCollection.Enumerator enumerator6;
						((IDisposable)enumerator6).Dispose();
					}
				}
			}
			catch (Exception ex3)
			{
			}
			this.MyACSHelper.SessionGUID = Guid.Empty;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001AB08 File Offset: 0x00018D08
		private void HandleACSMapSwitch_Proceed(object sender, RoutedEventArgs e)
		{
			try
			{
				this.brdMapSwitch.Visibility = Visibility.Collapsed;
				if (this.brdMapSwitch.Tag != null)
				{
					Map map = this.brdMapSwitch.Tag as Map;
					if (map != null)
					{
						this.LoadMap(map);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001AB6C File Offset: 0x00018D6C
		private void HandleACSMapSwitch_Cancel(object sender, RoutedEventArgs e)
		{
			this.brdMapSwitch.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001AB7C File Offset: 0x00018D7C
		private void HandleACSMapChanged()
		{
			checked
			{
				if (Conversions.ToBoolean(this.MyACSHelper.Connected))
				{
					this.MyACSHelper.WorldName = "none";
					this.MyACSHelper.WorldDisplayName = "none";
					if (this.Map_Current != null)
					{
						this.MyACSHelper.WorldName = this.Map_Current.WorldName;
						this.MyACSHelper.WorldDisplayName = this.Map_Current.DisplayName;
					}
					this.MyACSHelper.SubmitSessionUpdateRequest(this.MyACSHelper.Callsign, this.MyACSHelper.Player, this.MyACSHelper.SteamID, this.MyACSHelper.WorldName, this.MyACSHelper.WorldDisplayName);
					List<RoomDictionaryItem> list = (from x in this.MyACSHelper.DictionaryOfRooms
					where x.Value.ParticipantDictionary.Keys.Contains(this.MyACSHelper.SessionGUID) & x.Value.Room.OwnerSessionGUID == this.MyACSHelper.SessionGUID & !x.Value.Room.WorldName.Equals(this.MyACSHelper.WorldName, StringComparison.InvariantCultureIgnoreCase)
					select x).Select((MainWindow._Closure$__.$I338-1 == null) ? (MainWindow._Closure$__.$I338-1 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I338-1).ToList<RoomDictionaryItem>();
					if (list != null && list.Count != 0)
					{
						try
						{
							foreach (RoomDictionaryItem roomDictionaryItem in list)
							{
								this.MyACSHelper.SubmitUpdateRoomRequest(roomDictionaryItem.Room.RoomGUID, roomDictionaryItem.Room.OwnerSessionGUID, roomDictionaryItem.Room.Action, roomDictionaryItem.Room.Security, roomDictionaryItem.Room.Side, roomDictionaryItem.Room.Name, roomDictionaryItem.Room.Server, roomDictionaryItem.Room.Mission, this.MyACSHelper.WorldName, this.MyACSHelper.WorldDisplayName);
								RoomParticipantDictionaryItem value = roomDictionaryItem.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
								if (value != null && value.LayerControl != null && value.LayerControl.LayerCanvas != null)
								{
									try
									{
										foreach (Stroke item in value.LayerControl.LayerCanvas.Strokes)
										{
											value.LayerControl.LayerCanvas.Strokes.Remove(item);
										}
									}
									finally
									{
										IEnumerator<Stroke> enumerator2;
										if (enumerator2 != null)
										{
											enumerator2.Dispose();
										}
									}
								}
							}
						}
						finally
						{
							List<RoomDictionaryItem>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					List<RoomDictionaryItem> list2 = (from x in this.MyACSHelper.DictionaryOfRooms
					where x.Value.ParticipantDictionary.Keys.Contains(this.MyACSHelper.SessionGUID) & !(x.Value.Room.OwnerSessionGUID == this.MyACSHelper.SessionGUID) & !x.Value.Room.WorldName.Equals(this.MyACSHelper.WorldName, StringComparison.InvariantCultureIgnoreCase)
					select x).Select((MainWindow._Closure$__.$I338-4 == null) ? (MainWindow._Closure$__.$I338-4 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I338-4).ToList<RoomDictionaryItem>();
					if (list2 != null)
					{
						try
						{
							foreach (RoomDictionaryItem roomDictionaryItem2 in list2)
							{
								RoomParticipantDictionaryItem value2 = roomDictionaryItem2.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
								if (value2 != null && value2.LayerControl != null && value2.LayerControl.LayerCanvas != null)
								{
									for (int i = value2.LayerControl.LayerCanvas.Strokes.Count - 1; i >= 0; i += -1)
									{
										value2.LayerControl.LayerCanvas.Strokes.RemoveAt(i);
									}
								}
							}
						}
						finally
						{
							List<RoomDictionaryItem>.Enumerator enumerator3;
							((IDisposable)enumerator3).Dispose();
						}
					}
					List<RoomDictionaryItem> list3 = (from x in this.MyACSHelper.DictionaryOfRooms
					where x.Value.ParticipantDictionary.Keys.Contains(this.MyACSHelper.SessionGUID) & !(x.Value.Room.OwnerSessionGUID == this.MyACSHelper.SessionGUID) & x.Value.Room.WorldName.Equals(this.MyACSHelper.WorldName, StringComparison.InvariantCultureIgnoreCase)
					select x).Select((MainWindow._Closure$__.$I338-7 == null) ? (MainWindow._Closure$__.$I338-7 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I338-7).ToList<RoomDictionaryItem>();
					if (list3 != null)
					{
						try
						{
							foreach (RoomDictionaryItem roomDictionaryItem3 in list3)
							{
								this.MyACSHelper.SubmitStrokesRequest(roomDictionaryItem3.Room.RoomGUID);
							}
						}
						finally
						{
							List<RoomDictionaryItem>.Enumerator enumerator4;
							((IDisposable)enumerator4).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001AFD0 File Offset: 0x000191D0
		private void HandleACSMapPublish(object sender, RoutedEventArgs e)
		{
			string World = string.Empty;
			if (this.ddlACSMapList.SelectedItem != null)
			{
				ComboBoxItem comboBoxItem = this.ddlACSMapList.SelectedItem as ComboBoxItem;
				if (comboBoxItem != null & comboBoxItem.Tag != null)
				{
					World = comboBoxItem.Tag.ToString();
				}
			}
			if (string.IsNullOrEmpty(World))
			{
				this.lblACSMapPublish.Text = "Unable to locate map.";
				return;
			}
			if (this.MyACSHelper.DictionaryOfMaps != null && this.MyACSHelper.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, World, false) == 0).Value != null)
			{
				this.lblACSMapPublish.Text = "The server has the selected map.";
				return;
			}
			this.BeginPublishMap(World);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001B09C File Offset: 0x0001929C
		private void BeginPublishMap(string World)
		{
			this.btnACSMapPublish.IsEnabled = false;
			Map value = this.Maps.SingleOrDefault((KeyValuePair<string, Map> x) => Operators.CompareString(x.Key, World, false) == 0).Value;
			if (value != null)
			{
				this.MyACSHelper.SubmitCreateMapRequest(value.DisplayName, value.WorldName, Conversions.ToString(value.WorldSize));
				this.Pending_MapPublish_World = value.WorldName;
			}
			this.btnACSMapPublish.IsEnabled = true;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001B124 File Offset: 0x00019324
		private void FinishPublishMap()
		{
			string MapWorld = this.Pending_MapPublish_World;
			string text = string.Empty;
			this.Pending_MapPublish_World = string.Empty;
			base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
			{
				this.lblACSMapPublish.Text = "Packaging map folder as zip.";
			}), new object[0]);
			Map value = this.Maps.SingleOrDefault((KeyValuePair<string, Map> x) => Operators.CompareString(x.Key, MapWorld, false) == 0).Value;
			if (value != null)
			{
				text = this.DIR_Maps + "\\" + value.WorldName + ".zip";
				try
				{
					if (!Directory.Exists(value.Folder))
					{
						base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
						{
							this.lblACSMapPublish.Text = "Unable to locate the map folder.";
						}), new object[0]);
						return;
					}
					if (File.Exists(text))
					{
						File.Delete(text);
					}
					ZipFile.CreateFromDirectory(value.Folder, text, CompressionLevel.Fastest, true);
				}
				catch (Exception ex)
				{
					Exception ex3;
					Exception $VB$Local_ex = ex3;
					Exception ex = $VB$Local_ex;
					base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.lblACSMapPublish.Text = "An error occurred while trying to publish the map. More info: " + ex.Message;
					}), new object[0]);
					return;
				}
			}
			try
			{
				if (!File.Exists(text))
				{
					base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						this.lblACSMapPublish.Text = "Unable to locate zip file.";
					}), new object[0]);
					return;
				}
			}
			catch (Exception ex2)
			{
				Exception $VB$Local_ex2 = ex2;
				Exception ex = $VB$Local_ex2;
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.lblACSMapPublish.Text = "An error occurred while trying to verify file creation. More info: " + ex.Message;
				}), new object[0]);
				return;
			}
			try
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.lblACSMapPublish.Text = "Preparing to upload to ACS.";
				}), new object[0]);
				this.MyACSHelper.SubmitFilePutMapRequest(MapWorld, text);
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.lblACSMapPublish.Text = "File transfer request submitted";
				}), new object[0]);
			}
			catch (Exception ex3)
			{
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001B354 File Offset: 0x00019554
		private void PopulatePublishMaps()
		{
			this.ddlACSMapList.Items.Clear();
			this.btnACSMapPublish.IsEnabled = true;
			if (this.Maps != null)
			{
				List<Map> list = this.Maps.Values.OrderBy((MainWindow._Closure$__.$I342-0 == null) ? (MainWindow._Closure$__.$I342-0 = ((Map x) => x.DisplayName)) : MainWindow._Closure$__.$I342-0).ToList<Map>();
				try
				{
					foreach (Map map in list)
					{
						this.ddlACSMapList.Items.Add(new ComboBoxItem
						{
							Content = map.DisplayName,
							Tag = map.WorldName
						});
					}
				}
				finally
				{
					List<Map>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			if (this.ddlACSMapList.Items.Count == 0)
			{
				this.ddlACSMapList.Items.Add(new ComboBoxItem
				{
					Content = "None",
					Tag = string.Empty
				});
				this.btnACSMapPublish.IsEnabled = false;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001B474 File Offset: 0x00019674
		private void HandleACSMapGet(object sender, RoutedEventArgs e)
		{
			try
			{
				if (e.Source != null)
				{
					System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;
					if (button != null && button.Tag != null)
					{
						string World = button.Tag.ToString();
						button.IsEnabled = false;
						this.MyACSHelper.SubmitFileGetMapRequest(World);
						MapDictionaryItem value = this.MyACSHelper.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, World, false) == 0).Value;
						if (value != null)
						{
							value.MapControl.StatusLabel.Text = "Request submitted...";
							button.IsEnabled = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001B540 File Offset: 0x00019740
		private void HandleCreateRoomClicked()
		{
			if (this.Map_Current == null | this.MyACSHelper.SessionGUID.Equals(Guid.Empty))
			{
				this.ShowMessage("CANNOT CREATE ROOM", "Because rooms are so closely tied to maps, we require that users be viewing a map before creating a room. Please select a map from the menu (Controls->View Map->Pick Map) and then try again.");
				return;
			}
			if (!this.MyACSHelper.SessionGUID.Equals(Guid.Empty))
			{
				this.ACSShowRoomCreate();
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001B59C File Offset: 0x0001979C
		private void HandleACSRoomCreateForm_Create(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.Map_Current == null)
				{
					return;
				}
				if (this.MyACSHelper.SessionGUID.Equals(Guid.Empty))
				{
					return;
				}
				Guid roomGUID = Guid.NewGuid();
				string name = this.txtACSRoomCreate_Name.Text.Trim();
				this.MyACSHelper.SubmitCreateRoomRequest(roomGUID, "drawing", "open", "all", name, "none", "none", this.Map_Current.WorldName, this.Map_Current.DisplayName);
			}
			catch (Exception ex)
			{
			}
			this.brdRoomCreate.Visibility = Visibility.Collapsed;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001B650 File Offset: 0x00019850
		private void HandleACSRoomCreateForm_Cancel(object sender, RoutedEventArgs e)
		{
			this.brdRoomCreate.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001B660 File Offset: 0x00019860
		private void HandleJoinRoomClicked(object sender, RoutedEventArgs e)
		{
			if (!Conversions.ToBoolean(Operators.NotObject(this.MyACSHelper.Connected)))
			{
				try
				{
					Guid RoomGUID = Guid.Empty;
					if (e.Source != null)
					{
						System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;
						if (button != null && button.Tag != null)
						{
							Guid.TryParse(button.Tag.ToString(), out RoomGUID);
						}
					}
					if (!RoomGUID.Equals(Guid.Empty))
					{
						if (this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value.ParticipantDictionary.ContainsKey(this.MyACSHelper.SessionGUID))
						{
							this.MyACSHelper.SubmitLeaveRoomRequest(RoomGUID);
						}
						else
						{
							this.MyACSHelper.SubmitJoinRoomRequest(RoomGUID);
							this.HandleACSMapChanged();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001B76C File Offset: 0x0001996C
		private void HandleActiveRoomClicked(object sender, RoutedEventArgs e)
		{
			if (!this.MyACSHelper.SessionGUID.Equals(Guid.Empty))
			{
				try
				{
					Guid RoomGUID = Guid.Empty;
					if (e.Source != null)
					{
						System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;
						if (button != null && button.Tag != null)
						{
							Guid.TryParse(button.Tag.ToString(), out RoomGUID);
						}
					}
					if (!RoomGUID.Equals(Guid.Empty))
					{
						if (!this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value.ParticipantDictionary.ContainsKey(this.MyACSHelper.SessionGUID))
						{
							this.MyACSHelper.SubmitJoinRoomRequest(RoomGUID);
							this.HandleACSMapChanged();
						}
						this.SetActivateInkLayer(null, new RoutedEventArgs(null, new System.Windows.Controls.Button
						{
							Tag = RoomGUID.ToString()
						}));
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001B890 File Offset: 0x00019A90
		private void HandlePromptTimerExpired(Guid RoomGUID)
		{
			this.MyACSHelper.SubmitLeaveRoomRequest(RoomGUID);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		private bool IsARoomOwner()
		{
			try
			{
				if (Conversions.ToBoolean(this.MyACSHelper.Connected))
				{
					List<RoomDictionaryItem> list = (from x in this.MyACSHelper.DictionaryOfRooms
					where x.Value.ParticipantDictionary.Keys.Contains(this.MyACSHelper.SessionGUID) & x.Value.Room.OwnerSessionGUID.Equals(this.MyACSHelper.SessionGUID)
					select x).Select((MainWindow._Closure$__.$I350-1 == null) ? (MainWindow._Closure$__.$I350-1 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I350-1).ToList<RoomDictionaryItem>();
					if (list != null && list.Count != 0)
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001B940 File Offset: 0x00019B40
		private bool IsARoomParticipant()
		{
			bool result = false;
			try
			{
				if (!this.MyACSHelper.SessionGUID.Equals(Guid.Empty))
				{
					List<RoomDictionaryItem> list = (from x in this.MyACSHelper.DictionaryOfRooms
					where x.Value.ParticipantDictionary.Keys.Contains(this.MyACSHelper.SessionGUID) & !(x.Value.Room.OwnerSessionGUID == this.MyACSHelper.SessionGUID)
					select x).Select((MainWindow._Closure$__.$I351-1 == null) ? (MainWindow._Closure$__.$I351-1 = ((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Value)) : MainWindow._Closure$__.$I351-1).ToList<RoomDictionaryItem>();
					if (list != null && list.Count != 0)
					{
						this.ShowMessage("MAP SWITCH CANCELLED", "You're currently a participant in one or more rooms in which this is the active map. Please leave the room(s) before attempting to switch maps again otherwise wait for the room owner to initiate the room switch. You can find the room(s) on the COMMUNITY tab.");
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		private void HandleACSStrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
		{
			if (!this.MyACSHelper.SessionGUID.Equals(Guid.Empty))
			{
				if (e.Added != null)
				{
					try
					{
						try
						{
							foreach (Stroke stroke in e.Added)
							{
								bool flag = false;
								try
								{
									List<Guid> list = stroke.GetPropertyDataIds().ToList<Guid>();
									if (list != null && list.Count != 0)
									{
										try
										{
											foreach (Guid propertyDataId in list)
											{
												if (Operators.CompareString(stroke.GetPropertyData(propertyDataId).ToString().ToLower(), "creatorguid", false) == 0)
												{
													flag = true;
													break;
												}
											}
										}
										finally
										{
											List<Guid>.Enumerator enumerator2;
											((IDisposable)enumerator2).Dispose();
										}
									}
								}
								catch (Exception ex)
								{
									flag = false;
								}
								if (!flag && this.InkLayerActive.Tag != null)
								{
									string text = this.InkLayerActive.Tag.ToString();
									if (!string.IsNullOrEmpty(text))
									{
										Guid guid = Guid.NewGuid();
										stroke.AddPropertyData(this.MyACSHelper.SessionGUID, "creatorguid");
										stroke.AddPropertyData(guid, "strokeguid");
										this.MyACSHelper.SubmitStrokeCreationCommand(text, guid, ref stroke);
									}
								}
							}
						}
						finally
						{
							IEnumerator<Stroke> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					catch (Exception ex2)
					{
					}
				}
				if (e.Removed != null)
				{
					try
					{
						try
						{
							foreach (Stroke stroke2 in e.Removed)
							{
								bool flag2 = false;
								Guid guid2 = Guid.Empty;
								try
								{
									List<Guid> list2 = stroke2.GetPropertyDataIds().ToList<Guid>();
									if (list2 != null && list2.Count != 0)
									{
										try
										{
											foreach (Guid guid3 in list2)
											{
												string left = stroke2.GetPropertyData(guid3).ToString().ToLower();
												if (Operators.CompareString(left, "strokeguid", false) != 0)
												{
													if (Operators.CompareString(left, "deleterguid", false) == 0)
													{
														flag2 = true;
													}
												}
												else
												{
													guid2 = guid3;
												}
											}
										}
										finally
										{
											List<Guid>.Enumerator enumerator4;
											((IDisposable)enumerator4).Dispose();
										}
									}
								}
								catch (Exception ex3)
								{
									flag2 = false;
								}
								if ((!flag2 & !guid2.Equals(Guid.Empty)) && this.InkLayerActive.Tag != null)
								{
									string text2 = this.InkLayerActive.Tag.ToString();
									if (!string.IsNullOrEmpty(text2))
									{
										string.Concat(new string[]
										{
											"stroke<ath_sep>delete<ath_sep>",
											this.MyACSHelper.SessionGUID.ToString(),
											"<ath_sep>",
											text2,
											"<ath_sep>",
											guid2.ToString()
										});
										this.MyACSHelper.SubmitStrokeDeletionCommand(text2, guid2.ToString());
									}
								}
							}
						}
						finally
						{
							IEnumerator<Stroke> enumerator3;
							if (enumerator3 != null)
							{
								enumerator3.Dispose();
							}
						}
					}
					catch (Exception ex4)
					{
					}
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		private void HandleACSOnAuthenticated(bool Success, string Message)
		{
			if (Success)
			{
				this.ACS_IsAuthenticated = true;
				this.UpdateServerConnectStatus("Authentication successful.");
				this.AdjustVisibility_FlyoutACS_Connect(false);
				this.AdjustVisibility_FlyoutACS_Connected(true);
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.PopulatePublishMaps();
				}), new object[0]);
				return;
			}
			this.ACS_IsAuthenticated = false;
			this.ShowMessage("Athena Community Server", "Authentication failed: " + Message);
			this.UpdateServerConnectStatus(string.Empty);
			this.MyACSHelper.Disconnect();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001BE36 File Offset: 0x0001A036
		private void HandleACSOnConnected(bool Success, string Message)
		{
			if (Success)
			{
				this.ACS_IsConnected = true;
				this.ACS_IsAuthenticated = false;
				base.Dispatcher.Invoke(delegate()
				{
					this.ACSDisconnectButton.IsEnabled = true;
				});
				this.UpdateServerConnectStatus("Connected. Authenticating...");
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001BE6C File Offset: 0x0001A06C
		private void HandleACSOnDisconnected()
		{
			this.ACS_IsConnected = false;
			this.ACS_IsAuthenticated = false;
			this.AdjustVisibility_FlyoutACS_Connect(true);
			this.AdjustVisibility_FlyoutACS_Connected(false);
			this.CleanupACSData();
			if (Conversions.ToBoolean(this.MyACSHelper.Connecting))
			{
				this.MyACSHelper.Disconnect();
				return;
			}
			base.Dispatcher.Invoke(delegate()
			{
				this.ACSConnectButton.IsEnabled = true;
			});
			this.UpdateServerConnectStatus("Disconnected");
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleACSOnValidated()
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001BEDC File Offset: 0x0001A0DC
		private void HandleFileGetMap(string World, string FileName, byte[] Content)
		{
			try
			{
				MapDictionaryItem MDI = this.MyACSHelper.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, World, false) == 0).Value;
				if (MDI != null)
				{
					base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						MDI.MapControl.StatusLabel.Text = "File content received. Writing to disk.";
					}), new object[0]);
					string text = this.DIR_Maps + "\\" + World;
					string text2 = text + ".zip";
					try
					{
						File.WriteAllBytes(text2, Content);
						if (!File.Exists(text2))
						{
							base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
							{
								MDI.MapControl.StatusLabel.Text = "Write to disk failed.";
							}), new object[0]);
						}
						else
						{
							base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
							{
								MDI.MapControl.StatusLabel.Text = "File saved. Attempting to unzip into destination folder.";
							}), new object[0]);
							if (Directory.Exists(this.DIR_Maps + "\\" + World))
							{
								Directory.Delete(this.DIR_Maps + "\\" + World, true);
							}
							ZipFile.ExtractToDirectory(text2, this.DIR_Maps);
							if (!Directory.Exists(text))
							{
								base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
								{
									MDI.MapControl.StatusLabel.Text = "Extraction failed.";
								}), new object[0]);
							}
							else
							{
								base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
								{
									MDI.MapControl.StatusLabel.Text = "Extraction complete. Updating client side maps.";
								}), new object[0]);
								this.EnumerateMapFolders();
								base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
								{
									this.UpdateMapMenu();
								}), new object[0]);
								base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
								{
									this.PopulatePublishMaps();
								}), new object[0]);
								base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
								{
									MDI.MapControl.StatusLabel.Text = "Download complete.";
								}), new object[0]);
							}
						}
					}
					catch (Exception ex)
					{
						Exception ex2;
						Exception $VB$Local_ex = ex2;
						Exception ex = $VB$Local_ex;
						base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
						{
							MDI.MapControl.StatusLabel.Text = "Error: " + ex.Message;
						}), new object[0]);
					}
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001C140 File Offset: 0x0001A340
		private void HandleMapCreated(string World)
		{
			try
			{
				MapDictionaryItem DictItem = this.MyACSHelper.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, World, false) == 0).Value;
				double FileSizeMB = 0.0;
				long num;
				if (!string.IsNullOrEmpty(DictItem.Map.FileSize) && long.TryParse(DictItem.Map.FileSize, NumberStyles.Number, CultureInfo.InvariantCulture, out num))
				{
					FileSizeMB = (double)((float)num / 1024f / 1024f);
				}
				DictItem.MapControl = base.Dispatcher.Invoke<ACSMapControl>(delegate()
				{
					ACSMapControl acsmapControl = new ACSMapControl();
					acsmapControl.CreateControl(DictItem.Map.World, DictItem.Map.Name);
					try
					{
						if (string.IsNullOrEmpty(DictItem.Map.FileSize) || Operators.CompareString(DictItem.Map.FileSize, "0", false) == 0)
						{
							acsmapControl.MapButton.Visibility = Visibility.Collapsed;
						}
						else
						{
							acsmapControl.MapLabel.Text = DictItem.Map.Name + " (" + FileSizeMB.ToString("0.00") + "MB)";
							acsmapControl.MapButton.Visibility = Visibility.Visible;
						}
						acsmapControl.MapButton.Click += this.HandleACSMapGet;
						this.pnlACSMaps.Children.Add(acsmapControl.MapPanel);
					}
					catch (Exception ex)
					{
						acsmapControl = null;
					}
					return acsmapControl;
				});
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.PopulatePublishMaps();
				}), new object[0]);
				if (!string.IsNullOrWhiteSpace(this.Pending_MapPublish_World) & this.Pending_MapPublish_World.Equals(World, StringComparison.InvariantCultureIgnoreCase))
				{
					this.FinishPublishMap();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001C294 File Offset: 0x0001A494
		private void HandleMapDeleted(string World)
		{
			try
			{
				MapDictionaryItem DictItem = this.MyACSHelper.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, World, false) == 0).Value;
				base.Dispatcher.Invoke(delegate()
				{
					try
					{
						DictItem.MapControl.MapButton.Click -= this.HandleACSMapGet;
						this.pnlACSMaps.Children.Remove(DictItem.MapControl.MapPanel);
					}
					catch (Exception ex)
					{
					}
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001C328 File Offset: 0x0001A528
		private void HandleMapUpdated(string World)
		{
			try
			{
				MapDictionaryItem DictItem = this.MyACSHelper.DictionaryOfMaps.SingleOrDefault((KeyValuePair<string, MapDictionaryItem> x) => Operators.CompareString(x.Key, World, false) == 0).Value;
				double FileSizeMB = 0.0;
				long num;
				if (!string.IsNullOrEmpty(DictItem.Map.FileSize) && long.TryParse(DictItem.Map.FileSize, NumberStyles.Number, CultureInfo.InvariantCulture, out num))
				{
					FileSizeMB = (double)((float)num / 1024f / 1024f);
				}
				base.Dispatcher.Invoke(delegate()
				{
					try
					{
						if (string.IsNullOrEmpty(DictItem.Map.FileSize) || Operators.CompareString(DictItem.Map.FileSize, "0", false) == 0)
						{
							DictItem.MapControl.MapButton.Visibility = Visibility.Collapsed;
						}
						else
						{
							DictItem.MapControl.MapLabel.Text = DictItem.Map.Name + " (" + FileSizeMB.ToString("0.00") + "MB)";
							DictItem.MapControl.MapButton.Visibility = Visibility.Visible;
						}
					}
					catch (Exception ex)
					{
					}
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001C408 File Offset: 0x0001A608
		private void HandleRoomCreated(Guid RoomGUID)
		{
			try
			{
				RoomDictionaryItem DictItem = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
				DictItem.RoomControl = base.Dispatcher.Invoke<ACSRoomControl>(delegate()
				{
					ACSRoomControl acsroomControl = new ACSRoomControl();
					ACSRoomControl acsroomControl2 = acsroomControl;
					ACSHelper myACSHelper = this.MyACSHelper;
					ACSRoomControl.CurrentMapDelegate currentMapDelegate = new ACSRoomControl.CurrentMapDelegate(this.GetCurrentMap);
					acsroomControl2.CreateControls(ref myACSHelper.SessionGUID, ref DictItem, ref currentMapDelegate);
					try
					{
						acsroomControl.Header.Text = DictItem.Room.Name;
						if (!string.IsNullOrEmpty(DictItem.Room.WorldDisplayName))
						{
							TextBlock header;
							(header = acsroomControl.Header).Text = header.Text + Environment.NewLine + " World: " + DictItem.Room.WorldDisplayName;
						}
						acsroomControl.ButtonJoin.Click += this.HandleJoinRoomClicked;
						acsroomControl.ButtonActive.Click += this.HandleActiveRoomClicked;
						acsroomControl.PromptTimerExpired += this.HandlePromptTimerExpired;
						this.pnlRooms.Children.Add(acsroomControl.RoomPanel);
					}
					catch (Exception ex)
					{
						acsroomControl = null;
					}
					return acsroomControl;
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		private void HandleRoomDeleted(Guid RoomGUID)
		{
			RoomDictionaryItem DictItem = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
			base.Dispatcher.Invoke(delegate()
			{
				try
				{
					DictItem.RoomControl.ButtonJoin.Click -= this.HandleJoinRoomClicked;
					DictItem.RoomControl.ButtonActive.Click -= this.HandleActiveRoomClicked;
					DictItem.RoomControl.PromptTimerExpired -= this.HandlePromptTimerExpired;
					this.pnlRooms.Children.Remove(DictItem.RoomControl.RoomPanel);
				}
				catch (Exception ex)
				{
				}
			});
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001C50C File Offset: 0x0001A70C
		private void HandleRoomUpdated(Guid RoomGUID)
		{
			MainWindow._Closure$__363-0 CS$<>8__locals1 = new MainWindow._Closure$__363-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_RoomGUID = RoomGUID;
			CS$<>8__locals1.$VB$Local_DictItem = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == CS$<>8__locals1.$VB$Local_RoomGUID).Value;
			base.Dispatcher.Invoke(delegate()
			{
				try
				{
					CS$<>8__locals1.$VB$Local_DictItem.RoomControl.Header.Text = CS$<>8__locals1.$VB$Local_DictItem.Room.Name;
					if (!string.IsNullOrEmpty(CS$<>8__locals1.$VB$Local_DictItem.Room.WorldDisplayName))
					{
						TextBlock header;
						(header = CS$<>8__locals1.$VB$Local_DictItem.RoomControl.Header).Text = header.Text + Environment.NewLine + " World: " + CS$<>8__locals1.$VB$Local_DictItem.Room.WorldDisplayName;
					}
				}
				catch (Exception ex)
				{
				}
			});
			try
			{
				Dictionary<Guid, RoomParticipantDictionaryItem>.ValueCollection.Enumerator enumerator = CS$<>8__locals1.$VB$Local_DictItem.ParticipantDictionary.Values.GetEnumerator();
				while (enumerator.MoveNext())
				{
					MainWindow._Closure$__363-1 CS$<>8__locals2 = new MainWindow._Closure$__363-1(CS$<>8__locals2);
					CS$<>8__locals2.$VB$Local_Participant = enumerator.Current;
					if (CS$<>8__locals2.$VB$Local_Participant.Participant.SessionGUID == CS$<>8__locals1.$VB$Local_DictItem.Room.OwnerSessionGUID)
					{
						base.Dispatcher.Invoke(delegate()
						{
							CS$<>8__locals2.$VB$Local_Participant.Control.FontWeight = FontWeights.Bold;
						});
					}
					else
					{
						base.Dispatcher.Invoke(delegate()
						{
							CS$<>8__locals2.$VB$Local_Participant.Control.FontWeight = FontWeights.Normal;
						});
					}
				}
			}
			finally
			{
				Dictionary<Guid, RoomParticipantDictionaryItem>.ValueCollection.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandlePlayerCreated(Guid RoomGUID, string SteamID)
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandlePlayerDeleted(Guid RoomGUID, string SteamID)
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001C614 File Offset: 0x0001A814
		private void HandleRoomParticipantCreated(Guid RoomGUID, Guid SessionGUID)
		{
			try
			{
				RoomDictionaryItem DictItemRoom = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
				RoomParticipantDictionaryItem DictItemRoomParticipant = DictItemRoom.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == SessionGUID).Value;
				SessionDictionaryItem DictItemSession = this.MyACSHelper.DictionaryOfSessions.SingleOrDefault((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Key == SessionGUID).Value;
				DictItemRoomParticipant.Control = base.Dispatcher.Invoke<TextBlock>(delegate()
				{
					TextBlock textBlock = new TextBlock();
					try
					{
						textBlock.Text = DictItemSession.Session.Callsign;
						DictItemRoom.RoomControl.ParticipantPanel.Children.Add(textBlock);
						if (DictItemSession.Session.SessionGUID == DictItemRoom.Room.OwnerSessionGUID)
						{
							textBlock.FontWeight = FontWeights.Bold;
						}
						if (SessionGUID.Equals(this.MyACSHelper.SessionGUID))
						{
							(DictItemRoom.RoomControl.ButtonJoin.Content as TextBlock).Text = "LEAVE";
							DictItemRoomParticipant.LayerControl = new ACSRoomLayerControl();
							DictItemRoomParticipant.LayerControl.CreateControl(ref RoomGUID, DictItemRoom.Room.Name);
							DictItemRoomParticipant.LayerControl.LayerCheckbox.Checked += this.HandleLayerToggle;
							DictItemRoomParticipant.LayerControl.LayerCheckbox.Unchecked += this.HandleLayerToggle;
							DictItemRoomParticipant.LayerControl.LayerButton.Click += this.SetActivateInkLayer;
							this.pnlInkLayers.Children.Add(DictItemRoomParticipant.LayerControl.LayerPanel);
							this.MapVC.LayerHost.Children.Add(DictItemRoomParticipant.LayerControl.LayerCanvas);
							DictItemRoomParticipant.LayerControl.LayerCanvas.Tag = RoomGUID.ToString();
							DictItemRoomParticipant.LayerControl.LayerCanvas.Width = this.MapVC.Ink.Width;
							DictItemRoomParticipant.LayerControl.LayerCanvas.Height = this.MapVC.Ink.Height;
							DictItemRoomParticipant.LayerControl.LayerCanvas.PreviewStylusDown += this.HandleOnInkCanvasStyusDown;
							DictItemRoomParticipant.LayerControl.LayerCanvas.PreviewMouseDown += this.HandleOnInkCanvasMosueDown;
							DictItemRoomParticipant.LayerControl.LayerCanvas.Strokes.StrokesChanged += this.HandleACSStrokesChanged;
						}
					}
					catch (Exception ex)
					{
						textBlock = null;
					}
					return textBlock;
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001C71C File Offset: 0x0001A91C
		private void HandleRoomParticipantDeleted(Guid RoomGUID, Guid SessionGUID)
		{
			try
			{
				RoomDictionaryItem DictItemRoom = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == RoomGUID).Value;
				RoomParticipantDictionaryItem DictItemRoomParticipant = DictItemRoom.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == SessionGUID).Value;
				base.Dispatcher.Invoke(delegate()
				{
					try
					{
						if (DictItemRoomParticipant.Control.Parent != null)
						{
							StackPanel stackPanel = DictItemRoomParticipant.Control.Parent as StackPanel;
							if (stackPanel != null)
							{
								stackPanel.Children.Remove(DictItemRoomParticipant.Control);
							}
						}
						if (SessionGUID.Equals(this.MyACSHelper.SessionGUID))
						{
							(DictItemRoom.RoomControl.ButtonJoin.Content as TextBlock).Text = "MONITOR";
							if (DictItemRoomParticipant.LayerControl != null && this.pnlInkLayers.Children.Contains(DictItemRoomParticipant.LayerControl.LayerPanel))
							{
								DictItemRoomParticipant.LayerControl.LayerCheckbox.Checked -= this.HandleLayerToggle;
								DictItemRoomParticipant.LayerControl.LayerCheckbox.Unchecked -= this.HandleLayerToggle;
								DictItemRoomParticipant.LayerControl.LayerButton.Click -= this.SetActivateInkLayer;
								this.pnlInkLayers.Children.Remove(DictItemRoomParticipant.LayerControl.LayerPanel);
								if (DictItemRoomParticipant.LayerControl.LayerCanvas != null)
								{
									this.MapVC.LayerHost.Children.Remove(DictItemRoomParticipant.LayerControl.LayerCanvas);
									DictItemRoomParticipant.LayerControl.LayerCanvas.PreviewStylusDown -= this.HandleOnInkCanvasStyusDown;
									DictItemRoomParticipant.LayerControl.LayerCanvas.PreviewMouseDown -= this.HandleOnInkCanvasMosueDown;
									DictItemRoomParticipant.LayerControl.LayerCanvas.Strokes.StrokesChanged -= this.HandleACSStrokesChanged;
									if (this.InkLayerActive != null && this.InkLayerActive.Equals(DictItemRoomParticipant.LayerControl.LayerCanvas))
									{
										this.SetActivateInkLayer(null, new RoutedEventArgs(null, new System.Windows.Controls.Button
										{
											Tag = "2"
										}));
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
					}
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleRoomPermissionCreated(Guid RoomGUID, Guid SessionGUID)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleRoomPermissionDeleted(Guid RoomGUID, Guid SessionGUID)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		private void HandleSessionCreated(Guid SessionGUID)
		{
			try
			{
				SessionDictionaryItem DictItem = this.MyACSHelper.DictionaryOfSessions.SingleOrDefault((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Key == SessionGUID).Value;
				DictItem.Control = base.Dispatcher.Invoke<TextBlock>(delegate()
				{
					TextBlock textBlock = new TextBlock();
					try
					{
						textBlock.Text = DictItem.Session.Callsign + " (" + DictItem.Session.WorldDisplayName + ")";
						this.pnlSessions.Children.Add(textBlock);
					}
					catch (Exception ex)
					{
						textBlock = null;
					}
					return textBlock;
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001C888 File Offset: 0x0001AA88
		private void HandleSessionDeleted(Guid SessionGUID)
		{
			SessionDictionaryItem DictItem = this.MyACSHelper.DictionaryOfSessions.SingleOrDefault((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Key == SessionGUID).Value;
			base.Dispatcher.Invoke(delegate()
			{
				try
				{
					this.pnlSessions.Children.Remove(DictItem.Control);
				}
				catch (Exception ex)
				{
				}
			});
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		private void HandleSessionUpdated(Guid SessionGUID)
		{
			SessionDictionaryItem DictItem = this.MyACSHelper.DictionaryOfSessions.SingleOrDefault((KeyValuePair<Guid, SessionDictionaryItem> x) => x.Key == SessionGUID).Value;
			base.Dispatcher.Invoke(delegate()
			{
				DictItem.Control.Text = DictItem.Session.Callsign + " (" + DictItem.Session.WorldDisplayName + ")";
			});
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleSssionValidated(Guid SessionGUID)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001C948 File Offset: 0x0001AB48
		private void HandleStrokeCreated(Guid RoomGUID, Guid StrokeGUID, StrokeCollection StrokeCollection)
		{
			MainWindow._Closure$__374-0 CS$<>8__locals1 = new MainWindow._Closure$__374-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_RoomGUID = RoomGUID;
			CS$<>8__locals1.$VB$Local_StrokeGUID = StrokeGUID;
			try
			{
				MainWindow._Closure$__374-1 CS$<>8__locals2 = new MainWindow._Closure$__374-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
				RoomDictionaryItem value = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RoomGUID).Value;
				CS$<>8__locals2.$VB$Local_DictItemRoomPart = value.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
				if (this.Map_Current != null && this.Map_Current.WorldName.Equals(value.Room.WorldName))
				{
					try
					{
						IEnumerator<Stroke> enumerator = StrokeCollection.GetEnumerator();
						while (enumerator.MoveNext())
						{
							MainWindow._Closure$__374-2 CS$<>8__locals3 = new MainWindow._Closure$__374-2(CS$<>8__locals3);
							CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3 = CS$<>8__locals2;
							CS$<>8__locals3.$VB$Local_Stroke = enumerator.Current;
							base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
							{
								List<Stroke> list = CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DictItemRoomPart.LayerControl.LayerCanvas.Strokes.Where((CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$NonLocal_$VB$Closure_2.$I3 == null) ? (CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$NonLocal_$VB$Closure_2.$I3 = ((Stroke x) => x.ContainsPropertyData(CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$NonLocal_$VB$Closure_2.$VB$Local_StrokeGUID))) : CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$NonLocal_$VB$Closure_2.$I3).ToList<Stroke>();
								if (list == null || list.Count == 0)
								{
									CS$<>8__locals3.$VB$NonLocal_$VB$Closure_3.$VB$Local_DictItemRoomPart.LayerControl.LayerCanvas.Strokes.Add(CS$<>8__locals3.$VB$Local_Stroke);
								}
							}), new object[0]);
						}
					}
					finally
					{
						IEnumerator<Stroke> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		private void HandleStrokeDeleted(Guid RoomGUID, Guid StrokeGUID, Guid DeleterGUID)
		{
			MainWindow._Closure$__375-0 CS$<>8__locals1 = new MainWindow._Closure$__375-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_RoomGUID = RoomGUID;
			CS$<>8__locals1.$VB$Local_StrokeGUID = StrokeGUID;
			CS$<>8__locals1.$VB$Local_DeleterGUID = DeleterGUID;
			try
			{
				MainWindow._Closure$__375-1 CS$<>8__locals2 = new MainWindow._Closure$__375-1(CS$<>8__locals2);
				CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2 = CS$<>8__locals1;
				RoomDictionaryItem value = this.MyACSHelper.DictionaryOfRooms.SingleOrDefault((KeyValuePair<Guid, RoomDictionaryItem> x) => x.Key == CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_RoomGUID).Value;
				CS$<>8__locals2.$VB$Local_DictItemRoomPart = value.ParticipantDictionary.SingleOrDefault((KeyValuePair<Guid, RoomParticipantDictionaryItem> x) => x.Key == this.MyACSHelper.SessionGUID).Value;
				if (this.Map_Current != null && this.Map_Current.WorldName.Equals(value.Room.WorldName) && !this.MyACSHelper.SessionGUID.Equals(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_DeleterGUID))
				{
					base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						try
						{
							List<Stroke> list = CS$<>8__locals2.$VB$Local_DictItemRoomPart.LayerControl.LayerCanvas.Strokes.Where((CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$I3 == null) ? (CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$I3 = ((Stroke x) => x.ContainsPropertyData(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_StrokeGUID))) : CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$I3).ToList<Stroke>();
							if (list != null)
							{
								try
								{
									foreach (Stroke stroke in list)
									{
										stroke.AddPropertyData(CS$<>8__locals2.$VB$NonLocal_$VB$Closure_2.$VB$Local_DeleterGUID, "deleterguid");
										CS$<>8__locals2.$VB$Local_DictItemRoomPart.LayerControl.LayerCanvas.Strokes.Remove(stroke);
									}
								}
								finally
								{
									List<Stroke>.Enumerator enumerator;
									((IDisposable)enumerator).Dispose();
								}
							}
						}
						catch (Exception ex)
						{
						}
					}), new object[0]);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600042D RID: 1069 RVA: 0x0001CB84 File Offset: 0x0001AD84
		// (remove) Token: 0x0600042E RID: 1070 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		public event MainWindow.OnOrbatUpdateRequestedEventHandler OnOrbatUpdateRequested;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600042F RID: 1071 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		// (remove) Token: 0x06000430 RID: 1072 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public event MainWindow.OnOrbatFollowUnitSelectedEventHandler OnOrbatFollowUnitSelected;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000431 RID: 1073 RVA: 0x0001CC64 File Offset: 0x0001AE64
		// (remove) Token: 0x06000432 RID: 1074 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		public event MainWindow.OnOrbatLocateUnitSelectedEventHandler OnOrbatLocateUnitSelected;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000433 RID: 1075 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		// (remove) Token: 0x06000434 RID: 1076 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public event MainWindow.OnOrbatTrackUnitEventHandler OnOrbatTrackUnit;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000435 RID: 1077 RVA: 0x0001CD44 File Offset: 0x0001AF44
		// (remove) Token: 0x06000436 RID: 1078 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		public event MainWindow.OnOrbatFollowGroupSelectedEventHandler OnOrbatFollowGroupSelected;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000437 RID: 1079 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		// (remove) Token: 0x06000438 RID: 1080 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public event MainWindow.OnOrbatLocateGroupSelectedEventHandler OnOrbatLocateGroupSelected;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000439 RID: 1081 RVA: 0x0001CE24 File Offset: 0x0001B024
		// (remove) Token: 0x0600043A RID: 1082 RVA: 0x0001CE5C File Offset: 0x0001B05C
		public event MainWindow.OnOrbatTrackGroupEventHandler OnOrbatTrackGroup;

		// Token: 0x0600043B RID: 1083 RVA: 0x0001CE94 File Offset: 0x0001B094
		private void HandleOrbatFollowUnitSelected(Unit SelectedUnit)
		{
			if (SelectedUnit != null && this.UnitList.Items != null)
			{
				try
				{
					try
					{
						foreach (object obj in ((IEnumerable)this.UnitList.Items))
						{
							ComboBoxItem comboBoxItem = (ComboBoxItem)obj;
							if (Operators.CompareString(comboBoxItem.Tag.ToString(), SelectedUnit.Name.ToString(), false) == 0)
							{
								comboBoxItem.IsSelected = true;
								MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
								if (user_Initiated_FollowChangeEvent != null)
								{
									user_Initiated_FollowChangeEvent(true);
								}
								Common.ConvertPos(this.Map_Current.WorldSize, SelectedUnit.PosX, SelectedUnit.PosY, ref SelectedUnit.CanvasX, ref SelectedUnit.CanvasY);
								this.MapVC.SetHorizontalOffset(SelectedUnit.CanvasX * this.ZoomFactor - this.MapScroll.ViewportWidth / 2.0);
								this.MapVC.SetVerticalOffset(SelectedUnit.CanvasY * this.ZoomFactor - this.MapScroll.ViewportHeight / 2.0);
								this.MapVC.StartUpdate();
								break;
							}
						}
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001D008 File Offset: 0x0001B208
		private void HandleOrbatLocateUnitSelected(Unit SelectedUnit)
		{
			MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
			if (user_Initiated_FollowChangeEvent != null)
			{
				user_Initiated_FollowChangeEvent(false);
			}
			Common.ConvertPos(this.Map_Current.WorldSize, SelectedUnit.PosX, SelectedUnit.PosY, ref SelectedUnit.CanvasX, ref SelectedUnit.CanvasY);
			this.MapVC.SetHorizontalOffset(SelectedUnit.CanvasX * this.ZoomFactor - this.MapScroll.ViewportWidth / 2.0);
			this.MapVC.SetVerticalOffset(SelectedUnit.CanvasY * this.ZoomFactor - this.MapScroll.ViewportHeight / 2.0);
			this.MapVC.StartUpdate();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		private void HandleOrbatTrackGroup(Group Group)
		{
			if (this.TrackingGroup == null)
			{
				this.TrackingGroup = Group;
			}
			else if (Operators.CompareString(this.TrackingGroup.Name, Group.Name, false) == 0)
			{
				this.TrackingGroup = null;
				this.TrackingGroupLine.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.TrackingGroup = Group;
			}
			if (this.TrackingGroup != null && this.Map_Current != null)
			{
				Unit followedUnit = this.GetFollowedUnit();
				if (followedUnit != null)
				{
					Unit groupLeader = this.GetGroupLeader(this.TrackingGroup);
					if (groupLeader != null)
					{
						if (groupLeader.CanvasX == -1.0)
						{
							Common.ConvertPos(this.Map_Current.WorldSize, groupLeader.PosX, groupLeader.PosY, ref groupLeader.CanvasX, ref groupLeader.CanvasY);
						}
						if (followedUnit.CanvasX == -1.0)
						{
							Common.ConvertPos(this.Map_Current.WorldSize, followedUnit.PosX, followedUnit.PosY, ref followedUnit.CanvasX, ref followedUnit.CanvasY);
						}
						Point point = new Point(followedUnit.CanvasX, followedUnit.CanvasY);
						Point point2 = new Point(groupLeader.CanvasX, groupLeader.CanvasY);
						Color green = Colors.Green;
						TextBlock lblTrackingGroup = this.lblTrackingGroup;
						this.UpdateTrackingLine(ref this.TrackingGroupLine, ref point, ref point2, ref green, ref lblTrackingGroup);
						this.lblTrackingGroup = lblTrackingGroup;
					}
				}
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001D208 File Offset: 0x0001B408
		private void HandleOrbatTrackUnit(Unit Unit)
		{
			if (this.TrackingUnit == null)
			{
				this.TrackingUnit = Unit;
			}
			else if (Operators.CompareString(this.TrackingUnit.Name, Unit.Name, false) == 0)
			{
				this.TrackingUnit = null;
				this.TrackingUnitLine.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.TrackingUnit = Unit;
			}
			if (this.TrackingUnit != null && this.Map_Current != null)
			{
				Unit followedUnit = this.GetFollowedUnit();
				if (followedUnit != null)
				{
					if (this.TrackingUnit.CanvasX == -1.0)
					{
						Common.ConvertPos(this.Map_Current.WorldSize, this.TrackingUnit.PosX, this.TrackingUnit.PosY, ref this.TrackingUnit.CanvasX, ref this.TrackingUnit.CanvasY);
					}
					if (followedUnit.CanvasX == -1.0)
					{
						Common.ConvertPos(this.Map_Current.WorldSize, followedUnit.PosX, followedUnit.PosY, ref followedUnit.CanvasX, ref followedUnit.CanvasY);
					}
					Point point = new Point(followedUnit.CanvasX, followedUnit.CanvasY);
					Point point2 = new Point(this.TrackingUnit.CanvasX, this.TrackingUnit.CanvasY);
					Color blue = Colors.Blue;
					TextBlock lblTrackingUnit = this.lblTrackingUnit;
					this.UpdateTrackingLine(ref this.TrackingUnitLine, ref point, ref point2, ref blue, ref lblTrackingUnit);
					this.lblTrackingUnit = lblTrackingUnit;
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001D364 File Offset: 0x0001B564
		public void ClearORBAT()
		{
			this.mySelectedBlock = null;
			if (this.pnlUnitOptions.Parent != null)
			{
				System.Windows.Controls.Panel panel = (System.Windows.Controls.Panel)this.pnlUnitOptions.Parent;
				if (panel != null)
				{
					panel.Children.Remove(this.pnlUnitOptions);
				}
			}
			this.pnlUnitOptions.Visibility = Visibility.Collapsed;
			foreach (StackPanel stackPanel in new StackPanel[]
			{
				this.pnlCiv,
				this.pnlWest,
				this.pnlEast,
				this.pnlGuer
			})
			{
				if (stackPanel != null && stackPanel.Children != null && stackPanel.Children.Count != 0)
				{
					try
					{
						foreach (object obj in stackPanel.Children)
						{
							UIElement uielement = (UIElement)obj;
							if (uielement.GetType() == typeof(System.Windows.Controls.Label))
							{
								System.Windows.Controls.Label label = uielement as System.Windows.Controls.Label;
								if (label != null)
								{
									label.GotFocus -= this.HandleLabelSelection;
									label.MouseDown -= new MouseButtonEventHandler(this.HandleLabelClick);
								}
							}
						}
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
					stackPanel.Children.Clear();
				}
			}
			this.pnlCiv.Children.Add(this.pnlUnitOptions);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public StackPanel GetSideStackPanel(ref Enums.Side SideID)
		{
			StackPanel result = null;
			switch (SideID)
			{
			case Enums.Side.UNDEFINED:
			case Enums.Side.CIV:
			case Enums.Side.UNKNOWN:
				result = this.pnlCiv;
				break;
			case Enums.Side.EAST:
				result = this.pnlEast;
				break;
			case Enums.Side.WEST:
				result = this.pnlWest;
				break;
			case Enums.Side.GUER:
				result = this.pnlGuer;
				break;
			}
			return result;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001D534 File Offset: 0x0001B734
		public StackPanel CreateGroupPanel(ref Group Group)
		{
			StackPanel result;
			if (Group == null)
			{
				result = null;
			}
			else
			{
				StackPanel stackPanel = new StackPanel();
				stackPanel.Children.Add(new Separator
				{
					Height = 10.0
				});
				System.Windows.Controls.Label label = new System.Windows.Controls.Label();
				label.FontSize = 14.0;
				label.FontWeight = FontWeights.Bold;
				label.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				label.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
				label.IsHitTestVisible = true;
				label.IsTabStop = true;
				label.Focusable = true;
				label.Cursor = System.Windows.Input.Cursors.Hand;
				label.GotFocus += this.HandleLabelSelection;
				label.MouseDown += new MouseButtonEventHandler(this.HandleLabelClick);
				label.Content = Group.DisplayName;
				label.Tag = Group;
				stackPanel.Children.Add(label);
				this.GetSideStackPanel(ref Group.SideID).Children.Add(stackPanel);
				result = stackPanel;
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001D674 File Offset: 0x0001B874
		public void UpdateGroupPanel(ref StackPanel GroupPanel, ref Group Group)
		{
			if (GroupPanel != null && Group != null)
			{
				try
				{
					if (GroupPanel.Children.Count >= 2)
					{
						System.Windows.Controls.Label label = GroupPanel.Children[1] as System.Windows.Controls.Label;
						if (label != null)
						{
							label.Content = Group.DisplayName;
							label.Tag = Group;
						}
					}
					StackPanel stackPanel = GroupPanel.Parent as StackPanel;
					if (stackPanel != null)
					{
						StackPanel sideStackPanel = this.GetSideStackPanel(ref Group.SideID);
						if (!sideStackPanel.Equals(stackPanel))
						{
							stackPanel.Children.Remove(GroupPanel);
							sideStackPanel.Children.Add(GroupPanel);
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001D730 File Offset: 0x0001B930
		public void RemoveGroupPanel(ref StackPanel GroupPanel)
		{
			if (GroupPanel != null && GroupPanel.Parent != null)
			{
				try
				{
					if (GroupPanel.Children.Count >= 2)
					{
						System.Windows.Controls.Label label = GroupPanel.Children[1] as System.Windows.Controls.Label;
						if (label != null)
						{
							label.GotFocus -= this.HandleLabelSelection;
							label.MouseDown -= new MouseButtonEventHandler(this.HandleLabelClick);
						}
					}
				}
				catch (Exception ex)
				{
				}
				try
				{
					StackPanel stackPanel = GroupPanel.Parent as StackPanel;
					if (stackPanel != null)
					{
						stackPanel.Children.Remove(GroupPanel);
					}
				}
				catch (Exception ex2)
				{
				}
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
		public System.Windows.Controls.Label CreateUnitLabel(ref StackPanel GroupPanel, ref Unit Unit, bool UnitIsGroupLeader)
		{
			System.Windows.Controls.Label result;
			if (GroupPanel == null)
			{
				result = null;
			}
			else if (Unit == null)
			{
				result = null;
			}
			else
			{
				System.Windows.Controls.Label label = new System.Windows.Controls.Label();
				label.FontSize = 12.0;
				label.Margin = new Thickness(10.0, 0.0, 0.0, 5.0);
				label.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
				label.IsHitTestVisible = true;
				label.IsTabStop = true;
				label.Focusable = true;
				label.Cursor = System.Windows.Input.Cursors.Hand;
				label.GotFocus += this.HandleLabelSelection;
				label.MouseDown += new MouseButtonEventHandler(this.HandleLabelClick);
				label.Content = Common.CreateUnitString(ref Unit, true, UnitIsGroupLeader);
				if (!string.IsNullOrWhiteSpace(Unit.SteamID))
				{
					label.FontStyle = FontStyles.Italic;
				}
				label.Tag = Unit;
				GroupPanel.Children.Add(label);
				result = label;
			}
			return result;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001D914 File Offset: 0x0001BB14
		public void UpdateUnitLabel(ref System.Windows.Controls.Label UnitLabel, ref Unit Unit, bool UnitIsGroupLeader)
		{
			if (UnitLabel != null && Unit != null)
			{
				try
				{
					UnitLabel.Content = Common.CreateUnitString(ref Unit, true, UnitIsGroupLeader);
					UnitLabel.Tag = Unit;
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001D964 File Offset: 0x0001BB64
		public void RemoveUnitLabel(ref System.Windows.Controls.Label UnitLabel)
		{
			if (UnitLabel != null && UnitLabel.Parent != null)
			{
				try
				{
					UnitLabel.GotFocus -= this.HandleLabelSelection;
					UnitLabel.MouseDown -= new MouseButtonEventHandler(this.HandleLabelClick);
				}
				catch (Exception ex)
				{
				}
				try
				{
					StackPanel stackPanel = UnitLabel.Parent as StackPanel;
					if (stackPanel != null)
					{
						stackPanel.Children.Remove(UnitLabel);
					}
				}
				catch (Exception ex2)
				{
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001DA04 File Offset: 0x0001BC04
		private void HandleLabelClick(object Sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Label label = Sender as System.Windows.Controls.Label;
			if (label != null)
			{
				label.Focus();
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001DA24 File Offset: 0x0001BC24
		private void HandleLabelSelection(object sender, RoutedEventArgs e)
		{
			this.mySelectedBlock = (sender as System.Windows.Controls.Label);
			checked
			{
				if (this.mySelectedBlock != null)
				{
					if ((this.pnlUnitOptions.Parent != null & this.mySelectedBlock.Parent != null) && this.pnlUnitOptions.Parent.Equals(this.mySelectedBlock.Parent))
					{
						StackPanel stackPanel = this.pnlUnitOptions.Parent as StackPanel;
						if (stackPanel != null && stackPanel.Children.IndexOf(this.pnlUnitOptions) == stackPanel.Children.IndexOf(this.mySelectedBlock) + 1)
						{
							if (this.pnlUnitOptions.Visibility == Visibility.Collapsed)
							{
								this.pnlUnitOptions.Visibility = Visibility.Visible;
								return;
							}
							this.pnlUnitOptions.Visibility = Visibility.Collapsed;
							return;
						}
					}
					if (this.pnlUnitOptions.Parent != null)
					{
						System.Windows.Controls.Panel panel = (System.Windows.Controls.Panel)this.pnlUnitOptions.Parent;
						if (panel != null)
						{
							panel.Children.Remove(this.pnlUnitOptions);
						}
					}
					StackPanel stackPanel2 = this.mySelectedBlock.Parent as StackPanel;
					if (stackPanel2 != null)
					{
						stackPanel2.Children.Insert(stackPanel2.Children.IndexOf(this.mySelectedBlock) + 1, this.pnlUnitOptions);
					}
					this.pnlUnitOptions.Visibility = Visibility.Visible;
					this.pnlUnitOptions.Margin = this.mySelectedBlock.Margin;
				}
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001DB74 File Offset: 0x0001BD74
		private void DoFollow()
		{
			if (this.mySelectedBlock != null && this.mySelectedBlock.Tag != null)
			{
				Type type = this.mySelectedBlock.Tag.GetType();
				if (type == typeof(Group))
				{
					Group group = this.mySelectedBlock.Tag as Group;
					if (group != null)
					{
						MainWindow.OnOrbatFollowGroupSelectedEventHandler onOrbatFollowGroupSelectedEvent = this.OnOrbatFollowGroupSelectedEvent;
						if (onOrbatFollowGroupSelectedEvent != null)
						{
							onOrbatFollowGroupSelectedEvent(group);
							return;
						}
					}
				}
				else if (type == typeof(Unit))
				{
					Unit unit = this.mySelectedBlock.Tag as Unit;
					if (unit != null)
					{
						MainWindow.OnOrbatFollowUnitSelectedEventHandler onOrbatFollowUnitSelectedEvent = this.OnOrbatFollowUnitSelectedEvent;
						if (onOrbatFollowUnitSelectedEvent != null)
						{
							onOrbatFollowUnitSelectedEvent(unit);
						}
					}
				}
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001DC20 File Offset: 0x0001BE20
		private void DoLocate()
		{
			if (this.mySelectedBlock != null && this.mySelectedBlock.Tag != null)
			{
				Type type = this.mySelectedBlock.Tag.GetType();
				if (type == typeof(Group))
				{
					Group group = this.mySelectedBlock.Tag as Group;
					if (group != null && !string.IsNullOrEmpty(group.LeaderNetID) && Common.DictionaryOfUnits != null)
					{
						DictionaryOfUnitItem dictionaryOfUnitItem = null;
						Common.DictionaryOfUnits.TryGetValue(group.LeaderNetID, out dictionaryOfUnitItem);
						if (dictionaryOfUnitItem != null && dictionaryOfUnitItem.Unit != null)
						{
							MainWindow.OnOrbatLocateUnitSelectedEventHandler onOrbatLocateUnitSelectedEvent = this.OnOrbatLocateUnitSelectedEvent;
							if (onOrbatLocateUnitSelectedEvent != null)
							{
								onOrbatLocateUnitSelectedEvent(dictionaryOfUnitItem.Unit);
							}
						}
						dictionaryOfUnitItem = null;
						return;
					}
				}
				else if (type == typeof(Unit))
				{
					Unit unit = this.mySelectedBlock.Tag as Unit;
					if (unit != null)
					{
						MainWindow.OnOrbatLocateUnitSelectedEventHandler onOrbatLocateUnitSelectedEvent2 = this.OnOrbatLocateUnitSelectedEvent;
						if (onOrbatLocateUnitSelectedEvent2 != null)
						{
							onOrbatLocateUnitSelectedEvent2(unit);
						}
					}
				}
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001DD0C File Offset: 0x0001BF0C
		private void DoTrack()
		{
			if (this.mySelectedBlock != null && this.mySelectedBlock.Tag != null)
			{
				Type type = this.mySelectedBlock.Tag.GetType();
				if (type == typeof(Group))
				{
					Group group = this.mySelectedBlock.Tag as Group;
					if (group != null)
					{
						MainWindow.OnOrbatTrackGroupEventHandler onOrbatTrackGroupEvent = this.OnOrbatTrackGroupEvent;
						if (onOrbatTrackGroupEvent != null)
						{
							onOrbatTrackGroupEvent(group);
							return;
						}
					}
				}
				else if (type == typeof(Unit))
				{
					Unit unit = this.mySelectedBlock.Tag as Unit;
					if (unit != null)
					{
						MainWindow.OnOrbatTrackUnitEventHandler onOrbatTrackUnitEvent = this.OnOrbatTrackUnitEvent;
						if (onOrbatTrackUnitEvent != null)
						{
							onOrbatTrackUnitEvent(unit);
						}
					}
				}
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		private void PageUp()
		{
			ScrollViewer scrollViewer = null;
			if (this.tabOrbatCiv.IsSelected)
			{
				scrollViewer = this.scrollCiv;
			}
			if (this.tabOrbatWest.IsSelected)
			{
				scrollViewer = this.scrollWest;
			}
			if (this.tabOrbatEast.IsSelected)
			{
				scrollViewer = this.scrollEast;
			}
			if (this.tabOrbatGuer.IsSelected)
			{
				scrollViewer = this.scrollGuer;
			}
			if (scrollViewer != null)
			{
				scrollViewer.PageUp();
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001DE20 File Offset: 0x0001C020
		private void PageDown()
		{
			ScrollViewer scrollViewer = null;
			if (this.tabOrbatCiv.IsSelected)
			{
				scrollViewer = this.scrollCiv;
			}
			if (this.tabOrbatWest.IsSelected)
			{
				scrollViewer = this.scrollWest;
			}
			if (this.tabOrbatEast.IsSelected)
			{
				scrollViewer = this.scrollEast;
			}
			if (this.tabOrbatGuer.IsSelected)
			{
				scrollViewer = this.scrollGuer;
			}
			if (scrollViewer != null)
			{
				scrollViewer.PageDown();
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001DE88 File Offset: 0x0001C088
		private void btnToolNoteMap_Click(object sender, RoutedEventArgs e)
		{
			if (this.Map_Current != null)
			{
				if (this.Tool.Current == Enums.ActiveToolEnum.MapNote)
				{
					this.Tool.Current = Enums.ActiveToolEnum.None;
					return;
				}
				this.Tool.Current = Enums.ActiveToolEnum.MapNote;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001DEBC File Offset: 0x0001C0BC
		private void ShowNoteTool(double PosX, double PosY)
		{
			Note element = new Note();
			Canvas.SetLeft(element, PosX - 10.0 * this.ZoomFactor);
			Canvas.SetTop(element, PosY - 15.0 * this.ZoomFactor);
			System.Windows.Controls.Panel.SetZIndex(element, 1000);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void Tool_ToolChanged(Enums.ActiveToolEnum Previous, Enums.ActiveToolEnum Current)
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001DF08 File Offset: 0x0001C108
		private void MainWindow_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (this.Map_Current != null && this.Tool_Visible)
			{
				switch (this.Tool.Current)
				{
				case Enums.ActiveToolEnum.MarkerCircle:
				{
					Key key = e.Key;
					if (key == Key.Return)
					{
						this.Tool_Visible = false;
						return;
					}
					if (key == Key.Escape)
					{
						this.Tool_Visible = false;
						return;
					}
					break;
				}
				case Enums.ActiveToolEnum.MarkerIcon:
				{
					Key key2 = e.Key;
					if (key2 != Key.Return)
					{
						if (key2 == Key.Escape)
						{
							this.MarkerToolIcon.Cancel();
							return;
						}
					}
					else
					{
						this.MarkerToolIcon.Save();
					}
					break;
				}
				case Enums.ActiveToolEnum.MarkerRect:
				{
					Key key3 = e.Key;
					if (key3 == Key.Return)
					{
						this.Tool_Visible = false;
						return;
					}
					if (key3 == Key.Escape)
					{
						this.Tool_Visible = false;
						return;
					}
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		private void MainWindow_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.Map_Current != null && this.MapScroll.IsMouseOver && e.LeftButton == MouseButtonState.Pressed)
			{
				try
				{
					double x = Mouse.GetPosition(this.MapScroll).X;
					double y = Mouse.GetPosition(this.MapScroll).Y;
					switch (this.Tool.Current)
					{
					case Enums.ActiveToolEnum.MarkerCircle:
						if (!this.Tool_Visible)
						{
							this.Tool_Visible = true;
							e.Handled = true;
						}
						break;
					case Enums.ActiveToolEnum.MarkerIcon:
						if (!this.Tool_Visible)
						{
						}
						break;
					case Enums.ActiveToolEnum.MarkerRect:
						if (!this.Tool_Visible)
						{
							this.Tool_Visible = true;
							e.Handled = true;
						}
						break;
					case Enums.ActiveToolEnum.MarkerText:
						this.ShowNoteTool(x, y);
						break;
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001E09C File Offset: 0x0001C29C
		private void Mode_Online_Button_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.User_Initiated_OnlineEventHandler user_Initiated_OnlineEvent = this.User_Initiated_OnlineEvent;
			if (user_Initiated_OnlineEvent != null)
			{
				user_Initiated_OnlineEvent();
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		private void Mode_Online_Follow_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
			if (user_Initiated_FollowChangeEvent != null)
			{
				user_Initiated_FollowChangeEvent(!this.Map_Scroll_Follow);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
		private void Mode_Offline_Button_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.User_Initiated_OfflineEventHandler user_Initiated_OfflineEvent = this.User_Initiated_OfflineEvent;
			if (user_Initiated_OfflineEvent != null)
			{
				user_Initiated_OfflineEvent();
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001E104 File Offset: 0x0001C304
		private void Mode_Record_Start_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.User_Initiated_RecordStartEventHandler user_Initiated_RecordStartEvent = this.User_Initiated_RecordStartEvent;
			if (user_Initiated_RecordStartEvent != null)
			{
				user_Initiated_RecordStartEvent();
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001E124 File Offset: 0x0001C324
		private void Mode_Record_Stop_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.User_Initiated_RecordStopEventHandler user_Initiated_RecordStopEvent = this.User_Initiated_RecordStopEvent;
			if (user_Initiated_RecordStopEvent != null)
			{
				user_Initiated_RecordStopEvent();
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001E144 File Offset: 0x0001C344
		private void GoOnline()
		{
			this.MenuPlayStart.IsEnabled = false;
			this.MenuPlayStop.IsEnabled = true;
			this.Mode_Online_Button.IsEnabled = false;
			this.Mode_Offline_Button.IsEnabled = true;
			this.Status_Relay = Enums.RelayStatus.Disconnecting;
			if (this.Status_Source == Enums.SourceStatus.Recording)
			{
				this.StartPlaybackCycle();
				this.DisableFrameControls();
				return;
			}
			this.Status_Source = Enums.SourceStatus.Relay;
			this.Status_Relay = Enums.RelayStatus.Connecting;
			this.StartIO();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		private void GoOffline()
		{
			this.MenuPlayStart.IsEnabled = true;
			this.MenuPlayStop.IsEnabled = false;
			this.Mode_Online_Button.IsEnabled = true;
			this.Mode_Offline_Button.IsEnabled = false;
			switch (this.Status_Source)
			{
			case Enums.SourceStatus.Offline:
				this.StopIO();
				this.HideAll();
				return;
			case Enums.SourceStatus.ACS:
				this.StopIO();
				this.HideAll();
				return;
			case Enums.SourceStatus.Relay:
				this.Status_Source = Enums.SourceStatus.Offline;
				this.Status_Relay = Enums.RelayStatus.Disconnecting;
				this.StopIO();
				this.HideAll();
				return;
			case Enums.SourceStatus.Recording:
				this.StopPlaybackCycle();
				this.EnableFrameControls();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001E254 File Offset: 0x0001C454
		private void ChangeFollow(bool ShouldFollow)
		{
			if (ShouldFollow)
			{
				this.Map_Scroll_Follow = true;
				this.Mode_Online_Follow.Content = "UNFOLLOW";
				this.MenuTrackPlayer.IsChecked = true;
				return;
			}
			this.Map_Scroll_Follow = false;
			this.Mode_Online_Follow.Content = "FOLLOW";
			this.MenuTrackPlayer.IsChecked = false;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001E2AC File Offset: 0x0001C4AC
		private void ToggleTrack_Click(object Sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Controls.MenuItem menuItem = e.Source as System.Windows.Controls.MenuItem;
				if (menuItem.IsCheckable)
				{
					MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
					if (user_Initiated_FollowChangeEvent != null)
					{
						user_Initiated_FollowChangeEvent(menuItem.IsChecked);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001E308 File Offset: 0x0001C508
		private void RecordStart()
		{
			this.Mode_Record_Start.IsEnabled = false;
			this.Mode_Record_Stop.IsEnabled = true;
			this.MenuRecordingStart.IsEnabled = false;
			this.MenuRecordingStop.IsEnabled = true;
			this.PlaybackHelper.DeleteFiles(this.DIR_Record_Live);
			this.Rec_IsRecording = true;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001E360 File Offset: 0x0001C560
		private void RecordStop()
		{
			this.Mode_Record_Start.IsEnabled = true;
			this.Mode_Record_Stop.IsEnabled = false;
			this.MenuRecordingStart.IsEnabled = true;
			this.MenuRecordingStop.IsEnabled = false;
			this.Rec_IsRecording = false;
			string text = this.GenerateLiveFileName();
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = new Data().Save(this.Frames_Recording, text);
				if (!string.IsNullOrEmpty(text2) && Operators.CompareString(text2.Trim().ToLower(), "ok", false) == 0)
				{
					this.PackageLiveFolder();
				}
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001E3ED File Offset: 0x0001C5ED
		private Map GetCurrentMap()
		{
			return this.Map_Current;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		private void handleMenuMap_Click(object sender, RoutedEventArgs e)
		{
			if (this.Status_Source == Enums.SourceStatus.Offline)
			{
				try
				{
					System.Windows.Controls.MenuItem SelectedItem = e.Source as System.Windows.Controls.MenuItem;
					if (SelectedItem != null)
					{
						Map map = this.Maps.Values.SingleOrDefault((Map x) => x.WorldName.Trim().Equals(SelectedItem.CommandParameter.ToString().Trim(), StringComparison.InvariantCultureIgnoreCase));
						if (map != null)
						{
							if (this.IsARoomOwner())
							{
								this.brdMapSwitch.Tag = map;
								this.brdMapSwitch.Visibility = Visibility.Visible;
							}
							else
							{
								MainWindow.OnUserInitiatedMapChangeEventHandler onUserInitiatedMapChangeEvent = this.OnUserInitiatedMapChangeEvent;
								if (onUserInitiatedMapChangeEvent != null)
								{
									onUserInitiatedMapChangeEvent(map);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		private void LoadMap(Map SelectedMap)
		{
			bool flag = false;
			if (SelectedMap != null && !string.IsNullOrEmpty(SelectedMap.WorldName))
			{
				if (this.Map_Current == null)
				{
					flag = true;
				}
				else if (Operators.CompareString(this.Map_Current.WorldName.Trim().ToLower(), SelectedMap.WorldName.Trim().ToLower(), false) != 0)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.IsMapReady = false;
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.HideAll();
					this.Map_Current = SelectedMap;
					this.imgBackground.Visibility = Visibility.Collapsed;
					this.imgBackgroundLogo.Visibility = Visibility.Collapsed;
					this.RemoveLocationShapes();
					this.RemoveMapLines();
					this.DisplayMap();
					this.ZoomLines = -1.0;
					this.ZoomLinesStep = -1;
					this.GenerateMapLines();
				}), new object[0]);
				this.ForceUpdate();
				this.IsMapReady = true;
				MainWindow.OnMapChangedEventHandler onMapChangedEvent = this.OnMapChangedEvent;
				if (onMapChangedEvent != null)
				{
					onMapChangedEvent();
				}
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001E56C File Offset: 0x0001C76C
		private void DisplayMap()
		{
			try
			{
				if (this.Map_Current != null)
				{
					string str = this.Map_Current.WorldName;
					if (!string.IsNullOrEmpty(this.Map_Current.DisplayName))
					{
						str = this.Map_Current.DisplayName;
					}
					this.ShowPleaseWait("Loading " + str);
					this.Zoom(1.0, false, true);
					MapHelper myMapHelper = this.myMapHelper;
					VirtualCanvas mapVC = this.MapVC;
					myMapHelper.DisplayMap(ref this.Map_Current, ref mapVC);
					this.MapVC = mapVC;
					this.ParseAnchorsFiles(ref this.Map_Current, ref this.Map_Current_Anchors);
					this.DrawMapAnchors();
					this.ParseLocationsFiles(ref this.Map_Current, ref this.Map_Current_Locations);
					this.DrawMapLocations();
					this.MapVC.SetHorizontalOffset(this.Map_Current.CenterX - this.MapScroll.ViewportWidth / 2.0);
					this.MapVC.SetVerticalOffset((double)this.Map_Current.WorldSize - this.Map_Current.CenterY - this.MapScroll.ViewportHeight / 2.0);
					this.MapVC.StartUpdate();
					base.UpdateLayout();
					this.DisplayAnchorsInOrbat();
					this.DisplayLocationsInOrbat();
					this.HidePleaseWait();
					if (this.InkLayerActive == null)
					{
						this.InkLayerActive = this.MapVC.Ink;
						this.SetActivateInkLayer(null, new RoutedEventArgs(null, new System.Windows.Controls.Button
						{
							Tag = "2"
						}));
					}
					if (this.InkLayerActive != null)
					{
						this.InkLayerActive.DefaultDrawingAttributes.Width = this.InkThickness.Value;
						this.InkLayerActive.DefaultDrawingAttributes.Height = this.InkThickness.Value;
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001E74C File Offset: 0x0001C94C
		private void SwitchRenderMode(object Sender, RoutedEventArgs E)
		{
			int num = -1;
			try
			{
				Type type = Sender.GetType();
				if (type == typeof(System.Windows.Controls.MenuItem))
				{
					System.Windows.Controls.MenuItem menuItem = Sender as System.Windows.Controls.MenuItem;
					if (menuItem != null && menuItem.Tag != null)
					{
						num = Convert.ToInt32(menuItem.Tag.ToString(), CultureInfo.InvariantCulture);
					}
				}
				else if (type == typeof(System.Windows.Controls.Button))
				{
					System.Windows.Controls.Button button = Sender as System.Windows.Controls.Button;
					if (button != null && button.Tag != null)
					{
						num = Convert.ToInt32(button.Tag.ToString(), CultureInfo.InvariantCulture);
					}
				}
			}
			catch (Exception ex)
			{
			}
			if (num != -1)
			{
				MainWindow.OnUserInitiatedMapRenderModeChangeEventHandler onUserInitiatedMapRenderModeChangeEvent = this.OnUserInitiatedMapRenderModeChangeEvent;
				if (onUserInitiatedMapRenderModeChangeEvent != null)
				{
					onUserInitiatedMapRenderModeChangeEvent((Enums.RenderMode)num);
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001E814 File Offset: 0x0001CA14
		private void HandleSwitchRenderMode(Enums.RenderMode Mode)
		{
			try
			{
				if (this.myMapHelper != null)
				{
					this.myMapHelper.RenderMode = Mode;
					if (this.Map_Current != null)
					{
						this.ShowPleaseWait("Switching render mode to " + Enum.GetName(typeof(Enums.RenderMode), this.myMapHelper.RenderMode));
						this.myMapHelper.RenderMode = this.myMapHelper.RenderMode;
						MapHelper myMapHelper = this.myMapHelper;
						VirtualCanvas mapVC = this.MapVC;
						myMapHelper.GenerateBackground(ref this.Map_Current, ref mapVC);
						this.MapVC = mapVC;
						this.HidePleaseWait();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001E8CC File Offset: 0x0001CACC
		private void Zoom(double ZoomFactorNew, bool UseMouse, bool UpdateSlider)
		{
			if (ZoomFactorNew < this.ZoomFactorMin)
			{
				ZoomFactorNew = this.ZoomFactorMin;
			}
			if (ZoomFactorNew > this.ZoomFactorMax)
			{
				ZoomFactorNew = this.ZoomFactorMax;
			}
			if (ZoomFactorNew != this.ZoomFactor)
			{
				Point point = default(Point);
				if (UseMouse)
				{
					point = Mouse.GetPosition(this.MapVC);
					point.X = this.MapScroll.HorizontalOffset + point.X;
					point.Y = this.MapScroll.VerticalOffset + point.Y;
				}
				else
				{
					point = new Point(this.MapScroll.HorizontalOffset + this.MapScroll.ViewportWidth / 2.0, this.MapScroll.VerticalOffset + this.MapScroll.ViewportHeight / 2.0);
				}
				point.X /= this.ZoomFactor;
				point.Y /= this.ZoomFactor;
				this.MapVC.Scale.ScaleX = ZoomFactorNew;
				this.MapVC.Scale.ScaleY = ZoomFactorNew;
				point.X *= ZoomFactorNew;
				point.Y *= ZoomFactorNew;
				double num = point.X - this.MapScroll.ViewportWidth / 2.0;
				double num2 = point.Y - this.MapScroll.ViewportHeight / 2.0;
				if (num < 0.0)
				{
					num = 0.0;
				}
				if (num2 < 0.0)
				{
					num2 = 0.0;
				}
				this.MapScroll.ScrollToHorizontalOffset(num);
				this.MapScroll.ScrollToVerticalOffset(num2);
			}
			this.DebugZoomLevel.Text = ZoomFactorNew.ToString() + "x";
			if (UpdateSlider)
			{
				this.slideZoom.Value = ZoomFactorNew;
			}
			this.ZoomFactor = ZoomFactorNew;
			this.GenerateMapLines();
			this.ZoomTimer.Stop();
			this.ZoomTimer.Start();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001EAD7 File Offset: 0x0001CCD7
		private void ZoomIn(bool UseMouse)
		{
			this.Zoom(Math.Round(this.ZoomFactor + this.ZoomStep, 1), UseMouse, true);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001EAF4 File Offset: 0x0001CCF4
		private void ZoomOut(bool UseMouse)
		{
			this.Zoom(Math.Round(this.ZoomFactor - this.ZoomStep, 1), UseMouse, true);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001EB11 File Offset: 0x0001CD11
		private void ZoomInMenu()
		{
			this.ZoomIn(false);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001EB1A File Offset: 0x0001CD1A
		private void ZoomOutMenu()
		{
			this.ZoomOut(false);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001EB23 File Offset: 0x0001CD23
		private void slideZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.Zoom(e.NewValue, false, false);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001EB33 File Offset: 0x0001CD33
		private void ZoomTimer_Tick(object sender, EventArgs e)
		{
			this.MapVC.StartUpdate();
			this.ZoomTimer.Stop();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001EB4B File Offset: 0x0001CD4B
		private void MapScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta > 0)
			{
				this.ZoomIn(false);
			}
			else
			{
				this.ZoomOut(false);
			}
			e.Handled = true;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001EB70 File Offset: 0x0001CD70
		private void HandleMapVCScaleChange(double Scale)
		{
			MainWindow.ScaleChangedEventHandler scaleChangedEvent = this.ScaleChangedEvent;
			if (scaleChangedEvent != null)
			{
				scaleChangedEvent(Scale, Scale);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001EB90 File Offset: 0x0001CD90
		private void Map_PreviewStylusDown(object sender, StylusDownEventArgs e)
		{
			Enums.ActiveToolEnum activeToolEnum = this.Tool.Current;
			if (activeToolEnum != Enums.ActiveToolEnum.Ink)
			{
				if (activeToolEnum == Enums.ActiveToolEnum.MapAnchor)
				{
					MainWindow.OnOrbatAnchorPosRetrievedEventHandler onOrbatAnchorPosRetrievedEvent = this.OnOrbatAnchorPosRetrievedEvent;
					if (onOrbatAnchorPosRetrievedEvent != null)
					{
						MainWindow.OnOrbatAnchorPosRetrievedEventHandler onOrbatAnchorPosRetrievedEventHandler = onOrbatAnchorPosRetrievedEvent;
						Point position = e.GetPosition(this.MapVC);
						ScrollViewer mapScroll = this.MapScroll;
						Point target = Common.ConvertMouseToCanvas(ref position, ref mapScroll, ref this.ZoomFactor);
						this.MapScroll = mapScroll;
						onOrbatAnchorPosRetrievedEventHandler(target);
					}
					this.Tool.Current = Enums.ActiveToolEnum.None;
					return;
				}
				this.pointStart = e.GetPosition(this);
				this.pointOffset.X = this.MapScroll.HorizontalOffset;
				this.pointOffset.Y = this.MapScroll.VerticalOffset;
				e.Handled = true;
			}
			else if (!MySettings.Default.UseTouchForInk & e.StylusDevice.TabletDevice.Type == TabletDeviceType.Touch)
			{
				this.pointStart = e.GetPosition(this);
				this.pointOffset.X = this.MapScroll.HorizontalOffset;
				this.pointOffset.Y = this.MapScroll.VerticalOffset;
				e.Handled = true;
				return;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
		private void MapScroll_StylusUp(object Sender, StylusEventArgs e)
		{
			this.pointStart.X = -1.0;
			this.pointStart.Y = -1.0;
			this.MapVC.StartUpdate();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001ECDC File Offset: 0x0001CEDC
		private void Map_StylusMove(object sender, StylusEventArgs e)
		{
			if (this.Tool.Current == Enums.ActiveToolEnum.Ink)
			{
				if (!MySettings.Default.UseTouchForInk & e.StylusDevice.TabletDevice.Type == TabletDeviceType.Touch)
				{
					this.HandleMove(e.GetPosition(this));
					e.Handled = true;
					return;
				}
			}
			else
			{
				this.HandleMove(e.GetPosition(this));
				e.Handled = true;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001ED44 File Offset: 0x0001CF44
		private void MapScroll_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this.Map_Current != null && e.StylusDevice == null && this.MapScroll.IsMouseOver)
			{
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					Enums.ActiveToolEnum activeToolEnum = this.Tool.Current;
					if (activeToolEnum != Enums.ActiveToolEnum.MarkerCircle)
					{
						if (activeToolEnum != Enums.ActiveToolEnum.MarkerRect)
						{
							if (activeToolEnum == Enums.ActiveToolEnum.MapAnchor)
							{
								MainWindow.OnOrbatAnchorPosRetrievedEventHandler onOrbatAnchorPosRetrievedEvent = this.OnOrbatAnchorPosRetrievedEvent;
								if (onOrbatAnchorPosRetrievedEvent != null)
								{
									MainWindow.OnOrbatAnchorPosRetrievedEventHandler onOrbatAnchorPosRetrievedEventHandler = onOrbatAnchorPosRetrievedEvent;
									Point position = e.GetPosition(this.MapVC);
									ScrollViewer mapScroll = this.MapScroll;
									Point target = Common.ConvertMouseToCanvas(ref position, ref mapScroll, ref this.ZoomFactor);
									this.MapScroll = mapScroll;
									onOrbatAnchorPosRetrievedEventHandler(target);
								}
								this.Tool.Current = Enums.ActiveToolEnum.None;
							}
						}
						else if (this.Tool_Visible)
						{
							this.Tool_Visible = false;
						}
					}
					else if (this.Tool_Visible)
					{
						this.Tool_Visible = false;
					}
				}
				if (e.RightButton == MouseButtonState.Pressed)
				{
					this.pointStart = e.GetPosition(this);
					this.pointOffset.X = this.MapScroll.HorizontalOffset;
					this.pointOffset.Y = this.MapScroll.VerticalOffset;
				}
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001EE48 File Offset: 0x0001D048
		private void Map_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (this.Tool_Visible && this.Map_Current != null)
			{
				Enums.ActiveToolEnum activeToolEnum = this.Tool.Current;
				if (activeToolEnum != Enums.ActiveToolEnum.MarkerCircle)
				{
				}
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001EE79 File Offset: 0x0001D079
		private void Map_MouseUp(object sender, MouseButtonEventArgs e)
		{
			base.Cursor = System.Windows.Input.Cursors.Arrow;
			this.pointStart.X = -1.0;
			this.pointStart.Y = -1.0;
			this.MapVC.StartUpdate();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001EEB9 File Offset: 0x0001D0B9
		private void Map_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (!this.MapScroll.IsStylusOver && e.RightButton == MouseButtonState.Pressed)
			{
				this.HandleMove(e.GetPosition(this));
				e.Handled = true;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		private void HandleMove(Point Point)
		{
			if (!(this.pointStart.X == -1.0 | this.pointStart.Y == -1.0))
			{
				MainWindow.User_Initiated_FollowChangeEventHandler user_Initiated_FollowChangeEvent = this.User_Initiated_FollowChangeEvent;
				if (user_Initiated_FollowChangeEvent != null)
				{
					user_Initiated_FollowChangeEvent(false);
				}
				base.Cursor = System.Windows.Input.Cursors.ScrollAll;
				double x;
				if (Point.X > this.pointStart.X)
				{
					x = (Point.X - this.pointStart.X) * -1.0;
				}
				else
				{
					x = this.pointStart.X - Point.X;
				}
				double y;
				if (Point.Y > this.pointStart.Y)
				{
					y = (Point.Y - this.pointStart.Y) * -1.0;
				}
				else
				{
					y = this.pointStart.Y - Point.Y;
				}
				Point point = new Point(x, y);
				this.MapScroll.ScrollToHorizontalOffset(this.pointOffset.X + point.X);
				this.MapScroll.ScrollToVerticalOffset(this.pointOffset.Y + point.Y);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001F02C File Offset: 0x0001D22C
		private void MapPan(string Dir)
		{
			if (Operators.CompareString(Dir, "left", false) == 0)
			{
				this.MapScroll.LineLeft();
				return;
			}
			if (Operators.CompareString(Dir, "right", false) == 0)
			{
				this.MapScroll.LineRight();
				return;
			}
			if (Operators.CompareString(Dir, "up", false) == 0)
			{
				this.MapScroll.LineUp();
				return;
			}
			if (Operators.CompareString(Dir, "down", false) != 0)
			{
				return;
			}
			this.MapScroll.LineDown();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		private void UpdateGrid(double PosX, double PosY)
		{
			try
			{
				if (this.CursorGridPos.Parent == null)
				{
					this.CursorGridPos.Foreground = Brushes.Red;
					this.CursorGridPos.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
					this.CursorGridPos.IsHitTestVisible = false;
					this.MapVC.Content.Children.Add(this.CursorGridPos);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001F14C File Offset: 0x0001D34C
		private void ParseAnchorsFiles(ref Map Map, ref List<MapAnchor> MapAnchors)
		{
			try
			{
				MapAnchors = new List<MapAnchor>();
				if (File.Exists(Map.Folder + "\\Anchors.txt"))
				{
					string value = File.ReadAllText(Map.Folder + "\\Anchors.txt");
					MapAnchors = JsonConvert.DeserializeObject<List<MapAnchor>>(value);
					if (MapAnchors != null)
					{
						try
						{
							foreach (MapAnchor mapAnchor in MapAnchors)
							{
								if (mapAnchor.CanvasX == -1.0)
								{
									Common.ConvertPos(Map.WorldSize, mapAnchor.PosX.ToString(), mapAnchor.PosY.ToString(), ref mapAnchor.CanvasX, ref mapAnchor.CanvasY);
								}
							}
						}
						finally
						{
							List<MapAnchor>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MapAnchors = new List<MapAnchor>();
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001F240 File Offset: 0x0001D440
		private void DrawMapAnchors()
		{
			this.DrawMapAnchors(ref this.Map_Current, ref this.Map_Current_Anchors);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001F254 File Offset: 0x0001D454
		private void DrawMapAnchors(ref Map Map, ref List<MapAnchor> MapAnchors)
		{
			this.MapVC.Anchors.Children.Clear();
			try
			{
				bool flag = false;
				if (MapAnchors != null && MapAnchors.Count != 0)
				{
					flag = true;
				}
				if (!flag)
				{
					this.ParseAnchorsFiles(ref Map, ref MapAnchors);
				}
				if (MapAnchors != null)
				{
					this.GenerateAnchorShapes(ref MapAnchors);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		private void GenerateAnchorShapes(ref List<MapAnchor> MapAnchors)
		{
			if (MapAnchors.Count == 0)
			{
				this.RemoveAnchorShapes();
				return;
			}
			try
			{
				foreach (MapAnchor mapAnchor in MapAnchors)
				{
					this.GenerateAnchorShape(ref mapAnchor);
				}
			}
			finally
			{
				List<MapAnchor>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001F324 File Offset: 0x0001D524
		private void GenerateAnchorShape(ref MapAnchor Anchor)
		{
			try
			{
				if (this.myMapHelper != null)
				{
					anchor anchor = new anchor(ref Anchor, this.myMapHelper.RenderMode);
					this.OnUserInitiatedMapRenderModeChange += anchor.HandleRenderModeChange;
					this.MapVC.Anchors.Children.Add(anchor);
					if (Anchor.CanvasX != -1.0 & Anchor.CanvasY != -1.0)
					{
						Canvas.SetLeft(anchor, Anchor.CanvasX - anchor.OffsetHorizontal);
						Canvas.SetTop(anchor, Anchor.CanvasY - anchor.OffsetVertical);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		private void RemoveAnchorShapes()
		{
			try
			{
				foreach (object obj in this.MapVC.Anchors.Children)
				{
					UIElement uielement = (UIElement)obj;
					if (uielement.GetType() == typeof(anchor))
					{
						anchor anchor = uielement as anchor;
						if (anchor != null)
						{
							this.OnUserInitiatedMapRenderModeChange -= anchor.HandleRenderModeChange;
						}
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			this.MapVC.Anchors.Children.Clear();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001F498 File Offset: 0x0001D698
		private void ParseLocationsFiles(ref Map Map, ref List<MapLocation> MapLocations)
		{
			try
			{
				MapLocations = new List<MapLocation>();
				string value = File.ReadAllText(Map.Folder + "\\Locations.txt");
				MapLocations = JsonConvert.DeserializeObject<List<MapLocation>>(value);
				if (MapLocations != null)
				{
					try
					{
						foreach (MapLocation mapLocation in MapLocations)
						{
							if (mapLocation.CanvasX == -1.0)
							{
								Common.ConvertPos(Map.WorldSize, mapLocation.PosX, mapLocation.PosY, ref mapLocation.CanvasX, ref mapLocation.CanvasY);
							}
						}
					}
					finally
					{
						List<MapLocation>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				MapLocations = new List<MapLocation>();
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001F564 File Offset: 0x0001D764
		private void DrawMapLocations()
		{
			this.DrawMapLocations(ref this.Map_Current, ref this.Map_Current_Locations);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001F578 File Offset: 0x0001D778
		private void DrawMapLocations(ref Map Map, ref List<MapLocation> MapLocations)
		{
			this.MapVC.Locations.Children.Clear();
			try
			{
				bool flag = false;
				if (MapLocations != null && MapLocations.Count != 0)
				{
					flag = true;
				}
				if (!flag)
				{
					this.ParseLocationsFiles(ref Map, ref MapLocations);
				}
				if (MapLocations != null)
				{
					this.GenerateLocationShapes(ref MapLocations);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		private void GenerateLocationShapes(ref List<MapLocation> MapLocations)
		{
			if (MapLocations.Count == 0)
			{
				this.RemoveLocationShapes();
				return;
			}
			try
			{
				foreach (MapLocation mapLocation in MapLocations)
				{
					string left = mapLocation.Type.Trim().ToLower();
					if (Operators.CompareString(left, "namemarine", false) != 0)
					{
						if (Operators.CompareString(left, "namecitycapital", false) != 0)
						{
							this.GenerateLocationTextblock(ref mapLocation, 24.0, FontWeights.SemiBold, Colors.SaddleBrown);
						}
						else
						{
							this.GenerateLocationTextblock(ref mapLocation, 36.0, FontWeights.SemiBold, Colors.SaddleBrown);
						}
					}
					else
					{
						this.GenerateLocationTextblock(ref mapLocation, 18.0, FontWeights.Regular, Colors.Blue);
					}
				}
			}
			finally
			{
				List<MapLocation>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		private void GenerateLocationTextblock(ref MapLocation Location, double FontSize, FontWeight FontWeight, Color ForeColor)
		{
			try
			{
				if (!string.IsNullOrEmpty(Location.Text.Trim()))
				{
					TextBlock textBlock = new TextBlock();
					textBlock.Text = Location.Text.ToUpper();
					textBlock.FontFamily = new FontFamily("Century Gothic");
					textBlock.FontWeight = FontWeight;
					textBlock.FontSize = FontSize;
					textBlock.TextAlignment = TextAlignment.Center;
					textBlock.Foreground = new SolidColorBrush(ForeColor);
					textBlock.IsHitTestVisible = false;
					textBlock.Tag = Location;
					this.MapVC.Locations.Children.Add(textBlock);
					if (Location.CanvasX != -1.0 & Location.CanvasY != -1.0)
					{
						Canvas.SetLeft(textBlock, Location.CanvasX - textBlock.ActualWidth / 2.0);
						Canvas.SetTop(textBlock, Location.CanvasY - FontSize / 2.0);
						System.Windows.Controls.Panel.SetZIndex(textBlock, 1000);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001F7EC File Offset: 0x0001D9EC
		private void RemoveLocationShapes()
		{
			this.MapVC.Locations.Children.Clear();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001F804 File Offset: 0x0001DA04
		private void GenerateMapLines()
		{
			if (this.Map_Current == null)
			{
				this.RemoveMapLines();
				return;
			}
			if (this.ZoomLines != this.ZoomFactor)
			{
				this.MapVC.Lines.Height = (double)this.Map_Current.WorldSize;
				this.MapVC.Lines.Width = (double)this.Map_Current.WorldSize;
				if (this.ZoomFactor >= 0.75)
				{
					if (this.ZoomLinesStep != 1)
					{
						this.ZoomLines = this.ZoomFactor;
						this.ZoomLinesStep = 1;
						this.DrawMapLines(100.0, 1.0);
						return;
					}
				}
				else if (this.ZoomLinesStep != 0)
				{
					this.RemoveMapLines();
					this.ZoomLines = this.ZoomFactor;
					this.ZoomLinesStep = 0;
					this.DrawMapLines(1000.0, 2.0);
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		private void DrawMapLines(double StepAmount, double Thickness)
		{
			double num = (double)this.Map_Current.OffsetX;
			double num2 = (double)this.Map_Current.OffsetY;
			double num3 = num;
			double num4 = num;
			if (num > StepAmount)
			{
				while (num3 > 0.0)
				{
					num3 -= StepAmount;
				}
				num3 += StepAmount;
			}
			if (num < (double)this.Map_Current.WorldSize - StepAmount)
			{
				while (num4 < (double)this.Map_Current.WorldSize)
				{
					num4 += StepAmount;
				}
				num4 -= StepAmount;
			}
			double num5 = num2;
			double num6 = num2;
			if (num2 > StepAmount)
			{
				while (num5 > 0.0)
				{
					num5 -= StepAmount;
				}
				num5 += StepAmount;
			}
			if (num2 < (double)this.Map_Current.WorldSize - StepAmount)
			{
				while (num6 < (double)this.Map_Current.WorldSize)
				{
					num6 += StepAmount;
				}
				num6 -= StepAmount;
			}
			checked
			{
				if (!double.IsNaN(this.MapVC.Lines.Width) & !double.IsNaN(this.MapVC.Lines.Height))
				{
					int num7 = (int)Math.Round(num3);
					int num8 = (int)Math.Round(num4);
					int num9 = (int)Math.Round(StepAmount);
					int num10 = num7;
					while ((num9 >> 31 ^ num10) <= (num9 >> 31 ^ num8))
					{
						this.DrawMapLine(num10, 0, Thickness);
						num10 += num9;
					}
					int num11 = (int)Math.Round(num5);
					int num12 = (int)Math.Round(num6);
					int num13 = (int)Math.Round(StepAmount);
					int num14 = num11;
					while ((num13 >> 31 ^ num14) <= (num13 >> 31 ^ num12))
					{
						this.DrawMapLine(0, num14, Thickness);
						num14 += num13;
					}
				}
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001FA64 File Offset: 0x0001DC64
		private void DrawMapLine(int CanvasX, int CanvasY, double Thickness)
		{
			try
			{
				Line line = new Line();
				line.Stroke = Brushes.Gray;
				line.StrokeThickness = Thickness;
				if (CanvasX == 0)
				{
					line.X1 = 0.0;
					line.Y1 = (double)CanvasY;
					line.X2 = this.MapVC.Lines.Width;
					line.Y2 = (double)CanvasY;
				}
				else
				{
					line.X1 = (double)CanvasX;
					line.Y1 = 0.0;
					line.X2 = (double)CanvasX;
					line.Y2 = this.MapVC.Lines.Height;
				}
				this.MapVC.Lines.Children.Add(line);
				System.Windows.Controls.Panel.SetZIndex(line, 2000);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		private void RemoveMapLines()
		{
			this.MapVC.Lines.Children.Clear();
			this.ZoomLines = -1.0;
			this.ZoomLinesStep = -1;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001FB6C File Offset: 0x0001DD6C
		private void MapImport_Proceed()
		{
			this.Map_Import_InProgress = true;
			this.brdMapMissing.Visibility = Visibility.Collapsed;
			this.ShowMapImportProgress();
			this.SocketClientRelay.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "command<ath_sep>mapexport"
			});
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001FBB9 File Offset: 0x0001DDB9
		private void ShowMapImportProgress()
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.brdMapImport.Visibility = Visibility.Visible;
				this.txtMapImportProgressReceivingMapStart.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressReceivingRows.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressReceivingTrees.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressReceivingLocations.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressReceivingObjects.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressReceivingRoads.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressReceivingMapEnd.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressProcessRows.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressProcessTrees.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressProcessLocations.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressProcessObjects.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressProcessRoads.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressCleanup.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressComplete.Visibility = Visibility.Collapsed;
				this.txtMapImportProgressFail.Visibility = Visibility.Collapsed;
			});
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001FBD4 File Offset: 0x0001DDD4
		private void CancelMapImport()
		{
			MainWindow.MapImportFailEventHandler mapImportFailEvent = this.MapImportFailEvent;
			if (mapImportFailEvent != null)
			{
				mapImportFailEvent("Cancelled by user request");
			}
			this.brdMapImport.Visibility = Visibility.Collapsed;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001FC04 File Offset: 0x0001DE04
		private void HandleMapImportFail(string Reason)
		{
			if (this.Map_Import_InProgress)
			{
				this.Map_Import_InProgress = false;
				try
				{
					if (this.Map_Import != null && !string.IsNullOrEmpty(this.Map_Import.Folder) && Directory.Exists(this.Map_Import.Folder))
					{
						Directory.Delete(this.Map_Import.Folder, true);
					}
					this.RemoveUnneededData();
				}
				catch (Exception ex)
				{
				}
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressFail.Text = "The map import failed." + Reason;
					this.txtMapImportProgressFail.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		private void RemoveUnneededData()
		{
			this.Map_Import = null;
			this.Map_Import_Data = null;
			this.Map_Import_Elevations = null;
			this.Map_Import_FoliageTrees = null;
			this.Map_Import_Forests = null;
			this.Map_Import_Locations = null;
			this.Map_Import_Objects = null;
			this.Map_Import_Roads = null;
			this.Map_Import_RoadSegments = null;
			this.Map_Import_Trees = null;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001FD0C File Offset: 0x0001DF0C
		private void HandleMapImportStart(string Data)
		{
			if (this.Map_Import_InProgress)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingMapStart.Visibility = Visibility.Visible;
				}), new object[0]);
				this.Map_Import = null;
				this.Map_Import_Data = null;
				this.Map_Import_Elevations = null;
				this.Map_Import_FoliageTrees = null;
				this.Map_Import_Forests = null;
				this.Map_Import_Locations = null;
				this.Map_Import_Objects = null;
				this.Map_Import_Roads = null;
				this.Map_Import_RoadSegments = null;
				this.Map_Import_Trees = null;
				try
				{
					Data data = new Data();
					this.Map_Import = data.ReadMap(Data);
					if (this.Map_Import != null && !string.IsNullOrEmpty(this.Map_Import.WorldName))
					{
						this.Map_Import.Folder = this.DIR_Maps + "\\" + this.Map_Import.WorldName;
						this.EmptyMapFolder();
						this.CreateMapFolder();
						this.CreateMapIndex();
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001FE10 File Offset: 0x0001E010
		private void EmptyMapFolder()
		{
			try
			{
				string[] array = new string[]
				{
					"map.txt",
					"locations.txt",
					"roads.txt",
					"forests.txt",
					"bushes.txt",
					"trees.txt",
					"objects.txt"
				};
				string[] array2 = new string[]
				{
					"Foliage",
					"Height",
					"Locations",
					"Objects",
					"Rows",
					"Roads"
				};
				if (array != null && array.Count<string>() != 0)
				{
					foreach (string str in array)
					{
						if (File.Exists(this.Map_Import.Folder + "\\" + str))
						{
							File.Delete(this.Map_Import.Folder + "\\" + str);
						}
					}
				}
				if (array2 != null && array2.Count<string>() != 0)
				{
					foreach (string str2 in array2)
					{
						if (Directory.Exists(this.Map_Import.Folder + "\\" + str2))
						{
							Directory.Delete(this.Map_Import.Folder + "\\" + str2, true);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001FF80 File Offset: 0x0001E180
		private void CreateMapFolder()
		{
			try
			{
				if (!Directory.Exists(this.Map_Import.Folder))
				{
					Directory.CreateDirectory(this.Map_Import.Folder);
				}
				if (!Directory.Exists(this.Map_Import.Folder + "\\Foliage"))
				{
					Directory.CreateDirectory(this.Map_Import.Folder + "\\Foliage");
				}
				if (!Directory.Exists(this.Map_Import.Folder + "\\Height"))
				{
					Directory.CreateDirectory(this.Map_Import.Folder + "\\Height");
				}
				if (!Directory.Exists(this.Map_Import.Folder + "\\Locations"))
				{
					Directory.CreateDirectory(this.Map_Import.Folder + "\\Locations");
				}
				if (!Directory.Exists(this.Map_Import.Folder + "\\Objects"))
				{
					Directory.CreateDirectory(this.Map_Import.Folder + "\\Objects");
				}
				if (!Directory.Exists(this.Map_Import.Folder + "\\Roads"))
				{
					Directory.CreateDirectory(this.Map_Import.Folder + "\\Roads");
				}
				if (!Directory.Exists(this.Map_Import.Folder + "\\Rows"))
				{
					Directory.CreateDirectory(this.Map_Import.Folder + "\\Rows");
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00020128 File Offset: 0x0001E328
		private void CreateMapIndex()
		{
			try
			{
				string contents = JsonConvert.SerializeObject(this.Map_Import);
				File.WriteAllText(this.Map_Import.Folder + "\\Map.txt", contents);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0002017C File Offset: 0x0001E37C
		private void CreateMapRowFile(MapRow MapRow)
		{
			try
			{
				if (MapRow != null)
				{
					string text = JsonConvert.SerializeObject(MapRow);
					if (!string.IsNullOrEmpty(text))
					{
						File.WriteAllText(this.Map_Import.Folder + "\\Rows\\" + MapRow.Y + ".txt", text);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000201E4 File Offset: 0x0001E3E4
		private void ProcessElevations()
		{
			try
			{
				this.CreateMapArray();
				this.ParseMapRowFiles();
				this.GetMapLowHigh(ref this.Map_Import, ref this.Map_Import_Data);
				this.CreateElevationList(ref this.Map_Import, ref this.Map_Import_Elevations, 10);
				this.ParseElevations();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0002024C File Offset: 0x0001E44C
		private void CreateMapArray()
		{
			checked
			{
				try
				{
					if (this.Map_Import != null && (this.Map_Import.WorldSize != 0 & this.Map_Import.WorldCell != 0))
					{
						this.Map_Import_Data = null;
						this.Map_Import_Data = new int[(int)Math.Round((double)this.Map_Import.WorldSize / (double)this.Map_Import.WorldCell) + 1][];
						int num = this.Map_Import_Data.Length - 1;
						for (int i = 0; i <= num; i++)
						{
							this.Map_Import_Data[i] = new int[(int)Math.Round((double)this.Map_Import.WorldSize / (double)this.Map_Import.WorldCell) + 1];
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0002031C File Offset: 0x0001E51C
		private void ParseMapRowFiles()
		{
			checked
			{
				try
				{
					if (this.Map_Import.WorldCell != 0 && Directory.Exists(this.Map_Import.Folder + "\\Rows"))
					{
						List<string> list = Directory.EnumerateFiles(this.Map_Import.Folder + "\\Rows").ToList<string>();
						if (list != null && list.Count != 0)
						{
							list.Sort();
							Data data = new Data();
							int num = list.Count - 1;
							for (int i = 0; i <= num; i++)
							{
								string text = File.ReadAllText(list[i]);
								if (!string.IsNullOrEmpty(text))
								{
									MapRow mapRow = data.ReadMapRow(text);
									if (mapRow != null)
									{
										string[] array = mapRow.Z.Split(new char[]
										{
											','
										});
										if (array != null && array.Length != 0)
										{
											int num2 = array.Length - 1;
											for (int j = 0; j <= num2; j++)
											{
												if (this.Map_Import_Data != null)
												{
													try
													{
														this.Map_Import_Data[(int)Math.Round((double)(this.Map_Import.WorldSize - Convert.ToInt32(mapRow.Y, CultureInfo.InvariantCulture)) / (double)this.Map_Import.WorldCell)][j] = Convert.ToInt32(array[j], CultureInfo.InvariantCulture);
													}
													catch (Exception ex)
													{
													}
												}
											}
										}
										array = null;
									}
									mapRow = null;
								}
							}
							data = null;
						}
						list = null;
					}
				}
				catch (Exception ex2)
				{
				}
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000204D0 File Offset: 0x0001E6D0
		private void GetMapLowHigh(ref Map Map, ref int[][] MapData)
		{
			checked
			{
				if (MapData != null)
				{
					int num = MapData.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						int num2 = MapData[i].Length - 1;
						for (int j = 0; j <= num2; j++)
						{
							if (MapData[i][j] < Map.MinZ)
							{
								Map.MinZ = MapData[i][j];
							}
							if (MapData[i][j] > Map.MaxZ)
							{
								Map.MaxZ = MapData[i][j];
							}
						}
					}
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00020544 File Offset: 0x0001E744
		private bool[][] CreateBoolArray(int Size)
		{
			checked
			{
				bool[][] array;
				try
				{
					array = new bool[Size - 1 + 1][];
					int num = Size - 1;
					for (int i = 0; i <= num; i++)
					{
						array[i] = new bool[Size - 1 + 1];
					}
				}
				catch (Exception ex)
				{
					array = null;
				}
				return array;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0002059C File Offset: 0x0001E79C
		private void CreateElevationList(ref Map Map, ref List<ElevationCells> MapElevations, int ZStep)
		{
			checked
			{
				try
				{
					MapElevations = null;
					if (Map.MinZ <= Map.MaxZ)
					{
						MapElevations = new List<ElevationCells>();
						MapElevations.Add(new ElevationCells
						{
							Z = 0
						});
						int num = 0 + ZStep;
						int maxZ = Map.MaxZ;
						int num2 = num;
						while ((ZStep >> 31 ^ num2) <= (ZStep >> 31 ^ maxZ))
						{
							MapElevations.Add(new ElevationCells
							{
								Z = num2
							});
							num2 += ZStep;
						}
						int num3 = 0 - ZStep;
						int minZ = Map.MinZ;
						int num4 = ZStep * -1;
						int num5 = num3;
						while ((num4 >> 31 ^ num5) <= (num4 >> 31 ^ minZ))
						{
							MapElevations.Add(new ElevationCells
							{
								Z = num5
							});
							num5 += num4;
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00020670 File Offset: 0x0001E870
		private void ParseElevations()
		{
			checked
			{
				try
				{
					if (this.Map_Import_Elevations != null && this.Map_Import_Elevations.Count != 0)
					{
						try
						{
							foreach (ElevationCells elevationCells in this.Map_Import_Elevations)
							{
								int num = Convert.ToInt32((double)this.Map_Import.WorldSize / (double)this.Map_Import.WorldCell, CultureInfo.InvariantCulture);
								double num2 = (double)this.Map_Import.WorldSize / (double)this.Map_Import.WorldCell;
								if (Convert.ToDouble(num, CultureInfo.InvariantCulture) < num2)
								{
									num++;
								}
								elevationCells.Cells = this.CreateBoolArray(num + 1);
								int z = elevationCells.Z;
								ElevationCells elevationCells2;
								bool[][] cells = (elevationCells2 = elevationCells).Cells;
								this.FillElevationCells(z, ref cells);
								elevationCells2.Cells = cells;
								this.PopulateElevationHitTests(ref elevationCells);
								this.PopulateElevationPoints(ref elevationCells);
								this.SaveElevation(ref elevationCells);
							}
						}
						finally
						{
							List<ElevationCells>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000207A0 File Offset: 0x0001E9A0
		private void FillElevationCells(int Z, ref bool[][] CellData)
		{
			checked
			{
				if (CellData != null)
				{
					int upperBound = CellData.GetUpperBound(0);
					int num = upperBound;
					for (int i = 0; i <= num; i++)
					{
						int num2 = upperBound;
						for (int j = 0; j <= num2; j++)
						{
							bool flag = true;
							bool flag2 = true;
							bool flag3 = true;
							if (i == upperBound)
							{
								flag = false;
							}
							if (i == upperBound | j == upperBound)
							{
								flag2 = false;
							}
							if (j == upperBound)
							{
								flag3 = false;
							}
							int num3 = this.Map_Import_Data[i][j];
							int num4 = this.Map_Import_Data[i][j];
							int num5 = this.Map_Import_Data[i][j];
							int num6 = this.Map_Import_Data[i][j];
							if (flag)
							{
								num3 = this.Map_Import_Data[i + 1][j];
							}
							if (flag2)
							{
								num4 = this.Map_Import_Data[i + 1][j + 1];
							}
							if (flag3)
							{
								num6 = this.Map_Import_Data[i][j + 1];
							}
							int num7 = 0;
							bool flag4 = false;
							bool flag5 = false;
							bool flag6 = false;
							bool flag7 = false;
							if (flag && num3 >= Z)
							{
								num7++;
								flag4 = true;
							}
							if (flag2 && num4 >= Z)
							{
								num7++;
								flag5 = true;
							}
							if (num5 >= Z)
							{
								num7++;
								flag6 = true;
							}
							if (flag3 && num6 >= Z)
							{
								num7++;
								flag7 = true;
							}
							switch (num7)
							{
							case 0:
								CellData[i][j] = false;
								break;
							case 1:
								CellData[i][j] = false;
								break;
							case 2:
								CellData[i][j] = false;
								if (flag4 && flag7)
								{
									CellData[i][j] = true;
								}
								if (flag6 && flag5)
								{
									CellData[i][j] = true;
								}
								break;
							case 3:
								CellData[i][j] = true;
								break;
							case 4:
								CellData[i][j] = true;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0002093C File Offset: 0x0001EB3C
		private void PopulateElevationHitTests(ref ElevationCells Elevation)
		{
			checked
			{
				try
				{
					int num = Elevation.Cells.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						Elevation.HitTests.Add(new List<int>());
						bool flag = false;
						int num2 = Elevation.Cells.Length - 1;
						for (int j = 0; j <= num2; j++)
						{
							if (Elevation.Cells[i][j])
							{
								if (!flag)
								{
									Elevation.HitTests[i].Add(j);
								}
								flag = true;
							}
							else
							{
								flag = false;
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000209DC File Offset: 0x0001EBDC
		private void PopulateElevationPoints(ref ElevationCells Elevation)
		{
			checked
			{
				try
				{
					MarchingSquare marchingSquare = new MarchingSquare();
					if (Elevation != null && Elevation.HitTests != null && Elevation.HitTests.Count != 0)
					{
						int num = Elevation.HitTests.Count - 1;
						for (int i = 0; i <= num; i++)
						{
							try
							{
								foreach (int num2 in Elevation.HitTests[i])
								{
									if (!Elevation.ContainsPoint(i, num2))
									{
										List<Point> list = marchingSquare.DoMarch(Elevation.Cells, num2, i);
										if (list != null && list.Count > 1)
										{
											Elevation.PointGroups.Add(list);
										}
									}
								}
							}
							finally
							{
								List<int>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
					}
					marchingSquare = null;
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		private void SaveElevation(ref ElevationCells Elevation)
		{
			try
			{
				if (Elevation != null)
				{
					Elevation.Cells = new bool[0][];
					Elevation.HitTests = new List<List<int>>();
					string contents = JsonConvert.SerializeObject(Elevation);
					File.WriteAllText(this.Map_Import.Folder + "\\Height\\Z" + Elevation.Z.ToString() + ".txt", contents);
					Elevation.PointGroups.Clear();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00020B60 File Offset: 0x0001ED60
		private void CreateMapFoliageFile(ref MapFoliage MapFoliage)
		{
			try
			{
				if (MapFoliage != null)
				{
					if (MapFoliage.Bushes != null && MapFoliage.Bushes.Count != 0)
					{
						string text = JsonConvert.SerializeObject(MapFoliage);
						if (!string.IsNullOrEmpty(text))
						{
							File.WriteAllText(this.Map_Import.Folder + "\\Foliage\\" + Guid.NewGuid().ToString() + ".txt", text);
						}
					}
					if (MapFoliage.Trees != null && MapFoliage.Trees.Count != 0)
					{
						string text2 = JsonConvert.SerializeObject(MapFoliage);
						if (!string.IsNullOrEmpty(text2))
						{
							File.WriteAllText(this.Map_Import.Folder + "\\Foliage\\" + Guid.NewGuid().ToString() + ".txt", text2);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00020C4C File Offset: 0x0001EE4C
		private void ProcessFoliage()
		{
			try
			{
				this.ParseMapFoliageFiles();
				this.UpdateMapTreesPos();
				this.ParseMapTrees();
				this.SaveMapFoliage(ref this.Map_Import_Trees, ref this.Map_Import_Forests);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00020CA0 File Offset: 0x0001EEA0
		private void ParseMapFoliageFiles()
		{
			try
			{
				Dictionary<string, MapObject> dictionary = new Dictionary<string, MapObject>();
				this.Map_Import_Trees = new List<MapBush>();
				this.Map_Import_FoliageTrees = new MapFoliage();
				if (Directory.Exists(this.Map_Import.Folder + "\\Foliage"))
				{
					List<string> list = Directory.EnumerateFiles(this.Map_Import.Folder + "\\Foliage").ToList<string>();
					if (list != null && list.Count != 0)
					{
						list.Sort();
						try
						{
							foreach (string path in list)
							{
								string value = File.ReadAllText(path);
								if (!string.IsNullOrEmpty(value))
								{
									MapFoliage mapFoliage = JsonConvert.DeserializeObject<MapFoliage>(value);
									if (mapFoliage != null && mapFoliage.Trees != null && mapFoliage.Trees.Count != 0)
									{
										try
										{
											foreach (MapObject mapObject in mapFoliage.Trees)
											{
												if (!dictionary.ContainsKey(mapObject.ObjectID))
												{
													dictionary.Add(mapObject.ObjectID, mapObject);
													this.Map_Import_Trees.Add((MapBush)this.CreateBushObject(ref mapObject));
													this.Map_Import_FoliageTrees.Trees.Add(mapObject);
												}
											}
										}
										finally
										{
											List<MapObject>.Enumerator enumerator2;
											((IDisposable)enumerator2).Dispose();
										}
									}
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00020E64 File Offset: 0x0001F064
		private object CreateBushObject(ref MapObject MapObject)
		{
			MapBush mapBush = null;
			if (MapObject != null && !string.IsNullOrEmpty(MapObject.PosX) && !string.IsNullOrEmpty(MapObject.PosY))
			{
				mapBush = new MapBush();
				Common.ConvertPos(this.Map_Import.WorldSize, MapObject.PosX, MapObject.PosY, ref mapBush.CanvasX, ref mapBush.CanvasY);
			}
			return mapBush;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		private void UpdateMapTreesPos()
		{
			try
			{
				if (this.Map_Import_FoliageTrees != null && this.Map_Import_FoliageTrees.Trees != null)
				{
					try
					{
						foreach (MapObject mapObject in this.Map_Import_FoliageTrees.Trees)
						{
							Common.ConvertPos(this.Map_Import.WorldSize, mapObject.PosX, mapObject.PosY, ref mapObject.CanvasX, ref mapObject.CanvasY);
						}
					}
					finally
					{
						List<MapObject>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00020F70 File Offset: 0x0001F170
		private void ParseMapTrees()
		{
			try
			{
				this.Map_Import_Forests = new MapForests();
				if (this.Map_Import_FoliageTrees != null && this.Map_Import_FoliageTrees.Trees != null && this.Map_Import_FoliageTrees.Trees.Count != 0)
				{
					int num = 16;
					int num2 = checked(num * 60);
					int size = Convert.ToInt32(Math.Ceiling((double)this.Map_Import.WorldSize / (double)num), CultureInfo.InvariantCulture);
					int cellsTotal = Convert.ToInt32(Math.Ceiling((double)this.Map_Import.WorldSize / (double)num2), CultureInfo.InvariantCulture);
					int[][] array = this.CreateIntArray(size);
					bool[][] array2 = this.CreateBoolArray(size);
					bool[][] array3 = this.CreateBoolArray(size);
					this.PrepGroupCells(ref this.Map_Import_Forests, cellsTotal, num2);
					this.FillTreeValues(ref array, ref this.Map_Import_FoliageTrees, num);
					this.FillTreeCellsHeavy(ref array2, ref array, 3);
					this.FillTreeCellsLight(ref array3, ref array, 2);
					this.PopulateForestPoints(ref this.Map_Import_Forests, ref array2, ref array3);
					this.Map_Import_Trees = this.RemoveForestedTrees(ref array2, ref array3, ref this.Map_Import_Trees, num);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000210AC File Offset: 0x0001F2AC
		private int[][] CreateIntArray(int Size)
		{
			checked
			{
				int[][] array;
				try
				{
					array = new int[Size - 1 + 1][];
					int num = Size - 1;
					for (int i = 0; i <= num; i++)
					{
						array[i] = new int[Size + 1];
					}
				}
				catch (Exception ex)
				{
					array = null;
				}
				return array;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00021104 File Offset: 0x0001F304
		private void PrepGroupCells(ref MapForests MapForest, int CellsTotal, int CellSize)
		{
			checked
			{
				int num = CellsTotal - 1;
				for (int i = 0; i <= num; i++)
				{
					int num2 = CellsTotal - 1;
					for (int j = 0; j <= num2; j++)
					{
						MapForest.CellsHeavy.Add(new MapForestCells());
						MapForest.CellsHeavy[MapForest.CellsHeavy.Count - 1].X = j;
						MapForest.CellsHeavy[MapForest.CellsHeavy.Count - 1].Y = i;
						MapForest.CellsHeavy[MapForest.CellsHeavy.Count - 1].Size = CellSize;
						MapForest.CellsLight.Add(new MapForestCells());
						MapForest.CellsLight[MapForest.CellsLight.Count - 1].X = j;
						MapForest.CellsLight[MapForest.CellsLight.Count - 1].Y = i;
						MapForest.CellsLight[MapForest.CellsLight.Count - 1].Size = CellSize;
					}
				}
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00021220 File Offset: 0x0001F420
		private void FillTreeValues(ref int[][] ValueData, ref MapFoliage MapFoliageTrees, int CellSize)
		{
			checked
			{
				if ((ValueData != null & MapFoliageTrees != null) && MapFoliageTrees.Trees != null && MapFoliageTrees.Trees.Count != 0)
				{
					try
					{
						foreach (MapObject mapObject in MapFoliageTrees.Trees)
						{
							try
							{
								int num = Convert.ToInt32(Math.Floor(mapObject.CanvasX / (double)CellSize), CultureInfo.InvariantCulture);
								int num2 = Convert.ToInt32(Math.Floor(mapObject.CanvasY / (double)CellSize), CultureInfo.InvariantCulture);
								if (num2 <= ValueData.Length - 1 && num <= ValueData[num2].Length - 1)
								{
									int[] array = ValueData[num2];
									int num3 = num;
									ref int ptr = ref array[num3];
									array[num3] = ptr + 1;
								}
							}
							catch (Exception ex)
							{
							}
						}
					}
					finally
					{
						List<MapObject>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00021320 File Offset: 0x0001F520
		private void FillTreeCellsHeavy(ref bool[][] CellData, ref int[][] ValuesData, int MinPerCell)
		{
			checked
			{
				if (CellData != null & ValuesData != null)
				{
					int num = ValuesData.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						int num2 = ValuesData[i].Length - 1;
						for (int j = 0; j <= num2; j++)
						{
							if (ValuesData[i][j] >= MinPerCell && i <= CellData.Length - 1 && j <= CellData[i].Length - 1)
							{
								CellData[i][j] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00021388 File Offset: 0x0001F588
		private void FillTreeCellsLight(ref bool[][] LightData, ref int[][] ValuesData, int AmountPerCell)
		{
			checked
			{
				if (LightData != null & ValuesData != null)
				{
					int num = LightData.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						int num2 = LightData[i].Length - 1;
						for (int j = 0; j <= num2; j++)
						{
							if (ValuesData[i][j] == AmountPerCell && i <= LightData.Length - 1 && j <= LightData[i].Length - 1)
							{
								LightData[i][j] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000213F0 File Offset: 0x0001F5F0
		private void PopulateForestPoints(ref MapForests ForestData, ref bool[][] CellsHeavy, ref bool[][] CellsLight)
		{
			checked
			{
				if (ForestData.CellsHeavy != null && ForestData.CellsHeavy.Count != 0)
				{
					try
					{
						foreach (MapForestCells mapForestCells in ForestData.CellsHeavy)
						{
							if (CellsHeavy != null && CellsHeavy.Length != 0)
							{
								int num = mapForestCells.Y * 60;
								int num2 = mapForestCells.Y * 60 + 60 - 1;
								for (int i = num; i <= num2; i++)
								{
									if (i < CellsHeavy.Length && CellsHeavy[i] != null && CellsHeavy[i].Length != 0)
									{
										int num3 = mapForestCells.X * 60;
										int num4 = mapForestCells.X * 60 + 60 - 1;
										for (int j = num3; j <= num4; j++)
										{
											if (j < CellsHeavy[i].Length && CellsHeavy[i][j])
											{
												mapForestCells.Cells.Add(new Point((double)j, (double)i));
											}
										}
									}
								}
							}
						}
					}
					finally
					{
						List<MapForestCells>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				if (ForestData.CellsLight != null && ForestData.CellsLight.Count != 0)
				{
					try
					{
						foreach (MapForestCells mapForestCells2 in ForestData.CellsLight)
						{
							if (CellsLight != null && CellsLight.Length != 0)
							{
								int num5 = mapForestCells2.Y * 60;
								int num6 = mapForestCells2.Y * 60 + 60 - 1;
								for (int k = num5; k <= num6; k++)
								{
									if (k < CellsLight.Length && CellsLight[k] != null && CellsLight[k].Length != 0)
									{
										int num7 = mapForestCells2.X * 60;
										int num8 = mapForestCells2.X * 60 + 60 - 1;
										for (int l = num7; l <= num8; l++)
										{
											if (l < CellsLight[k].Length && CellsLight[k][l])
											{
												mapForestCells2.Cells.Add(new Point((double)l, (double)k));
											}
										}
									}
								}
							}
						}
					}
					finally
					{
						List<MapForestCells>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00021604 File Offset: 0x0001F804
		private List<MapBush> RemoveForestedTrees(ref bool[][] HeavyData, ref bool[][] LightData, ref List<MapBush> MapTrees, int CellSize)
		{
			List<MapBush> list = new List<MapBush>();
			checked
			{
				try
				{
					if ((HeavyData != null & LightData != null & MapTrees != null) && MapTrees.Count != 0)
					{
						for (int i = MapTrees.Count - 1; i >= 0; i += -1)
						{
							try
							{
								int num = Convert.ToInt32(Math.Floor(MapTrees[i].CanvasX / (double)CellSize), CultureInfo.InvariantCulture);
								int num2 = Convert.ToInt32(Math.Floor(MapTrees[i].CanvasY / (double)CellSize), CultureInfo.InvariantCulture);
								if ((num2 <= HeavyData.Length - 1 & num2 <= LightData.Length - 1) && (num <= HeavyData[num2].Length - 1 & num <= LightData[num2].Length - 1) && (!HeavyData[num2][num] & !LightData[num2][num]) && i <= MapTrees.Count - 1)
								{
									list.Add(MapTrees[i]);
								}
							}
							catch (Exception ex)
							{
							}
						}
					}
				}
				catch (Exception ex2)
				{
				}
				return list;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00021760 File Offset: 0x0001F960
		private void SaveMapFoliage(ref List<MapBush> MapTrees, ref MapForests MapForests)
		{
			try
			{
				string text = JsonConvert.SerializeObject(MapTrees);
				if (!string.IsNullOrEmpty(text))
				{
					File.WriteAllText(this.Map_Import.Folder + "\\Trees.txt", text);
				}
				MapTrees = null;
			}
			catch (Exception ex)
			{
			}
			try
			{
				if (MapForests != null)
				{
					string text2 = JsonConvert.SerializeObject(MapForests);
					if (!string.IsNullOrEmpty(text2))
					{
						File.WriteAllText(this.Map_Import.Folder + "\\Forests.txt", text2);
					}
					MapForests = null;
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0002180C File Offset: 0x0001FA0C
		private void CreateMapLocationsFile(ref MapLocations MapLocations)
		{
			try
			{
				if (MapLocations != null && MapLocations.Locations != null && MapLocations.Locations.Count != 0)
				{
					string text = JsonConvert.SerializeObject(MapLocations);
					if (!string.IsNullOrEmpty(text))
					{
						File.WriteAllText(this.Map_Import.Folder + "\\Locations\\" + MapLocations.Locations[0].Text + ".txt", text);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00021898 File Offset: 0x0001FA98
		private void ProcessLocations()
		{
			try
			{
				this.ParseMapLocationsFiles();
				this.UpdateMapLocationsData();
				this.SaveMapLocations();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000218D8 File Offset: 0x0001FAD8
		private void ParseMapLocationsFiles()
		{
			try
			{
				this.Map_Import_Locations = new List<MapLocation>();
				if (Directory.Exists(this.Map_Import.Folder + "\\Locations"))
				{
					List<string> list = Directory.EnumerateFiles(this.Map_Import.Folder + "\\Locations").ToList<string>();
					if (list != null && list.Count != 0)
					{
						list.Sort();
						try
						{
							foreach (string path in list)
							{
								string value = File.ReadAllText(path);
								if (!string.IsNullOrEmpty(value))
								{
									MapLocations mapLocations = JsonConvert.DeserializeObject<MapLocations>(value);
									if (mapLocations != null)
									{
										this.Map_Import_Locations.AddRange(mapLocations.Locations);
									}
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000219C4 File Offset: 0x0001FBC4
		private void UpdateMapLocationsData()
		{
			try
			{
				if (this.Map_Import_Locations != null)
				{
					try
					{
						foreach (MapLocation mapLocation in this.Map_Import_Locations)
						{
							Common.ConvertPos(this.Map_Import.WorldSize, mapLocation.PosX, mapLocation.PosY, ref mapLocation.CanvasX, ref mapLocation.CanvasY);
							try
							{
								mapLocation.Width = Convert.ToDouble(mapLocation.WidthStr, CultureInfo.InvariantCulture);
							}
							catch (Exception ex)
							{
								mapLocation.Width = 0.0;
							}
							try
							{
								mapLocation.Length = Convert.ToDouble(mapLocation.LengthStr, CultureInfo.InvariantCulture);
							}
							catch (Exception ex2)
							{
								mapLocation.Length = 0.0;
							}
						}
					}
					finally
					{
						List<MapLocation>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex3)
			{
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00021AEC File Offset: 0x0001FCEC
		private void SaveMapLocations()
		{
			try
			{
				string text = JsonConvert.SerializeObject(this.Map_Import_Locations);
				if (!string.IsNullOrEmpty(text))
				{
					File.WriteAllText(this.Map_Import.Folder + "\\Locations.txt", text);
				}
				this.Map_Import_Locations = null;
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00021B50 File Offset: 0x0001FD50
		private void CreateMapObjectsFile(ref MapObjects MapObjects)
		{
			try
			{
				if (MapObjects != null && MapObjects.Objects != null && MapObjects.Objects.Count != 0)
				{
					string text = JsonConvert.SerializeObject(MapObjects);
					if (!string.IsNullOrEmpty(text))
					{
						File.WriteAllText(this.Map_Import.Folder + "\\Objects\\" + Guid.NewGuid().ToString() + ".txt", text);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		private void ProcessObjects()
		{
			try
			{
				this.ParseMapObjectsFiles();
				this.UpdateMapObjectsPos();
				this.SaveMapObjects();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00021C20 File Offset: 0x0001FE20
		private void ParseMapObjectsFiles()
		{
			try
			{
				Dictionary<string, MapObject> dictionary = new Dictionary<string, MapObject>();
				if (Directory.Exists(this.Map_Import.Folder + "\\Objects"))
				{
					List<string> list = Directory.EnumerateFiles(this.Map_Import.Folder + "\\Objects").ToList<string>();
					if (list != null && list.Count != 0)
					{
						list.Sort();
						try
						{
							foreach (string path in list)
							{
								string value = File.ReadAllText(path);
								if (!string.IsNullOrEmpty(value))
								{
									MapObjects mapObjects = JsonConvert.DeserializeObject<MapObjects>(value);
									if (mapObjects != null)
									{
										try
										{
											foreach (MapObject mapObject in mapObjects.Objects)
											{
												if (!dictionary.ContainsKey(mapObject.ObjectID))
												{
													dictionary.Add(mapObject.ObjectID, mapObject);
												}
											}
										}
										finally
										{
											List<MapObject>.Enumerator enumerator2;
											((IDisposable)enumerator2).Dispose();
										}
									}
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
				this.Map_Import_Objects = dictionary.Values.ToList<MapObject>();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00021D90 File Offset: 0x0001FF90
		private void UpdateMapObjectsPos()
		{
			try
			{
				if (this.Map_Import_Objects != null)
				{
					try
					{
						foreach (MapObject mapObject in this.Map_Import_Objects)
						{
							Common.ConvertPos(this.Map_Import.WorldSize, mapObject.PosX, mapObject.PosY, ref mapObject.CanvasX, ref mapObject.CanvasY);
						}
					}
					finally
					{
						List<MapObject>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00021E28 File Offset: 0x00020028
		private void SaveMapObjects()
		{
			try
			{
				if (File.Exists(this.Map_Import.Folder + "\\Objects.txt"))
				{
					File.Delete(this.Map_Import.Folder + "\\Objects.txt");
				}
				using (TextWriter textWriter = File.CreateText(this.Map_Import.Folder + "\\Objects.txt"))
				{
					new JsonSerializer().Serialize(textWriter, this.Map_Import_Objects);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00021ED0 File Offset: 0x000200D0
		private void CreateMapRoadsFile(ref MapRoads MapRoads)
		{
			try
			{
				if (MapRoads != null && MapRoads.Segments != null && MapRoads.Segments.Count != 0)
				{
					string text = JsonConvert.SerializeObject(MapRoads);
					if (!string.IsNullOrEmpty(text))
					{
						File.WriteAllText(this.Map_Import.Folder + "\\Roads\\" + Guid.NewGuid().ToString() + ".txt", text);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00021F60 File Offset: 0x00020160
		private void ProcessRoads()
		{
			try
			{
				this.ParseMapRoadsFiles();
				this.PopulateRoadPaths();
				this.SaveRoads();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00021FA0 File Offset: 0x000201A0
		private void ParseMapRoadsFiles()
		{
			try
			{
				this.Map_Import_Roads = new List<MapRoads>();
				if (Directory.Exists(this.Map_Import.Folder + "\\Roads"))
				{
					List<string> list = Directory.EnumerateFiles(this.Map_Import.Folder + "\\Roads").ToList<string>();
					if (list != null && list.Count != 0)
					{
						list.Sort();
						try
						{
							foreach (string path in list)
							{
								string value = File.ReadAllText(path);
								if (!string.IsNullOrEmpty(value))
								{
									MapRoads mapRoads = JsonConvert.DeserializeObject<MapRoads>(value);
									if (mapRoads != null)
									{
										this.Map_Import_Roads.Add(mapRoads);
									}
								}
							}
						}
						finally
						{
							List<string>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00022088 File Offset: 0x00020288
		private void PopulateRoadPaths()
		{
			if (this.Map_Import_Roads != null && this.Map_Import_Roads.Count != 0)
			{
				this.PopulateIndividualRoadSegments();
				this.PopulateRoadSegments();
				this.BuildRoadPaths();
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000220B4 File Offset: 0x000202B4
		private void PopulateIndividualRoadSegments()
		{
			checked
			{
				if (this.Map_Import_Roads != null && this.Map_Import_Roads.Count != 0)
				{
					try
					{
						foreach (MapRoads mapRoads in this.Map_Import_Roads)
						{
							if (mapRoads.Segments != null)
							{
								for (int i = mapRoads.Segments.Count - 1; i >= 0; i += -1)
								{
									if (mapRoads.Segments[i].Connections.Count == 0)
									{
										MapRoad mapRoad = new MapRoad();
										mapRoad.Model = mapRoads.Segments[i].Model.Trim();
										mapRoad.RoadType = Enums.RoadType.Concrete;
										if (!string.IsNullOrEmpty(mapRoads.Segments[i].Dir))
										{
											mapRoad.Dir = Convert.ToDouble(mapRoads.Segments[i].Dir, CultureInfo.InvariantCulture);
										}
										if (!string.IsNullOrEmpty(mapRoads.Segments[i].Width))
										{
											mapRoad.Width = Convert.ToDouble(mapRoads.Segments[i].Width, CultureInfo.InvariantCulture);
										}
										if (!string.IsNullOrEmpty(mapRoads.Segments[i].Length))
										{
											mapRoad.Length = Convert.ToDouble(mapRoads.Segments[i].Length, CultureInfo.InvariantCulture);
										}
										mapRoad.Nodes.Add(Common.ConvertPosToPoint(this.Map_Import.WorldSize, ref mapRoads.Segments[i].PosX, ref mapRoads.Segments[i].PosY));
										this.Map_Import_Roads[0].Roads.Add(mapRoad);
									}
								}
							}
						}
					}
					finally
					{
						List<MapRoads>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002229C File Offset: 0x0002049C
		private void PopulateRoadSegments()
		{
			this.Map_Import_RoadSegments = new List<MapRoadSegment>();
			if (this.Map_Import_Roads != null)
			{
				try
				{
					foreach (MapRoads mapRoads in this.Map_Import_Roads)
					{
						if (mapRoads.Segments != null)
						{
							try
							{
								foreach (MapRoadSegment item in mapRoads.Segments)
								{
									this.Map_Import_RoadSegments.Add(item);
								}
							}
							finally
							{
								List<MapRoadSegment>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
						}
					}
				}
				finally
				{
					List<MapRoads>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002234C File Offset: 0x0002054C
		private void BuildRoadPaths()
		{
			if (this.Map_Import_RoadSegments != null)
			{
				MapRoadTool mapRoadTool = new MapRoadTool();
				mapRoadTool.ResetVars();
				mapRoadTool.RoadSegments.AddRange(this.Map_Import_RoadSegments);
				mapRoadTool.WalkSegmentsNew();
				if (mapRoadTool.Roads != null)
				{
					this.CreateNodes(ref mapRoadTool);
					this.Map_Import_Roads[0].Roads.AddRange(mapRoadTool.Roads.Values);
				}
				mapRoadTool = null;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000223B8 File Offset: 0x000205B8
		private void CreateNodes(ref MapRoadTool Tool)
		{
			try
			{
				if (Tool.Roads != null)
				{
					try
					{
						foreach (MapRoad mapRoad in Tool.Roads.Values)
						{
							if (mapRoad.Segments != null)
							{
								try
								{
									foreach (MapRoadSegment mapRoadSegment in mapRoad.Segments.Values)
									{
										mapRoad.Nodes.Add(Common.ConvertPosToPoint(this.Map_Import.WorldSize, ref mapRoadSegment.PosX, ref mapRoadSegment.PosY));
									}
								}
								finally
								{
									Dictionary<string, MapRoadSegment>.ValueCollection.Enumerator enumerator2;
									((IDisposable)enumerator2).Dispose();
								}
							}
							mapRoad.Segments.Clear();
						}
					}
					finally
					{
						Dictionary<string, MapRoad>.ValueCollection.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000224B0 File Offset: 0x000206B0
		private void SaveRoads()
		{
			try
			{
				this.Map_Import_Roads[0].Segments.Clear();
				string text = JsonConvert.SerializeObject(this.Map_Import_Roads[0]);
				if (!string.IsNullOrEmpty(text))
				{
					File.WriteAllText(this.Map_Import.Folder + "\\Roads.txt", text);
				}
				this.Map_Import_Roads = null;
				this.Map_Import_RoadSegments = null;
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00022538 File Offset: 0x00020738
		private void HandleMapImportComplete()
		{
			if (this.Map_Import_InProgress)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingMapEnd.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.Map_Import_InProgress = false;
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressProcessRows.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.ProcessElevations();
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressProcessTrees.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.ProcessFoliage();
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressProcessObjects.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.ProcessObjects();
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressProcessRoads.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.ProcessRoads();
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressProcessLocations.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.ProcessLocations();
				this.CreateMapIndex();
				this.RemoveUnneededFiles();
				this.RemoveUnneededData();
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressCleanup.Visibility = Visibility.Visible;
					this.scrollImport.ScrollToBottom();
				}), new object[0]);
				this.EnumerateMapFolders();
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.UpdateMapMenu();
				}), new object[0]);
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.brdMapImport.Visibility = Visibility.Collapsed;
				}), new object[0]);
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002269C File Offset: 0x0002089C
		private void RemoveUnneededFiles()
		{
			try
			{
				string[] array = new string[]
				{
					"Foliage",
					"Locations",
					"Objects",
					"Roads",
					"Rows"
				};
				if (array != null && array.Count<string>() != 0)
				{
					foreach (string str in array)
					{
						if (Directory.Exists(this.Map_Import.Folder + "\\" + str))
						{
							Directory.Delete(this.Map_Import.Folder + "\\" + str, true);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00022754 File Offset: 0x00020954
		private void StartIO()
		{
			this.PrepVariables();
			this.ClearInQueues();
			this.Connect();
			this.StartWatchingInk();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00022770 File Offset: 0x00020970
		private void PrepVariables()
		{
			this.Comm_LastUpdated = DateTime.Now;
			this.Comm_LastArchived = DateTime.Now;
			this.Comm_IsInMission = false;
			this.LastUpdated_Units = DateTime.MinValue;
			this.LastUpdated_Groups = DateTime.MinValue;
			this.LastUpdated_Markers = DateTime.MinValue;
			this.LastUpdated_Vehicles = DateTime.MinValue;
			this.Frames_Recording = new Frames();
			this.Rec_LastFrameRecordedOn = DateTime.Now;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000227DC File Offset: 0x000209DC
		private void Connect()
		{
			try
			{
				this.LoadSettings();
				this.SocketClientRelay.OnErrorOccurred += this.HandleRelayError;
				this.SocketClientRelay.OnMessageGenerated += this.HandleRelayMessage;
				this.SocketClientRelay.OnConnected += this.HandleConnected;
				this.SocketClientRelay.OnDisconnected += this.HandleDisconnected;
				this.SocketClientRelay.OnReceivedData += this.HandleReceivedData;
				this.SocketClientRelay.OnSendReceive += this.HandleSendReceive;
				this.SocketClientRelay.StartConnecting(MySettings.Default.ARMAPCIP, MySettings.Default.ARMAPCPort, new ResponsePackage
				{
					Type = ResponseType.Text,
					Content = "General"
				});
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Error Connecting: " + ex.Message);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000228E8 File Offset: 0x00020AE8
		private void StartTimers()
		{
			this.MissionTimer.AutoReset = true;
			this.MissionTimer.Interval = 1000.0;
			this.MissionTimer.Enabled = true;
			this.MissionTimer.Start();
			this.ArchiveTimer.AutoReset = true;
			this.ArchiveTimer.Interval = 1000.0;
			this.ArchiveTimer.Enabled = true;
			this.ArchiveTimer.Start();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00022964 File Offset: 0x00020B64
		private void CreateInitialRelayRequests()
		{
			this.SocketClientRelay.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "guid"
			});
			this.SocketClientRelay.OutQueue.Enqueue(new ResponsePackage
			{
				Type = ResponseType.Text,
				Content = "strokerequest"
			});
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000229BF File Offset: 0x00020BBF
		private void StartWatchingInk()
		{
			this.MapVC.Ink.Strokes.StrokesChanged += this.HandleStrokesChanged;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000229E2 File Offset: 0x00020BE2
		private void StopIO()
		{
			this.StopTimers();
			this.Disconnect();
			if (this.Rec_IsRecording)
			{
				this.CleanUpData();
			}
			this.StopWatchingInk();
			this.AddClearMessage();
			this.CloseForms();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00022A10 File Offset: 0x00020C10
		private void Disconnect()
		{
			try
			{
				this.SocketClientRelay.StopConnecting();
				this.SocketClientRelay.OnErrorOccurred -= this.HandleRelayError;
				this.SocketClientRelay.OnMessageGenerated -= this.HandleRelayMessage;
				this.SocketClientRelay.OnConnected -= this.HandleConnected;
				this.SocketClientRelay.OnDisconnected -= this.HandleDisconnected;
				this.SocketClientRelay.OnReceivedData -= this.HandleReceivedData;
				this.SocketClientRelay.OnSendReceive -= this.HandleSendReceive;
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Error Disconnecting: " + ex.Message);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00022AE8 File Offset: 0x00020CE8
		private void StopTimers()
		{
			this.MissionTimer.Enabled = false;
			this.MissionTimer.Stop();
			this.ArchiveTimer.Enabled = false;
			this.ArchiveTimer.Stop();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00022B18 File Offset: 0x00020D18
		private void StopWatchingInk()
		{
			this.MapVC.Ink.Strokes.StrokesChanged -= this.HandleStrokesChanged;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00022B3B File Offset: 0x00020D3B
		private void AddClearMessage()
		{
			this.OtherQueue.Enqueue(new string[]
			{
				"clear",
				string.Empty
			});
			this.InQueueMRE.Set();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00022B6A File Offset: 0x00020D6A
		private void CloseForms()
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.brdMapMissing.Visibility = Visibility.Collapsed;
			});
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00022B84 File Offset: 0x00020D84
		private void MissionTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			int num = 10;
			if (this.Map_Import_InProgress)
			{
				num = 60;
			}
			if (Math.Abs(DateTime.Now.Subtract(this.Comm_LastUpdated).TotalSeconds) > (double)num && this.Comm_IsInMission)
			{
				MainWindow.Mission_StoppedEventHandler mission_StoppedEvent = this.Mission_StoppedEvent;
				if (mission_StoppedEvent != null)
				{
					mission_StoppedEvent();
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00022BDC File Offset: 0x00020DDC
		private void ArchiveTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (Math.Abs(DateTime.Now.Subtract(this.Comm_LastArchived).TotalSeconds) > 300.0)
			{
				MainWindow.Archive_ElapsedEventHandler archive_ElapsedEvent = this.Archive_ElapsedEvent;
				if (archive_ElapsedEvent != null)
				{
					archive_ElapsedEvent();
				}
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00022C24 File Offset: 0x00020E24
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

		// Token: 0x060004CE RID: 1230 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleRelayError(string Source, string Message)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleRelayMessage(string Source, string Message)
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00022C8C File Offset: 0x00020E8C
		private void HandleConnected(bool Success, string Message)
		{
			if (Success)
			{
				this.CreateInitialRelayRequests();
				this.StartTimers();
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void HandleDisconnected()
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00022CA0 File Offset: 0x00020EA0
		private void HandleReceivedData(string Command, List<byte[]> Data)
		{
			try
			{
				this.Comm_LastUpdated = DateTime.Now;
				string left = Command.ToLower();
				if (Operators.CompareString(left, "file", false) != 0)
				{
					if (Operators.CompareString(left, "frame", false) == 0)
					{
						string[] item = this.ByteListToStringArray(ref Data);
						if (this.FrameQueue.Count == 0)
						{
							this.FrameQueue.Enqueue(item);
						}
						this.InQueueMRE.Set();
					}
					else
					{
						string[] item2 = this.ByteListToStringArray(ref Data);
						this.OtherQueue.Enqueue(item2);
						this.InQueueMRE.Set();
					}
				}
				this.FormStatus.UpdateQueueCounts(this.FrameQueue.Count, this.OtherQueue.Count);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00022D70 File Offset: 0x00020F70
		private void HandleSendReceive(string Direction, string Message)
		{
			this.FormStatus.UpdateStatus(Direction + ", " + Message);
			string left = Direction.ToLower();
			if (Operators.CompareString(left, "sending", false) == 0)
			{
				this.FormStatus.UpdateMessageOut(Message);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00022DB8 File Offset: 0x00020FB8
		private void ProcessQueues()
		{
			string[] array = null;
			for (;;)
			{
				if (this.Status_Source != Enums.SourceStatus.ACS & !(this.Status_Source == Enums.SourceStatus.Relay & this.Status_Relay == Enums.RelayStatus.Connecting))
				{
					this.ClearInQueues();
				}
				this.InQueueMRE.WaitOne();
				try
				{
					if (this.FrameQueue.TryDequeue(out array))
					{
						this.FormStatus.UpdateMessageIn(string.Join(Environment.NewLine, array));
						if (array.Length > 1)
						{
							string left = array[0].ToLower();
							if (Operators.CompareString(left, "frame", false) == 0)
							{
								this.HandleReceivedFrame(array);
							}
						}
					}
					else if (this.OtherQueue.TryDequeue(out array))
					{
						this.FormStatus.UpdateMessageIn(string.Join(Environment.NewLine, array));
						if (array.Length > 1)
						{
							string text = array[0].ToLower();
							uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
							if (num <= 1550717474U)
							{
								if (num <= 748388108U)
								{
									if (num != 361068890U)
									{
										if (num == 748388108U)
										{
											if (Operators.CompareString(text, "guid", false) == 0)
											{
												this.HandleReceivedGUID(array);
											}
										}
									}
									else if (Operators.CompareString(text, "strokedelete", false) == 0)
									{
										this.HandleStrokeDeleted(array);
									}
								}
								else if (num != 1242963981U)
								{
									if (num != 1249200795U)
									{
										if (num == 1550717474U)
										{
											if (Operators.CompareString(text, "clear", false) == 0)
											{
												this.ClearDictionaries();
											}
										}
									}
									else if (Operators.CompareString(text, "maplocations", false) == 0)
									{
										this.HandleReceivedMapLocations(array);
									}
								}
								else if (Operators.CompareString(text, "strokecreate", false) == 0)
								{
									this.HandleStrokeCreated(array);
								}
							}
							else if (num <= 3644693370U)
							{
								if (num != 2510196943U)
								{
									if (num != 2543585508U)
									{
										if (num == 3644693370U)
										{
											if (Operators.CompareString(text, "mapbegin", false) == 0)
											{
												this.HandleReceivedMapBegin(array);
											}
										}
									}
									else if (Operators.CompareString(text, "mapfoliage", false) == 0)
									{
										this.HandleReceivedMapFoliage(array);
									}
								}
								else if (Operators.CompareString(text, "maprow", false) == 0)
								{
									this.HandleReceivedMapRow(array);
								}
							}
							else if (num != 3800938684U)
							{
								if (num != 3810301174U)
								{
									if (num == 4074695303U)
									{
										if (Operators.CompareString(text, "mapobjects", false) == 0)
										{
											this.HandleReceivedMapObject(array);
										}
									}
								}
								else if (Operators.CompareString(text, "mapend", false) == 0)
								{
									this.HandleReceivedMapEnd();
								}
							}
							else if (Operators.CompareString(text, "maproads", false) == 0)
							{
								this.HandleReceivedMapRoads(array);
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
				array = null;
				if (this.FrameQueue.IsEmpty & this.OtherQueue.IsEmpty)
				{
					this.InQueueMRE.Reset();
				}
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000230C0 File Offset: 0x000212C0
		private void ClearInQueues()
		{
			try
			{
				string[] array = null;
				while (!this.FrameQueue.IsEmpty)
				{
					this.FrameQueue.TryDequeue(out array);
				}
				while (!this.OtherQueue.IsEmpty)
				{
					this.OtherQueue.TryDequeue(out array);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0002312C File Offset: 0x0002132C
		private void HandleReceivedFrame(string[] Data)
		{
			try
			{
				if (Data.Length == 2 && !string.IsNullOrEmpty(Data[1]) && !Data[1].Trim().Equals("nodata", StringComparison.InvariantCultureIgnoreCase) && this.IsMapReady)
				{
					this.ProcessFrame(Data[1]);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00023190 File Offset: 0x00021390
		private void HandleReceivedGUID(string[] Data)
		{
			try
			{
				if (Data.Length == 2 && !string.IsNullOrEmpty(Data[1]))
				{
					Guid empty = Guid.Empty;
					if (Guid.TryParse(Data[1], out empty))
					{
						this.Comm_RelayClientGUID = empty;
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000231E8 File Offset: 0x000213E8
		private void HandleReceivedMapBegin(string[] Data)
		{
			if (this.Map_Import_InProgress && Data.Length == 2)
			{
				MainWindow.MapImportStartEventHandler mapImportStartEvent = this.MapImportStartEvent;
				if (mapImportStartEvent != null)
				{
					mapImportStartEvent(Data[1]);
				}
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00023218 File Offset: 0x00021418
		private void HandleReceivedMapRow(string[] Data)
		{
			if (this.Map_Import_InProgress && Data.Length == 2)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingRows.Visibility = Visibility.Visible;
				}), new object[0]);
				MapRow mapRow = new Data().ReadMapRow(Data[1]);
				if (mapRow != null)
				{
					this.CreateMapRowFile(mapRow);
				}
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0002326C File Offset: 0x0002146C
		private void HandleReceivedMapObject(string[] Data)
		{
			if (this.Map_Import_InProgress && Data.Length == 2)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingObjects.Visibility = Visibility.Visible;
				}), new object[0]);
				if (!string.IsNullOrEmpty(Data[1]))
				{
					MapObjects mapObjects = JsonConvert.DeserializeObject<MapObjects>(Data[1]);
					if (mapObjects != null)
					{
						this.CreateMapObjectsFile(ref mapObjects);
					}
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000232C4 File Offset: 0x000214C4
		private void HandleReceivedMapLocations(string[] Data)
		{
			if (this.Map_Import_InProgress && Data.Length == 2)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingLocations.Visibility = Visibility.Visible;
				}), new object[0]);
				if (!string.IsNullOrEmpty(Data[1]))
				{
					MapLocations mapLocations = JsonConvert.DeserializeObject<MapLocations>(Data[1]);
					if (mapLocations != null)
					{
						this.CreateMapLocationsFile(ref mapLocations);
					}
				}
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0002331C File Offset: 0x0002151C
		private void HandleReceivedMapRoads(string[] Data)
		{
			if (this.Map_Import_InProgress && Data.Length == 2)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingRoads.Visibility = Visibility.Visible;
				}), new object[0]);
				if (!string.IsNullOrEmpty(Data[1]))
				{
					MapRoads mapRoads = JsonConvert.DeserializeObject<MapRoads>(Data[1]);
					if (mapRoads != null)
					{
						this.CreateMapRoadsFile(ref mapRoads);
					}
				}
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00023374 File Offset: 0x00021574
		private void HandleReceivedMapFoliage(string[] Data)
		{
			if (this.Map_Import_InProgress && Data.Length == 2)
			{
				base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtMapImportProgressReceivingTrees.Visibility = Visibility.Visible;
				}), new object[0]);
				if (!string.IsNullOrEmpty(Data[1]))
				{
					MapFoliage mapFoliage = JsonConvert.DeserializeObject<MapFoliage>(Data[1]);
					if (mapFoliage != null)
					{
						this.CreateMapFoliageFile(ref mapFoliage);
					}
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000233CC File Offset: 0x000215CC
		private void HandleReceivedMapEnd()
		{
			MainWindow.MapImportCompleteEventHandler mapImportCompleteEvent = this.MapImportCompleteEvent;
			if (mapImportCompleteEvent != null)
			{
				mapImportCompleteEvent();
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000233EC File Offset: 0x000215EC
		private void HandleStrokeCreated(string[] Data)
		{
			if (!string.IsNullOrEmpty(Data[1]) & !string.IsNullOrEmpty(Data[2]) & !string.IsNullOrEmpty(Data[3]))
			{
				try
				{
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					if (Guid.TryParse(Data[1], out empty) && Guid.TryParse(Data[2], out empty2))
					{
						StrokeCollection strokeCollection = Common.DeserializeStrokeCollection(Data[3]);
						if ((!empty.Equals(Guid.Empty) & !empty2.Equals(Guid.Empty) & !empty2.Equals(this.Comm_RelayClientGUID) & strokeCollection != null) && strokeCollection.Count != 0)
						{
							Stroke Stroke = strokeCollection[0];
							if (Stroke != null)
							{
								base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
								{
									this.MapVC.Ink.Strokes.Add(Stroke);
								}), new object[0]);
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000234F8 File Offset: 0x000216F8
		private void HandleStrokeDeleted(string[] Data)
		{
			if (!string.IsNullOrEmpty(Data[1]) & !string.IsNullOrEmpty(Data[2]))
			{
				MainWindow._Closure$__580-0 CS$<>8__locals1 = new MainWindow._Closure$__580-0(CS$<>8__locals1);
				CS$<>8__locals1.$VB$Me = this;
				CS$<>8__locals1.$VB$Local_StrokeGUID = Guid.Empty;
				CS$<>8__locals1.$VB$Local_DeleterGUID = Guid.Empty;
				if (Guid.TryParse(Data[1], out CS$<>8__locals1.$VB$Local_StrokeGUID) && Guid.TryParse(Data[2], out CS$<>8__locals1.$VB$Local_DeleterGUID) && (!CS$<>8__locals1.$VB$Local_StrokeGUID.Equals(Guid.Empty) & !CS$<>8__locals1.$VB$Local_DeleterGUID.Equals(Guid.Empty) & !CS$<>8__locals1.$VB$Local_DeleterGUID.Equals(this.Comm_RelayClientGUID)))
				{
					base.Dispatcher.BeginInvoke(new VB$AnonymousDelegate_0(delegate()
					{
						try
						{
							List<Stroke> list = CS$<>8__locals1.$VB$Me.MapVC.Ink.Strokes.Where((CS$<>8__locals1.$I1 == null) ? (CS$<>8__locals1.$I1 = ((Stroke x) => x.ContainsPropertyData(CS$<>8__locals1.$VB$Local_StrokeGUID))) : CS$<>8__locals1.$I1).ToList<Stroke>();
							if (list != null)
							{
								try
								{
									foreach (Stroke stroke in list)
									{
										if (stroke != null)
										{
											stroke.AddPropertyData(CS$<>8__locals1.$VB$Local_DeleterGUID, "deleterguid");
											CS$<>8__locals1.$VB$Me.MapVC.Ink.Strokes.Remove(stroke);
										}
									}
								}
								finally
								{
									List<Stroke>.Enumerator enumerator;
									((IDisposable)enumerator).Dispose();
								}
							}
						}
						catch (Exception ex)
						{
						}
					}), new object[0]);
				}
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000235C1 File Offset: 0x000217C1
		private void OnMissionStarted()
		{
			this.Comm_IsInMission = true;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000235CC File Offset: 0x000217CC
		private void OnMissionStopped()
		{
			if (this.Comm_IsInMission)
			{
				this.Comm_IsInMission = false;
				if (this.Rec_IsRecording)
				{
					this.CleanUpData();
				}
				if (this.Map_Import_InProgress)
				{
					MainWindow.MapImportFailEventHandler mapImportFailEvent = this.MapImportFailEvent;
					if (mapImportFailEvent != null)
					{
						mapImportFailEvent("Either the mission changed or the relay has stopped forwarding us information.");
					}
				}
				this.HideAll();
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0002361C File Offset: 0x0002181C
		private void OnArchiveElapsed()
		{
			if (this.Rec_IsRecording)
			{
				Frames frames = new Frames();
				frames.Mission = this.Frames_Recording.Mission;
				frames.Children.AddRange(this.Frames_Recording.Children);
				this.Frames_Recording.Children.Clear();
				this.SaveData(frames);
			}
			this.Comm_LastArchived = DateTime.Now;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00023680 File Offset: 0x00021880
		private void CleanUpData()
		{
			this.SaveData(this.Frames_Recording);
			this.ZipLiveFolder();
			this.EmptyLiveFolder();
			this.PlaybackHelper.Frames = new Frames();
			this.Frames_Recording = new Frames();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000236B8 File Offset: 0x000218B8
		private void SaveData(Frames FramesToSave)
		{
			if (FramesToSave != null && FramesToSave.Children.Count > 0)
			{
				string text = new Data().SaveString(FramesToSave);
				if (!string.IsNullOrEmpty(text))
				{
					DateTime now = DateTime.Now;
					string str = string.Concat(new string[]
					{
						now.Year.ToString(),
						now.Month.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Day.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Hour.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Minute.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Second.ToString().PadLeft(2, "0".ToCharArray()[0]),
						".json"
					});
					try
					{
						File.WriteAllText(this.DIR_Record_Live + "\\" + str, text);
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002380C File Offset: 0x00021A0C
		private void ZipLiveFolder()
		{
			try
			{
				if (Directory.GetFiles(this.DIR_Record_Live).Length > 0)
				{
					DateTime now = DateTime.Now;
					string str = string.Concat(new string[]
					{
						now.Year.ToString(),
						now.Month.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Day.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Hour.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Minute.ToString().PadLeft(2, "0".ToCharArray()[0]),
						now.Second.ToString().PadLeft(2, "0".ToCharArray()[0]),
						".zip"
					});
					ZipFile.CreateFromDirectory(this.DIR_Record_Live, this.DIR_Record_Archives + "\\" + str);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00023954 File Offset: 0x00021B54
		private void EmptyLiveFolder()
		{
			try
			{
				List<string> list = Directory.EnumerateFiles(this.DIR_Record_Live).ToList<string>();
				if (list != null && list.Count > 0)
				{
					try
					{
						foreach (string path in list)
						{
							File.Delete(path);
						}
					}
					finally
					{
						List<string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000239DC File Offset: 0x00021BDC
		private void PlayFrame(bool TriggeredByTimer = false)
		{
			if (this.PlaybackHelper != null && this.Rec_FrameCurrent < this.PlaybackHelper.TotalFrames)
			{
				if (TriggeredByTimer)
				{
					this.PlaybackTimer.Stop();
				}
				this.PlaybackHelper.LoadFramesWithIndex(this.Rec_FrameCurrent);
				int frameIndexOffsetFromChild = this.PlaybackHelper.GetFrameIndexOffsetFromChild(this.Rec_FrameCurrent);
				this.DisplayData(this.PlaybackHelper.Frames, frameIndexOffsetFromChild);
				ref int ptr = ref this.Rec_FrameCurrent;
				this.Rec_FrameCurrent = checked(ptr + 1);
				this.UpdateFrameControls(this.Rec_FrameCurrent);
				if (TriggeredByTimer)
				{
					this.PlaybackTimer.Start();
				}
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00023A6E File Offset: 0x00021C6E
		private void StartPlaybackCycle()
		{
			this.PlaybackTimer.AutoReset = true;
			this.PlaybackTimer.Interval = 1000.0 / this.Rec_PlaybackSpeed;
			this.PlaybackTimer.Enabled = true;
			this.PlaybackTimer.Start();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00023AAE File Offset: 0x00021CAE
		private void StopPlaybackCycle()
		{
			this.PlaybackTimer.Enabled = false;
			this.PlaybackTimer.Stop();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00023AC7 File Offset: 0x00021CC7
		private void PlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			this.PlayFrame(true);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00023AD0 File Offset: 0x00021CD0
		private void FrameFirst_Click(object sender, RoutedEventArgs e)
		{
			this.Rec_FrameCurrent = 0;
			this.PlayFrame(false);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00023AE0 File Offset: 0x00021CE0
		private void FramePrev_Click(object sender, RoutedEventArgs e)
		{
			if (this.Rec_FrameCurrent > 1)
			{
				ref int ptr = ref this.Rec_FrameCurrent;
				this.Rec_FrameCurrent = checked(ptr - 2);
				this.PlayFrame(false);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00023B0A File Offset: 0x00021D0A
		private void FrameNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.PlaybackHelper != null && this.Rec_FrameCurrent < this.PlaybackHelper.TotalFrames)
			{
				this.PlayFrame(false);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00023B2E File Offset: 0x00021D2E
		private void FrameLast_Click(object sender, RoutedEventArgs e)
		{
			if (this.PlaybackHelper != null)
			{
				this.Rec_FrameCurrent = checked(this.PlaybackHelper.TotalFrames - 1);
				this.PlayFrame(false);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00023B54 File Offset: 0x00021D54
		private void Frame_Current_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return && !string.IsNullOrEmpty(this.Frame_Current.Text))
			{
				int num = -1;
				try
				{
					num = Convert.ToInt32(this.Frame_Current.Text.Trim(), CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					num = -1;
				}
				if (num != -1)
				{
					this.Rec_FrameCurrent = checked(num - 1);
					this.PlayFrame(false);
				}
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00023BD0 File Offset: 0x00021DD0
		private void SpeedSlow_Click(object sender, RoutedEventArgs e)
		{
			if (this.Rec_PlaybackSpeed > this.Rec_PlaybackSpeedMin)
			{
				ref double ptr = ref this.Rec_PlaybackSpeed;
				this.Rec_PlaybackSpeed = ptr - this.Rec_PlaybackSpeedStep;
			}
			this.UpdateFrameControls(this.Rec_FrameCurrent);
			this.PlaybackTimer.Interval = 1000.0 / this.Rec_PlaybackSpeed;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00023C24 File Offset: 0x00021E24
		private void SpeedFast_Click(object sender, RoutedEventArgs e)
		{
			if (this.Rec_PlaybackSpeed < this.Rec_PlaybackSpeedMax)
			{
				ref double ptr = ref this.Rec_PlaybackSpeed;
				this.Rec_PlaybackSpeed = ptr + this.Rec_PlaybackSpeedStep;
			}
			this.UpdateFrameControls(this.Rec_FrameCurrent);
			this.PlaybackTimer.Interval = 1000.0 / this.Rec_PlaybackSpeed;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00023C78 File Offset: 0x00021E78
		private void ProcessFrame(string Frame)
		{
			Frames frames = this.ConvertFrameString(Frame);
			this.CurrentFrames = frames;
			if (frames != null && frames.Mission != null && !string.IsNullOrEmpty(frames.Mission.Map) && this.CheckHasMap(frames.Mission.Map))
			{
				this.DisplayData(frames, -1);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00023CCC File Offset: 0x00021ECC
		private Frames ConvertFrameString(string FrameString)
		{
			Frames frames = null;
			if (!string.IsNullOrEmpty(FrameString))
			{
				try
				{
					frames = new Data().Read(FrameString);
					if (frames != null && frames.Children != null && frames.Children.Count == 1)
					{
						if (!frames.MissionGUID.Equals(this.CurrentMissionGUID, StringComparison.InvariantCultureIgnoreCase))
						{
							MainWindow.Mission_StoppedEventHandler mission_StoppedEvent = this.Mission_StoppedEvent;
							if (mission_StoppedEvent != null)
							{
								mission_StoppedEvent();
							}
							MainWindow.Mission_StartedEventHandler mission_StartedEvent = this.Mission_StartedEvent;
							if (mission_StartedEvent != null)
							{
								mission_StartedEvent();
							}
							this.CurrentMissionGUID = frames.MissionGUID;
							this.CurrentMissionSteamID = frames.Mission.SteamID;
							this.Frames_Recording.MissionGUID = frames.MissionGUID;
						}
						this.Frames_Recording.Mission.Name = frames.Mission.Name;
						this.Frames_Recording.Mission.Description = frames.Mission.Description;
						this.Frames_Recording.Mission.Author = frames.Mission.Author;
						this.Frames_Recording.Mission.Profile = frames.Mission.Profile;
						this.Frames_Recording.Mission.Map = frames.Mission.Map;
						this.Frames_Recording.Mission.IsMultiplayer = frames.Mission.IsMultiplayer;
						this.Frames_Recording.Mission.SteamID = frames.Mission.SteamID;
						if (this.Rec_IsRecording && DateTime.Now.Subtract(this.Rec_LastFrameRecordedOn).TotalSeconds >= (double)this.Rec_RecordFrameIntervalSeconds)
						{
							this.Frames_Recording.Children.Add(frames.Children[checked(frames.Children.Count - 1)]);
							this.Rec_LastFrameRecordedOn = DateTime.Now;
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
			return frames;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00023EC4 File Offset: 0x000220C4
		private bool CheckHasMap(string World)
		{
			bool result;
			if (this.Map_Current != null && Operators.CompareString(this.Map_Current.WorldName, World, false) == 0)
			{
				result = true;
			}
			else
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(World))
				{
					Map map = null;
					if (this.Maps != null || this.Maps.Count != 0)
					{
						map = this.Maps.SingleOrDefault((KeyValuePair<string, Map> x) => x.Value.WorldName.Equals(World, StringComparison.InvariantCultureIgnoreCase)).Value;
					}
					if (map == null || string.IsNullOrEmpty(map.WorldName))
					{
						base.Dispatcher.Invoke(delegate()
						{
							if (this.brdMapMissing.Visibility > Visibility.Visible & !this.brdMapImport.IsVisible & !this.Map_Import_InProgress)
							{
								this.brdMapMissing.Visibility = Visibility.Visible;
							}
						});
					}
					else
					{
						base.Dispatcher.Invoke(delegate()
						{
							this.brdMapMissing.Visibility = Visibility.Collapsed;
						});
						this.LoadMap(map);
						flag = true;
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00023F9C File Offset: 0x0002219C
		private void DisplayData(Frames MyFrames, int FrameIndex = -1)
		{
			try
			{
				base.Dispatcher.Invoke(delegate()
				{
					this.DebugDataReceived.Text = string.Concat(new string[]
					{
						DateTime.Now.Hour.ToString(),
						":",
						DateTime.Now.Minute.ToString(),
						":",
						DateTime.Now.Second.ToString(),
						":",
						DateTime.Now.Millisecond.ToString()
					});
				});
				if (this.IsMapReady)
				{
					this.UpdateMissionInfo(MyFrames, FrameIndex);
					this.UpdateDictionaries(MyFrames, FrameIndex);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00023FF8 File Offset: 0x000221F8
		private void ClearDictionaries()
		{
			try
			{
				List<Vehicle> list = null;
				this.UpdateDictionaryOfVehicles(ref list);
				List<Unit> list2 = null;
				this.UpdateDictionaryOfUnits(ref list2);
				List<Group> list3 = null;
				this.UpdateDictionaryOfGroups(ref list3);
				List<Marker> list4 = null;
				this.UpdateDictionaryOfMarkers(ref list4);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00024050 File Offset: 0x00022250
		private void UpdateDictionaries(Frames MyFrames, int FrameIndex = -1)
		{
			try
			{
				if (MyFrames != null && MyFrames.Children != null && MyFrames.Children.Count != 0)
				{
					if (FrameIndex < 0)
					{
						FrameIndex = 0;
					}
					else if (FrameIndex >= MyFrames.Children.Count)
					{
						FrameIndex = checked(MyFrames.Children.Count - 1);
					}
					Athena.Objects.v2.Frame frame = MyFrames.Children[FrameIndex];
					if (frame != null)
					{
						this.UpdateDictionaryOfVehicles(ref frame.Vehicles);
						this.UpdateDictionaryOfUnits(ref frame.Units);
						this.UpdateDictionaryOfGroups(ref frame.Groups);
						this.UpdateDictionaryOfMarkers(ref frame.Markers);
						this.UpdateVehicleLinks();
						this.UpdateUnitIcons();
						this.UpdateGroupIcons();
						this.UpdateVehicleIcons();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00024120 File Offset: 0x00022320
		private void UpdateDictionaryOfMarkers(ref List<Marker> Markers)
		{
			if (Common.DictionaryOfMarkers != null && Common.DictionaryOfMarkers.Count != 0)
			{
				try
				{
					foreach (KeyValuePair<string, DictionaryOfMarkerItem> keyValuePair in Common.DictionaryOfMarkers)
					{
						keyValuePair.Value.Updated = false;
					}
				}
				finally
				{
					Dictionary<string, DictionaryOfMarkerItem>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			if ((Common.DictionaryOfMarkers != null & Markers != null) && Markers.Count != 0)
			{
				bool flag = false;
				if (DateAndTime.DateDiff(DateInterval.Second, this.LastUpdated_Markers, DateTime.Now, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) >= 1L)
				{
					this.LastUpdated_Markers = DateTime.Now;
					flag = true;
				}
				try
				{
					foreach (Marker marker in Markers)
					{
						if (this.Map_Current != null)
						{
							Common.ConvertPos(this.Map_Current.WorldSize, marker.PosX, marker.PosY, ref marker.CanvasX, ref marker.CanvasY);
						}
						if (!string.IsNullOrEmpty(marker.Dir) && !double.TryParse(marker.Dir, NumberStyles.Number, CultureInfo.InvariantCulture, out marker.Direction))
						{
							marker.Direction = 0.0;
						}
						double.TryParse(marker.SizeX, NumberStyles.Number, CultureInfo.InvariantCulture, out marker.Width);
						double.TryParse(marker.SizeY, NumberStyles.Number, CultureInfo.InvariantCulture, out marker.Height);
						if (!(marker.ShapeID == Enums.MarkerShape.ICON & marker.TypeID == Enums.MarkerType.Empty))
						{
							MainWindow._Closure$__607-0 CS$<>8__locals1 = new MainWindow._Closure$__607-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							CS$<>8__locals1.$VB$Local_MarkerItem = null;
							Common.DictionaryOfMarkers.TryGetValue(marker.Name, out CS$<>8__locals1.$VB$Local_MarkerItem);
							if (CS$<>8__locals1.$VB$Local_MarkerItem == null)
							{
								CS$<>8__locals1.$VB$Local_MarkerItem = new DictionaryOfMarkerItem
								{
									Marker = marker,
									Icon = null,
									Updated = true
								};
								Common.DictionaryOfMarkers.Add(marker.Name, CS$<>8__locals1.$VB$Local_MarkerItem);
							}
							else
							{
								CS$<>8__locals1.$VB$Local_MarkerItem.Marker = marker;
								CS$<>8__locals1.$VB$Local_MarkerItem.Updated = true;
							}
							if (CS$<>8__locals1.$VB$Local_MarkerItem != null && flag)
							{
								base.Dispatcher.Invoke(delegate()
								{
									CS$<>8__locals1.$VB$Me.UpdateMarkerIcon(ref CS$<>8__locals1.$VB$Local_MarkerItem);
								});
							}
						}
					}
				}
				finally
				{
					List<Marker>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
			}
			if (Common.DictionaryOfMarkers != null && Common.DictionaryOfMarkers.Count != 0)
			{
				List<DictionaryOfMarkerItem> list = new List<DictionaryOfMarkerItem>();
				try
				{
					foreach (KeyValuePair<string, DictionaryOfMarkerItem> keyValuePair2 in Common.DictionaryOfMarkers)
					{
						if (!keyValuePair2.Value.Updated)
						{
							list.Add(keyValuePair2.Value);
						}
					}
				}
				finally
				{
					Dictionary<string, DictionaryOfMarkerItem>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
				try
				{
					List<DictionaryOfMarkerItem>.Enumerator enumerator4 = list.GetEnumerator();
					while (enumerator4.MoveNext())
					{
						MainWindow._Closure$__607-1 CS$<>8__locals2 = new MainWindow._Closure$__607-1(CS$<>8__locals2);
						CS$<>8__locals2.$VB$Me = this;
						CS$<>8__locals2.$VB$Local_StaleItem = enumerator4.Current;
						base.Dispatcher.Invoke(delegate()
						{
							CS$<>8__locals2.$VB$Me.RemoveMarkerIcon(ref CS$<>8__locals2.$VB$Local_StaleItem);
						});
						Common.DictionaryOfMarkers.Remove(CS$<>8__locals2.$VB$Local_StaleItem.Marker.Name);
					}
				}
				finally
				{
					List<DictionaryOfMarkerItem>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
				list = null;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000244B4 File Offset: 0x000226B4
		private void UpdateDictionaryOfGroups(ref List<Group> Groups)
		{
			if (Common.DictionaryOfGroups != null)
			{
				if (Common.DictionaryOfGroups.Count != 0)
				{
					try
					{
						foreach (KeyValuePair<string, DictionaryOfGroupItem> keyValuePair in Common.DictionaryOfGroups)
						{
							keyValuePair.Value.Updated = false;
						}
					}
					finally
					{
						Dictionary<string, DictionaryOfGroupItem>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				if (Groups != null && Groups.Count != 0)
				{
					this.LastUpdated_Groups = DateTime.Now;
					bool updateIcon = true;
					try
					{
						foreach (Group group in Groups)
						{
							if (this.Map_Current != null && (!string.IsNullOrEmpty(group.WPX) & !string.IsNullOrEmpty(group.WPY)))
							{
								Common.ConvertPos(this.Map_Current.WorldSize, group.WPX, group.WPY, ref group.CanvasWPX, ref group.CanvasWPY);
							}
							if (!string.IsNullOrEmpty(group.LeaderNetID) && Common.DictionaryOfUnits.ContainsKey(group.LeaderNetID))
							{
								DictionaryOfGroupItem dictionaryOfGroupItem = null;
								if (Common.DictionaryOfGroups.TryGetValue(group.GroupNetID, out dictionaryOfGroupItem))
								{
									dictionaryOfGroupItem.Group = group;
									dictionaryOfGroupItem.Updated = true;
								}
								else
								{
									dictionaryOfGroupItem = new DictionaryOfGroupItem
									{
										Group = group,
										Icon = null,
										Updated = true
									};
									Common.DictionaryOfGroups.Add(group.GroupNetID, dictionaryOfGroupItem);
								}
								if (dictionaryOfGroupItem != null)
								{
									dictionaryOfGroupItem.UpdateIcon = updateIcon;
									if (!dictionaryOfGroupItem.UpdateIcon && this.PlayerUnit != null && dictionaryOfGroupItem.Group != null && Operators.CompareString(this.PlayerUnit.GroupNetID, dictionaryOfGroupItem.Group.GroupNetID, false) == 0)
									{
										dictionaryOfGroupItem.UpdateIcon = true;
									}
								}
							}
						}
					}
					finally
					{
						List<Group>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				List<DictionaryOfGroupItem> list = Common.DictionaryOfGroups.Where((MainWindow._Closure$__.$I608-0 == null) ? (MainWindow._Closure$__.$I608-0 = ((KeyValuePair<string, DictionaryOfGroupItem> x) => !x.Value.Updated)) : MainWindow._Closure$__.$I608-0).Select((MainWindow._Closure$__.$I608-1 == null) ? (MainWindow._Closure$__.$I608-1 = ((KeyValuePair<string, DictionaryOfGroupItem> x) => x.Value)) : MainWindow._Closure$__.$I608-1).ToList<DictionaryOfGroupItem>();
				if (list != null)
				{
					try
					{
						List<DictionaryOfGroupItem>.Enumerator enumerator3 = list.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							MainWindow._Closure$__608-0 CS$<>8__locals1 = new MainWindow._Closure$__608-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							CS$<>8__locals1.$VB$Local_StaleItem = enumerator3.Current;
							base.Dispatcher.Invoke(delegate()
							{
								CS$<>8__locals1.$VB$Me.RemoveGroupIcon(ref CS$<>8__locals1.$VB$Local_StaleItem);
								CS$<>8__locals1.$VB$Me.RemoveGroupORBAT(ref CS$<>8__locals1.$VB$Local_StaleItem);
								CS$<>8__locals1.$VB$Me.RemoveGroupLine(ref CS$<>8__locals1.$VB$Local_StaleItem);
							});
							Common.DictionaryOfGroups.Remove(CS$<>8__locals1.$VB$Local_StaleItem.Group.GroupNetID);
						}
					}
					finally
					{
						List<DictionaryOfGroupItem>.Enumerator enumerator3;
						((IDisposable)enumerator3).Dispose();
					}
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000247B0 File Offset: 0x000229B0
		private void UpdateDictionaryOfUnits(ref List<Unit> Units)
		{
			if (Common.DictionaryOfUnits != null)
			{
				if (Common.DictionaryOfUnits.Count != 0)
				{
					try
					{
						foreach (KeyValuePair<string, DictionaryOfUnitItem> keyValuePair in Common.DictionaryOfUnits)
						{
							keyValuePair.Value.Updated = false;
						}
					}
					finally
					{
						Dictionary<string, DictionaryOfUnitItem>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				if (Units != null && Units.Count != 0)
				{
					bool flag = false;
					this.LastUpdated_Units = DateTime.Now;
					bool updateIcon = true;
					try
					{
						foreach (Unit unit in Units)
						{
							MainWindow._Closure$__609-0 CS$<>8__locals1 = new MainWindow._Closure$__609-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							if (this.Map_Current != null && (!string.IsNullOrEmpty(unit.PosX) & !string.IsNullOrEmpty(unit.PosY)))
							{
								Common.ConvertPos(this.Map_Current.WorldSize, unit.PosX, unit.PosY, ref unit.CanvasX, ref unit.CanvasY);
							}
							if (!string.IsNullOrEmpty(unit.Dir) && !double.TryParse(unit.Dir, NumberStyles.Number, CultureInfo.InvariantCulture, out unit.Direction))
							{
								unit.Direction = 0.0;
							}
							CS$<>8__locals1.$VB$Local_UnitItem = null;
							if (Common.DictionaryOfUnits.TryGetValue(unit.NetID, out CS$<>8__locals1.$VB$Local_UnitItem))
							{
								CS$<>8__locals1.$VB$Local_UnitItem.Unit = unit;
								CS$<>8__locals1.$VB$Local_UnitItem.Updated = true;
							}
							else
							{
								CS$<>8__locals1.$VB$Local_UnitItem = new DictionaryOfUnitItem
								{
									Unit = unit,
									Icon = null,
									Updated = true
								};
								Common.DictionaryOfUnits.Add(unit.NetID, CS$<>8__locals1.$VB$Local_UnitItem);
								base.Dispatcher.Invoke(delegate()
								{
									CS$<>8__locals1.$VB$Me.AddUnitToUnitList(ref CS$<>8__locals1.$VB$Local_UnitItem);
								});
							}
							if (CS$<>8__locals1.$VB$Local_UnitItem != null)
							{
								CS$<>8__locals1.$VB$Local_UnitItem.UpdateIcon = updateIcon;
								if (CS$<>8__locals1.$VB$Local_UnitItem.Unit != null && Operators.CompareString(CS$<>8__locals1.$VB$Local_UnitItem.Unit.SteamID, this.CurrentMissionSteamID, false) == 0)
								{
									this.PlayerUnit = CS$<>8__locals1.$VB$Local_UnitItem.Unit;
								}
								if (!flag && this.PlayerUnit != null && CS$<>8__locals1.$VB$Local_UnitItem.Unit != null && Operators.CompareString(this.PlayerUnit.GroupNetID, CS$<>8__locals1.$VB$Local_UnitItem.Unit.GroupNetID, false) == 0)
								{
									CS$<>8__locals1.$VB$Local_UnitItem.UpdateIcon = true;
								}
							}
						}
					}
					finally
					{
						List<Unit>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
				}
				List<DictionaryOfUnitItem> list = Common.DictionaryOfUnits.Where((MainWindow._Closure$__.$I609-1 == null) ? (MainWindow._Closure$__.$I609-1 = ((KeyValuePair<string, DictionaryOfUnitItem> x) => !x.Value.Updated)) : MainWindow._Closure$__.$I609-1).Select((MainWindow._Closure$__.$I609-2 == null) ? (MainWindow._Closure$__.$I609-2 = ((KeyValuePair<string, DictionaryOfUnitItem> x) => x.Value)) : MainWindow._Closure$__.$I609-2).ToList<DictionaryOfUnitItem>();
				if (list != null)
				{
					try
					{
						List<DictionaryOfUnitItem>.Enumerator enumerator3 = list.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							MainWindow._Closure$__609-1 CS$<>8__locals2 = new MainWindow._Closure$__609-1(CS$<>8__locals2);
							CS$<>8__locals2.$VB$Me = this;
							CS$<>8__locals2.$VB$Local_StaleItem = enumerator3.Current;
							base.Dispatcher.Invoke(delegate()
							{
								CS$<>8__locals2.$VB$Me.RemoveUnitFromUnitList(ref CS$<>8__locals2.$VB$Local_StaleItem);
								CS$<>8__locals2.$VB$Me.RemoveUnitIcon(ref CS$<>8__locals2.$VB$Local_StaleItem);
								CS$<>8__locals2.$VB$Me.RemoveUnitORBAT(ref CS$<>8__locals2.$VB$Local_StaleItem);
							});
							Common.DictionaryOfUnits.Remove(CS$<>8__locals2.$VB$Local_StaleItem.Unit.NetID);
						}
					}
					finally
					{
						List<DictionaryOfUnitItem>.Enumerator enumerator3;
						((IDisposable)enumerator3).Dispose();
					}
				}
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00024B60 File Offset: 0x00022D60
		private void UpdateDictionaryOfVehicles(ref List<Vehicle> Vehicles)
		{
			if (Common.DictionaryOfVehicles != null && Common.DictionaryOfVehicles.Count != 0)
			{
				try
				{
					foreach (KeyValuePair<string, DictionaryOfVehicleItem> keyValuePair in Common.DictionaryOfVehicles)
					{
						keyValuePair.Value.Updated = false;
					}
				}
				finally
				{
					Dictionary<string, DictionaryOfVehicleItem>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			if ((Common.DictionaryOfVehicles != null & Vehicles != null) && Vehicles.Count != 0)
			{
				bool updateIcon = false;
				this.LastUpdated_Vehicles = DateTime.Now;
				updateIcon = true;
				try
				{
					foreach (Vehicle vehicle in Vehicles)
					{
						if (this.Map_Current != null && (!string.IsNullOrEmpty(vehicle.PosX) & !string.IsNullOrEmpty(vehicle.PosY)))
						{
							Common.ConvertPos(this.Map_Current.WorldSize, vehicle.PosX, vehicle.PosY, ref vehicle.CanvasX, ref vehicle.CanvasY);
						}
						if (!string.IsNullOrEmpty(vehicle.Dir) && !double.TryParse(vehicle.Dir, NumberStyles.Number, CultureInfo.InvariantCulture, out vehicle.Direction))
						{
							vehicle.Direction = 0.0;
						}
						if (vehicle.Crew != null)
						{
							List<string> list = vehicle.Crew.Split(new char[]
							{
								','
							}).ToList<string>();
							if (list != null && list.Count != 0)
							{
								try
								{
									foreach (string text in list)
									{
										string[] array = text.Split(new char[]
										{
											';'
										});
										if (array != null && array.Count<string>() == 2 && !vehicle.CrewDictionary.ContainsKey(array[0]))
										{
											vehicle.CrewDictionary.Add(array[0], new CrewDictionaryItem
											{
												Position = array[1]
											});
										}
									}
								}
								finally
								{
									List<string>.Enumerator enumerator3;
									((IDisposable)enumerator3).Dispose();
								}
							}
						}
						DictionaryOfVehicleItem dictionaryOfVehicleItem = null;
						if (Common.DictionaryOfVehicles.TryGetValue(vehicle.NetID, out dictionaryOfVehicleItem))
						{
							dictionaryOfVehicleItem.Vehicle = vehicle;
							dictionaryOfVehicleItem.Updated = true;
						}
						else
						{
							dictionaryOfVehicleItem = new DictionaryOfVehicleItem
							{
								Vehicle = vehicle,
								Icon = null,
								Updated = true
							};
							Common.DictionaryOfVehicles.Add(vehicle.NetID, dictionaryOfVehicleItem);
						}
						if (dictionaryOfVehicleItem != null)
						{
							dictionaryOfVehicleItem.UpdateIcon = updateIcon;
							if (!dictionaryOfVehicleItem.UpdateIcon && this.PlayerUnit != null && dictionaryOfVehicleItem.Vehicle != null && string.Equals(this.PlayerUnit.VehicleNetID, dictionaryOfVehicleItem.Vehicle.NetID, StringComparison.InvariantCultureIgnoreCase))
							{
								dictionaryOfVehicleItem.UpdateIcon = true;
							}
						}
					}
				}
				finally
				{
					List<Vehicle>.Enumerator enumerator2;
					((IDisposable)enumerator2).Dispose();
				}
			}
			List<DictionaryOfVehicleItem> list2 = Common.DictionaryOfVehicles.Where((MainWindow._Closure$__.$I610-0 == null) ? (MainWindow._Closure$__.$I610-0 = ((KeyValuePair<string, DictionaryOfVehicleItem> x) => !x.Value.Updated)) : MainWindow._Closure$__.$I610-0).Select((MainWindow._Closure$__.$I610-1 == null) ? (MainWindow._Closure$__.$I610-1 = ((KeyValuePair<string, DictionaryOfVehicleItem> x) => x.Value)) : MainWindow._Closure$__.$I610-1).ToList<DictionaryOfVehicleItem>();
			if (list2 != null)
			{
				try
				{
					List<DictionaryOfVehicleItem>.Enumerator enumerator4 = list2.GetEnumerator();
					while (enumerator4.MoveNext())
					{
						MainWindow._Closure$__610-0 CS$<>8__locals1 = new MainWindow._Closure$__610-0(CS$<>8__locals1);
						CS$<>8__locals1.$VB$Me = this;
						CS$<>8__locals1.$VB$Local_StaleItem = enumerator4.Current;
						base.Dispatcher.Invoke(delegate()
						{
							CS$<>8__locals1.$VB$Me.RemoveVehicleIcon(ref CS$<>8__locals1.$VB$Local_StaleItem);
						});
						Common.DictionaryOfVehicles.Remove(CS$<>8__locals1.$VB$Local_StaleItem.Vehicle.NetID);
					}
				}
				finally
				{
					List<DictionaryOfVehicleItem>.Enumerator enumerator4;
					((IDisposable)enumerator4).Dispose();
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00024F48 File Offset: 0x00023148
		private void UpdateMissionInfo(Frames MyFrames, int FrameIndex = -1)
		{
			if (this.Map_Current != null && MyFrames != null && MyFrames.Mission != null && MyFrames.Children != null && MyFrames.Children.Count != 0)
			{
				Athena.Objects.v2.Frame frame = MyFrames.Children[checked(MyFrames.Children.Count - 1)];
				if (FrameIndex >= 0 & FrameIndex < MyFrames.Children.Count)
				{
					frame = MyFrames.Children[FrameIndex];
				}
				string FrameDate = string.Empty;
				string FrameTime = string.Empty;
				if (!string.IsNullOrEmpty(frame.Date))
				{
					FrameDate = frame.Date;
				}
				if (!string.IsNullOrEmpty(frame.Time) && frame.Time.Contains(":"))
				{
					string[] array = frame.Time.Split(new char[]
					{
						':'
					});
					if (array.Length == 2)
					{
						FrameTime = array[0] + ":" + array[1].PadLeft(2, "0".ToCharArray()[0]);
					}
				}
				base.Dispatcher.Invoke(delegate()
				{
					this.MissionTime.Text = FrameDate + " " + FrameTime;
					this.MissionTime.Visibility = Visibility.Visible;
				});
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00025081 File Offset: 0x00023281
		private void RemoveMissionInfo()
		{
			this.MissionTime.Text = string.Empty;
			this.MissionTime.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000250A0 File Offset: 0x000232A0
		private void UpdateUnitIcons()
		{
			if (Common.DictionaryOfUnits != null)
			{
				List<DictionaryOfUnitItem> list = Common.DictionaryOfUnits.Where((MainWindow._Closure$__.$I613-0 == null) ? (MainWindow._Closure$__.$I613-0 = ((KeyValuePair<string, DictionaryOfUnitItem> x) => x.Value.UpdateIcon)) : MainWindow._Closure$__.$I613-0).Select((MainWindow._Closure$__.$I613-1 == null) ? (MainWindow._Closure$__.$I613-1 = ((KeyValuePair<string, DictionaryOfUnitItem> x) => x.Value)) : MainWindow._Closure$__.$I613-1).ToList<DictionaryOfUnitItem>();
				if (list != null)
				{
					try
					{
						List<DictionaryOfUnitItem>.Enumerator enumerator = list.GetEnumerator();
						while (enumerator.MoveNext())
						{
							MainWindow._Closure$__613-0 CS$<>8__locals1 = new MainWindow._Closure$__613-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							CS$<>8__locals1.$VB$Local_UnitItem = enumerator.Current;
							base.Dispatcher.Invoke(delegate()
							{
								CS$<>8__locals1.$VB$Me.UpdateUnitIcon(ref CS$<>8__locals1.$VB$Local_UnitItem);
								CS$<>8__locals1.$VB$Me.UpdateUnitORBAT(ref CS$<>8__locals1.$VB$Local_UnitItem);
							});
						}
					}
					finally
					{
						List<DictionaryOfUnitItem>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00025180 File Offset: 0x00023380
		private void UpdateUnitIcon(ref DictionaryOfUnitItem UnitItem)
		{
			if (this.Map_Current != null && UnitItem != null && UnitItem.Unit != null)
			{
				DictionaryOfGroupItem dictionaryOfGroupItem = null;
				Common.DictionaryOfGroups.TryGetValue(UnitItem.Unit.GroupNetID, out dictionaryOfGroupItem);
				if (dictionaryOfGroupItem != null)
				{
					if (UnitItem.Icon == null)
					{
						UnitItem.Icon = new Unit(ref UnitItem.Unit, ref dictionaryOfGroupItem.Group);
						if (UnitItem.Icon != null && !UnitItem.Unit.Side.ToLower().Contains("logic"))
						{
							this.MapVC.Units.Children.Add(UnitItem.Icon);
							this.OnUserInitiatedMapRenderModeChange += UnitItem.Icon.HandleRenderModeChange;
							this.ScaleChanged += UnitItem.Icon.HandleScaleChange;
						}
					}
					else
					{
						UnitItem.Icon.Unit = UnitItem.Unit;
						UnitItem.Icon.Group = dictionaryOfGroupItem.Group;
						UnitItem.Icon.SetVisibility();
						UnitItem.Icon.Update();
					}
				}
				dictionaryOfGroupItem = null;
				if (UnitItem.Icon != null)
				{
					this.UpdateUnitIconPosition(UnitItem.Icon);
					if (!string.IsNullOrEmpty(this.FollowedUnitNetID) && (Operators.CompareString(UnitItem.Unit.NetID, this.FollowedUnitNetID, false) == 0 | (Operators.CompareString(this.FollowedUnitNetID, "special_player", false) == 0 & Operators.CompareString(UnitItem.Unit.SteamID, this.CurrentMissionSteamID, false) == 0)))
					{
						string empty = string.Empty;
						Common.ConvertPosToGrid(ref this.Map_Current, UnitItem.Unit.PosX, UnitItem.Unit.PosY, ref empty);
						if (!string.IsNullOrEmpty(empty))
						{
							this.UnitGridPos.Text = "G:" + empty;
						}
						this.UpdateTracking(ref UnitItem.Unit);
						if (this.Map_Scroll_Follow)
						{
							this.MapScroll.ScrollToHorizontalOffset(UnitItem.Unit.CanvasX * this.ZoomFactor - this.MapScroll.ActualWidth / 2.0);
							this.MapScroll.ScrollToVerticalOffset(UnitItem.Unit.CanvasY * this.ZoomFactor - this.MapScroll.ActualHeight / 2.0);
							this.MapVC.StartUpdate();
							string.IsNullOrEmpty(UnitItem.Unit.VehicleNetID);
						}
					}
				}
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002540B File Offset: 0x0002360B
		private void HandleUnitIconSizeChanged(Unit Icon)
		{
			this.UpdateUnitIconPosition(Icon);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00025414 File Offset: 0x00023614
		private void UpdateUnitIconPosition(Unit Icon)
		{
			Canvas.SetLeft(Icon, Icon.Unit.CanvasX - Icon.MarkerIcon.IconWidth / 2.0);
			Canvas.SetTop(Icon, Icon.Unit.CanvasY - Icon.MarkerIcon.IconHeight / 2.0);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00025470 File Offset: 0x00023670
		private void RemoveUnitIcon(ref DictionaryOfUnitItem UnitItem)
		{
			if (UnitItem != null && UnitItem.Icon != null)
			{
				this.MapVC.Units.Children.Remove(UnitItem.Icon);
				this.OnUserInitiatedMapRenderModeChange -= UnitItem.Icon.HandleRenderModeChange;
				this.ScaleChanged -= UnitItem.Icon.HandleScaleChange;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000254D8 File Offset: 0x000236D8
		private void UpdateUnitORBAT(ref DictionaryOfUnitItem UnitItem)
		{
			if (UnitItem != null && UnitItem.Unit != null)
			{
				DictionaryOfGroupItem dictionaryOfGroupItem = null;
				if (Common.DictionaryOfGroups != null)
				{
					Common.DictionaryOfGroups.TryGetValue(UnitItem.Unit.GroupNetID, out dictionaryOfGroupItem);
					if (dictionaryOfGroupItem != null && (dictionaryOfGroupItem.Group != null & dictionaryOfGroupItem.ORBATPanel != null))
					{
						if (UnitItem.ORBATLabel == null)
						{
							UnitItem.ORBATLabel = this.CreateUnitLabel(ref dictionaryOfGroupItem.ORBATPanel, ref UnitItem.Unit, Conversions.ToBoolean(Interaction.IIf(Operators.CompareString(UnitItem.Unit.NetID, dictionaryOfGroupItem.Group.LeaderNetID, false) == 0, true, false)));
						}
						else
						{
							this.UpdateUnitLabel(ref UnitItem.ORBATLabel, ref UnitItem.Unit, Conversions.ToBoolean(Interaction.IIf(Operators.CompareString(UnitItem.Unit.NetID, dictionaryOfGroupItem.Group.LeaderNetID, false) == 0, true, false)));
							if (!dictionaryOfGroupItem.ORBATPanel.Children.Contains(UnitItem.ORBATLabel))
							{
								try
								{
									StackPanel stackPanel = UnitItem.ORBATLabel.Parent as StackPanel;
									if (stackPanel != null)
									{
										stackPanel.Children.Remove(UnitItem.ORBATLabel);
										dictionaryOfGroupItem.ORBATPanel.Children.Add(UnitItem.ORBATLabel);
									}
								}
								catch (Exception ex)
								{
								}
							}
						}
					}
				}
				dictionaryOfGroupItem = null;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00025660 File Offset: 0x00023860
		private void RemoveUnitORBAT(ref DictionaryOfUnitItem UnitItem)
		{
			if (UnitItem != null && UnitItem.ORBATLabel != null)
			{
				this.RemoveUnitLabel(ref UnitItem.ORBATLabel);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0002567C File Offset: 0x0002387C
		private void UpdateZoomBasedOnVehicleSpeed(ref DictionaryOfUnitItem UnitItem)
		{
			if (UnitItem != null && UnitItem.Unit != null && !string.IsNullOrEmpty(UnitItem.Unit.Speed))
			{
				try
				{
					double num = Convert.ToDouble(UnitItem.Unit.Speed, CultureInfo.InvariantCulture);
					if (num < 50.0)
					{
						this.Zoom(1.5, false, true);
					}
					else if (num < 100.0)
					{
						this.Zoom(1.2, false, true);
					}
					else if (num < 150.0)
					{
						this.Zoom(0.9, false, true);
					}
					else if (num < 200.0)
					{
						this.Zoom(0.6, false, true);
					}
					else
					{
						this.Zoom(0.3, false, true);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00025778 File Offset: 0x00023978
		private Unit GetFollowedUnit()
		{
			Unit result = null;
			try
			{
				if (Common.DictionaryOfUnits != null)
				{
					if (Operators.CompareString(this.FollowedUnitNetID, "special_player", false) == 0)
					{
						try
						{
							foreach (KeyValuePair<string, DictionaryOfUnitItem> keyValuePair in Common.DictionaryOfUnits)
							{
								if (keyValuePair.Value != null && keyValuePair.Value.Unit != null && Operators.CompareString(keyValuePair.Value.Unit.SteamID, this.CurrentMissionSteamID, false) == 0)
								{
									result = keyValuePair.Value.Unit;
									break;
								}
							}
							goto IL_B3;
						}
						finally
						{
							Dictionary<string, DictionaryOfUnitItem>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					DictionaryOfUnitItem dictionaryOfUnitItem = null;
					Common.DictionaryOfUnits.TryGetValue(this.FollowedUnitNetID, out dictionaryOfUnitItem);
					if (dictionaryOfUnitItem != null)
					{
						result = dictionaryOfUnitItem.Unit;
					}
					dictionaryOfUnitItem = null;
				}
				IL_B3:;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00025868 File Offset: 0x00023A68
		private Unit GetGroupLeader(Group Group)
		{
			Unit result = null;
			try
			{
				if (Group != null & Common.DictionaryOfUnits != null)
				{
					DictionaryOfUnitItem dictionaryOfUnitItem = null;
					Common.DictionaryOfUnits.TryGetValue(Group.LeaderNetID, out dictionaryOfUnitItem);
					if (dictionaryOfUnitItem != null)
					{
						result = dictionaryOfUnitItem.Unit;
					}
					dictionaryOfUnitItem = null;
				}
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000258CC File Offset: 0x00023ACC
		private void UpdateTracking(ref Unit Unit)
		{
			if (this.Map_Current != null & Unit != null)
			{
				Point point = new Point(Unit.CanvasX, Unit.CanvasY);
				if (this.TrackingAnchor != null)
				{
					Point point2 = new Point(this.TrackingAnchor.CanvasX, this.TrackingAnchor.CanvasY);
					Color color = Colors.Red;
					TextBlock textBlock = this.lblTrackingAnchor;
					this.UpdateTrackingLine(ref this.TrackingAnchorLine, ref point, ref point2, ref color, ref textBlock);
					this.lblTrackingAnchor = textBlock;
				}
				if (this.TrackingGroup != null)
				{
					Unit groupLeader = this.GetGroupLeader(this.TrackingGroup);
					if (groupLeader == null)
					{
						this.TrackingGroup = null;
						this.TrackingGroupLine.Visibility = Visibility.Collapsed;
					}
					else
					{
						Point point2 = new Point(groupLeader.CanvasX, groupLeader.CanvasY);
						Color color = Colors.Green;
						TextBlock textBlock = this.lblTrackingGroup;
						this.UpdateTrackingLine(ref this.TrackingGroupLine, ref point, ref point2, ref color, ref textBlock);
						this.lblTrackingGroup = textBlock;
					}
				}
				if (this.TrackingUnit != null)
				{
					DictionaryOfUnitItem dictionaryOfUnitItem = null;
					Common.DictionaryOfUnits.TryGetValue(this.TrackingUnit.NetID, out dictionaryOfUnitItem);
					if (dictionaryOfUnitItem != null)
					{
						this.TrackingUnit = dictionaryOfUnitItem.Unit;
					}
					if (this.TrackingUnit == null)
					{
						this.TrackingUnit = null;
						this.TrackingUnitLine.Visibility = Visibility.Collapsed;
						return;
					}
					Point point2 = new Point(this.TrackingUnit.CanvasX, this.TrackingUnit.CanvasY);
					Color color = Colors.Blue;
					TextBlock textBlock = this.lblTrackingUnit;
					this.UpdateTrackingLine(ref this.TrackingUnitLine, ref point, ref point2, ref color, ref textBlock);
					this.lblTrackingUnit = textBlock;
				}
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00025A58 File Offset: 0x00023C58
		private void UpdateTrackingLine(ref Line Line, ref Point UnitPoint, ref Point TargetPoint, ref Color Color, ref TextBlock Label)
		{
			if (Line == null)
			{
				Line = new Line();
				Line.StrokeThickness = 1.0;
				Line.Stroke = new SolidColorBrush(Color);
				this.MapVC.Tracking.Children.Add(Line);
			}
			double num = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(UnitPoint.X - TargetPoint.X), 2.0) + Math.Pow(Math.Abs(UnitPoint.Y - TargetPoint.Y), 2.0)));
			double num2 = Math.Round(Math.Atan2(UnitPoint.Y - TargetPoint.Y, UnitPoint.X - TargetPoint.X) * 57.29577951308232);
			if (num2 < 0.0)
			{
				num2 = 180.0 + (180.0 - Math.Abs(num2));
			}
			if (num2 < 90.0)
			{
				num2 += 270.0;
			}
			else
			{
				num2 -= 90.0;
			}
			num2 = num2;
			if (num2 >= 360.0)
			{
				num2 -= Math.Floor(num2 / 360.0) * 360.0;
			}
			if (Label != null)
			{
				Label.Text = num.ToString() + "m, " + num2.ToString() + "d";
			}
			double num3 = num / (double)this.Map_Current.WorldSize;
			Line.StrokeDashArray = new DoubleCollection(new double[]
			{
				num3 * 100.0,
				2.0
			});
			Line.X1 = UnitPoint.X;
			Line.Y1 = UnitPoint.Y;
			Line.X2 = TargetPoint.X;
			Line.Y2 = TargetPoint.Y;
			Line.Visibility = Visibility.Visible;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00025C40 File Offset: 0x00023E40
		private void HideTrackingLines()
		{
			this.TrackingAnchor = null;
			this.TrackingGroup = null;
			this.TrackingUnit = null;
			try
			{
				foreach (object obj in this.MapVC.Tracking.Children)
				{
					Line line = ((UIElement)obj) as Line;
					if (line != null)
					{
						line.Visibility = Visibility.Collapsed;
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00025CC4 File Offset: 0x00023EC4
		private void AddUnitToUnitList(ref DictionaryOfUnitItem Item)
		{
			if (Item != null && Item.Unit != null && !string.IsNullOrEmpty(Item.Unit.DisplayName))
			{
				Item.ListItem = new ComboBoxItem();
				Item.ListItem.Content = Item.Unit.DisplayName;
				Item.ListItem.Tag = Item.Unit.NetID;
				this.UnitList.Items.Add(Item.ListItem);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00025D45 File Offset: 0x00023F45
		private void RemoveUnitFromUnitList(ref DictionaryOfUnitItem Item)
		{
			if (Item != null && Item.ListItem != null && this.UnitList.Items.Contains(Item.ListItem))
			{
				this.UnitList.Items.Remove(Item.ListItem);
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00025D84 File Offset: 0x00023F84
		private void HandleUnitListSelectionChanged(object Sender, SelectionChangedEventArgs e)
		{
			try
			{
				ComboBoxItem comboBoxItem = this.UnitList.SelectedItem as ComboBoxItem;
				if (comboBoxItem != null && comboBoxItem.Tag != null)
				{
					this.FollowedUnitNetID = comboBoxItem.Tag.ToString();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00025DE0 File Offset: 0x00023FE0
		private void ChangeFollowedUnit(int Dir)
		{
			checked
			{
				if (this.UnitList.HasItems && this.UnitList.Items != null && this.UnitList.Items.Count != 0)
				{
					if (this.UnitList.SelectedIndex == -1)
					{
						((ComboBoxItem)this.UnitList.Items[0]).IsSelected = true;
						return;
					}
					if (Dir == 0)
					{
						if (this.UnitList.SelectedIndex != 0)
						{
							((ComboBoxItem)this.UnitList.Items[this.UnitList.SelectedIndex - 1]).IsSelected = true;
							return;
						}
					}
					else if (this.UnitList.SelectedIndex != this.UnitList.Items.Count - 1)
					{
						((ComboBoxItem)this.UnitList.Items[this.UnitList.SelectedIndex + 1]).IsSelected = true;
					}
				}
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00025ED0 File Offset: 0x000240D0
		private void UpdateGroupIcons()
		{
			if (Common.DictionaryOfGroups != null)
			{
				List<DictionaryOfGroupItem> list = Common.DictionaryOfGroups.Where((MainWindow._Closure$__.$I630-0 == null) ? (MainWindow._Closure$__.$I630-0 = ((KeyValuePair<string, DictionaryOfGroupItem> x) => x.Value.UpdateIcon)) : MainWindow._Closure$__.$I630-0).Select((MainWindow._Closure$__.$I630-1 == null) ? (MainWindow._Closure$__.$I630-1 = ((KeyValuePair<string, DictionaryOfGroupItem> x) => x.Value)) : MainWindow._Closure$__.$I630-1).ToList<DictionaryOfGroupItem>();
				if (list != null)
				{
					try
					{
						List<DictionaryOfGroupItem>.Enumerator enumerator = list.GetEnumerator();
						while (enumerator.MoveNext())
						{
							MainWindow._Closure$__630-0 CS$<>8__locals1 = new MainWindow._Closure$__630-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							CS$<>8__locals1.$VB$Local_GroupItem = enumerator.Current;
							base.Dispatcher.Invoke(delegate()
							{
								CS$<>8__locals1.$VB$Me.UpdateGroupIcon(ref CS$<>8__locals1.$VB$Local_GroupItem);
								CS$<>8__locals1.$VB$Me.UpdateGroupORBAT(ref CS$<>8__locals1.$VB$Local_GroupItem);
								if (CS$<>8__locals1.$VB$Local_GroupItem.Group.CanvasWPX != -1.0 & CS$<>8__locals1.$VB$Local_GroupItem.Group.CanvasWPY != -1.0)
								{
									CS$<>8__locals1.$VB$Me.UpdateGroupLine(ref CS$<>8__locals1.$VB$Local_GroupItem);
									return;
								}
								CS$<>8__locals1.$VB$Me.RemoveGroupLine(ref CS$<>8__locals1.$VB$Local_GroupItem);
							});
						}
					}
					finally
					{
						List<DictionaryOfGroupItem>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00025FB0 File Offset: 0x000241B0
		private void UpdateGroupIcon(ref DictionaryOfGroupItem GroupItem)
		{
			if (this.Map_Current != null && GroupItem != null && GroupItem.Group != null)
			{
				Unit unit = null;
				if (!string.IsNullOrEmpty(GroupItem.Group.LeaderNetID) && Common.DictionaryOfUnits != null)
				{
					DictionaryOfUnitItem dictionaryOfUnitItem = null;
					Common.DictionaryOfUnits.TryGetValue(GroupItem.Group.LeaderNetID, out dictionaryOfUnitItem);
					if (dictionaryOfUnitItem != null)
					{
						unit = dictionaryOfUnitItem.Unit;
					}
					dictionaryOfUnitItem = null;
				}
				if (unit == null)
				{
					if (GroupItem.Icon != null)
					{
						GroupItem.Icon.Visibility = Visibility.Collapsed;
						return;
					}
				}
				else
				{
					Vehicle groupVehicle = null;
					bool flag = false;
					if (unit != null && !string.IsNullOrEmpty(unit.VehicleNetID) && Common.DictionaryOfVehicles != null)
					{
						DictionaryOfVehicleItem dictionaryOfVehicleItem = null;
						Common.DictionaryOfVehicles.TryGetValue(unit.VehicleNetID, out dictionaryOfVehicleItem);
						if (dictionaryOfVehicleItem != null)
						{
							groupVehicle = dictionaryOfVehicleItem.Vehicle;
							flag = true;
							if (dictionaryOfVehicleItem.Group != null)
							{
								flag = string.Equals(dictionaryOfVehicleItem.Group.GroupNetID, GroupItem.Group.GroupNetID, StringComparison.InvariantCultureIgnoreCase);
							}
						}
						dictionaryOfVehicleItem = null;
					}
					if (GroupItem.Icon == null)
					{
						if (this.myMapHelper != null)
						{
							GroupItem.Icon = new Group(ref GroupItem.Group, ref unit, ref groupVehicle, flag, this.myMapHelper.RenderMode, this.GroupMaximumZoomFactor);
							if (GroupItem.Icon != null)
							{
								GroupItem.Icon.HandleScaleChange(this.ZoomFactor, this.ZoomFactor);
								this.MapVC.Groups.Children.Add(GroupItem.Icon);
								GroupItem.Icon.GroupIconSizeChanged += this.HandleGroupIconSizeChanged;
								this.OnUserInitiatedMapRenderModeChange += GroupItem.Icon.HandleRenderModeChange;
								this.ScaleChanged += GroupItem.Icon.HandleScaleChange;
							}
						}
					}
					else
					{
						GroupItem.Icon.Group = GroupItem.Group;
						GroupItem.Icon.GroupLeader = unit;
						GroupItem.Icon.GroupVehicle = groupVehicle;
						GroupItem.Icon.GroupVehicleIsPrimary = flag;
						GroupItem.Icon.Update();
					}
					try
					{
						if (GroupItem.Icon != null)
						{
							this.UpdateGroupIconPosition(ref GroupItem.Icon, unit.CanvasX, unit.CanvasY, GroupItem.Icon.Icon.IconWidth / 2.0, GroupItem.Icon.Icon.IconHeight / 2.0);
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00026230 File Offset: 0x00024430
		private void RemoveGroupIcon(ref DictionaryOfGroupItem GroupItem)
		{
			if (GroupItem != null && GroupItem.Icon != null)
			{
				this.MapVC.Groups.Children.Remove(GroupItem.Icon);
				GroupItem.Icon.GroupIconSizeChanged -= this.HandleGroupIconSizeChanged;
				this.OnUserInitiatedMapRenderModeChange -= GroupItem.Icon.HandleRenderModeChange;
				this.ScaleChanged -= GroupItem.Icon.HandleScaleChange;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000262B0 File Offset: 0x000244B0
		private void UpdateGroupORBAT(ref DictionaryOfGroupItem GroupItem)
		{
			if (GroupItem != null && GroupItem.Group != null)
			{
				if (GroupItem.ORBATPanel == null)
				{
					GroupItem.ORBATPanel = this.CreateGroupPanel(ref GroupItem.Group);
					return;
				}
				this.UpdateGroupPanel(ref GroupItem.ORBATPanel, ref GroupItem.Group);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000262FC File Offset: 0x000244FC
		private void RemoveGroupORBAT(ref DictionaryOfGroupItem GroupItem)
		{
			if (GroupItem != null && GroupItem.ORBATPanel != null)
			{
				this.RemoveGroupPanel(ref GroupItem.ORBATPanel);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00026318 File Offset: 0x00024518
		private void UpdateGroupIconPosition(ref Group Icon, double Canvas_X, double Canvas_Y, double OffsetX, double OffsetY)
		{
			Canvas.SetLeft(Icon, Canvas_X - OffsetX);
			Canvas.SetTop(Icon, Canvas_Y - OffsetY);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00026330 File Offset: 0x00024530
		private void HandleGroupIconSizeChanged(Group Icon)
		{
			if (Icon.GroupLeader != null)
			{
				this.UpdateGroupIconPosition(ref Icon, Icon.GroupLeader.CanvasX, Icon.GroupLeader.CanvasY, Icon.Icon.IconWidth / 2.0, Icon.Icon.IconHeight / 2.0);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00026390 File Offset: 0x00024590
		private void UpdateGroupLine(ref DictionaryOfGroupItem GroupItem)
		{
			if (this.Map_Current != null && GroupItem != null && GroupItem.Group != null)
			{
				Unit unit = null;
				if (!string.IsNullOrEmpty(GroupItem.Group.LeaderNetID) && Common.DictionaryOfUnits != null)
				{
					DictionaryOfUnitItem dictionaryOfUnitItem = null;
					Common.DictionaryOfUnits.TryGetValue(GroupItem.Group.LeaderNetID, out dictionaryOfUnitItem);
					if (dictionaryOfUnitItem != null && dictionaryOfUnitItem.Unit != null)
					{
						unit = dictionaryOfUnitItem.Unit;
					}
					dictionaryOfUnitItem = null;
				}
				if (unit != null)
				{
					if (GroupItem.Dot == null && (Operators.CompareString(GroupItem.Group.WPX, "0", false) != 0 & Operators.CompareString(GroupItem.Group.WPY, "0", false) != 0))
					{
						GroupItem.Dot = new Ellipse();
						GroupItem.Dot.Width = 10.0;
						GroupItem.Dot.Height = 10.0;
						GroupItem.Dot.Stroke = new SolidColorBrush(Colors.Black);
						GroupItem.Dot.StrokeThickness = 1.0;
						this.MapVC.Tracking.Children.Add(GroupItem.Dot);
						SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Black);
						solidColorBrush.Opacity = 0.2;
						GroupItem.Dot.Fill = solidColorBrush;
					}
					if (GroupItem.Dot != null)
					{
						Canvas.SetLeft(GroupItem.Dot, GroupItem.Group.CanvasWPX - GroupItem.Dot.Width / 2.0);
						Canvas.SetTop(GroupItem.Dot, GroupItem.Group.CanvasWPY - GroupItem.Dot.Height / 2.0);
						DictionaryOfGroupItem dictionaryOfGroupItem = GroupItem;
						Point point = new Point(unit.CanvasX, unit.CanvasY);
						Point point2 = new Point(GroupItem.Group.CanvasWPX, GroupItem.Group.CanvasWPY);
						Color black = Colors.Black;
						TextBlock textBlock = null;
						this.UpdateTrackingLine(ref dictionaryOfGroupItem.Line, ref point, ref point2, ref black, ref textBlock);
					}
				}
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000265B4 File Offset: 0x000247B4
		private void RemoveGroupLine(ref DictionaryOfGroupItem GroupItem)
		{
			if (this.Map_Current != null)
			{
				if (GroupItem.Dot != null)
				{
					this.MapVC.Tracking.Children.Remove(GroupItem.Dot);
				}
				if (GroupItem.Line != null)
				{
					this.MapVC.Tracking.Children.Remove(GroupItem.Line);
				}
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00026614 File Offset: 0x00024814
		private void UpdateMarkerIcon(ref DictionaryOfMarkerItem MarkerItem)
		{
			checked
			{
				if (this.Map_Current != null)
				{
					if (MarkerItem != null && MarkerItem.Marker != null)
					{
						if (MarkerItem.Icon == null)
						{
							MarkerItem.Icon = new Marker();
							MarkerItem.Icon.ShowText = true;
							MarkerItem.Icon.Update(MarkerItem.Marker);
							if (MarkerItem.Icon != null)
							{
								this.MapVC.Markers.Children.Add(MarkerItem.Icon);
								MarkerItem.Icon.MarkerIconSizeChanged += this.HandleMarkerIconSizeChanged;
								this.ScaleChanged += MarkerItem.Icon.HandleScaleChange;
								MarkerItem.Icon.HandleScaleChange(this.ZoomFactor, this.ZoomFactor);
							}
						}
						else
						{
							MarkerItem.Icon.Update(MarkerItem.Marker);
						}
					}
					if (MarkerItem.Icon != null)
					{
						try
						{
							this.UpdateMarkerIconPosition(ref MarkerItem.Icon, (int)Math.Round(MarkerItem.Marker.CanvasX), (int)Math.Round(MarkerItem.Marker.CanvasY), MarkerItem.Icon.IconWidth / 2.0, MarkerItem.Icon.IconHeight / 2.0);
						}
						catch (Exception ex)
						{
						}
					}
				}
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00026780 File Offset: 0x00024980
		private void RemoveMarkerIcon(ref DictionaryOfMarkerItem MarkerItem)
		{
			if (MarkerItem != null && MarkerItem.Icon != null)
			{
				this.MapVC.Markers.Children.Remove(MarkerItem.Icon);
				MarkerItem.Icon.MarkerIconSizeChanged -= this.HandleMarkerIconSizeChanged;
				this.ScaleChanged -= MarkerItem.Icon.HandleScaleChange;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000267E6 File Offset: 0x000249E6
		private void UpdateMarkerIconPosition(ref Marker MarkerIcon, int Canvas_X, int Canvas_Y, double OffsetX, double OffsetY)
		{
			Canvas.SetLeft(MarkerIcon, (double)Canvas_X - OffsetX);
			Canvas.SetTop(MarkerIcon, (double)Canvas_Y - OffsetY);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00026800 File Offset: 0x00024A00
		private void HandleMarkerIconSizeChanged(Marker MarkerIcon)
		{
			checked
			{
				if (MarkerIcon.Marker.CanvasX != -1.0 & MarkerIcon.Marker.CanvasY != -1.0)
				{
					this.UpdateMarkerIconPosition(ref MarkerIcon, (int)Math.Round(MarkerIcon.Marker.CanvasX), (int)Math.Round(MarkerIcon.Marker.CanvasY), MarkerIcon.IconWidth / 2.0, MarkerIcon.IconHeight / 2.0);
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002688C File Offset: 0x00024A8C
		private void UpdateVehicleLinks()
		{
			if (Common.DictionaryOfVehicles != null & Common.DictionaryOfUnits != null)
			{
				try
				{
					foreach (KeyValuePair<string, DictionaryOfVehicleItem> keyValuePair in Common.DictionaryOfVehicles)
					{
						try
						{
							foreach (KeyValuePair<string, CrewDictionaryItem> keyValuePair2 in keyValuePair.Value.Vehicle.CrewDictionary)
							{
								DictionaryOfUnitItem dictionaryOfUnitItem = null;
								if (Common.DictionaryOfUnits.TryGetValue(keyValuePair2.Key, out dictionaryOfUnitItem))
								{
									keyValuePair2.Value.Unit = dictionaryOfUnitItem.Unit;
								}
								dictionaryOfUnitItem = null;
							}
						}
						finally
						{
							Dictionary<string, CrewDictionaryItem>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					Dictionary<string, DictionaryOfVehicleItem>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			checked
			{
				if (Common.DictionaryOfVehicles != null & Common.DictionaryOfGroups != null)
				{
					try
					{
						foreach (KeyValuePair<string, DictionaryOfVehicleItem> keyValuePair3 in Common.DictionaryOfVehicles)
						{
							string[] array = new string[]
							{
								"commander",
								"gunner",
								"driver",
								"turret",
								"cargo"
							};
							if (!new string[]
							{
								"1",
								"2",
								"3"
							}.Contains(keyValuePair3.Value.Vehicle.Class))
							{
								array = new string[]
								{
									"driver",
									"commander",
									"gunner",
									"turret",
									"cargo"
								};
							}
							bool flag = false;
							string[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								MainWindow._Closure$__643-0 CS$<>8__locals1 = new MainWindow._Closure$__643-0(CS$<>8__locals1);
								CS$<>8__locals1.$VB$Local_Position = array2[i];
								List<CrewDictionaryItem> list = (from x in keyValuePair3.Value.Vehicle.CrewDictionary.Values
								where Operators.CompareString(x.Position, CS$<>8__locals1.$VB$Local_Position, false) == 0
								select x).ToList<CrewDictionaryItem>();
								if (list != null && list.Count != 0)
								{
									try
									{
										foreach (CrewDictionaryItem crewDictionaryItem in list)
										{
											if (crewDictionaryItem.Unit != null && !string.IsNullOrEmpty(crewDictionaryItem.Unit.GroupNetID) && Common.DictionaryOfGroups.ContainsKey(crewDictionaryItem.Unit.GroupNetID))
											{
												flag = true;
												keyValuePair3.Value.Group = Common.DictionaryOfGroups[crewDictionaryItem.Unit.GroupNetID].Group;
												break;
											}
										}
									}
									finally
									{
										List<CrewDictionaryItem>.Enumerator enumerator4;
										((IDisposable)enumerator4).Dispose();
									}
									if (flag)
									{
										break;
									}
								}
							}
						}
					}
					finally
					{
						Dictionary<string, DictionaryOfVehicleItem>.Enumerator enumerator3;
						((IDisposable)enumerator3).Dispose();
					}
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00026B98 File Offset: 0x00024D98
		private void UpdateVehicleIcons()
		{
			if (Common.DictionaryOfVehicles != null)
			{
				List<DictionaryOfVehicleItem> list = Common.DictionaryOfVehicles.Where((MainWindow._Closure$__.$I644-0 == null) ? (MainWindow._Closure$__.$I644-0 = ((KeyValuePair<string, DictionaryOfVehicleItem> x) => x.Value.UpdateIcon)) : MainWindow._Closure$__.$I644-0).Select((MainWindow._Closure$__.$I644-1 == null) ? (MainWindow._Closure$__.$I644-1 = ((KeyValuePair<string, DictionaryOfVehicleItem> x) => x.Value)) : MainWindow._Closure$__.$I644-1).ToList<DictionaryOfVehicleItem>();
				if (list != null)
				{
					try
					{
						List<DictionaryOfVehicleItem>.Enumerator enumerator = list.GetEnumerator();
						while (enumerator.MoveNext())
						{
							MainWindow._Closure$__644-0 CS$<>8__locals1 = new MainWindow._Closure$__644-0(CS$<>8__locals1);
							CS$<>8__locals1.$VB$Me = this;
							CS$<>8__locals1.$VB$Local_VehicleItem = enumerator.Current;
							base.Dispatcher.Invoke(delegate()
							{
								CS$<>8__locals1.$VB$Me.UpdateVehicleIcon(ref CS$<>8__locals1.$VB$Local_VehicleItem);
							});
						}
					}
					finally
					{
						List<DictionaryOfVehicleItem>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00026C78 File Offset: 0x00024E78
		private void UpdateVehicleIcon(ref DictionaryOfVehicleItem VehicleItem)
		{
			if (this.Map_Current != null && VehicleItem != null && VehicleItem.Vehicle != null)
			{
				if (VehicleItem.Icon == null)
				{
					VehicleItem.Icon = new Vehicle();
					VehicleItem.Icon.Vehicle = VehicleItem.Vehicle;
					this.OnUserInitiatedMapRenderModeChange += VehicleItem.Icon.HandleRenderModeChange;
					VehicleItem.Icon.Update();
					if (VehicleItem.Icon != null)
					{
						this.MapVC.Vehicles.Children.Add(VehicleItem.Icon);
					}
				}
				else
				{
					VehicleItem.Icon.Vehicle = VehicleItem.Vehicle;
					VehicleItem.Icon.Update();
				}
				if (VehicleItem.Icon != null)
				{
					this.UpdateVehicleIconPosition(VehicleItem.Icon);
				}
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00026D4C File Offset: 0x00024F4C
		private void UpdateVehicleIconPosition(Vehicle Icon)
		{
			try
			{
				if (!double.IsNaN(Icon.MarkerIcon.IconWidth))
				{
					Canvas.SetLeft(Icon, Icon.Vehicle.CanvasX - Icon.MarkerIcon.IconWidth / 2.0);
				}
				if (!double.IsNaN(Icon.MarkerIcon.IconHeight))
				{
					Canvas.SetTop(Icon, Icon.Vehicle.CanvasY - Icon.MarkerIcon.IconHeight / 2.0);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00026DEC File Offset: 0x00024FEC
		private void RemoveVehicleIcon(ref DictionaryOfVehicleItem VehicleItem)
		{
			if (VehicleItem != null && VehicleItem.Icon != null)
			{
				this.MapVC.Vehicles.Children.Remove(VehicleItem.Icon);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00026E17 File Offset: 0x00025017
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00026E1F File Offset: 0x0002501F
		internal virtual MainWindow MainWindow { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00026E28 File Offset: 0x00025028
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00026E30 File Offset: 0x00025030
		internal virtual Grid Main { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00026E39 File Offset: 0x00025039
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00026E41 File Offset: 0x00025041
		internal virtual System.Windows.Controls.Menu menuMain { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00026E4A File Offset: 0x0002504A
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00026E52 File Offset: 0x00025052
		internal virtual System.Windows.Controls.MenuItem MenuShowFlyoutRight { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00026E5B File Offset: 0x0002505B
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00026E63 File Offset: 0x00025063
		internal virtual System.Windows.Controls.MenuItem MenuTrackPlayer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00026E6C File Offset: 0x0002506C
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00026E74 File Offset: 0x00025074
		internal virtual System.Windows.Controls.MenuItem MenuPlayStart { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00026E7D File Offset: 0x0002507D
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00026E85 File Offset: 0x00025085
		internal virtual System.Windows.Controls.MenuItem MenuPlayStop { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00026E8E File Offset: 0x0002508E
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00026E96 File Offset: 0x00025096
		internal virtual System.Windows.Controls.MenuItem MenuRecordingStart { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00026E9F File Offset: 0x0002509F
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00026EA7 File Offset: 0x000250A7
		internal virtual System.Windows.Controls.MenuItem MenuRecordingStop { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00026EB0 File Offset: 0x000250B0
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00026EB8 File Offset: 0x000250B8
		internal virtual System.Windows.Controls.MenuItem menuMap { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00026EC1 File Offset: 0x000250C1
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00026ECC File Offset: 0x000250CC
		internal virtual ScrollViewer MapScroll
		{
			[CompilerGenerated]
			get
			{
				return this._MapScroll;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseWheelEventHandler value2 = new MouseWheelEventHandler(this.MapScroll_PreviewMouseWheel);
				StylusDownEventHandler value3 = new StylusDownEventHandler(this.Map_PreviewStylusDown);
				StylusEventHandler value4 = new StylusEventHandler(this.MapScroll_StylusUp);
				StylusEventHandler value5 = new StylusEventHandler(this.Map_StylusMove);
				MouseButtonEventHandler value6 = new MouseButtonEventHandler(this.MapScroll_PreviewMouseDown);
				System.Windows.Input.MouseEventHandler value7 = new System.Windows.Input.MouseEventHandler(this.Map_PreviewMouseMove);
				MouseButtonEventHandler value8 = new MouseButtonEventHandler(this.Map_MouseUp);
				System.Windows.Input.MouseEventHandler value9 = new System.Windows.Input.MouseEventHandler(this.Map_MouseMove);
				ScrollViewer mapScroll = this._MapScroll;
				if (mapScroll != null)
				{
					mapScroll.PreviewMouseWheel -= value2;
					mapScroll.PreviewStylusDown -= value3;
					mapScroll.StylusUp -= value4;
					mapScroll.StylusMove -= value5;
					mapScroll.PreviewMouseDown -= value6;
					mapScroll.PreviewMouseMove -= value7;
					mapScroll.MouseUp -= value8;
					mapScroll.MouseMove -= value9;
				}
				this._MapScroll = value;
				mapScroll = this._MapScroll;
				if (mapScroll != null)
				{
					mapScroll.PreviewMouseWheel += value2;
					mapScroll.PreviewStylusDown += value3;
					mapScroll.StylusUp += value4;
					mapScroll.StylusMove += value5;
					mapScroll.PreviewMouseDown += value6;
					mapScroll.PreviewMouseMove += value7;
					mapScroll.MouseUp += value8;
					mapScroll.MouseMove += value9;
				}
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00026FEC File Offset: 0x000251EC
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00026FF4 File Offset: 0x000251F4
		internal virtual VirtualCanvas MapVC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00026FFD File Offset: 0x000251FD
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00027005 File Offset: 0x00025205
		internal virtual Image imgBackground { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0002700E File Offset: 0x0002520E
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00027016 File Offset: 0x00025216
		internal virtual Image imgBackgroundLogo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002701F File Offset: 0x0002521F
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00027027 File Offset: 0x00025227
		internal virtual Border brdWait { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00027030 File Offset: 0x00025230
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00027038 File Offset: 0x00025238
		internal virtual TextBlock txtWait { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00027041 File Offset: 0x00025241
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00027049 File Offset: 0x00025249
		internal virtual Border brdMessage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00027052 File Offset: 0x00025252
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0002705A File Offset: 0x0002525A
		internal virtual TextBlock txtMessageTitle { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00027063 File Offset: 0x00025263
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0002706B File Offset: 0x0002526B
		internal virtual TextBlock txtMessageValue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00027074 File Offset: 0x00025274
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0002707C File Offset: 0x0002527C
		internal virtual Border brdMapMissing { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00027085 File Offset: 0x00025285
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0002708D File Offset: 0x0002528D
		internal virtual Border brdMapImport { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00027096 File Offset: 0x00025296
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0002709E File Offset: 0x0002529E
		internal virtual ScrollViewer scrollImport { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000270A7 File Offset: 0x000252A7
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x000270AF File Offset: 0x000252AF
		internal virtual StackPanel txtMapImportProgress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000270B8 File Offset: 0x000252B8
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x000270C0 File Offset: 0x000252C0
		internal virtual TextBlock txtMapImportProgressReceivingMapStart { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x000270C9 File Offset: 0x000252C9
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x000270D1 File Offset: 0x000252D1
		internal virtual TextBlock txtMapImportProgressReceivingRows { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000270DA File Offset: 0x000252DA
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x000270E2 File Offset: 0x000252E2
		internal virtual TextBlock txtMapImportProgressReceivingTrees { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000270EB File Offset: 0x000252EB
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x000270F3 File Offset: 0x000252F3
		internal virtual TextBlock txtMapImportProgressReceivingLocations { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000270FC File Offset: 0x000252FC
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00027104 File Offset: 0x00025304
		internal virtual TextBlock txtMapImportProgressReceivingObjects { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0002710D File Offset: 0x0002530D
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00027115 File Offset: 0x00025315
		internal virtual TextBlock txtMapImportProgressReceivingRoads { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0002711E File Offset: 0x0002531E
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00027126 File Offset: 0x00025326
		internal virtual TextBlock txtMapImportProgressReceivingMapEnd { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0002712F File Offset: 0x0002532F
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00027137 File Offset: 0x00025337
		internal virtual TextBlock txtMapImportProgressProcessRows { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00027140 File Offset: 0x00025340
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00027148 File Offset: 0x00025348
		internal virtual TextBlock txtMapImportProgressProcessTrees { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00027151 File Offset: 0x00025351
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00027159 File Offset: 0x00025359
		internal virtual TextBlock txtMapImportProgressProcessLocations { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00027162 File Offset: 0x00025362
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0002716A File Offset: 0x0002536A
		internal virtual TextBlock txtMapImportProgressProcessObjects { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00027173 File Offset: 0x00025373
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0002717B File Offset: 0x0002537B
		internal virtual TextBlock txtMapImportProgressProcessRoads { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00027184 File Offset: 0x00025384
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0002718C File Offset: 0x0002538C
		internal virtual TextBlock txtMapImportProgressCleanup { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00027195 File Offset: 0x00025395
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0002719D File Offset: 0x0002539D
		internal virtual TextBlock txtMapImportProgressComplete { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000271A6 File Offset: 0x000253A6
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x000271AE File Offset: 0x000253AE
		internal virtual TextBlock txtMapImportProgressFail { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000271B7 File Offset: 0x000253B7
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x000271BF File Offset: 0x000253BF
		internal virtual System.Windows.Controls.Button btnMapImportProgressCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000271C8 File Offset: 0x000253C8
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x000271D0 File Offset: 0x000253D0
		internal virtual Border brdMapSwitch { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000271D9 File Offset: 0x000253D9
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x000271E1 File Offset: 0x000253E1
		internal virtual System.Windows.Controls.TextBox txtMapSwitch_Map { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000271EA File Offset: 0x000253EA
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x000271F2 File Offset: 0x000253F2
		internal virtual Border brdRoomCreate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000271FB File Offset: 0x000253FB
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00027203 File Offset: 0x00025403
		internal virtual System.Windows.Controls.TextBox txtACSRoomCreate_Name { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0002720C File Offset: 0x0002540C
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00027214 File Offset: 0x00025414
		internal virtual System.Windows.Controls.ComboBox cmbACSRoomCreate_Action { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0002721D File Offset: 0x0002541D
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00027225 File Offset: 0x00025425
		internal virtual System.Windows.Controls.ComboBox cmbACSRoomCreate_Security { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0002722E File Offset: 0x0002542E
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00027236 File Offset: 0x00025436
		internal virtual System.Windows.Controls.ComboBox cmbACSRoomCreate_SideRequirement { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0002723F File Offset: 0x0002543F
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00027247 File Offset: 0x00025447
		internal virtual TextBlock lblActiveLayerName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00027250 File Offset: 0x00025450
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00027258 File Offset: 0x00025458
		internal virtual System.Windows.Controls.Button btnInkModeDraw { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00027261 File Offset: 0x00025461
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00027269 File Offset: 0x00025469
		internal virtual System.Windows.Controls.Button btnInkModeLight { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00027272 File Offset: 0x00025472
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0002727A File Offset: 0x0002547A
		internal virtual System.Windows.Controls.Button btnInkModeErase { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00027283 File Offset: 0x00025483
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0002728B File Offset: 0x0002548B
		internal virtual System.Windows.Controls.Button btnInkModeSelect { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00027294 File Offset: 0x00025494
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0002729C File Offset: 0x0002549C
		internal virtual Slider InkThickness { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000272A5 File Offset: 0x000254A5
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000272AD File Offset: 0x000254AD
		internal virtual TextBlock MissionTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000272B6 File Offset: 0x000254B6
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x000272BE File Offset: 0x000254BE
		internal virtual TextBlock DebugDataReceived { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000272C7 File Offset: 0x000254C7
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x000272CF File Offset: 0x000254CF
		internal virtual TextBlock lblTrackingAnchor { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000272D8 File Offset: 0x000254D8
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000272E0 File Offset: 0x000254E0
		internal virtual TextBlock lblTrackingGroup { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000272E9 File Offset: 0x000254E9
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x000272F1 File Offset: 0x000254F1
		internal virtual TextBlock lblTrackingUnit { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000272FA File Offset: 0x000254FA
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00027302 File Offset: 0x00025502
		internal virtual TextBlock DebugZoomLevel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0002730B File Offset: 0x0002550B
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00027313 File Offset: 0x00025513
		internal virtual Slider slideZoom { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0002731C File Offset: 0x0002551C
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00027324 File Offset: 0x00025524
		internal virtual System.Windows.Controls.ComboBox UnitList
		{
			[CompilerGenerated]
			get
			{
				return this._UnitList;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.HandleUnitListSelectionChanged);
				System.Windows.Controls.ComboBox unitList = this._UnitList;
				if (unitList != null)
				{
					unitList.SelectionChanged -= value2;
				}
				this._UnitList = value;
				unitList = this._UnitList;
				if (unitList != null)
				{
					unitList.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00027367 File Offset: 0x00025567
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0002736F File Offset: 0x0002556F
		internal virtual TextBlock UnitGridPos { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00027378 File Offset: 0x00025578
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00027380 File Offset: 0x00025580
		internal virtual System.Windows.Controls.Button Mode_Online_Button
		{
			[CompilerGenerated]
			get
			{
				return this._Mode_Online_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.Mode_Online_Button_Click);
				System.Windows.Controls.Button mode_Online_Button = this._Mode_Online_Button;
				if (mode_Online_Button != null)
				{
					mode_Online_Button.Click -= value2;
				}
				this._Mode_Online_Button = value;
				mode_Online_Button = this._Mode_Online_Button;
				if (mode_Online_Button != null)
				{
					mode_Online_Button.Click += value2;
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000273C3 File Offset: 0x000255C3
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000273CC File Offset: 0x000255CC
		internal virtual System.Windows.Controls.Button Mode_Online_Follow
		{
			[CompilerGenerated]
			get
			{
				return this._Mode_Online_Follow;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.Mode_Online_Follow_Click);
				System.Windows.Controls.Button mode_Online_Follow = this._Mode_Online_Follow;
				if (mode_Online_Follow != null)
				{
					mode_Online_Follow.Click -= value2;
				}
				this._Mode_Online_Follow = value;
				mode_Online_Follow = this._Mode_Online_Follow;
				if (mode_Online_Follow != null)
				{
					mode_Online_Follow.Click += value2;
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0002740F File Offset: 0x0002560F
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x00027418 File Offset: 0x00025618
		internal virtual System.Windows.Controls.Button Mode_Offline_Button
		{
			[CompilerGenerated]
			get
			{
				return this._Mode_Offline_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.Mode_Offline_Button_Click);
				System.Windows.Controls.Button mode_Offline_Button = this._Mode_Offline_Button;
				if (mode_Offline_Button != null)
				{
					mode_Offline_Button.Click -= value2;
				}
				this._Mode_Offline_Button = value;
				mode_Offline_Button = this._Mode_Offline_Button;
				if (mode_Offline_Button != null)
				{
					mode_Offline_Button.Click += value2;
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0002745B File Offset: 0x0002565B
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00027463 File Offset: 0x00025663
		internal virtual TextBlock PlaybackTotalFrames { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0002746C File Offset: 0x0002566C
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00027474 File Offset: 0x00025674
		internal virtual TextBlock PlaybackCurrentFrame { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0002747D File Offset: 0x0002567D
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00027485 File Offset: 0x00025685
		internal virtual TextBlock PlaybackSpeed { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0002748E File Offset: 0x0002568E
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00027496 File Offset: 0x00025696
		internal virtual StackPanel Playback_Recording { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0002749F File Offset: 0x0002569F
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x000274A8 File Offset: 0x000256A8
		internal virtual System.Windows.Controls.Button Mode_Record_Start
		{
			[CompilerGenerated]
			get
			{
				return this._Mode_Record_Start;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.Mode_Record_Start_Click);
				System.Windows.Controls.Button mode_Record_Start = this._Mode_Record_Start;
				if (mode_Record_Start != null)
				{
					mode_Record_Start.Click -= value2;
				}
				this._Mode_Record_Start = value;
				mode_Record_Start = this._Mode_Record_Start;
				if (mode_Record_Start != null)
				{
					mode_Record_Start.Click += value2;
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000274EB File Offset: 0x000256EB
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x000274F4 File Offset: 0x000256F4
		internal virtual System.Windows.Controls.Button Mode_Record_Stop
		{
			[CompilerGenerated]
			get
			{
				return this._Mode_Record_Stop;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.Mode_Record_Stop_Click);
				System.Windows.Controls.Button mode_Record_Stop = this._Mode_Record_Stop;
				if (mode_Record_Stop != null)
				{
					mode_Record_Stop.Click -= value2;
				}
				this._Mode_Record_Stop = value;
				mode_Record_Stop = this._Mode_Record_Stop;
				if (mode_Record_Stop != null)
				{
					mode_Record_Stop.Click += value2;
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00027537 File Offset: 0x00025737
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0002753F File Offset: 0x0002573F
		internal virtual StackPanel Playback_Play { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00027548 File Offset: 0x00025748
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00027550 File Offset: 0x00025750
		internal virtual System.Windows.Controls.Button FrameFirst
		{
			[CompilerGenerated]
			get
			{
				return this._FrameFirst;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.FrameFirst_Click);
				System.Windows.Controls.Button frameFirst = this._FrameFirst;
				if (frameFirst != null)
				{
					frameFirst.Click -= value2;
				}
				this._FrameFirst = value;
				frameFirst = this._FrameFirst;
				if (frameFirst != null)
				{
					frameFirst.Click += value2;
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00027593 File Offset: 0x00025793
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0002759C File Offset: 0x0002579C
		internal virtual System.Windows.Controls.Button FramePrev
		{
			[CompilerGenerated]
			get
			{
				return this._FramePrev;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.FramePrev_Click);
				System.Windows.Controls.Button framePrev = this._FramePrev;
				if (framePrev != null)
				{
					framePrev.Click -= value2;
				}
				this._FramePrev = value;
				framePrev = this._FramePrev;
				if (framePrev != null)
				{
					framePrev.Click += value2;
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000275DF File Offset: 0x000257DF
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x000275E8 File Offset: 0x000257E8
		internal virtual System.Windows.Controls.TextBox Frame_Current
		{
			[CompilerGenerated]
			get
			{
				return this._Frame_Current;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				System.Windows.Input.KeyEventHandler value2 = new System.Windows.Input.KeyEventHandler(this.Frame_Current_KeyDown);
				System.Windows.Controls.TextBox frame_Current = this._Frame_Current;
				if (frame_Current != null)
				{
					frame_Current.KeyDown -= value2;
				}
				this._Frame_Current = value;
				frame_Current = this._Frame_Current;
				if (frame_Current != null)
				{
					frame_Current.KeyDown += value2;
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0002762B File Offset: 0x0002582B
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00027634 File Offset: 0x00025834
		internal virtual System.Windows.Controls.Button FrameNext
		{
			[CompilerGenerated]
			get
			{
				return this._FrameNext;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.FrameNext_Click);
				System.Windows.Controls.Button frameNext = this._FrameNext;
				if (frameNext != null)
				{
					frameNext.Click -= value2;
				}
				this._FrameNext = value;
				frameNext = this._FrameNext;
				if (frameNext != null)
				{
					frameNext.Click += value2;
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00027677 File Offset: 0x00025877
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00027680 File Offset: 0x00025880
		internal virtual System.Windows.Controls.Button FrameLast
		{
			[CompilerGenerated]
			get
			{
				return this._FrameLast;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.FrameLast_Click);
				System.Windows.Controls.Button frameLast = this._FrameLast;
				if (frameLast != null)
				{
					frameLast.Click -= value2;
				}
				this._FrameLast = value;
				frameLast = this._FrameLast;
				if (frameLast != null)
				{
					frameLast.Click += value2;
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000276C3 File Offset: 0x000258C3
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x000276CC File Offset: 0x000258CC
		internal virtual System.Windows.Controls.Button SpeedSlow
		{
			[CompilerGenerated]
			get
			{
				return this._SpeedSlow;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.SpeedSlow_Click);
				System.Windows.Controls.Button speedSlow = this._SpeedSlow;
				if (speedSlow != null)
				{
					speedSlow.Click -= value2;
				}
				this._SpeedSlow = value;
				speedSlow = this._SpeedSlow;
				if (speedSlow != null)
				{
					speedSlow.Click += value2;
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0002770F File Offset: 0x0002590F
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00027718 File Offset: 0x00025918
		internal virtual System.Windows.Controls.Button SpeedFast
		{
			[CompilerGenerated]
			get
			{
				return this._SpeedFast;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.SpeedFast_Click);
				System.Windows.Controls.Button speedFast = this._SpeedFast;
				if (speedFast != null)
				{
					speedFast.Click -= value2;
				}
				this._SpeedFast = value;
				speedFast = this._SpeedFast;
				if (speedFast != null)
				{
					speedFast.Click += value2;
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0002775B File Offset: 0x0002595B
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00027763 File Offset: 0x00025963
		internal virtual System.Windows.Controls.Button Frame_Exit { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0002776C File Offset: 0x0002596C
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00027774 File Offset: 0x00025974
		internal virtual System.Windows.Controls.TabControl FlyoutRight { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0002777D File Offset: 0x0002597D
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00027785 File Offset: 0x00025985
		internal virtual StackPanel LayerPanel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0002778E File Offset: 0x0002598E
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00027796 File Offset: 0x00025996
		internal virtual System.Windows.Controls.CheckBox chkLayer0 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0002779F File Offset: 0x0002599F
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x000277A7 File Offset: 0x000259A7
		internal virtual System.Windows.Controls.CheckBox chkLayer6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000277B0 File Offset: 0x000259B0
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000277B8 File Offset: 0x000259B8
		internal virtual System.Windows.Controls.CheckBox chkLayer1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000277C1 File Offset: 0x000259C1
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x000277C9 File Offset: 0x000259C9
		internal virtual System.Windows.Controls.CheckBox chkLayer3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000277D2 File Offset: 0x000259D2
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x000277DA File Offset: 0x000259DA
		internal virtual System.Windows.Controls.CheckBox chkLayer7 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000277E3 File Offset: 0x000259E3
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x000277EB File Offset: 0x000259EB
		internal virtual System.Windows.Controls.CheckBox chkLayer4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000277F4 File Offset: 0x000259F4
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000277FC File Offset: 0x000259FC
		internal virtual System.Windows.Controls.CheckBox chkLayer5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00027805 File Offset: 0x00025A05
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0002780D File Offset: 0x00025A0D
		internal virtual StackPanel pnlInkLayers { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00027816 File Offset: 0x00025A16
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0002781E File Offset: 0x00025A1E
		internal virtual StackPanel pnlLayer2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00027827 File Offset: 0x00025A27
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0002782F File Offset: 0x00025A2F
		internal virtual System.Windows.Controls.CheckBox chkLayer2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00027838 File Offset: 0x00025A38
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00027840 File Offset: 0x00025A40
		internal virtual TextBlock txtLayer2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00027849 File Offset: 0x00025A49
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00027851 File Offset: 0x00025A51
		internal virtual TabItem tabMap { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0002785A File Offset: 0x00025A5A
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00027862 File Offset: 0x00025A62
		internal virtual ScrollViewer scrollMap { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0002786B File Offset: 0x00025A6B
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x00027873 File Offset: 0x00025A73
		internal virtual StackPanel pnlMap { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0002787C File Offset: 0x00025A7C
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00027884 File Offset: 0x00025A84
		internal virtual StackPanel pnlLocationOptions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0002788D File Offset: 0x00025A8D
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00027895 File Offset: 0x00025A95
		internal virtual System.Windows.Controls.Button btnLocationLocate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0002789E File Offset: 0x00025A9E
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000278A6 File Offset: 0x00025AA6
		internal virtual TabItem tabAnchor { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000278AF File Offset: 0x00025AAF
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000278B7 File Offset: 0x00025AB7
		internal virtual ScrollViewer scrollAnchor { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000278C0 File Offset: 0x00025AC0
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x000278C8 File Offset: 0x00025AC8
		internal virtual StackPanel pnlAnchor { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x000278D1 File Offset: 0x00025AD1
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x000278D9 File Offset: 0x00025AD9
		internal virtual StackPanel pnlAnchorOptions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000278E2 File Offset: 0x00025AE2
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x000278EA File Offset: 0x00025AEA
		internal virtual System.Windows.Controls.Button btnAnchorLocate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000278F3 File Offset: 0x00025AF3
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x000278FB File Offset: 0x00025AFB
		internal virtual System.Windows.Controls.Button btnAnchorTrack { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00027904 File Offset: 0x00025B04
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0002790C File Offset: 0x00025B0C
		internal virtual System.Windows.Controls.Button btnAnchorRemove { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00027915 File Offset: 0x00025B15
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0002791D File Offset: 0x00025B1D
		internal virtual TextBlock lblAnchorName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00027926 File Offset: 0x00025B26
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0002792E File Offset: 0x00025B2E
		internal virtual TextBlock lblAnchorGrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00027937 File Offset: 0x00025B37
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0002793F File Offset: 0x00025B3F
		internal virtual TextBlock lblAnchorPosX { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00027948 File Offset: 0x00025B48
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00027950 File Offset: 0x00025B50
		internal virtual TextBlock lblAnchorPosY { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00027959 File Offset: 0x00025B59
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00027961 File Offset: 0x00025B61
		internal virtual System.Windows.Controls.TextBox txtAnchorName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0002796A File Offset: 0x00025B6A
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00027972 File Offset: 0x00025B72
		internal virtual System.Windows.Controls.TextBox txtAnchorGrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0002797B File Offset: 0x00025B7B
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00027983 File Offset: 0x00025B83
		internal virtual System.Windows.Controls.TextBox txtAnchorPosX { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0002798C File Offset: 0x00025B8C
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00027994 File Offset: 0x00025B94
		internal virtual System.Windows.Controls.TextBox txtAnchorPosY { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0002799D File Offset: 0x00025B9D
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x000279A5 File Offset: 0x00025BA5
		internal virtual System.Windows.Controls.Button btnORBATAnchorGet { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x000279AE File Offset: 0x00025BAE
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x000279B6 File Offset: 0x00025BB6
		internal virtual System.Windows.Controls.Button btnORBATAnchroSave { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000279BF File Offset: 0x00025BBF
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x000279C7 File Offset: 0x00025BC7
		internal virtual System.Windows.Controls.CheckBox chkPermanent { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000279D0 File Offset: 0x00025BD0
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x000279D8 File Offset: 0x00025BD8
		internal virtual System.Windows.Controls.TabControl tabOrbat { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000279E1 File Offset: 0x00025BE1
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x000279E9 File Offset: 0x00025BE9
		internal virtual TabItem tabOrbatWest { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000279F2 File Offset: 0x00025BF2
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x000279FA File Offset: 0x00025BFA
		internal virtual ScrollViewer scrollWest { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00027A03 File Offset: 0x00025C03
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00027A0B File Offset: 0x00025C0B
		internal virtual StackPanel pnlWest { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00027A14 File Offset: 0x00025C14
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00027A1C File Offset: 0x00025C1C
		internal virtual StackPanel pnlUnitOptions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00027A25 File Offset: 0x00025C25
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00027A2D File Offset: 0x00025C2D
		internal virtual System.Windows.Controls.Button btnFollow { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00027A36 File Offset: 0x00025C36
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00027A3E File Offset: 0x00025C3E
		internal virtual System.Windows.Controls.Button btnTrack { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00027A47 File Offset: 0x00025C47
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00027A4F File Offset: 0x00025C4F
		internal virtual System.Windows.Controls.Button btnLocate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00027A58 File Offset: 0x00025C58
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00027A60 File Offset: 0x00025C60
		internal virtual TabItem tabOrbatEast { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00027A69 File Offset: 0x00025C69
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00027A71 File Offset: 0x00025C71
		internal virtual ScrollViewer scrollEast { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00027A7A File Offset: 0x00025C7A
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00027A82 File Offset: 0x00025C82
		internal virtual StackPanel pnlEast { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00027A8B File Offset: 0x00025C8B
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00027A93 File Offset: 0x00025C93
		internal virtual TabItem tabOrbatGuer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00027A9C File Offset: 0x00025C9C
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00027AA4 File Offset: 0x00025CA4
		internal virtual ScrollViewer scrollGuer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00027AAD File Offset: 0x00025CAD
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00027AB5 File Offset: 0x00025CB5
		internal virtual StackPanel pnlGuer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00027ABE File Offset: 0x00025CBE
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00027AC6 File Offset: 0x00025CC6
		internal virtual TabItem tabOrbatCiv { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00027ACF File Offset: 0x00025CCF
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00027AD7 File Offset: 0x00025CD7
		internal virtual ScrollViewer scrollCiv { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00027AE0 File Offset: 0x00025CE0
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00027AE8 File Offset: 0x00025CE8
		internal virtual StackPanel pnlCiv { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00027AF1 File Offset: 0x00025CF1
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00027AF9 File Offset: 0x00025CF9
		internal virtual System.Windows.Controls.Button btnMapPagePrev { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00027B02 File Offset: 0x00025D02
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00027B0A File Offset: 0x00025D0A
		internal virtual System.Windows.Controls.Button btnMapPageNext { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00027B13 File Offset: 0x00025D13
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00027B1B File Offset: 0x00025D1B
		internal virtual Grid FlyoutACS { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00027B24 File Offset: 0x00025D24
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00027B2C File Offset: 0x00025D2C
		internal virtual Grid FlyoutACS_Connect { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00027B35 File Offset: 0x00025D35
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00027B3D File Offset: 0x00025D3D
		internal virtual System.Windows.Controls.TextBox txtACSCallsign { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00027B46 File Offset: 0x00025D46
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00027B4E File Offset: 0x00025D4E
		internal virtual System.Windows.Controls.TextBox txtACSServerAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00027B57 File Offset: 0x00025D57
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00027B5F File Offset: 0x00025D5F
		internal virtual System.Windows.Controls.TextBox txtACSServerPort { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00027B68 File Offset: 0x00025D68
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00027B70 File Offset: 0x00025D70
		internal virtual PasswordBox txtACSPassword { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00027B79 File Offset: 0x00025D79
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00027B81 File Offset: 0x00025D81
		internal virtual TextBlock txtACSConnectStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00027B8A File Offset: 0x00025D8A
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00027B92 File Offset: 0x00025D92
		internal virtual System.Windows.Controls.Button ACSConnectButton { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00027B9B File Offset: 0x00025D9B
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00027BA3 File Offset: 0x00025DA3
		internal virtual Grid FlyoutACS_Connected { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00027BAC File Offset: 0x00025DAC
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00027BB4 File Offset: 0x00025DB4
		internal virtual StackPanel pnlSessions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00027BBD File Offset: 0x00025DBD
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00027BC5 File Offset: 0x00025DC5
		internal virtual StackPanel pnlRooms { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00027BCE File Offset: 0x00025DCE
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00027BD6 File Offset: 0x00025DD6
		internal virtual System.Windows.Controls.ComboBox ddlACSMapList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00027BDF File Offset: 0x00025DDF
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00027BE7 File Offset: 0x00025DE7
		internal virtual System.Windows.Controls.Button btnACSMapPublish { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00027BF0 File Offset: 0x00025DF0
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00027BF8 File Offset: 0x00025DF8
		internal virtual TextBlock lblACSMapPublish { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00027C01 File Offset: 0x00025E01
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00027C09 File Offset: 0x00025E09
		internal virtual StackPanel pnlACSMaps { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00027C12 File Offset: 0x00025E12
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00027C1A File Offset: 0x00025E1A
		internal virtual System.Windows.Controls.Button ACSDisconnectButton { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x04000174 RID: 372
		private FramesSet PlaybackHelper;

		// Token: 0x04000175 RID: 373
		private Frames CurrentFrames;

		// Token: 0x04000176 RID: 374
		private Frames Frames_Recording;

		// Token: 0x04000177 RID: 375
		private MapAnchor TrackingAnchor;

		// Token: 0x04000178 RID: 376
		private Line TrackingAnchorLine;

		// Token: 0x04000179 RID: 377
		private Group TrackingGroup;

		// Token: 0x0400017A RID: 378
		private Line TrackingGroupLine;

		// Token: 0x0400017B RID: 379
		private Unit TrackingUnit;

		// Token: 0x0400017C RID: 380
		private Line TrackingUnitLine;

		// Token: 0x0400017E RID: 382
		private List<AthenaHotKey> AthenaHotKeys;

		// Token: 0x0400017F RID: 383
		private DateTime LastUpdated_Units;

		// Token: 0x04000180 RID: 384
		private DateTime LastUpdated_Groups;

		// Token: 0x04000181 RID: 385
		private DateTime LastUpdated_Markers;

		// Token: 0x04000182 RID: 386
		private DateTime LastUpdated_Vehicles;

		// Token: 0x04000183 RID: 387
		private Unit PlayerUnit;

		// Token: 0x0400018B RID: 395
		private Thread SocketRelayProcessThread;

		// Token: 0x0400018C RID: 396
		private Guid Comm_RelayClientGUID;

		// Token: 0x0400018D RID: 397
		private DateTime Comm_LastUpdated;

		// Token: 0x0400018E RID: 398
		private DateTime Comm_LastArchived;

		// Token: 0x0400018F RID: 399
		private bool Comm_IsInMission;

		// Token: 0x04000190 RID: 400
		private Enums.SourceStatus Status_Source;

		// Token: 0x04000191 RID: 401
		private Enums.RelayStatus Status_Relay;

		// Token: 0x04000192 RID: 402
		private string DIR_Athena;

		// Token: 0x04000193 RID: 403
		private string DIR_Data;

		// Token: 0x04000194 RID: 404
		private string DIR_Maps;

		// Token: 0x04000195 RID: 405
		private string DIR_Record;

		// Token: 0x04000196 RID: 406
		private string DIR_Record_Live;

		// Token: 0x04000197 RID: 407
		private string DIR_Record_Archives;

		// Token: 0x04000198 RID: 408
		private string DIR_Record_Playback;

		// Token: 0x04000199 RID: 409
		private string DIR_Save;

		// Token: 0x0400019A RID: 410
		private Dictionary<string, Map> Maps;

		// Token: 0x0400019B RID: 411
		private Map Map_Current;

		// Token: 0x0400019C RID: 412
		private List<MapAnchor> Map_Current_Anchors;

		// Token: 0x0400019D RID: 413
		private List<MapLocation> Map_Current_Locations;

		// Token: 0x0400019E RID: 414
		private Map Map_Import;

		// Token: 0x0400019F RID: 415
		private int[][] Map_Import_Data;

		// Token: 0x040001A0 RID: 416
		private List<ElevationCells> Map_Import_Elevations;

		// Token: 0x040001A1 RID: 417
		private MapFoliage Map_Import_FoliageTrees;

		// Token: 0x040001A2 RID: 418
		private MapForests Map_Import_Forests;

		// Token: 0x040001A3 RID: 419
		private List<MapLocation> Map_Import_Locations;

		// Token: 0x040001A4 RID: 420
		private List<MapObject> Map_Import_Objects;

		// Token: 0x040001A5 RID: 421
		private List<MapRoads> Map_Import_Roads;

		// Token: 0x040001A6 RID: 422
		private List<MapRoadSegment> Map_Import_RoadSegments;

		// Token: 0x040001A7 RID: 423
		private List<MapBush> Map_Import_Trees;

		// Token: 0x040001A8 RID: 424
		private bool Map_Import_InProgress;

		// Token: 0x040001A9 RID: 425
		private bool Map_Scroll_Follow;

		// Token: 0x040001AA RID: 426
		private double ZoomFactor;

		// Token: 0x040001AB RID: 427
		private double ZoomFactorMax;

		// Token: 0x040001AC RID: 428
		private double ZoomFactorMin;

		// Token: 0x040001AD RID: 429
		private double ZoomStep;

		// Token: 0x040001AE RID: 430
		private double ZoomLines;

		// Token: 0x040001AF RID: 431
		private int ZoomLinesStep;

		// Token: 0x040001B0 RID: 432
		private double GroupMaximumZoomFactor;

		// Token: 0x040001B1 RID: 433
		private bool IsMapReady;

		// Token: 0x040001B2 RID: 434
		private bool IsWaitingForUpdate;

		// Token: 0x040001B3 RID: 435
		private string FollowedUnitNetID;

		// Token: 0x040001B4 RID: 436
		private bool Rec_IsRecording;

		// Token: 0x040001B5 RID: 437
		private DateTime Rec_LastFrameRecordedOn;

		// Token: 0x040001B6 RID: 438
		private int Rec_RecordFrameIntervalSeconds;

		// Token: 0x040001B7 RID: 439
		private string Rec_ArchiveFile;

		// Token: 0x040001B8 RID: 440
		private int Rec_FrameCurrent;

		// Token: 0x040001B9 RID: 441
		private double Rec_PlaybackSpeed;

		// Token: 0x040001BA RID: 442
		private double Rec_PlaybackSpeedStep;

		// Token: 0x040001BB RID: 443
		private double Rec_PlaybackSpeedMin;

		// Token: 0x040001BC RID: 444
		private double Rec_PlaybackSpeedMax;

		// Token: 0x040001BD RID: 445
		private Point pointStart;

		// Token: 0x040001BE RID: 446
		private Point pointOffset;

		// Token: 0x040001BF RID: 447
		private TextBlock CursorGridPos;

		// Token: 0x040001C1 RID: 449
		private bool Tool_Visible;

		// Token: 0x040001C2 RID: 450
		private MarkerToolIcon MarkerToolIcon;

		// Token: 0x040001C3 RID: 451
		private Marker MarkerToolCircle;

		// Token: 0x040001C4 RID: 452
		private Marker MarkerToolRect;

		// Token: 0x040001D6 RID: 470
		private InkCanvas InkLayerActive;

		// Token: 0x040001D7 RID: 471
		private object InkCurrentMode;

		// Token: 0x040001DF RID: 479
		private List<MapLocation> myLocationsMission;

		// Token: 0x040001E0 RID: 480
		private System.Windows.Controls.Label mySelectedAnchor;

		// Token: 0x040001E1 RID: 481
		private System.Windows.Controls.Label mySelectedLocation;

		// Token: 0x040001E2 RID: 482
		private bool ACS_IsConnected;

		// Token: 0x040001E3 RID: 483
		private bool ACS_IsAuthenticated;

		// Token: 0x040001E4 RID: 484
		private string Pending_MapPublish_World;

		// Token: 0x040001EC RID: 492
		private System.Windows.Controls.Label mySelectedBlock;

		// Token: 0x040001ED RID: 493
		private ManualResetEvent InQueueMRE;

		// Token: 0x040001EE RID: 494
		private ConcurrentQueue<string[]> FrameQueue;

		// Token: 0x040001EF RID: 495
		private ConcurrentQueue<string[]> OtherQueue;

		// Token: 0x040001F0 RID: 496
		private string CurrentMissionGUID;

		// Token: 0x040001F1 RID: 497
		private string CurrentMissionSteamID;

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x06000788 RID: 1928
		public delegate void User_Initiated_OnlineEventHandler();

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x0600078C RID: 1932
		public delegate void User_Initiated_OfflineEventHandler();

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x06000790 RID: 1936
		public delegate void OnMapChangedEventHandler();

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x06000794 RID: 1940
		public delegate void OnUserInitiatedMapChangeEventHandler(Map Map);

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x06000798 RID: 1944
		public delegate void OnUserInitiatedMapRenderModeChangeEventHandler(Enums.RenderMode Mode);

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x0600079C RID: 1948
		public delegate void Mission_StartedEventHandler();

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x060007A0 RID: 1952
		public delegate void Mission_StoppedEventHandler();

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x060007A4 RID: 1956
		public delegate void Archive_ElapsedEventHandler();

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x060007A8 RID: 1960
		public delegate void User_Initiated_FollowChangeEventHandler(bool Track);

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x060007AC RID: 1964
		public delegate void User_Initiated_RecordStartEventHandler();

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x060007B0 RID: 1968
		public delegate void User_Initiated_RecordStopEventHandler();

		// Token: 0x02000085 RID: 133
		// (Invoke) Token: 0x060007B4 RID: 1972
		public delegate void User_Initiated_PlaybackLoadEventHandler(string Archive);

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x060007B8 RID: 1976
		public delegate void User_Initiated_PlaybackExitEventHandler();

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x060007BC RID: 1980
		public delegate void ScaleChangedEventHandler(double ScaleX, double ScaleY);

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x060007C0 RID: 1984
		public delegate void MapImportStartEventHandler(string Data);

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x060007C4 RID: 1988
		public delegate void MapImportCompleteEventHandler();

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x060007C8 RID: 1992
		public delegate void MapImportFailEventHandler(string Reason);

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x060007CC RID: 1996
		public delegate void OnOrbatLocateLocationSelectedEventHandler(MapLocation Location);

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x060007D0 RID: 2000
		public delegate void OnOrbatAnchorPosRequestedEventHandler();

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x060007D4 RID: 2004
		public delegate void OnOrbatAnchorPosRetrievedEventHandler(Point Target);

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x060007D8 RID: 2008
		public delegate void OnOrbatLocateAnchorEventHandler(MapAnchor Anchor);

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x060007DC RID: 2012
		public delegate void OnOrbatCreateAnchorEventHandler(MapAnchor Anchor);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x060007E0 RID: 2016
		public delegate void OnOrbatRemoveAnchorEventHandler(MapAnchor Anchor);

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x060007E4 RID: 2020
		public delegate void OnOrbatTrackAnchorEventHandler(MapAnchor Anchor);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x060007E8 RID: 2024
		public delegate void OnOrbatUpdateRequestedEventHandler();

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x060007EC RID: 2028
		public delegate void OnOrbatFollowUnitSelectedEventHandler(Unit Unit);

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x060007F0 RID: 2032
		public delegate void OnOrbatLocateUnitSelectedEventHandler(Unit Unit);

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x060007F4 RID: 2036
		public delegate void OnOrbatTrackUnitEventHandler(Unit Unit);

		// Token: 0x02000096 RID: 150
		// (Invoke) Token: 0x060007F8 RID: 2040
		public delegate void OnOrbatFollowGroupSelectedEventHandler(Group Group);

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x060007FC RID: 2044
		public delegate void OnOrbatLocateGroupSelectedEventHandler(Group Group);

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x06000800 RID: 2048
		public delegate void OnOrbatTrackGroupEventHandler(Group Group);
	}
}
