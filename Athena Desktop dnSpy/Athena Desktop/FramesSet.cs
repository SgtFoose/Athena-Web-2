using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Athena.JSON;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000023 RID: 35
	public class FramesSet
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000081BF File Offset: 0x000063BF
		public FramesSet()
		{
			this.Children = new List<FrameSetChild>();
			this.FramesFile = string.Empty;
			this.Frames = new Frames();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000081E8 File Offset: 0x000063E8
		public List<string> Files
		{
			get
			{
				List<string> list = new List<string>();
				if (this.Children != null && this.Children.Count != 0)
				{
					try
					{
						foreach (FrameSetChild frameSetChild in this.Children)
						{
							if (!string.IsNullOrEmpty(frameSetChild.File))
							{
								list.Add(frameSetChild.File);
							}
						}
					}
					finally
					{
						List<FrameSetChild>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
				return list;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000826C File Offset: 0x0000646C
		public int TotalFrames
		{
			get
			{
				int num = 0;
				checked
				{
					if (this.Children != null && this.Children.Count != 0)
					{
						try
						{
							foreach (FrameSetChild frameSetChild in this.Children)
							{
								if (!string.IsNullOrEmpty(frameSetChild.File))
								{
									num += frameSetChild.TotalFrames;
								}
							}
						}
						finally
						{
							List<FrameSetChild>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
					return num;
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000082E8 File Offset: 0x000064E8
		public void Populate(string SourceZip, string PlaybackFolder)
		{
			this.DeleteFiles(PlaybackFolder);
			this.ExtractFiles(SourceZip, PlaybackFolder);
			this.CreateChildren(PlaybackFolder);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008304 File Offset: 0x00006504
		public void LoadFramesWithIndex(int FrameIndex)
		{
			if (this.Children != null && this.Children.Count != 0)
			{
				try
				{
					foreach (FrameSetChild frameSetChild in this.Children)
					{
						if (FrameIndex >= frameSetChild.FrameIndexStart & FrameIndex <= frameSetChild.FrameIndexFinish)
						{
							if (Operators.CompareString(this.FramesFile, frameSetChild.File, false) != 0)
							{
								this.LoadFramesFromChild(ref frameSetChild);
								break;
							}
							break;
						}
					}
				}
				finally
				{
					List<FrameSetChild>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000083A0 File Offset: 0x000065A0
		public int GetFrameIndexOffsetFromChild(int FrameIndex)
		{
			int result = 0;
			if (this.Children != null && this.Children.Count != 0)
			{
				try
				{
					foreach (FrameSetChild frameSetChild in this.Children)
					{
						if (FrameIndex >= frameSetChild.FrameIndexStart & FrameIndex <= frameSetChild.FrameIndexFinish)
						{
							result = checked(FrameIndex - frameSetChild.FrameIndexStart);
							break;
						}
					}
				}
				finally
				{
					List<FrameSetChild>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000842C File Offset: 0x0000662C
		public void DeleteFiles(string PlaybackFolder)
		{
			try
			{
				if (Directory.Exists(PlaybackFolder))
				{
					List<string> list = Directory.EnumerateFiles(PlaybackFolder).ToList<string>();
					if (list.Count != 0)
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
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000084B0 File Offset: 0x000066B0
		private bool ExtractFiles(string SourceZip, string PlaybackFolder)
		{
			bool result = false;
			if (File.Exists(SourceZip))
			{
				try
				{
					ZipFile.ExtractToDirectory(SourceZip, PlaybackFolder);
					result = true;
				}
				catch (Exception ex)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000084F4 File Offset: 0x000066F4
		private void CreateChildren(string PlaybackFolder)
		{
			this.Children = new List<FrameSetChild>();
			this.Frames = new Frames();
			try
			{
				List<string> list = Directory.EnumerateFiles(PlaybackFolder).ToList<string>();
				if (list != null && list.Count != 0)
				{
					list.Sort();
					try
					{
						foreach (string text in list)
						{
							Frames frames = this.LoadFramesFromFile(ref text);
							if (frames != null && frames.Children != null && frames.Children.Count != 0)
							{
								FrameSetChild frameSetChild = new FrameSetChild();
								frameSetChild.File = text;
								frameSetChild.FrameIndexStart = this.TotalFrames;
								frameSetChild.FrameIndexFinish = checked(frameSetChild.FrameIndexStart + (frames.Children.Count - 1));
								this.Children.Add(frameSetChild);
								if (this.Children.Count == 1)
								{
									this.FramesFile = text;
									this.Frames.Mission = frames.Mission;
									this.Frames.Children.AddRange(frames.Children);
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
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000865C File Offset: 0x0000685C
		private void LoadFramesFromChild(ref FrameSetChild Child)
		{
			this.FramesFile = Child.File;
			this.Frames = this.LoadFramesFromFile(ref Child.File);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008680 File Offset: 0x00006880
		private Frames LoadFramesFromFile(ref string File)
		{
			Frames result = new Frames();
			try
			{
				Frames frames = new Data().LoadFile(File);
				if (frames != null && frames.Children != null && frames.Children.Count != 0)
				{
					result = frames;
				}
			}
			catch (Exception ex)
			{
				result = new Frames();
			}
			return result;
		}

		// Token: 0x040000AA RID: 170
		public List<FrameSetChild> Children;

		// Token: 0x040000AB RID: 171
		public string FramesFile;

		// Token: 0x040000AC RID: 172
		public Frames Frames;
	}
}
