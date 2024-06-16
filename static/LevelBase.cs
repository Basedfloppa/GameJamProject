using Godot;
using System.Collections.Generic;
using Classes;
using Utility;

namespace Bd
{
    public class LevelBase
    {
        public readonly List<MapTile> Lvls = new List<MapTile> 
        {
            new MapTile(
                _hazards: new List<Utils.Hazards> {},
                _path: new List<string>{"res://prefabs/EXIT/exit.tscn"},
                _type: Utils.TileType.Exit,
				_size: new Vector2I(1,1),
                _exits: new List<Vector2I> {new Vector2I(0,-1)},
                _entries: new List<Vector2I> {new Vector2I(0,-1)},
                _rules: new List<string> {"EXIT"}
            ),
            new MapTile(
                _hazards: new List<Utils.Hazards> {},
                _path: new List<string>{"res://prefabs/SPECIAL/special_1.tscn"},
                _type: Utils.TileType.Special,
				_size: new Vector2I(1,1),
                _exits: new List<Vector2I> {new Vector2I(1,0)},
                _entries: new List<Vector2I> {new Vector2I(1,0)},
                _rules: new List<string> {"SPECIAL"}
            ),
            new MapTile(
                _hazards: new List<Utils.Hazards> {},
                _path: new List<string>{"res://prefabs/SPECIAL/special_2.tscn"},
                _type: Utils.TileType.Special,
				_size: new Vector2I(1,1),
                _exits: new List<Vector2I> {new Vector2I(-1,0)},
                _entries: new List<Vector2I> {new Vector2I(-1,0)},
                _rules: new List<string> {"SPECIAL"}
            )
        };
    }
}
