﻿using System.IO;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.GC
{
	/// <summary>
	/// Represents a GameCube <see cref="Disc"/>.
	/// </summary>
	public sealed class DiscGC : Disc
	{
		#region Private Fields

		private const int ApploaderOffset = 0x2440;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filepath">Path to the GameCube <see cref="Disc"/>.</param>
		public DiscGC(string filepath) : base(filepath)
		{
			using (EndianBinaryReader reader = new EndianBinaryReader(File.OpenRead(filepath), Endian.BigEndian))
			{
				Header = new DiscHeaderGC(reader);

				// Skip to the apploader offset.
				reader.BaseStream.Position = ApploaderOffset;
				Apploader = new Apploader(reader);

				// FST
				FileSystemTable = new FST(reader);
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// GameCube <see cref="DiscHeader"/>.
		/// </summary>
		public override DiscHeader Header
		{
			get;
			protected set;
		}

		/// <summary>
		/// The Apploader on this GameCube <see cref="Disc"/>.
		/// </summary>
		public Apploader Apploader
		{
			get;
			private set;
		}

		/// <summary>
		/// File-system table of this disc.
		/// </summary>
		public FST FileSystemTable
		{
			get;
			private set;
		}

		#endregion
	}
}
