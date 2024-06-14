extends CharacterBody2D

const ACCELERATION = 3000
const LIMIT_SPEED_X = 18000
const LIMIT_SPEED_Y = 1200
const JUMP_HEIGHT = 36000
const MIN_JUMP_HEIGHT = 12000
const MAX_COYOTE_TIME = 6 #amount of frames
const JUMP_BUFFER_TIME = 10 #amount of frames
const WALL_JUMP_AMOUNT = 18000
const WALL_JUMP_TIME = 10 #amount of frames
const WALL_SLIDE_FACTOR = 0.8
const GRAVITY = 2100
const DASH_SPEED = 36000

var axis: Vector2

var coyoteTimer = 0
var jumpBufferTimer = 0
var wallJumpTimer = 0
var dashTime = 0

var spriteColor = "red"
var canJump = false
var friction = false
var wall_sliding = false
var trail = false
var isDashing = false
var hasDashed = false
var isGrabbing = false

@onready var rayCast = $Rotatable/RayCast2D
@onready var animation = $AnimationPlayer
@onready var text: RichTextLabel = $ShakeCamera2D/RichTextLabel

func _physics_process(delta):
	axis = Input.get_vector("ui_left", "ui_right","ui_up","ui_down")

	if !isDashing && velocity.y <= LIMIT_SPEED_Y: velocity.y += GRAVITY * delta
	
	#basic movement mechanics and coyote time
	if is_on_floor():
		hasDashed = false
		spriteColor = "red"

		friction = false
		canJump = true

		coyoteTimer = 0
	else:
		friction = true
		coyoteTimer += 1

		if coyoteTimer >= MAX_COYOTE_TIME:
			canJump = false
			coyoteTimer = 0

	dash(delta)

	#basic vertical movement mechanics
	if wallJumpTimer > WALL_JUMP_TIME:
		wallJumpTimer = WALL_JUMP_AMOUNT
		if !isDashing && !isGrabbing:
			horizontalMovement(delta)
	else:
		wallJumpTimer += 1
	
	if !canJump:
		wallSlide(delta)

		if !wall_sliding:
			if velocity.y >= 0:
				animation.play(str(spriteColor, "Fall"))
			else:
				animation.play(str(spriteColor, "Jump"))

	if Input.is_action_just_pressed("jump"):
		if canJump:
			jump(delta)
		elif rayCast.is_colliding():
			wallJump(delta)
		else:
			jumpBufferTimer = JUMP_BUFFER_TIME
		
		if friction: velocity.x = lerp(velocity.x, 0.0, 0.01)
	
	if Input.is_action_just_released("ui_up"):
		if velocity.y < -MIN_JUMP_HEIGHT * delta:
			velocity.y = -MIN_JUMP_HEIGHT * delta

	if jumpBufferTimer > 0:
		jumpBufferTimer -= 1
		if is_on_floor(): jump(delta)
	
	move_and_slide()

func jump(delta):
	velocity.y = -JUMP_HEIGHT * delta

func wallJump(delta):
	wallJumpTimer = 0
	velocity.x = -WALL_JUMP_AMOUNT * $Rotatable.scale.x * delta
	velocity.y = -JUMP_HEIGHT * delta
	$Rotatable.scale.x = -$Rotatable.scale.x

func wallSlide(delta: float) -> void:
	wall_sliding = rayCast.is_colliding()
	isGrabbing = Input.is_action_pressed("grab")

	if wall_sliding:
		if isGrabbing:
			if axis.y != 0:
				velocity.y = axis.y * 12000 * delta
				animation.play(str(spriteColor, "Climb"))
			else:
				velocity.y = 0
				animation.play(str(spriteColor, "Wall Slide"))
		else:
			velocity.y = velocity.y * WALL_SLIDE_FACTOR
			animation.play(str(spriteColor, "Wall Slide"))		

func horizontalMovement(delta: float) -> void:
	var dir: float = axis.x

	if dir != 0:
		if !is_on_floor() && rayCast.is_colliding(): #allows jumping up on the walls wihtout grabbing, floor check prevents wall sticking
			await get_tree().create_timer(0.1).timeout
		if canJump: #redundant under impression that if on_floor == can jump, but left alone just in case
			animation.play(str(spriteColor, "Run"))

		if dir > 0: #if input == "ui_left" 
			velocity.x = min(velocity.x + ACCELERATION * delta, LIMIT_SPEED_X * delta)
			$Rotatable.scale.x = 1 #rotate character and raycast (mb will swap for custom func and remove redundant node)
		else: #if input == "ui_right"
			velocity.x = max(velocity.x - ACCELERATION * delta, -LIMIT_SPEED_X * delta)
			$Rotatable.scale.x = -1 
	else:
		velocity.x = lerp(velocity.x, 0.0, 0.5)
		if canJump: animation.play(str(spriteColor, "Idle"))

func dash(delta):
	if !hasDashed:
		if Input.is_action_just_pressed("dash"):
			velocity = axis * DASH_SPEED * delta
			spriteColor = "blue"
			Input.start_joy_vibration(0, 1, 1, 0.2)
			isDashing = true
			hasDashed = true
	
	if isDashing:
		trail = true
		dashTime += 1
		if dashTime >= int(0.25 * 1 / delta):
			isDashing = false
			trail = false
			dashTime = 0
		
func _on_trailTimer_timeout() -> void:
	if trail:
		var trail_sprite = Sprite2D.new()

		trail_sprite.texture = load("res://assets/sprites/playerSprites.png") #sprite
		trail_sprite.vframes = 10 #vframes of the spritesheet
		trail_sprite.hframes = 8 #hframes of the spritesheet
		trail_sprite.frame = $Rotatable/Sprite2D.frame #current animation frame
		trail_sprite.scale = Vector2($Rotatable.scale.x * 2.4, $Rotatable.scale.y * 2.4) #set sprite scale to character scale
		trail_sprite.set_script(load("res://assets/scripts/trail_fade.gd")) #make trail dessapear over time
		trail_sprite.position = position #set position to character position
		trail_sprite.modulate = Color.RED #modulate sprite color
		trail_sprite.z_index = -1 #set trail behind character

		get_parent().add_child(trail_sprite)
