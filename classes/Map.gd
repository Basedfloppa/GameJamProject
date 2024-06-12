extends Node2D

class_name Map

var size: Vector2
var map: Array[Array]
var constraints: Array[String]

func generate() -> void:
	pass

func _init(_size: Vector2) -> void:
	size = _size
	
	for i in size.x:
		map.append([])
		for j in size.y:
			map[i].append([])
