extends Sprite2D

var color = Color( 1, 0.08, 0.58, 0.5 )

func _process(_delta):
	modulate = color
	color.a *= 0.90
	if (color.a < 0.001): queue_free()
