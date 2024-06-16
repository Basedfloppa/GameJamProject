using Godot;
using System;

namespace Utility
{
    public static class Utils
    {
        public enum Hazards {
            Crush,
            Spikes,
            Enemy
        }

        public enum TileType {
            Passage,
            Special,
            Hazard,
            Exit
        }
    }
}