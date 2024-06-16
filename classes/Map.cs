using Godot;
using System;
using Utility;
using Bd;
using System.Collections.Generic;
using System.Linq;
using Bd;

namespace Classes 
{
	public partial class Map : Node
	{
		public Vector2I size;
		public MapTile[,] map;
		public List<string> constraints;

		public Map(Vector2I _size)
		{
			size = _size;

			map = new MapTile[size.X , size.Y];
		}

		public void generate()
		{
			generate_requiered();
		}

		internal void generate_requiered()
		{
			var rng = new Random();
			int num_x;

			foreach (var i in new LevelBase().Lvls)
			{
				switch (i.tileType)
				{
					case Utils.TileType.Exit:
						num_x = rng.Next(1, size.X-2);
						map[num_x,0] = i;
						break;
					case Utils.TileType.Special:
						while (true)
						{
							num_x = rng.Next(1, size.X-2);
							var num_y = rng.Next(1, size.Y-2);

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
				}
			}
		}
	
		internal List<int>[,] get_possible() // gets map of all possible prefabs for map
		{
			var result = new List<int>[,] {};

			for (int x = 0; x < size.X; x++)
			{
				for (int y = 0; y < size.Y; y++)
				{
					var base_lvls = new LevelBase().Lvls;
					foreach (var lvl in base_lvls)
					{
						if (!CanBePlaced(lvl, x, y)) continue; // dont place if cant place
						if (!AdhearsToRules(lvl)) continue; // dont place too much of smth

						result[x,y].Append(base_lvls.FindIndex(x => x.path == lvl.path));
					}
				}
			}

			return result;
		}
		
		internal bool CanBePlaced(MapTile lvl, int x, int y) 
		{
			/// <NOTE>
			/// size of lvl counts from top left corner
			/// <NOTE>

			if (map[x,y] != null) return false; // check if lvl already exist in given coordinates

			foreach(Vector2I exit in lvl.exits) // check if exit pointing out of bounds
			{
				if (exit.X == 1 && x + 1 == size.X) return false;
				if (exit.X == -1 && x == 0) return false;

				if (exit.Y == 1 && y + 1 == size.Y)	return false;
				if (exit.Y == -1 && y == 0) return false;
			}

			foreach(Vector2I entry in lvl.entries) // check if entry pointing out of bounds
			{
				if (entry.X == 1 && x + 1 == size.X) return false;
				if (entry.X == -1 && x == 0) return false;
				
				if (entry.Y == 1 && y + 1 == size.Y) return false;
				if (entry.Y == -1 && y == 0) return false;
			}

			if (lvl.size != Vector2I.One)  // check if lvl with bigger size overlaps existing lvls
			{
				if (lvl.size.X + x + 1 > size.X) return false;
				if (lvl.size.Y + y + 1 > size.Y) return false;

				for (int i = x; i < lvl.size.X + x; i++)
				{
					for (int j = y; j < lvl.size.Y + y; j++)
					{
						if (map[i,j] != null) return false;
					}
				}
			}
			
			if(x != 0 && map[x-1,y] != null) // if we possibly have room to the left
			{ 
				// if room has entry that lvl does not provide exit to
				if (map[x-1,y].entries.Contains(new Vector2I(1,0)) && !lvl.exits.Contains(new Vector2I(-1,0))) return false;
				// if room has exit that lvl does not provide entry to
				if (map[x-1,y].exits.Contains(new Vector2I(1,0)) && !lvl.entries.Contains(new Vector2I(-1,0))) return false;
			}
			if(x + 1 != size.X && map[x+1,y] != null) // if we possibly have room to the right
			{
				// if room has entry that lvl does not provide exit to
				if (map[x+1,y].entries.Contains(new Vector2I(-1,0)) && !lvl.exits.Contains(new Vector2I(+1,0))) return false;
				// if room has exit that lvl does not provide entry to
				if (map[x+1,y].exits.Contains(new Vector2I(-1,0)) && !lvl.entries.Contains(new Vector2I(+1,0))) return false;
			}
			if(y != 0 && map[x,y-1] != null) // if we possibly have room to the top
			{ 
				// if room has entry that lvl does not provide exit to
				if (map[x,y-1].entries.Contains(new Vector2I(0,-1)) && !lvl.exits.Contains(new Vector2I(0,1))) return false;
				// if room has exit that lvl does not provide entry to
				if (map[x,y-1].exits.Contains(new Vector2I(0,-1)) && !lvl.entries.Contains(new Vector2I(0,1))) return false;
			}
			if(y + 1 != size.Y && map[x,y+1] != null) // if we possibly have room to the bottom
			{
				// if room has entry that lvl does not provide exit to
				if (map[x,y+1].entries.Contains(new Vector2I(0,1)) && !lvl.exits.Contains(new Vector2I(0,-1))) return false;
				// if room has exit that lvl does not provide entry to
				if (map[x,y+1].exits.Contains(new Vector2I(0,1)) && !lvl.entries.Contains(new Vector2I(0,-1))) return false;
			}

			return true; // success :D
		}

		internal bool AdhearsToRules(MapTile lvl)
		{
			return true;
		}

		internal void Collapse(List<int>[,] possibilities) // finds minimum room and picks random
		{
			List<Vector2I> minimums = new List<Vector2I>();
			int min = 999999999;
			var rnd = new Random();

			for (int x = 0; x < size.X; x++)
			{
				for (int y = 0; y < size.Y; y++)
				{
					if (possibilities[x,y].Count() < min) min = possibilities[x,y].Count; // get global minimum of possible rooms
				}
			}

			for (int x = 0; x < size.X; x++)
			{
				for (int y = 0; y < size.Y; y++)
				{
					if (possibilities[x,y].Count() == min) minimums.Append(new Vector2I(x,y)); // get all coordinates with minimum possible rooms
				}
			}
			
			var crd = minimums[rnd.Next(0,minimums.Count())];
			var rooms = possibilities[crd.X,crd.Y];
			var room = rooms[rnd.Next(0,rooms.Count())];
			map[crd.X,crd.Y] = new LevelBase().Lvls[room];
		}
	}
}
