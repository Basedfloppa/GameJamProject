using Godot;
using System;
using Utility;
using Bd;

namespace Classes 
{
	public partial class Map : Node
	{
		public Vector2I size;
		public MapTile[,] map;
		public String[] constraints;

		public Map(Vector2I _size)
		{
			size = _size;

			map = new MapTile[size.X , size.Y];
		}

		public void generate()
		{
			generate_requiered();
		}

		private void generate_requiered()
		{
			var rng = new Random();
			var num_x = rng.Next(1, size.X-2);
			var num_y = rng.Next(1, size.Y-2);

			foreach (var i in new LevelBase().Requiered)
			{
				switch (i.tileType)
				{
					case Utils.TileType.Exit:
						map[num_x,0] = i;
						break;
					case Utils.TileType.Special:
						while (true)
						{
							num_x = rng.Next(1, size.X-2);
							num_y = rng.Next(1, size.Y-2);

							if (map[num_x, num_y] != null) continue;
							if (map[num_x + 1,num_y + 1] != null) continue;
							if (map[num_x + 1, num_y] != null) continue;
							if (map[num_x, num_y + 1] != null) continue;
							if (map[num_x - 1, num_y] != null) continue;
							if (map[num_x, num_y - 1] != null) continue;
							if (map[num_x - 1, num_y - 1] != null) continue;
							if (map[num_x + 1, num_y - 1] != null) continue;
							if (map[num_x - 1, num_y + 1] != null) continue;

							map[num_x,num_y] = i;
							break;
						}
						break;
					case Utils.TileType.Hazard:
						break;
					case Utils.TileType.Passage:
						break;
				}
			}
		}
	}
}
