#easyPokerHUD

easyPokerHUD is a free and open-source poker HUD for PokerStars and 888 Poker. It is build with C# and SQLite. The code is released under the GPLv3-license.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development.

1. Download and install Visual Studio 2017
2. Download easyPokerHUD's repository into a folder of your choice
3. Import easyPokerHUD via "import" in Visual Studio
4. Download the full SQLite package via the NuGet package manager
5. Set .net framework to 4.6.1 if it hasn't been already
6. All set!


## Contributing

In case you want to contribute, these are the current points of interest:

* Extending easyPokerHUD to other poker rooms
* Reworking the overlay
* Adding new stats
* Building a proper database scheme for SQLite
* Code refactoring

## Extending easyPokerHUD to a new poker room

Each poker room has its own folder located in the [poker rooms folder](https://github.com/ylboerner/easyPokerHUD/tree/master/easyPokerHUD/Source/Poker%20Rooms). Three classes are needed for easyPokerHUD to support a poker room:

#### Main.cs
The main.cs file instantiates a new directory watcher to monitor the directory where the poker room stores the users hand history. It also contains a cache for the players as well as as method to store them in the database. Additionally, there is a method to check if the hand is valid to be processed (This can be limited by player count, game mode etc.)

#### Hand.cs
This class represents a hand for that specific poker room. It gets the path to the hand as input and outputs an object of type hand. This can then be used by the overlay. This class must inherit the class "PokerRoomHand.cs" which can be found in the "Inheritance" folder. It sports a few different methods which are handy to process the .txt containing the played hand.

#### Overlay.cs
Coming soon



## Authors

* **Yannick Boerner** - *Initial work* - (https://github.com/ylboerner)

## License

This project is licensed under the GPL v3 License - see the [LICENSE.md](LICENSE.md) file for details
