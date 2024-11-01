﻿using System;
using System.Collections.Generic;
using System.Linq;
// Абстрактний клас Worker
public abstract class Worker
{
    public string Name { get; set; }
    public string Position { get; set; }
    public int WorkDay { get; set; }
    // Конструктор з ім'ям
    public Worker(string name)
    {
        Name = name;
    }
    // Абстрактний метод FillWorkDay
    public abstract void FillWorkDay();
    // Метод Call
    public void Call()
    {
        Console.WriteLine($"{Name} makes a call.");
    }
    // Метод WriteCode
    public void WriteCode()
    {
        Console.WriteLine($"{Name} writes code.");
    }
    // Метод Relax
    public void Relax()
    {
        Console.WriteLine($"{Name} rest.");
    }
}
// Клас Developer, нащадок класу Worker
public class Developer : Worker
{
    // Конструктор, де визначається позиція "Розробник"
    public Developer(string name) : base(name)
    {
        Position = "Developer";
    }
    // Перевизначений метод FillWorkDay
    public override void FillWorkDay()
    {
        WriteCode();
        Call();
        Relax();
        WriteCode();
    }
}
// Клас Manager, нащадок класу Worker
public class Manager : Worker
{
    private Random random = new Random();
    // Конструктор, де визначається позиція "Менеджер"
    public Manager(string name) : base(name)
    {
        Position = "Manager";
    }
    // Перевизначений метод FillWorkDay
    public override void FillWorkDay()
    {
        int callCount1 = random.Next(1, 11);
        for (int i = 0; i < callCount1; i++)
        {
            Call();
        }
        Relax();
        int callCount2 = random.Next(1, 6);
        for (int i = 0; i < callCount2; i++)
        {
            Call();
        }
    }
}
// Клас Team
public class Team
{
    private List<Worker> teamMembers = new List<Worker>();
    public string TeamName { get; set; }
    public List<Worker> TeamMembers
    {
        get { return teamMembers; }
    }
    // Конструктор з ім'ям команди
    public Team(string teamName)
    {
        TeamName = teamName;
    }
    // Метод додавання нового співробітника до команди
    public void AddMember(Worker worker)
    {
        teamMembers.Add(worker);
    }
    // Метод виведення інформації про команду
    public void PrintTeamInfo()
    {
        Console.WriteLine($"Team: {TeamName}");
        Console.WriteLine("Workers:");
        foreach (var worker in teamMembers)
        {
            Console.WriteLine($"{worker.Name} - {worker.Position} - {worker.WorkDay}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Team team1 = new Team("Devs");
        Team team2 = new Team("Managers");
        while (true)
        {
            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Add person to team");
            Console.WriteLine("2. Show info about teams");
            Console.WriteLine("3. Show worker`s shcedule");
            Console.WriteLine("4. End");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Input name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Choose position (1 - Developer, 2 - Manager):");
                    string positionChoice = Console.ReadLine();
                    Worker newWorker = null;
                    if (positionChoice == "1")
                    {
                        newWorker = new Developer(name);
                    }
                    else if (positionChoice == "2")
                    {
                        newWorker = new Manager(name);
                    }
                    if (newWorker != null)
                    {
                        Console.WriteLine($"Worker {name} is successfully added.");
                        team1.AddMember(newWorker);
                    }
                    else
                    {
                        Console.WriteLine("Error: choose another position.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Info about teams:");
                    team1.PrintTeamInfo();
                    team2.PrintTeamInfo();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                case "3":
                    Console.WriteLine("Input worker's name to show their schedule:");
                    string employeeName = Console.ReadLine();
                    Console.WriteLine("Input name of the worker's team (Devs or Managers):");
                    string teamName = Console.ReadLine();
                    Team selectedTeam = (teamName == "Devs") ? team1 : (teamName == "Managers") ? team2 : null;
                    if (selectedTeam != null)
                    {
                        Worker selectedWorker = selectedTeam.TeamMembers.FirstOrDefault(w => w.Name == employeeName);
                        if (selectedWorker != null)
                        {
                            Console.WriteLine($"Schedule of {employeeName} in team {teamName}:");
                            selectedWorker.FillWorkDay();
                        }
                        else
                        {
                            Console.WriteLine($"Worker {employeeName} isn't found in {teamName}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} can't be found.");
                    }
                    break;
                default:
                    Console.WriteLine("Wrong choice. Try again.");
                    break;
            }
        }
    }
}