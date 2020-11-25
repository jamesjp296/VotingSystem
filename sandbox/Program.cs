﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace sandbox
{
    public class Counter
    {
        private double? _percentage;

        public Counter(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; }
        public int Count { get; private set; }

        public double GetPercent(int total) => _percentage ?? (_percentage = Math.Round(Count * 100.0 / total, 2)).Value;

        public void AddExcess(double excess) => _percentage += excess;

        public void Increment() => Count++;
    }

    public class CounterManager
    {
        public CounterManager(params Counter[] counters)
        {
            Counters = new List<Counter>(counters);
        }

        public List<Counter> Counters { get; set; }

        public int Total() => Counters.Sum(x => x.Count);

        public double TotalPercentage() => Counters.Sum(x => x.GetPercent(Total()));

        public void AnnounceWinner()
        {
            var excess = Math.Round(100 - TotalPercentage(), 2);

            Console.WriteLine($"Excess : {excess}");

            var biggestAmountOfVotes = Counters.Max(x => x.Count);

            var winners = Counters.Where(x => x.Count == biggestAmountOfVotes).ToList();

            if (winners.Count == 1)
            {
                var winner = winners.First();
                winner.AddExcess(excess);
                Console.WriteLine($"{winner.Name} Won!");
            }
            else
            {
                if (winners.Count != winners.Count)
                {
                    var lowestAmountOfVotes = Counters.Min(x => x.Count);
                    var loser = Counters.First(x => x.Count == biggestAmountOfVotes);
                    loser.AddExcess(excess);
                }
                Console.WriteLine(string.Join("-Draw-", winners.Select(x => x.Name)));

            }

            foreach (var c in Counters)
            {
                Console.WriteLine($"{c.Name} Counts : {c.Count}, Percentage : {c.GetPercent(Total())}%");
            }

        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            var yes = new Counter("Yes", 4);
            var no = new Counter("No", 4);
            var maybe = new Counter("Maybe", 4);



            var manager = new CounterManager(yes, no, maybe);

            var yesPercent = yes.GetPercent(manager.Total());
            var noPercent = no.GetPercent(manager.Total());
            var maybePercent = maybe.GetPercent(manager.Total());



        }
    }
}
