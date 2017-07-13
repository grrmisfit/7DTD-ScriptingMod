﻿namespace ObjectDumper
{
    public class DumpOptions
    {
        public static DumpOptions Default = new DumpOptions();

        public bool NoFields { get; set; }

        public int MaxDepth { get; set; }

        public DumpOptions()
        {
            NoFields = false;
            MaxDepth = 4;
        }

    }
}