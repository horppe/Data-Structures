using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Timetable
{
    class Program
    {
        static void Main(string[] args)
        {
            //The Debug work for this code is too much unnecessary stress on me right now. Update!! Although i eventually got around to write it
            TimeTable timeTable = new TimeTable();
            timeTable.AddEvent(new DateTime(2017, 12, 31, 23, 59, 59), new DateTime(2018, 1, 2, 0, 0, 0), @"Happy New Year!!!");
            timeTable.AddEvent(new DateTime(2018, 1, 3, 0, 0, 0), new DateTime(2018, 1, 4, 0, 0, 0), @"Biodun's Birthday");
            timeTable.AddEvent(new DateTime(2018, 1, 7, 0, 0, 0), new DateTime(2018, 1, 8, 0, 0, 0), @"Another Celebration of Something");
            timeTable.AddEvent(new DateTime(2018, 1, 10, 0, 0, 0), new DateTime(2018, 1, 12, 0, 0, 0), @"Gosh what to write?");
            timeTable.AddEvent(new DateTime(2018, 1, 13, 0, 0, 0), new DateTime(2018, 1, 20, 0, 0, 0), @"No One Celebration of Something");
            timeTable.AddEvent(new DateTime(2018, 1, 21, 0, 0, 0), new DateTime(2018, 1, 25, 0, 0, 0), @"No Two Celebration of Something");
            timeTable.AddEvent(new DateTime(2018, 1, 26, 0, 0, 0), new DateTime(2018, 1, 28, 0, 0, 0), @"No Three Celebration of Something");
            Console.WriteLine(@"Is our Event Available? {0}.", timeTable.IsAvailable(new DateTime(2018, 1, 28, 12, 0, 0), new DateTime(2018, 1, 29, 0, 0, 0)));
            Console.ReadKey();
        }
    }

    class TimeTable
    {
        public class Event : IComparable<Event>
        {
            public DateTime start;
            public DateTime end;
            public string eventName;
            public Event(DateTime strt, DateTime nd, string evntNme)
            {
                start = strt;
                end = nd;
                eventName = evntNme;
            }

            public int CompareTo(Event evnt)
            {
                bool isNegative = this.start < evnt.start;
                bool isPositive = this.start > evnt.start;

                if (isNegative)
                {
                    return -1;
                }
                else if (isPositive)
                {
                    return 1;
                }
                return 0;
            }

            public override int GetHashCode()
            {
                return start.GetHashCode() + end.GetHashCode() + eventName.GetHashCode();
            }
        }

        public List<Event> events;

        public TimeTable()
        {
            events = new List<Event>();
        }

        public bool IsAvailable(DateTime strt, DateTime nd)
        {
            List<Event> sortedEvents = new List<Event>(events);
            sortedEvents.Sort();
            HashSet<Event> start = new HashSet<Event>();
            HashSet<Event> end = new HashSet<Event>();
            int p = sortedEvents.Count;
            for (int i = 0; i < p; i++)
            {
                int mid = (i + p) / 2;
                if (sortedEvents[mid].start < strt)
                {
                    i = mid + 1;
                }
                else if (sortedEvents[mid].start > strt)
                {
                    p = mid - 1;
                }

                if (sortedEvents[mid].start >= strt)
                {
                    start.Add(sortedEvents[mid]);
                }
                if (sortedEvents[mid].end <= nd)
                {
                    end.Add(sortedEvents[mid]);
                }
            }
            //foreach (var evnt in sortedEvents)
            //{
            //    if (evnt.start >= strt)
            //    {
            //        start.Add(evnt);
            //    }
            //    if (evnt.end <= nd)
            //    {
            //        end.Add(evnt);
            //    }
            //}

            start.IntersectWith(end);
            return start.Count == 0;

        }
        
        public void AddEvent(DateTime start, DateTime end, string eventName)
        {
            events.Add(new Event(start, end, eventName));
        }

    }
}
