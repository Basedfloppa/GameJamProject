using Godot;
using Classes;
using System.Linq;
using System.Collections.Generic;

public partial class Game_Lvl : Node
{
	public const int TILE_SIZE = 500;

	[Export]
	Vector2I size = new Vector2I(5, 5);

	Map tilemap;
	Node2D sprites;
	Node2D lvls;

	public override void _Ready()
	{
		base._Ready();

		tilemap = new Map(size);
		sprites = GetNode<Node2D>("Sprites");
		lvls = GetNode<Node2D>("Lvls");

		tilemap.generate();
		fill_grid_lvl();
	}

	private void fill_grid_lvl()
	{
		for (int x = 0; x < size.X; x++)
		{
			for (int y = 0; y < size.Y; y++)
			{
				if (tilemap.map[x, y] != null)
				{
					var path = tilemap.map[x, y].path.First();
					var scene = GD.Load<PackedScene>(path).Instantiate() as Node2D;

					scene.Position = new Vector2I(1 + (x * TILE_SIZE), 1 + (y * TILE_SIZE));
					lvls.AddChild(scene);
				}
			}
		}
	}
}
