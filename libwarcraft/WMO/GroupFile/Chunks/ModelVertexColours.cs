//
//  ModelVertexColours.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2016 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.Collections.Generic;
using System.IO;
using Warcraft.Core;
using Warcraft.Core.Interfaces;

namespace Warcraft.WMO.GroupFile.Chunks
{
	public class ModelVertexColours : IRIFFChunk, IBinarySerializable
	{
		public const string Signature = "MOCV";

		public List<BGRA> VertexColours = new List<BGRA>();

		public ModelVertexColours()
		{
		}

		public ModelVertexColours(byte[] inData)
		{
			LoadBinaryData(inData);
		}

		public void LoadBinaryData(byte[] inData)
		{
			using (MemoryStream ms = new MemoryStream(inData))
            {
            	using (BinaryReader br = new BinaryReader(ms))
            	{
		            while (ms.Position < ms.Length)
		            {
			            this.VertexColours.Add(br.ReadBGRA());
		            }
            	}
            }
		}

		public string GetSignature()
		{
			return Signature;
		}

		public byte[] Serialize()
		{
			using (MemoryStream ms = new MemoryStream())
            {
            	using (BinaryWriter bw = new BinaryWriter(ms))
            	{
		            foreach (BGRA vertexColour in this.VertexColours)
		            {
			            bw.WriteBGRA(vertexColour);
		            }
            	}

            	return ms.ToArray();
            }
		}
	}
}

