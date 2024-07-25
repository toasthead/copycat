Copy Cat v.1.00.01
==================
Copy Cat - All I need are some tasty waves, a cool buzz, and I'm fine. A totes yeah man rotes totes frees as in frees trades copierz for NT8! Righteous Dudes! Up to 20 accounts follow one leader Cat!


Warnings
========
1. Free don't mean easy.
2. Free means be careful.
3. Lots of bugs means fun times.
4. asdf

ALWAYS
======
1.  Always. No matter what.  Add this indicator as a New Panel.  When you add the indicator there is a section called "Visual" and it has a drop down. Use the "Panel" dropdown and select "New Panel". Derk says, "Not on my price panel!".
2.  A cool feature man, is that the Indicator NAME will be the Cat Account you choose but you SHOULD use the text input Label to identify the underlying Kitten accounts.
3.  You will understand in time or you will stand under.
4.  asdf

Installation
=============
1. Deeeeeeeeeeeeeeeerrrrrrrrrrrrkkkkkkkkkkiiiiiiiieeeeee!
2. Just download it and then use the fucking NT8 import.
3. DO NOT FORGET to make sure the indicator is in it's own panel.  Panelrific!
4. It is totally free open source so you can open the .cs file in a text editor of your choice like HatePad, Derk means, Notepad and see there is nothing harmful.  Trust us, if we wanted to harm you, you would have already been harmed.
5. asdf
   
Setup
=====
1. Derk recommends you have your chart on the account you want to replicate.
2. Add the CopyCat indicator to your chart.
3. Set the "Copy from this Account" to the account on your Chart.
4. Set the various Account 1 through 20 to what you want to copy.
5. asdf

Ideas on Using with Strategies
==============================
1. NinjaTrader strategies are notoriously difficult to synchronize with the underlying accounts. If you understand then you don't stand under.
2. So, Derk suggests, you create a minimum of 3 Sim accounts for each Strategy so you can synchronize the underlying accounts.
3. I am not going to use the word "Sim" because for some reason NinjaTrader thinks that is mandatory.
4. Create SimTest1A, SimTest1B, SimTest1C simulation accounts before connecting to your live data feed. Set the commission and cash balance as you see fit. I used the werd.
5. Derk suggest you create a Sim account for garbage.  Create a Sim account called SimGarbageMan.  Why? So you can swap out Kitten follows from the Cat leader account without disabling too much shat. If you know NT8 indicator and strategy behavior you will understand or stand under!
6. Now, suppose you have a strategy on a 1m chart called "Bullshit".  You assign that strategy and/or chart to SimTest1A account. You add CopyCat with SimTest1A as the Cat (leader).  I say strategy bcuz you might be a fancy mutherfucker who does strats without charts. Still the Cat is the Cat and not a Kitten.
7. Add CopyCat and the Leader Cat is SimTest1A and the follower is SimTest1B and activate it.  Every trade SimTest1A takes will be copied to SimTest1B.
8. Here is where it gets fun.
9. Set up a 2nd chart with SimTest1B as the account.  It doesn't need a strategy.  Add CopyCat with SimTest1B as the Cat and then add your actual personal or prop firm accounts as Kittens.  Now you can either safely synchronize yourself to a running strategy on SimTest1A via SimTest1B or after synchronizing you can easily exit if you want to manually manage the trade.  Why? Because you can either activate it and when SimTest1A executes an order and copies to SimTest1B it will be copied to all of SimTest1B's Kittens.  OR OR OR OR OR OR....yeah, the big OR...you can leave the CopyCat deactivated on the SimTest1A chart/account but activate it on the SimTest1B chart/account to try to synchronize the underling Kitten accounts.  If this makes sense to you then you are smart.  If you are lost at this point then there is absolutely no hope for you.
10. asdf

TODO
====
1. Prevent Master from being a child/follower.  I think I did this. Deeeerkie?
2. Clear drop down when removing an account. This is a pain point right now. To avoid it create a Sim account with like a million bucks called GarbageMan and just assign it.
3. Instead of showing Master + Label in the Display override show Master and all active/real followers/kiddos
4. Farking redo the source code to use the werds Cat and Kitten and not the lame ass shit it does.
5. Active/Inactive boolean for each account in the drop down.
6. Ability to have a different instrument (symbol) for each Kitten.
7. Ability to have different lot sizes per Kitten.
8. Instead of dropdowns make it more like how Quantower does it with Groups.
9. asdf

BUGZ
====
1. Can't save templates etc... because the account is attached. Blah, blah, blah you will figure this out.  See the TODOs mofo!
2. asdf
   
WERDZ AKA CREDITS
=================
1. Werds to the Markdown kids [https://www.markdownguide.org/] Making coding less of a headache every day.

