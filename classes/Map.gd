extends Node2D

class_name Map

var size: Vector2
var map: Array[Array]
var constraints: Array[String]

func generate() -> void:
	_generate_requiered()

func _init(_size: Vector2) -> void:
	size = _size
	
	for i in size.x:
		map.append([])
		for j in size.y:
			map[i].append(null)

func _generate_requiered() -> void:
	var rng = RandomNumberGenerator.new()
	var num_x = rng.randi_range(1,size.x-2)
	var num_y = rng.randi_range(1,size.y-2)
	
	for i in LevelBase.Requiered:
		match(i.tileType):
			Utils.TileType.Exit:
				map[num_x][0] = i
			Utils.TileType.Special:
				while true:
					num_x = rng.randi_range(1,size.x-2)
					num_y = rng.randi_range(1,size.y-2)
					
					if map[num_x][num_y] != null: continue
					if map[num_x+1][num_y+1] != null: continue
					if map[num_x+1][num_y] != null: continue
					if map[num_x][num_y+1] != null: continue
					if map[num_x-1][num_y-1] != null: continue
					if map[num_x-1][num_y] != null: continue
					if map[num_x][num_y-1] != null: continue
					if map[num_x-1][num_y+1] != null: continue
					if map[num_x+1][num_y-1] != null: continue
					
					map[num_x][num_y] = i
					break
			Utils.TileType.Hazard:
				pass
			Utils.TileType.Passage:
				pass
