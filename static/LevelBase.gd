extends Node2D

class_name LvlDb

var Requiered: Array[MapTile] = [
	MapTile.new([], ["res://prefabs/EXIT/exit.tscn"], Utils.TileType.Exit,
				Vector2(1,1), [Vector2(0,-1)], [Vector2(0,-1)], ["EXIT"]),
	MapTile.new([], ["res://prefabs/SPECIAL/special_1.tscn"], Utils.TileType.Special,
				Vector2(1,1), [Vector2(1,0)], [Vector2(1,0)], ["SPECIAL"]),
	MapTile.new([], ["res://prefabs/SPECIAL/special_2.tscn"], Utils.TileType.Special,
				Vector2(1,1), [Vector2(-1,0)], [Vector2(-1,0)], ["SPECIAL"]),
]
