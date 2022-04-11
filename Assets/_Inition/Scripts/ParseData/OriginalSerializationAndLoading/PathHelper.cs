using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using UnityEngine;

namespace Honeywell.Battlefield.Model
{
    static class PathHelper
    {
        public static string GetPath(string path)
        {
            //Path.Combine(Application.streamingAssetsPath, "/Content/")
            var file = new FileInfo(Path.Combine(Application.streamingAssetsPath + "/Content/", path));
            if (file.Exists)
            {
                return file.FullName;
            }

            return null;
        }

        public static Uri GetUri(string path)
        {
            var fullPath = GetPath(path);

            if (fullPath != null)
            {
                var uri = new Uri("file:///" + fullPath);

                return uri;
            }

            return null;
        }
    }
}
