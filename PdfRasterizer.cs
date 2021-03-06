﻿using System.Drawing;
using System.IO;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Web;

namespace ImageProcessor.Plugins.Pdf
{
    internal class PdfRasterizer
    {
        private readonly int _desiredXDpi;
        private readonly int _desiredYDpi;
        private readonly GhostscriptVersionInfo _lastInstalledVersion;
        private readonly GhostscriptRasterizer _rasterizer;

        public PdfRasterizer(int xDpi = 96, int yDpi = 96)
        {
            _desiredXDpi = xDpi;
            _desiredYDpi = yDpi;

            _lastInstalledVersion = new GhostscriptVersionInfo(new System.Version(0, 0, 0), HttpContext.Current.Server.MapPath("~/bin/gsdll32.dll"), string.Empty, GhostscriptLicense.GPL);

            _rasterizer = new GhostscriptRasterizer();
        }

        public Image Rasterize(string pdfUri, int pageNumber = 1)
        {
            _rasterizer.Open(pdfUri, _lastInstalledVersion, false);

            var img = _rasterizer.GetPage(_desiredXDpi, _desiredYDpi, pageNumber);

            _rasterizer.Close();
            return img;
        }

        public Image Rasterize(Stream pdfStream, int pageNumber = 1)
        {
            _rasterizer.Open(pdfStream, _lastInstalledVersion, false);

            var img = _rasterizer.GetPage(_desiredXDpi, _desiredYDpi, pageNumber);

            _rasterizer.Close();
            return img;
        }
    }
}