﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;
using XPlat.Models;

namespace XPlat.Services;

internal class ThumbnailService
{
    public static List<GifRecords> Gifs = new List<GifRecords>();

    public async Task LoadGifs()
    {
        var gifPath = @"F:\Gifs\";

        var thumbPath = Path.GetFullPath(AppContext.BaseDirectory + "Thumbs");
        if (!Directory.Exists(thumbPath)) Directory.CreateDirectory(thumbPath);
        var gifPathStrings = new DirectoryInfo(gifPath).GetFiles("*.gif", SearchOption.AllDirectories);
        var parelellOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
        await Parallel.ForEachAsync(gifPathStrings, parelellOptions, async (info, token) =>
        {
            var fileName = DateTime.Now.ToFileTimeUtc() + info.Name;
            fileName = Path.Combine(thumbPath, fileName);
            var fs = File.OpenRead(info.FullName);
            Image? img = null;
            try
            {
                img = await Image.LoadAsync(fs, token);
                {

                    img.Mutate(x => x.Resize(400, 0));
                    var encoder = new GifEncoder()
                    {
                        ColorTableMode = GifColorTableMode.Global,
                        Quantizer = new OctreeQuantizer(new QuantizerOptions() { MaxColors = 32 })
                    };

                    await img.SaveAsync(fileName, encoder, token);
                }
                var fileInfo = new GifRecords(info.FullName, info.Length, fileName);
                Gifs.Add(fileInfo);
            }
            catch (UnknownImageFormatException e)
            {
                Debug.WriteLine(e);

            }
            catch (NotSupportedException e)
            {
                Debug.WriteLine(e);

            }
            finally
            {

                img?.Dispose();
            }

        });

    }
}