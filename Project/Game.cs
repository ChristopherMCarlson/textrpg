using System;
using System.Collections.Generic;
using CastleGrimtol.Project;


namespace CastleGrimtol.Project
{
  public class Game : IGame
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    bool playing = false;


    public void GetUserInput()
    {
      System.Console.WriteLine("What will you do?");
      string input = Console.ReadLine();
      input = input.ToLower();
      string[] inputArr = input.Split(" ");
      switch (inputArr[0])
      {
        case "quit":
          playing = false;
          break;
        case "go":
          Go(inputArr[1]);
          break;
      }

    }

    public void Go(string direction)
    {
      CurrentRoom = CurrentRoom.Go(direction);
    }

    public void Help()
    {
      System.Console.WriteLine("");
    }

    public void Inventory()
    {
      foreach (Item item in CurrentPlayer.Inventory)
      {
        System.Console.WriteLine($"{item.Name}");
        System.Console.WriteLine($"{item.Description}");
      }
    }

    public void Look()
    {
      System.Console.WriteLine($"{CurrentRoom.Description}");
    }

    public void Quit()
    {
      System.Console.WriteLine("");
    }

    public void Reset()
    {
      System.Console.WriteLine("");
    }

    public void Setup()
    {
      playing = true;
      Player CurrentPlayer = new Player("Hero");
      Room room1 = new Room("Room 1", "You enter into a dark room. To the east, you see an opening to another dimly lit room, to the south, you see a dark pit");
      Room room2 = new Room("Room 2", "As you walk into the dimly lit room, you see a small key sitting on the ground. To the east is another dimly lit room.");
      Room room3 = new Room("Room 3", "You enter the room and see a door with a lock on it. You wonder if the key from the previous room will fit this lock?");
      Room room4 = new Room("Room 4", "Having unlocked the door, you enter into the room behind the door. The room is very well lit, due to the large opening at the top of the room. In front of you is a ladder. You have escaped the cave safely.");
      Room pit = new Room("Bottomless Pit", "You walk towards the dark pit. As you look over the edge, you lose your balance and fall in. You continue to fall until you eventually pass out and succumb to starvation.");
      Item key = new Item("Key", "An old rusted key. Take care to not get cut by its jagged edges");
      room1.Exits.Add("east", room2);
      room2.Exits.Add("west", room1);
      room1.Exits.Add("south", pit);
      room2.Exits.Add("east", room3);
      room3.Exits.Add("west", room2);
      room3.Exits.Add("east", room4);
      room4.Exits.Add("west", room3);
      room2.Items.Add(key);
      Room CurrentRoom = room1;

    }
    public void StartGame()
    {
      Setup();
      while (playing)
      {
        System.Console.WriteLine($"{CurrentRoom.Name} - {CurrentRoom.Description}");
        GetUserInput();
      }
      Console.Clear();
    }


    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      if (item != null)
      {
        CurrentPlayer.Inventory.Add(item);
        CurrentRoom.Items.Remove(item);
        System.Console.WriteLine($"Added {item.Name} to inventory.");
        return;
      }
      System.Console.WriteLine("This item does not exist.");

    }

    public void UseItem(string itemName)
    {
      System.Console.WriteLine("");
    }
  }
}