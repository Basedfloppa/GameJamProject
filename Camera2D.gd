extends Camera2D

const SPEED = 300.0

func _physics_process(delta):
	var direction = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down")
	position = position + direction.normalized() * delta * SPEED
