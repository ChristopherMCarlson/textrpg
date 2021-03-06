using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, Room> Exits { get; set; }

    public Room Go(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      //fail message here
      System.Console.WriteLine("Why don't you try going a different direction, pal? That's a wall.");
      return this;
    }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, Room>();
    }

    public void TakeItem(string itemName)
    {

    }

    internal void GetDescription()
    {
      System.Console.WriteLine($"{Description}");
    }
  }
}