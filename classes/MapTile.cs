using Godot;
using System.Collections.Generic;
using Utility;

namespace Classes
{
    public partial class MapTile : Node
    {
        public List<Utils.Hazards> hazards;
        public List<string> path;
        public Utils.TileType tileType;
        public Vector2I size;
        public List<Vector2I> exits;
        public List<Vector2I> entries;
        public List<string> rules;

        public MapTile(List<Utils.Hazards> _hazards, List<string> _path, Utils.TileType _type,
                       Vector2I _size, List<Vector2I> _exits, List<Vector2I> _entries, List<string> _rules)
        {
            hazards = _hazards;
            path = _path;
            tileType = _type;
            size = _size;
            exits = _exits;
            entries = _entries;
            rules = _rules;
        }
    }
}



