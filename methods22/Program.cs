using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;


internal class Program
{
     static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus)
    {
        var random = new Random();
        int diceTotal = fixedBonus;
        while (numberOfRolls > 0)
        {
            int dice = random.Next(1, diceSides + 1);
            diceTotal += dice;
            numberOfRolls--;
        }
        return diceTotal;
}
    static void SimulateCombat(List<string> characterNames, string monsterName, int monsterHP, int savingThrowDC)
    {
        var random = new Random();
        int x = 0;
        Console.WriteLine($"Adventurers {string.Join(", ", characterNames)} descend into the dungeon.");
        Console.WriteLine($"A(n) {monsterName} with {monsterHP} HP appears!");
        while (monsterHP > 0 && characterNames.Count > 0)
        {
            int diceDmg = DiceRoll(2, 6, 0);
            monsterHP -= diceDmg;
            if (monsterHP <= 0)
            {
                Console.WriteLine($"{characterNames[x]} hits the {monsterName} for {diceDmg}! The {monsterName} has 0 HP remaining and has perished!");
                Console.WriteLine("The party celebrates their recent victory and heads to a nearby tavern for drinks.");
                break;
            }
            else
            {
                Console.WriteLine($"{characterNames[x++]} hits the {monsterName} for {diceDmg}! The {monsterName} has {monsterHP} HP remaining!");
                diceDmg = 0;

                if (x >= characterNames.Count)
                {
                    x = 0;
                    int DCTarget = random.Next(0, characterNames.Count);
                    Console.WriteLine($"The {monsterName} attacks {characterNames[DCTarget]}!");
                    int result = DiceRoll(1, 20, 3);
                    if (result >= savingThrowDC)
                    {
                        Console.WriteLine($"{characterNames[DCTarget]} rolls a {result} and is saved from the attack!");
                    }
                    else
                    {
                        Console.WriteLine($"{characterNames[DCTarget]} rolls a {result} and fails to save! {characterNames[DCTarget]} is killed!");
                        characterNames.Remove(characterNames[DCTarget]);
                        if (characterNames.Count == 0)
                        {
                            Console.WriteLine($"All adventurers have perished and the {monsterName} stands. Game over!");
                            break;
                        }
                    }
                }
            }
        }
    }

    private static void Main(string[] args)
    {
        var characterNames = new List<string> { "Garrosh", "Thrall", "Sylvanas", "Jaina" };
        var monsterName = new List<string> { "Orc", "Azer", "Troll" };


        SimulateCombat(characterNames, monsterName[0], DiceRoll(2, 8, 6), 10);
        if (characterNames.Count > 0)
        {
            SimulateCombat(characterNames, monsterName[1], DiceRoll(6, 8, 12), 18);
        }
        if (characterNames.Count > 0)
        {
            SimulateCombat(characterNames, monsterName[2], DiceRoll(8, 10, 40), 16);
            if (characterNames.Count > 0)
            {
                Console.WriteLine($"{string.Join(", ", characterNames)} survived the battles.");
            }
        }
    }
}