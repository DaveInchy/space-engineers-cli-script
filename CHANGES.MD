 ## Changelog:
 
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
 