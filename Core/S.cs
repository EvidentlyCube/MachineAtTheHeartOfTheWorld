using System;
using System.IO;

namespace IrregularMachine.Core {
    public static class S {
        public static readonly int ViewportWidth = 1920;
        public static readonly int ViewportHeight = 1080;
        public static readonly float ViewportRatio = 0f;
        public static readonly int GlyphAreaWidth = 850;
        public static readonly int GlyphAreaHeight = 630;
        public static readonly int GlyphAreaMonitorOffsetX = 180;
        public static readonly int GlyphAreaMonitorOffsetY = 223;
        
        public static readonly int GlyphMaxEdgeRenderSize = 256;
        public static readonly int GlyphImageEdge = GlyphMaxEdgeRenderSize;

        static S() {
            Logger.Init("Loading constants");
            var lines = File.ReadAllLines("Content/constants.txt");

            foreach (var line in lines) {
                var chunks = line.Split('=');
                if (chunks.Length != 2) {
                    continue;
                }

                var chunkInteger = int.Parse(chunks[1]);

                Logger.Init($"Setting '{chunks[0]}' to '{chunks[1]}'");
                
                switch (chunks[0]) {
                    case "ViewportWidth": ViewportWidth = chunkInteger; break;
                    case "ViewportHeight": ViewportHeight = chunkInteger; break;
                    case "GlyphAreaWidth": GlyphAreaWidth = chunkInteger; break;
                    case "GlyphAreaHeight": GlyphAreaHeight = chunkInteger; break;
                    case "GlyphAreaMonitorOffsetX": GlyphAreaMonitorOffsetX = chunkInteger; break;
                    case "GlyphAreaMonitorOffsetY": GlyphAreaMonitorOffsetY = chunkInteger; break;
                    case "GlyphMaxEdgeRenderSize": GlyphMaxEdgeRenderSize = chunkInteger; break;
                    case "GlyphImageEdge": GlyphImageEdge = chunkInteger; break;
                }
            }

            ViewportRatio = (float) ViewportWidth / ViewportHeight;
        }
    }
}