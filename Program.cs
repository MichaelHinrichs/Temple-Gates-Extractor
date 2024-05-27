//Written for games by Temple Gates.
//Dominion https://store.steampowered.com/app/1131620
//Race for the Galaxy https://store.steampowered.com/app/579940
//Shards of Infinity https://store.steampowered.com/app/1008800
//Roll for the Galaxy https://store.steampowered.com/app/893840
//Ascension VR https://store.steampowered.com/app/499940
//Bazaar https://store.steampowered.com/app/455230
//Cannon Brawl https://store.steampowered.com/app/230860
namespace Temple_Gates_Extractor
{
    internal class Program
    {
        public static BinaryReader br;
        static void Main(string[] args)
        {
            br = new(File.OpenRead(args[0]));

            if (new string(br.ReadChars(4)) != "KCPX")
                throw new Exception("This is not a Temple Gates Games xpack file.");

            br.ReadInt32();
            int fileCount = br.ReadInt32();
            List<Subfile> subfiles = new List<Subfile>();
            for (int i = 0; i < fileCount; i++)
                subfiles.Add(new());

            string path = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\";
            foreach (Subfile file in subfiles)
            {
                br.BaseStream.Position = file.start;
                Directory.CreateDirectory(path + Path.GetDirectoryName(file.name));
                BinaryWriter bw = new(File.Create(path + file.name));
                bw.Write(br.ReadBytes(file.size));
                bw.Close();
            }
        }

        class Subfile
        {
            int unknown1 = br.ReadInt32();
            int unknown2 = br.ReadInt32();
            float checksum = br.ReadSingle();
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
            int unknown3 = br.ReadInt32();
            int unknown4 = br.ReadInt32();
            int unknown5 = br.ReadInt32();
            int unknown6 = br.ReadInt32();
            int unknown7 = br.ReadInt32();
            public string name = new string(br.ReadChars(128)).TrimEnd('\0');
        }
    }
}
