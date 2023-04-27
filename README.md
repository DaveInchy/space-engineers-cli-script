# CommandLineAction CLI
CommandLineActions is a "PB" Script for Space Engineers (Game) embedded C# implementation

**Version**: V2.0.0

**Game**: Space Engineers

**Name**: CommandLineActions

**Author**: Dave Inchy <github.com/daveinchy> or 

**More**:
1. [Release + Download](/releases)
1. [Steam Workshop: CommandLineActions](https://steamcommunity.com/sharedfiles/filedetails/?id=2942030650)
2. [Space Dave](https://steamcommunity.com/id/holohkdofih)

```
Currently I am working on the project and this is under construction,
thus for now I do not have a documentation ready on how it works - but feel free to look whats been added, Its not hard to understand.

In essence there are initial parameters (/lcd for example) which specifies what type of command you want to execute.
The secondary parameter specifies an action associated with the initial parameter (for example /lcd toggle), These so called actions can execute functions I have assigned to them.
The third parameter is usually a block name or block id selector, With this the functionality gets assigned to a specific block on the grid. (for example /lcd toggle "LCD 1") - a * selector is for selecting each block with the same type on the grid.
The reset of the parameters will hold the arguments for the command.

Passive Effects while this scripts runs are:
> Doors close after opening, can be disabled per door by using [exclude] in its terminal name.
> Lights flicker now, there is a 0.5% chance that this happens to any light on the grid, this chance is calculated 6 times per second.

Example Commands
/lcd toggle <TargetName> <Positive> <Negative>
/lcd toggle LCD1 YES NO => This command can toggle the lcd between two states with a positive and negative message.

/lcd show <TargetName> <Type> <Message>
/lcd show LCDTop warning "Close Helmet" => Will show a orange lcd with the a message for no breathable air
/lcd show LCDTop success "Open Helmet" => Will show a green lcd with the a message for breathable air

Show -Types:
1. success
2. danger
3. warning
4. info

THIS SCRIPT IS UNDER CONSTRUCTION!! THIS MEANS THAT THIS DESCRIPTION IS AN EXAMPLE OF HOW ITS DESIGNED TO BE USED - ANYTHING IS UP FOR DEBATE
```