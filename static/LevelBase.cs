using Godot;
using System.Collections.Generic;
using Classes;
using Utility;

namespace Bd
{
	public class LevelBase
	{
		/// <NOTE>
		/// preferable index structure for lvls
		/// 0 exit
		/// 1-5 specials
		/// 6-n other
		/// n+1 blank wall
		/// <NOTE>

		public readonly List<MapTile> Requiered = new List<MapTile>
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

		public readonly List<MapTile> Lvls = new List<MapTile> 
		{
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/passage vert.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(0,1), new Vector2I(0,-1)},
				_entries: new List<Vector2I> {new Vector2I(0,1), new Vector2I(0,-1)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/passage horiz.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(1,0), new Vector2I(-1,0)},
				_entries: new List<Vector2I> {new Vector2I(1,0), new Vector2I(-1,0)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/block left.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(-1,0)},
				_entries: new List<Vector2I> {new Vector2I(-1,0)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/block right.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(1,0)},
				_entries: new List<Vector2I> {new Vector2I(1,0)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/block up.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(0,1)},
				_entries: new List<Vector2I> {new Vector2I(0,1)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/block down.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(0,-1)},
				_entries: new List<Vector2I> {new Vector2I(0,-1)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/passage left down.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(-1,0),new Vector2I(0,-1)},
				_entries: new List<Vector2I> {new Vector2I(-1,0),new Vector2I(0,-1)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/passage left up.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(-1,0),new Vector2I(0,1)},
				_entries: new List<Vector2I> {new Vector2I(-1,0),new Vector2I(0,1)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/passage right down.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(1,0),new Vector2I(0,-1)},
				_entries: new List<Vector2I> {new Vector2I(1,0),new Vector2I(0,-1)},
				_rules: new List<string>{"PASSAGE"}
			),
			new MapTile(
				_hazards: new List<Utils.Hazards> {},
				_path: new List<string>{"res://prefabs/1x1/passage right up.tscn"},
				_type: Utils.TileType.Passage,
				_size: new Vector2I(1,1),
				_exits: new List<Vector2I> {new Vector2I(1,0),new Vector2I(0,1)},
				_entries: new List<Vector2I> {new Vector2I(1,0),new Vector2I(0,1)},
				_rules: new List<string>{"PASSAGE"}
			)
		};
	}
}
