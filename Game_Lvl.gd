extends Node2D

@export var size : Vector2

@onready var tilemap = Map.new(size)

func _ready():
	fill_grid()
	fill2()

func _process(_delta):
	pass

func _draw():
	for i in size.x + 1:
		draw_line(Vector2(1 + (i * 100), 0), Vector2(1 + (i * 100), size.y * 100), Color.GREEN, 1.0)
	
	for i in size.y + 1:
		draw_line(Vector2(0, 1 + (i * 100)), Vector2(size.x * 100, 1 + (i * 100)), Color.GREEN, 1.0)

func fill_grid():
	for i in size.x:
		for j in size.y:
			var sprite = Sprite2D.new()
			
			sprite.position = Vector2(51 + i * 100, 51 + j * 100)
			sprite.texture = load("res://icon.svg")
			if sprite.scale * sprite.texture.get_size() != Vector2(100,100):
				sprite.scale = Vector2(99.0 / sprite.texture.get_size().x ,99.0 / sprite.texture.get_size().y)
			
			add_child(sprite)

func fill2():
	var rng = RandomNumberGenerator.new()
	
	for i in LevelBase.Requiered:
		match(i.tileType):
			Utils.TileType.Exit:
				var num = rng.randi_range(1,size.x-1)
				var path = i.path.pick_random()
				var scene = load(path).instantiate()
				scene.position = Vector2(num * 100, 0)
				add_child(scene)
				tilemap.map[num][0].append(i)
			Utils.TileType.Special:
				while true:
					var num_x = rng.randi_range(1,size.x-2)
					var num_y = rng.randi_range(1,size.y-2)
					
					print(tilemap.map[num_x][num_y])
					if tilemap.map[num_x][num_y] != []: continue
					if tilemap.map[num_x+1][num_y+1] != []: continue
					if tilemap.map[num_x+1][num_y] != []: continue
					if tilemap.map[num_x][num_y+1] != []: continue
					if tilemap.map[num_x-1][num_y-1] != []: continue
					if tilemap.map[num_x-1][num_y] != []: continue
					if tilemap.map[num_x][num_y-1] != []: continue
					if tilemap.map[num_x-1][num_y+1] != []: continue
					if tilemap.map[num_x+1][num_y-1] != []: continue
					
					var path = i.path.pick_random()
					var scene = load(path).instantiate()
					scene.position = Vector2(num_x * 100, num_y * 100)
					add_child(scene)
					tilemap.map[num_x][num_y].append(i)
					break
			Utils.TileType.Hazard:
				pass
			Utils.TileType.Passage:
				pass
