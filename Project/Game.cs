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

    bool room3locked = true;

    public void GetUserInput()
    {
      CurrentRoom.GetDescription();
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
          if (inputArr[1] != "north" && inputArr[1] != "south" && inputArr[1] != "west" && inputArr[1] != "east")
          {
            System.Console.WriteLine("I don't know what kind of compass you have, but that isn't a direction...");
            break;
          }
          if (CurrentRoom.Name == "Room 3" && room3locked == true && inputArr[1] == "east")
          {
            System.Console.WriteLine("You cannot proceed while the door is locked.");
            break;
          }
          if (CurrentRoom.Exits.Count == 0)
          {
            Look();
            Quit();
          }
          Go(inputArr[1]);
          if (CurrentRoom.Name == "Room 4")
          {
            CurrentRoom.GetDescription();
            Quit();
          }
          else if (CurrentRoom.Name == "Bottomless Pit")
          {
            CurrentRoom.GetDescription();
            Quit();
          }
          break;
        case "take":
          TakeItem(inputArr[1]);
          if (CurrentRoom.Name == "Room 2")
          {
            CurrentRoom.Description = "You are in a dimly lit room with rooms to your east and west.";
          }
          break;
        case "inventory":
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "reset":
          Reset();
          break;
        case "use":
          if (CurrentRoom.Name == "Room 3" && inputArr[1] == "key")
          {
            UseItem(inputArr[1]);
            room3locked = false;
            System.Console.WriteLine("You have unlocked the door! You may now proceed east.");
            CurrentRoom.Description = "To your east is an unlocked door.";
            System.Console.WriteLine("");
          }
          else if (inputArr[1] == "key")
          {
            System.Console.WriteLine("Now is not the time to use that!");
          }
          break;
        case "look":
          break;
        default:
          System.Console.WriteLine("Maybe you should type help...");
          System.Console.WriteLine("");
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
      System.Console.WriteLine("The following commands are available:");
      System.Console.WriteLine("Go <direction>");
      System.Console.WriteLine("Take <item>");
      System.Console.WriteLine("Use <item>");
      System.Console.WriteLine("Look");
      System.Console.WriteLine("Inventory");
      System.Console.WriteLine("Reset");
      System.Console.WriteLine("Quit");
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count == 0)
      {
        System.Console.WriteLine("You reach into your pockets to find they are empty. Too bad.");
        System.Console.WriteLine("");
      }
      else
      {
        System.Console.WriteLine("");
        System.Console.WriteLine("You have the following in your inventory:");
        System.Console.WriteLine("");
        foreach (Item item in CurrentPlayer.Inventory)
        {
          System.Console.Write($"{item.Name}: ");
          System.Console.Write($"{item.Description}");
          System.Console.WriteLine("");
        }
      }
    }

    public void Look()
    {
      CurrentRoom.GetDescription();
    }

    public void Quit()
    {
      System.Console.WriteLine("Thank you for playing, goodbye.");
      playing = false;
    }

    public void Reset()
    {
      Setup();
    }

    public void Setup()
    {
      playing = true;
      CurrentPlayer = new Player("Hero");
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
      CurrentRoom = room1;
    }
    public void StartGame()
    {
      Setup();
      Console.Clear();
      System.Console.WriteLine("You have been kidnapped! You wake up in a dark dungeon, and have no idea how you got here. In front of you is a doorway, and nothing else. You have no other choice but to walk forward into the doorway. The door locks beind you suddenly.");
      System.Console.WriteLine("");
      System.Console.WriteLine($@"
      _   |~  _
      [_]--'--[_]
      |'|  `  |'|
      | | /^\ | |
      |_|_|I|_|_|
      ");
      while (playing)
      {
        System.Console.WriteLine("");
        GetUserInput();
      }
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
      Item item = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName);
      if (item != null)
      {
        System.Console.WriteLine($"Used {item.Name}.");
        return;
      }
      System.Console.WriteLine("This item does not exist.");
    }
  }
}