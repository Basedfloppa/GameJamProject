extends Node2D

const TILE_SIZE = 500

@export var size : Vector2

@onready var tilemap = Map.new(size)
@onready var sprites = $Sprites
@onready var lvls = $Lvls

func _ready():
	fill_grid_background()
	tilemap.generate()
	fill_grid_lvl()

func _process(_delta):
	pass

#func _draw():
	#for i in size.x + 1:
		#draw_line(Vector2(1 + (i * TILE_SIZE), 0), Vector2(1 + (i * TILE_SIZE), size.y * TILE_SIZE), Color.GREEN, 1.0)
	#
	#for i in size.y + 1:
		#draw_line(Vector2(0, 1 + (i * TILE_SIZE)), Vector2(size.x * TILE_SIZE, 1 + (i * TILE_SIZE)), Color.GREEN, 1.0)

func fill_grid_background():
	for i in size.x:
		for j in size.y:
			var sprite = Sprite2D.new()
			
			sprite.position = Vector2(1 + (TILE_SIZE/2) + (i * TILE_SIZE), 1 + (TILE_SIZE/2) + (j * TILE_SIZE))
			sprite.texture = load("res://icon.svg")
			if sprite.scale * sprite.texture.get_size() != Vector2(TILE_SIZE,TILE_SIZE):
				sprite.scale = Vector2((TILE_SIZE - 1) / sprite.texture.get_size().x ,(TILE_SIZE - 1) / sprite.texture.get_size().y)
			
			sprites.add_child(sprite)

func fill_grid_lvl():
	for x in size.x:
		for y in size.y:
			if tilemap.map[x][y] != null:
				var a = tilemap.map[x][y]
				var path = tilemap.map[x][y].path.pick_random()
				var scene = load(path).instantiate()
				
				scene.position = Vector2(1 + x * TILE_SIZE, 1 + y * TILE_SIZE)
				lvls.add_child(scene)
