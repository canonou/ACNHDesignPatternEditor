﻿using System;

public class ACNHFileFormat
{
	public int Width
	{
		get
		{
			return IsPro ? 64 : 32;
		}
	}
	public int Height
	{
		get
		{
			return IsPro ? 64 : 32;
		}
	}
	public string Name;
	public byte[] Pixels;
	public DesignPattern.TypeEnum Type;
	public DesignPattern.Color[] Palette;
	public byte Version;

	public bool IsPro
	{
		get
		{
			return Type != DesignPattern.TypeEnum.SimplePattern;
		}
	}

	public static ACNHFileFormat FromPattern(DesignPattern pattern)
	{
		var result = new ACNHFileFormat();

		result.Name = pattern.Name;
		result.Type = pattern.Type;
		result.Palette = new DesignPattern.Color[15];

		for (int i = 0; i < pattern.Palette.Length; i++)
			result.Palette[i] = new DesignPattern.Color() { R = pattern.Palette[i].R, G = pattern.Palette[i].G, B = pattern.Palette[i].B };

		result.Pixels = new byte[(result.Width / 2) * result.Height];
		Array.Copy(pattern.Image, result.Pixels, result.Pixels.Length);
		return result;
	}

	public byte[] ToBytes()
	{
		int length = 89 + (Width / 2) * Height;
		byte[] ret = new byte[length];
		ret[0] = 0x00; // version
		if (Name != null)
		{
			var nameBytes = System.Text.Encoding.Unicode.GetBytes(Name);
			Array.Copy(nameBytes, 0, ret, 1, nameBytes.Length);
		}
		else
		{
			var nameBytes = System.Text.Encoding.Unicode.GetBytes("Unknown");
			Array.Copy(nameBytes, 0, ret, 1, nameBytes.Length);
		}
		ret[0x2B] = (byte) this.Type;
		for (int i = 0; i < this.Palette.Length; i++)
		{
			ret[0x2C + i * 3 + 0] = this.Palette[i].R;
			ret[0x2C + i * 3 + 1] = this.Palette[i].G;
			ret[0x2C + i * 3 + 2] = this.Palette[i].B;
		}
		Array.Copy(Pixels, 0, ret, 0x59, Pixels.Length);
		return ret;
	}

	public ACNHFileFormat()
	{

	}
	public ACNHFileFormat(byte[] bytes)
	{
		Version = bytes[0];
		Name = System.Text.Encoding.Unicode.GetString(bytes, 1, 0x29).Replace(" ", "").Trim();
		Palette = new DesignPattern.Color[15];
		this.Type = (DesignPattern.TypeEnum) bytes[0x2B];
		for (int i = 0; i < 15; i++)
			Palette[i] = new DesignPattern.Color() { R = bytes[0x2C + i * 3 + 0], G = bytes[0x2C + i * 3 + 1], B = bytes[0x2C + i * 3 + 2] };
		this.Pixels = new byte[Width / 2 * Height];
		Array.Copy(bytes, 0x59, this.Pixels, 0, this.Pixels.Length);
	}
}