# MultiplayerShooterProject
THIS README WILL NOT BE UPDATED AS THE PROJECT GOES ON. PLEASE REFER TO [ALTERNATE DOCUMENTATION PROVIDED](https://www.youtube.com/watch?v=2qBlE2-WL60) TO LEARN MORE. (haven't made it yet, currently sends you to google)


## So, why another shooter?
I love fps games. They're easily my favourite ones to work on. They're also the ones I enjoy making the most, too.<br>
Its been tough recently - I've finished uni about a month and a half ago at the time of creating this repo and I've been struggling to get back into game dev.
<br>This is (hopefully) part of the solution.

## What's the solution then?
My final major project (FMP) was a framework for a multiplayer FPS. But, with only 8 weeks to make it, it had lots of flaws as I had to push it to get to a stable point a little sooner than I'd have liked.<br>
The result: a messy project that's difficult to continue to work with.<br>
The code that drives the animations has its weird quirks. A lot of it is far too rigid.<br>
I want to integrate melee weapons, but I don't see any feasible way to do that without entirely rewriting the animation systems. And that means having a LOT of broken code whilst I do that.<br>
The fix for that, in my eyes, is to iterate upon the entire project once more. That's all these shooter projects ever really are; another iteration upon another, forever building whilst I rewrite the same code a million times until its damn near perfect.<br>
I'm getting there.

## And what exactly is _this_ one going to do differently?
I love Call of Duty, weirdly enough. Its a fantastic franchise that has always been close to my heart, I think.<br>
One of my favourite parts of that game, or most shooters, is modularity.<br>
<br>
Picture it this way: you're 11. You and your older brother are playing Black Ops II together for the first time and you discover you can put a scope on your gun.<br>
Now you can see further. Now you can aim better. You find different attachment combinations that make the gun feel a lot better to use. Now you're closer to winning.<br>
You still lost at the end of the day, because your brother is much older than you, but it was still a fun experience.<br>
<br>
I can't say I'm going to redefine people's childhoods or anything - that's not gonna happen anytime soon. I'm also not going to try to make something that fully recaptures that feeling, because I don't think I'd be able to. Not yet, anyway. I'm just one girl.<br>
What I _do_ want to do, though, is to start hammering away at a game that puts the player in control of how their weapon feels. Modularity should be intrinsic to this game.<br>

## Modularity!
Being able to build my own gun in a game is one of my favourite things. The newer call of duty games capture that excellently, but there's also something about the way that Battlefield 2042 allows you to customise your weapons in the field that excites me.<br>
The way classes work in CoD MW2019/2022, paired with on-the-fly customisation that's similar to BF2042 would be something to aspire to.
### Classes
The character itself should be customisable. The player should be able to equip different perks and/or abilities to work around their playstyle. A class/loadout may look similar to that of Call of Duty or The Finals.<br>
Currently, I envision a player being able to pick two weapons, two grenades and three perks.
### Weapons
Weapons will be comprised of a few pieces. The Core of a weapon is what determines the main properties of the weapon.<Br>
Players will be able to modify other parts of the weapon, such as optics for better aim, magazines that change the weapon's capacity, and ammo types that alter how the weapon interacts with the world and other players.<br>
Players will have two weapons - a "primary" and "secondary" weapon. This will conform to typical shooter conventions, in that your primary is usually a larger weapon while your secondary is smaller or less suited for normal combat.<br>
Weapons will have unique customisation options. Coming back to the core, that will determine the animation set your weapon uses. There will be a few of these cores, while each core will have its selection of additional parts.<br>
Changing weapons may not have as big a payoff as changing grenades (detailed below) but it'll help to mold your gameplay towards the best possible way you can play. Each part of the class is meant to compliment the others.
### Grenades
I would also like grenades to be customisable - Mixing and matching different types of grenades to create interesting combos.<br>
My immediate thoughts on this are three modules.
* Core - The Core - How the grenade functions on a basic level. Does it deal heavy damage, or make a smoke cloud? Does it electrify enemies, knock them back?
* Module 1 - The Trigger Modifier - Should they stick to a surface to lay a trap? Should a grenade hit a wall, jump off it and then explode? Should it explode multiple times?
* Module 2 - The Flight Modifier - Does it fly straight, meaning it can't get over a wall? Should you be able to aim the throw with a holographic arc, revealing your position to everyone?
The thought behind the two modules is that they grant benefits with drawbacks too. Most of them should be compatible with each other, but I'll have to figure out systems for when they might not be. <br>
Some wacky grenade builds should be entirely possible, though! I want players to be able to do things like stun grenades that follow you round corners before going off three times, or a smoke trap that blinds you and your team when you try to rush.<br>
### Perks
Perks will be simple but interchangeable modifiers to your character that can help compliment the rest of your build.<br>
Perhaps you're playing a sniper build with Trap grenades and a long range rifle, but you want to keep your back covered? Try the Motion Sensor perk - giving an audio-visual cue when someone moves near you!<br>
Opting for a Close Quarters build? Maybe shotguns are more your style? Why not use the Leg Day perk, increasing your movement speed by 10% to help you close gaps just a little bit easier!<br>
Scared of surveillance systems? We've got you covered - Off The Grid prevents you from appearing on the minimap when you're on the move by scrambling your signals to the eyes up above.<br>
<br>
Everything will (hopefully) work together to help players build the craziest things they can. I'd also like to discourage a meta from forming, but that will only be applicable when the game is actually in the real world, whether it's testing or a full release.<br>
This readme will likely stay this way to conserve my original intent with the project, but an alternative form of documentation may be used.
