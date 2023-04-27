 ## Changelog:
 
 ### 🚀 Release 2.0.0
  1. Added a Scheduler to run Tasks in parallel without performance impact.
  2. Updated Statistic Output in InfoPanel on your Programmable Block.
  3. Increased Tick Rate because Tasks are run on a float number representing seconds.
  4. Added 2 new passive effects to the grid. To give it some vibes and functionality
> 1. Doors close after opening, can be disabled per door by using [exclude] in its terminal name.
> 2. Lights flicker now, there is a 0.5% chance that this happens to any light on the grid, this chance is calculated 6 times per second.

 ### 👩‍🚀 Update 1.1.0
  1. Refactoring Project / Code Base
  2. LCDs can now show different colors / types of messages by using:
     <br>
     `/lcd show <BlockName> <info | danger | warning | success> <Text>`
  3. LCDs can now toggle between a danger and warning message by using:
     <br>
     `/lcd toggle <BlockName> <PositiveText> <NegativeText>`
  4. Removed Unicode Bitmap Images from script because of huge character bottleneck.
  5. Improved Performance by initiating Grid Blocks only once.

 ### 🚀 Release 1.0.0
  1. lcd toggle for positive and negative displays.
  2. lcd show command that currently only shows SDI. Ads.
  3. prepared codebase for expansion, cli logic finishes.
 
# Todo:
  1. Make some kind of event provider that can be listened to for grid events.
     as an example: door open/close -ing. => Autoclosing
  2. Auto closing doors, make it Oxygen Chamber proof.
  3. Actions (Activating events that are already exist.) to execute like what is already demonstrated with LCD's 