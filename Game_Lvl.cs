using Godot;
using Classes;
using System.Linq;

public partial class Game_Lvl : Node
{
	public const int TILE_SIZE = 500;

	[Export]
	Vector2I size = new Vector2I(5,5);

	Map tilemap;
	Node2D sprites;
	Node2D lvls;

	public override void _Ready()
	{
		base._Ready();

		tilemap = new Map(size);
		sprites = GetNode<Node2D>("Sprites");
		lvls = GetNode<Node2D>("Lvls");

		fill_grid_background();
		tilemap.generate();
		fill_grid_lvl();
	}

	private void fill_grid_background()
	{
		for (int i = 0; i < size.X; i++)
		{
			for (int j = 0; j < size.Y; j++)
			{
				Sprite2D sprite = new Sprite2D();

				sprite.Position = new Vector2(1 + (TILE_SIZE / 2) + (i * TILE_SIZE), 1 + (TILE_SIZE / 2) + (j * TILE_SIZE));
				sprite.Texture = GD.Load<Texture2D>("res://icon.svg");
				sprite.Scale = new Vector2((TILE_SIZE - 1) / sprite.Texture.GetSize().X, (TILE_SIZE - 1) / sprite.Texture.GetSize().Y);

				sprites.AddChild(sprite);
			}
		}
	}

	private void fill_grid_lvl()
	{
		for (int x = 0; x < size.X; x++)
		{
			for (int y = 0; y < size.Y; y++)
			{
				if (tilemap.map[x,y] != null)
				{
					var path = tilemap.map[x,y].path.First();
					var scene = GD.Load<PackedScene>(path).Instantiate() as Node2D;

					scene.Position = new Vector2(1 + (x * TILE_SIZE), 1 + (y * TILE_SIZE));
					lvls.AddChild(scene);
				}
			}
		}
	}
}
