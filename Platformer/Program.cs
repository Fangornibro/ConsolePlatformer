using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Chess
{
    //Структура одного блоку(в тому числi й гравця)
    struct Block
    {
        public char blockID { get; set; }
        public int curx { get; set; }
        public int cury { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public static implicit operator Block((char, int, int, int, int) value) =>
              new Block { blockID = value.Item1, curx = value.Item2, cury = value.Item3, width = value.Item4, height = value.Item5 };
        public List<string> blockPatterns()
        {
            List<string> block = new List<string>();
            //Вигляд блоку у грi
            switch (blockID)
            {
                case '-':
                    block.Add("********");
                    block.Add("*000000*");
                    block.Add("*000000*");
                    block.Add("*000000*");
                    block.Add("********");
                    break;
                case '|':
                    block.Add("        ");
                    block.Add("   /\\   ");
                    block.Add("  /00\\  ");
                    block.Add(" /0000\\ ");
                    block.Add("/******\\");
                    break;
                case '0':
                    block.Add("        ");
                    block.Add("        ");
                    block.Add("        ");
                    block.Add("        ");
                    block.Add("        ");
                    break;
                case 'P':
                    block.Add("  (\\_/)");
                    block.Add("  ('x')");
                    block.Add("o( _ _)");
                    break;
                case 'p':
                    block.Add("(\\_/)  ");
                    block.Add("('x')  ");
                    block.Add("(_ _ )o");
                    break;
                case 'E':
                    block.Add("  (00)  ");
                    block.Add(" (0++0) ");
                    block.Add("(0++++0)");
                    block.Add(" (0++0) ");
                    block.Add("  (00)  ");
                    break;
                case '1':
                    block.Add("((1111))");
                    block.Add(" ((11)) ");
                    block.Add("  (\\/)  ");
                    block.Add("  (\\/)  ");
                    block.Add("  (\\/)  ");
                    break;
                case '2':
                    block.Add("((2222))");
                    block.Add(" ((22)) ");
                    block.Add("  (\\/)  ");
                    block.Add("  (\\/)  ");
                    block.Add("  (\\/)  ");
                    break;
                case '3':
                    block.Add("((3333))");
                    block.Add(" ((33)) ");
                    block.Add("  (\\/)  ");
                    block.Add("  (\\/)  ");
                    block.Add("  (\\/)  ");
                    break;
                case 'r':
                    block.Add("))))))))");
                    block.Add("        ");
                    block.Add("        ");
                    block.Add("        ");
                    block.Add("        ");
                    break;
                case 'b':
                    block.Add(" || ");
                    block.Add(" || ");
                    block.Add(" || ");
                    block.Add(" || ");
                    block.Add(" || ");
                    break;
                case 'B':
                    block.Add("    ");
                    block.Add("    ");
                    block.Add("    ");
                    block.Add("    ");
                    block.Add("    ");
                    break;
            }
            return block;
        }
        //Вiдмалювання блоку 
        public void blockVisualization()
        {
            List<string> block = blockPatterns();
            Console.SetCursorPosition(curx + (8 - block[0].Length), cury + (5 - block.Count));
            for (int i = 0; i < block.Count; i++)
            {
                for (int j = 0; j < block[0].Length; j++)
                {
                    Console.Write(block[i].ToCharArray()[j]);
                }
                Console.SetCursorPosition(Console.CursorLeft - block[0].Length, Console.CursorTop + 1);
            }
        }

    }
    class Program
    {
        public static int jumpingTime = 0;
        public static int laserTime = 2;
        public static bool onFloor = true;
        public static List<string> worldCreation(int level)
        {
            //Мапа гри через список 
            List<string> world = new List<string>();
            if (level == 1)
            {
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("00000000000000-------------------------------");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-0000000000--0--0--00000000000-");
                world.Add("00000000000000-0P0000--000000000000000000000-");
                world.Add("00000000000000----||---||||||||||||-----0000-");
                world.Add("00000000000000---------------------------000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-000--------00-----------------");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-000000000000000000000000000E0-");
                world.Add("00000000000000------------||-----------------");
            }
            else if (level == 2)
            {
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("00000000000000-------------------------------");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-0000-00-0000000000000000000E0-");
                world.Add("00000000000000-00-0000000-0-00-0-00-0-00-----");
                world.Add("00000000000000-000--000000000000000000000000-");
                world.Add("00000000000000-000000-0000000000000000000000-");
                world.Add("00000000000000-000000000-00000|0000000000000-");
                world.Add("00000000000000-000000000000-0000000000000000-");
                world.Add("00000000000000-000P00000-0000000000000000000-");
                world.Add("00000000000000-000000-0000000000000000000000-");
                world.Add("00000000000000--------||||||||||||||||||||||-");
            }
            else if (level == 3)
            {
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("00000000000000-------------------------------");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00P00000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000------||-||-||-||-||-||---0000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-000----|--|--|--|--|----------");
                world.Add("00000000000000-00000000000000000000000000000-");
                world.Add("00000000000000-000000000000000000000000000E0-");
                world.Add("00000000000000---------|--|--|--|------------");
            }
            else if (level == 4)
            {
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("00000000000000-------------------------------");
                world.Add("00000000000000-0000-1rrrrrrrrrrrrrrrrrrrrrrr-");
                world.Add("00000000000000-00P0000000-00000-0000-0000----");
                world.Add("00000000000000-000000000000000000000000000E0-");
                world.Add("00000000000000-------------------------------");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
                world.Add("000000000000000000000000000000000000000000000");
            }
            return world;
        }
        //Вiдмалювання рiвня по мапi
        public static void worldVisualization(int level, out Block player, out Block laser)
        {
            player = ('d', 0, 0, 0, 0);
            laser = ('d', 0, 0, 0, 0);
            List<string> world = worldCreation(level);
            Console.SetBufferSize(1000, 1000);
            for (int i = 0; i < world.Count; i++)
            {
                for (int j = 0; j < world[i].Length; j++)
                {
                    
                    if (world[i].ToCharArray()[j] == 'P')
                    {
                        Block block = (world[i].ToCharArray()[j], j * 8, i * 5, 7, 3);
                        block.blockVisualization();
                        player = block;
                    }
                    else if (world[i].ToCharArray()[j] == '1')
                    {
                        Block block = (world[i].ToCharArray()[j], j * 8, i * 5, 8, 3);
                        block.blockVisualization();
                        laser = block;
                    }
                    else
                    {
                        Block block = (world[i].ToCharArray()[j], j * 8, i * 5, 8, 5);
                        block.blockVisualization();
                    }
                }
            }
        }
        //Операцiї з клавiатурою. Пересування вказiвника й видiлення фiгур(а також звуки при видiленнi)
        static Block KeyboardOperation(ConsoleKey key, Block player, Block laser, out Block newlaser, out bool win, out bool lose)
        {
            //Спочатку видаляємо гравця, мiняємо йому положення в залежностi вiд натиснутої кнопки й вiдмальовуємо знову
            eraseBlock(player);
            var temp1 = Console.CursorLeft;
            var temp2 = Console.CursorTop;
            char? wall;
            bool isSomething = false, laserIsSomethingLeft = false, laserIsSomethingRight = false;
            //AI лазеру
            if (!isDefault(laser))
            {
                eraseBlock(laser);
                int dir = player.curx - laser.curx;
                wall = ReadCharacterAt(laser.curx - 1, laser.cury);
                if (wall == '*')
                {
                    laserIsSomethingLeft = true;
                }
                wall = ReadCharacterAt(laser.curx + 8, laser.cury);
                if (wall == '*')
                {
                    laserIsSomethingRight = true;
                }
                if (!laserIsSomethingRight && dir > 0)
                {
                    laser.curx += 1;
                }
                else if (!laserIsSomethingLeft && dir < 0)
                {
                    laser.curx -= 1;
                }
                laserTime--;
                laser.blockID = ((laserTime / 25) + 1).ToString()[0];
                if (laserTime == 1)
                {
                    laserTime = 75;
                }
                laser.blockVisualization();
                laserAttack(laser);
                Console.SetCursorPosition(temp1, temp2);

                for (int i = 0; i < 7; i++)
                {
                    char? roof = ReadCharacterAt(Console.CursorLeft + i, Console.CursorTop - 4);
                    if (roof == '|')
                    {
                        lose = true;
                        win = false;
                        newlaser = laser;
                        return player;
                    }
                }
            }
            switch (key)
            {
                //Рух лiворуч
                case ConsoleKey.A:
                    Console.SetCursorPosition(Console.CursorLeft - Console.WindowWidth / 2, Console.CursorTop);
                    Console.SetCursorPosition(temp1, temp2);
                    if (Console.CursorLeft != 1)
                    {
                        player.blockID = 'p';
                        for (int i = 0; i < 3; i++)
                        {
                            wall = ReadCharacterAt(Console.CursorLeft -1, Console.CursorTop - 1 - i);
                            if (wall != ' ')
                            {
                                if (wall == '(' || wall == ')')
                                {
                                    lose = false;
                                    win = true;
                                    newlaser = laser;
                                    return player;
                                }
                                isSomething = true;
                            }
                        }
                        if (!isSomething)
                        {
                            player.curx -= 1;
                        }
                    }
                    break;
                //Рух праворуч
                case ConsoleKey.D:
                    if (Console.CursorLeft != 1000)
                    {
                        Console.SetCursorPosition(Console.CursorLeft + Console.WindowWidth / 2, Console.CursorTop);
                        Console.SetCursorPosition(temp1, temp2);
                        player.blockID = 'P';
                        for (int i = 0; i < 3; i++)
                        {
                            wall = ReadCharacterAt(Console.CursorLeft + 7, Console.CursorTop - 1 - i);
                            if (wall != ' ')
                            {
                                if (wall == '(' || wall == ')')
                                {
                                    lose = false;
                                    win = true;
                                    newlaser = laser;
                                    return player;
                                }
                                isSomething = true;
                            }
                        }
                        if (!isSomething)
                        {
                            player.curx += 1;
                        }                        
                    }
                    break;
                    //Стрибок
                case ConsoleKey.Spacebar:
                    if (onFloor)
                    {
                        string fileName = "jump.wav";
                        string path = Path.Combine(Environment.CurrentDirectory, @"Sounds\", fileName);
                        SoundPlayer sound = new SoundPlayer(path);
                        sound.Play();
                        Console.SetCursorPosition(Console.CursorLeft + Console.WindowWidth / 2, Console.CursorTop - Console.WindowHeight / 2);
                        Console.SetCursorPosition(temp1, temp2);
                        jumpingTime = 9;
                    }           
                    break;
            }
            Console.SetCursorPosition(player.curx, player.cury);
            player.blockVisualization();
            lose = false;
            win = false;
            newlaser = laser;
            return player;
        }
        public static void laserAttack(Block laser)
        {
            //Атака лазеру
            bool isSomething = false;
            for (int i = 0; i < 4; i++)
            {
                if (ReadCharacterAt(laser.curx + 2 + i, laser.cury + 8) == '*' || ReadCharacterAt(laser.curx + 2 + i, laser.cury + 8) == '0')
                {
                    isSomething = true;
                }
            }

            if (!isSomething)
            {
                char blockID = 'B';
                if (laser.blockID == '1')
                {
                    blockID = 'b';
                }
                for (int i = 1; i < 3; i++)
                {
                    Block beam = (blockID, laser.curx - 2, laser.cury + (i * 5), 2, 5);
                    beam.blockVisualization();
                }
            }
        }
        //Перевiрка на те, чи персонаж у стрибку
        public static bool checkJump(Block player, out Block newplayer)
        {
            if (jumpingTime != 0)
            {
                jumpingTime--;
                char? roof;
                bool isSomething = false;
                //Перевiряємо чи є зверху якийсь об'єкт
                for (int i = 0; i < 7; i++)
                {
                    roof = ReadCharacterAt(Console.CursorLeft + i, Console.CursorTop - 4);
                    if (roof != ' ')
                    {
                        isSomething = true;
                    }
                    if (roof == '/' || roof == '\\')
                    {
                        newplayer = player;
                        return false;
                    }
                }
                //Якщо немає то пiдiймаємо гравця вгору
                if (!isSomething)
                {
                    eraseBlock(player);
                    var temp1 = Console.CursorLeft;
                    var temp2 = Console.CursorTop;
                    Console.SetCursorPosition(Console.CursorLeft + Console.WindowWidth / 2, Console.CursorTop - Console.WindowHeight / 2);
                    Console.SetCursorPosition(temp1, temp2);
                    player.cury -= 1;
                    Console.SetCursorPosition(player.curx, player.cury);
                    player.blockVisualization();
                }
                else
                {
                    jumpingTime = 0;
                }
            }
            newplayer = player;
            return true;
        }
        //Функцiя стирання гравця
        public static void eraseBlock(Block block)
        {
            Console.SetCursorPosition(block.curx + 1, block.cury + 2);
            for (int i = 0; i < block.height; i++)
            {
                for (int j = 0; j < block.width; j++)
                {
                    Console.Write(' ');
                }
                Console.SetCursorPosition(block.curx + 1, Console.CursorTop + 1);
            }
        }
        //Функцiя, що вiдповiдає за падiння гравця(за гравiтацiю)
        public static bool checkFloorPlayer(Block player, out Block newplayer)
        {
            char? floor;
            bool isSomething = false;
            for (int i = 0; i < 7; i++)
            {
                floor = ReadCharacterAt(Console.CursorLeft + i, Console.CursorTop);
                if (floor != ' ')
                {
                    isSomething = true;
                }
                if (floor == '/' || floor == '\\')
                {
                    newplayer = player;
                    onFloor = true;
                    return false;
                }
            }
            if (!isSomething)
            {
                if (jumpingTime == 0)
                {
                    eraseBlock(player);
                    var temp1 = Console.CursorLeft;
                    var temp2 = Console.CursorTop;
                    Console.SetCursorPosition(Console.CursorLeft + Console.WindowWidth / 2, Console.CursorTop + Console.WindowHeight / 2);
                    Console.SetCursorPosition(temp1, temp2);
                    player.cury += 1;
                    Console.SetCursorPosition(player.curx, player.cury);
                    player.blockVisualization();
                }
                onFloor = false;
            }
            else
            {
                onFloor = true;
            }
            newplayer = player;
            return true;
        }
        //Функцiя, яка дає змогу зчитувати який символ написаний у консолi на обраному мiсцi
        public static char? ReadCharacterAt(int x, int y)
        {
            IntPtr consoleHandle = GetStdHandle(-11);
            if (consoleHandle == IntPtr.Zero)
            {
                return null;
            }
            Coord position = new Coord
            {
                X = (short)x,
                Y = (short)y
            };
            StringBuilder result = new StringBuilder(1);
            uint read = 0;
            if (ReadConsoleOutputCharacter(consoleHandle, result, 1, position, out read))
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput, [Out] StringBuilder lpCharacter, uint length, Coord bufferCoord, out uint lpNumberOfCharactersRead);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;
        }

        public static bool isDefault(Block block)
        {
            return block.blockID == 'd';
        }
        static void Main(string[] args)
        {
            int curLevel = 4;
            worldVisualization(curLevel, out Block player, out Block laser);
            Console.SetCursorPosition(player.curx - Console.WindowWidth / 2 + 2, player.cury - Console.WindowHeight / 2 + 5);
            Console.SetCursorPosition(player.curx, player.cury);
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    bool win;
                    player = KeyboardOperation(key.Key, player, laser, out laser, out win, out bool lose);
                    if (lose)
                    {
                        jumpingTime = 0;
                        laserTime = 2;
                        Console.SetCursorPosition(1, 1);
                        Console.Clear();
                        worldVisualization(curLevel, out player, out laser);
                        Console.SetCursorPosition(player.curx - Console.WindowWidth / 2 + 2, player.cury - Console.WindowHeight / 2 + 5);
                        Console.SetCursorPosition(player.curx, player.cury);
                    }
                    if (win)
                    {
                        jumpingTime = 0;
                        laserTime = 2;
                        if (curLevel == 4)
                        {
                            break;
                        }
                        curLevel++;
                        Console.SetCursorPosition(1, 1);
                        Console.Clear();
                        worldVisualization(curLevel, out player, out laser);
                        Console.SetCursorPosition(player.curx - Console.WindowWidth / 2 + 2, player.cury - Console.WindowHeight / 2 + 5);
                        Console.SetCursorPosition(player.curx, player.cury);
                    }
                    if (!checkJump(player, out player))
                    {
                        jumpingTime = 0;
                        laserTime = 2;
                        Console.SetCursorPosition(1, 1);
                        Console.Clear();
                        worldVisualization(curLevel, out player, out laser);
                        Console.SetCursorPosition(player.curx - Console.WindowWidth / 2 + 2, player.cury - Console.WindowHeight / 2 + 5);
                        Console.SetCursorPosition(player.curx, player.cury);
                    }
                    if (!checkFloorPlayer(player, out player))
                    {
                        jumpingTime = 0;
                        laserTime = 2;
                        Console.SetCursorPosition(1, 1);
                        Console.Clear();
                        worldVisualization(curLevel, out player, out laser);
                        Console.SetCursorPosition(player.curx - Console.WindowWidth / 2 + 2, player.cury - Console.WindowHeight / 2 + 5);
                        Console.SetCursorPosition(player.curx, player.cury);
                    }
                    
                }
            }
        }
    }
}