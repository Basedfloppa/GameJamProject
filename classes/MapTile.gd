extends Node2D

class_name MapTile

var hazards: Array[Utils.Hazards]
var path: Array[String]
var tileType: Utils.TileType
var size: Vector2
var exits: Array[Vector2]
var entries: Array[Vector2]
var rules: Array[String]

func _init(_haz: Array[Utils.Hazards] = [], _path: Array[String] = [], _type: Utils.TileType = Utils.TileType.Passage, 
		   _size: Vector2 = Vector2.ZERO, _exits: Array[Vector2] = [], _entries: Array[Vector2] = [], _rules: Array[String] = []) -> void:
	hazards = _haz
	path = _path
	tileType = _type
	size = _size
	exits = _exits
	entries = _entries
	rules = _rules
