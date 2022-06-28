using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    enum StartListColumns
    {
        SportKind,

        Pack,
        Apparatus,
        Event,

        Division,
        Rotation,

        EventTime,
        StartOrder,
        ParticipantGroup,
        ParticipantCategory,
        Description,
        NocOrClub,
        Bib,
        License,
        Surname,
        Name,
        Something1,
        Vault1,
        Vault2,
        SchedulePack,
        Score,
    }

    public class Program3
    {
        // TODO: Remove Unnecessary empty strings in values array
        public static void Main(string[] args)
        {
            dynamic column = new DataColumn();

            using (var stream = new FileStream(@"C:\Users\User\Desktop\tasks\Learning\Learning\StartList.txt", FileMode.Open))
            {
                string sportKind = null;
                string apparatus = null;
                string apparatusPack = null;

                string schedulePack = null;
                var startOrder = 0;

                string nocOrClub = null;
                int rotation = 0;
                string eventDescription = null;
                string division = null;
                string eventTime = null;
                string surname = null;
                string name = null;

                string jumpNumber = null;

                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        // these should be in each row
                        var bib = 0;
                        var group = "";
                        var licenseString = "";
                        var category = "";
                        var description = "";


                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line?.Trim())) continue;
                        Console.WriteLine("Processing: " + line);

                        for (int i = 0; i < 30; i++)
                        {
                            line += "\t";
                        }

                        string[] values = line.Split('\t');
                        values = values.Select(t => t.Trim()).ToArray();

                        ColumnParser parser = new ColumnParser(values);

                        parser.ParseToString(StartListColumns.Vault1, out jumpNumber);
                        parser.ParseToString(StartListColumns.SportKind, out sportKind);
                        parser.ParseToString(StartListColumns.ParticipantGroup, out group);
                        parser.ParseToString(StartListColumns.Description, out description);
                        parser.ParseToString(StartListColumns.ParticipantCategory, out category);
                        parser.ParseToString(StartListColumns.Apparatus, out apparatus);
                        parser.ParseToString(StartListColumns.Pack, out apparatusPack);
                        parser.ParseToInt(StartListColumns.Rotation, out rotation);
                        parser.ParseToString(StartListColumns.Division, out division);
                        parser.ParseToString(StartListColumns.Event, out eventDescription);
                        parser.ParseToString(StartListColumns.EventTime, out eventTime);
                        parser.ParseToString(StartListColumns.NocOrClub, out nocOrClub);
                        parser.ParseToInt(StartListColumns.StartOrder, out startOrder);
                        parser.ParseToInt(StartListColumns.Bib, out bib);
                        parser.ParseToString(StartListColumns.License, out licenseString);
                        parser.ParseToString(StartListColumns.SchedulePack, out schedulePack);
                    }   
                }
            }

        }
    }
}
