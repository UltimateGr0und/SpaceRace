using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceRace
{
    class body
    {
        private static int id = 0;
        public double prex { set; get; } = int.MaxValue; 
        public double prey { set; get; } = int.MaxValue; 
        protected double posx;
        protected double posy;
        protected double mass;
        protected double vecx;
        protected double vecy;
        protected Random rnd = new Random();

        public double posX
        {
            set { posx = value; }
            get { return posx; }
        }
        public double posY
        {
            set { posy = value; }
            get { return posy; }
        }
        public double vecX
        {
            set { vecx = value; }
            get { return vecx; }
        }
        public double vecY
        {
            set { vecy = value; }
            get { return vecy; }
        }
        public double Mass
        {
            set { mass = value; }
            get { return mass; }
        }
        public static int x_screen_size { set; get; } = 45;
        public static int y_screen_size { set; get; } = 45;
        public void show()
        {
            if (prey != int.MaxValue)
            {
                Console.SetCursorPosition((int)prey + y_screen_size, (int)prex + x_screen_size);
                Console.Write(' ');
            }
            prey = posy;
            prex = posx;
            Console.SetCursorPosition((int)posY + y_screen_size, (int)posX + x_screen_size);
            Console.Write('o');
        }
        public void move(double SpeedConst)
        {
            posx += (vecx*SpeedConst);
            posy += (vecy*SpeedConst);
        }
        public void cnahgeDir(body obj, double SpaceConst)
        {
            double F=0, tempY=0, tempX=0, temp=0, S=0;
            S = Math.Sqrt(Math.Pow(obj.posX - this.posx, 2) + Math.Pow(obj.posY - this.posy, 2));
            F = (obj.mass * SpaceConst) / (S * this.Mass);
            if (posx == 0)
            {
                tempY = F;
            }
            else if (posy == 0)
            {
                tempX = F;
            }
            else
            {
                temp = (obj.posX - this.posx) + (obj.posY - this.posy);
                tempX = Math.Abs(F * ((obj.posX - this.posx)/temp));
                tempY = Math.Abs(F * (temp/ (obj.posY - this.posy)/temp));
            }
            vecX += (posx>0?-tempX:tempX);
            vecY += (posy>0?-tempY:tempY);

        }
        public void reCreate()
        {
            if ((Math.Abs(posX) < 5 && Math.Abs(posY) < 5) || (posx < x_screen_size * -1 || posx > x_screen_size || posy < y_screen_size * -1 || posy > y_screen_size))
            {
                vecx = rnd.Next(-5, 5) / 10;
                vecy = rnd.Next(-5, 5) / 10;
                int temp = rnd.Next(0, 2 * (x_screen_size + y_screen_size));
                id = temp;
                if (temp>=0&&temp<x_screen_size)
                {
                    posx = temp*2;
                    posy = 1;
                }
                else if (temp>=x_screen_size && temp < x_screen_size+ y_screen_size)
                {
                    posx = 1;
                    posy = 2*(temp-x_screen_size);
                }
                else if (temp >= x_screen_size + y_screen_size && temp< x_screen_size*2+y_screen_size)
                {
                    posx = 2*(temp - x_screen_size - y_screen_size);
                    posy = 2*y_screen_size-1;
                }
                else
                {
                    posx = 2 * x_screen_size-1;
                    posy = 2 * (temp - (x_screen_size)*2 - y_screen_size);
                }
                posx -= x_screen_size;
                posy -= y_screen_size;
               
            }
        }
        public body()
        {
            posx = rnd.Next(-x_screen_size, x_screen_size);
            posy = rnd.Next(-y_screen_size, y_screen_size);
            vecx = rnd.Next(-5, 5)/5;
            vecy = rnd.Next(-5, 5)/5;
            mass = 1;
        }
    }
    class star : body
    {
        public new void show()
        {
            Console.SetCursorPosition((int)posY + y_screen_size-2, (int)posX + x_screen_size-2);
            Console.Write(" @@@  ");
            Console.SetCursorPosition((int)posY + y_screen_size - 2, (int)posX + x_screen_size - 1);
            Console.Write("@@@@@  ");
            Console.SetCursorPosition((int)posY + y_screen_size -2, (int)posX + x_screen_size );
            Console.Write("@@@@@  ");
            Console.SetCursorPosition((int)posY + y_screen_size - 2, (int)posX + x_screen_size + 1);
            Console.Write(" @@@  ");
        }
        public star(double pos_x, double pos_y) : base()
        {
            posx = pos_x;
            posy = pos_y;
            Mass = 500;
        }
    }
    class ship : body
    {
        public bool destroyed { get; set; } = false;
        public new void show()
        {
            if (prey != int.MaxValue)
            {
                Console.SetCursorPosition((int)prey + y_screen_size, (int)prex + x_screen_size);
                Console.Write("  ");
            }
            prey = posy;
            prex = posx;
            Console.SetCursorPosition((int)posY + y_screen_size, (int)posX + x_screen_size);
            Console.Write("<>");
        }
        public void getCom(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    vecx--;
                    break;
                case ConsoleKey.S:
                    vecx++;
                    break;
                case ConsoleKey.A:
                    vecy--;
                    break;
                case ConsoleKey.D:
                    vecy++;
                    break;
                case ConsoleKey.UpArrow:
                    vecx--;
                    break;
                case ConsoleKey.DownArrow:
                    vecx++;
                    break;
                case ConsoleKey.LeftArrow:
                    vecy--;
                    break;
                case ConsoleKey.RightArrow:
                    vecy++;
                    break;
                default:
                    break;
            }
        }
        public new void reCreate()
        {
            if ((Math.Abs(posX) < 5 && Math.Abs(posY) < 5) || (posx <= x_screen_size * -1 || posx >= x_screen_size || posy <= y_screen_size * -1 || posy >= y_screen_size))
            {
                destroyed = true;
            }
        }
        public void checkCollisin(body obj)
        {
            if (posx > obj.posX - 1 && posx < obj.posX + 1 && posy > obj.posY - 1 && posy < obj.posY + 1)
            {
                destroyed = true;
            }
        }
        public ship() : base()
        {
            posx = 0;
            posy = -y_screen_size/3*2;
            vecx = 0;
            vecy = 0;
            mass = 3;
        }
    }
    class StarSystem
    {
        static ConsoleKey key = ConsoleKey.N;
        bool scorecond = false;
        star Star;
        ship Ship;
        List<body> ast;
        Random rnd = new Random();
        public uint score { set; get; } = 0;
        public double SpaceConst { set; get; } = 0.1;
        public double SpeedConst { set; get; } = 0.1;
        public bool isover { set;  get; } = false;
        public void move(double timing)
        {
            for (int i = 0; i < 1/SpeedConst; i++)
            {
                changedir();
                foreach (var item in ast)
                {
                    item.move(SpeedConst);
                    item.reCreate();
                    Ship.checkCollisin(item);
                }
                Ship.move(SpeedConst);
                Ship.reCreate();
                isovercheck();
                if (isover)
                    return;
                updatescore();
                wrt();
                show();
                System.Threading.Thread.Sleep((int)timing);
            }
            Ship.getCom(key);
            key = ConsoleKey.N;
        }
        public void getCom()
        {
            Task.Factory.StartNew(() => {
                while (key != ConsoleKey.Escape && isover==false)
                {
                    key = Console.ReadKey().Key;
                }isover = true;
            });
        }
        private void updatescore()
        {
            if (Ship.posX * Ship.prex < 0 && Ship.posY>0)
            {
                if (scorecond)
                {
                    score++;
                }
                scorecond = false;
            }
            else if(Ship.posX * Ship.prex < 0 && Ship.posY < 0)
            {
                scorecond = true;
            }
        }
        private void changedir()
        {
            Ship.cnahgeDir(Star, SpaceConst);
            foreach (var item in ast)
            {
                item.cnahgeDir(Star, SpaceConst);
                item.cnahgeDir(Ship, SpaceConst);
                Ship.cnahgeDir(item, SpaceConst);
                foreach (var _item in ast)
                {
                    if (item != _item)
                        item.cnahgeDir(_item, SpaceConst);
                }
            }
        }
        private void isovercheck()
        {
            if (Ship.destroyed==true)
            {
                isover = true;
            }
        }
        private void wrt()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write("score: "+score);
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public void show()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Star.show();
            Ship.show();
            foreach (var item in ast)
                item.show();
            Console.ForegroundColor=ConsoleColor.Black;
        }
        public StarSystem()
        {
            Star = new star(0, 0); 
            Ship = new ship();
            ast = new List<body>();
            for (int i = 0; i < 7; i++)
            {
                ast.Add(new body());
            }

        }
    }
    class Program
    {
        static int x_size=45;
        static int y_size=45;
        static int bestscore=0;
        static void showmenu(int mode, string[] arr)
        {
            Console.Clear();
            write(x_size, y_size -3, "best score: "+bestscore, 0);
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == mode)
                {
                    write(x_size, y_size + i*3, ">"+arr[i]+"<", 0);
                }
                else
                {
                    write(x_size, y_size + i*3, arr[i], 0);
                }
            }
        }
        static void write(int x, int y, string text,int timing)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x-(text.Length / 2), y);
            foreach (var item in text)
            {
                Console.Write(item);
                System.Threading.Thread.Sleep(timing);
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void gameover(uint score)
        {
            System.Threading.Thread.Sleep(1500);
            Console.Clear();
            
            write(x_size, y_size, "game over",100);
            write(x_size, y_size+3, $"with score {score}",100);
            write(x_size, 2 * y_size - 3, "to continue press any button",70);

            Console.ReadKey();
        }
        static uint startgame()
        {
            const double timing = 100;
            uint temp = 0;
            StarSystem Sun = new StarSystem();
            Sun.SpaceConst = 0.01;
            Sun.SpeedConst = 0.5;
            body.x_screen_size = x_size;
            body.y_screen_size = y_size;
            Sun.getCom();
            while (!Sun.isover)
            {
                Sun.move(timing);
                if (temp % 10 == 0)
                {
                    Console.Clear();
                    temp = 0;
                }
                temp++;
            }
            gameover(Sun.score);
            return Sun.score;
        }
        static int mainmenu()
        {
            ConsoleKey key = ConsoleKey.N;
            int mode = 0;
            int size = 3;
            string[] arr = new string[size];
            arr[0] = "start game";
            arr[2] = "exit";
            arr[1] = "reset score";
            while (key != ConsoleKey.Enter && key != ConsoleKey.Spacebar)
            {
                showmenu(mode, arr);
                key = Console.ReadKey().Key;
                if (key==ConsoleKey.UpArrow || key==ConsoleKey.W)
                {
                    if (mode == 0) { mode = size - 1; }
                    else { mode--; }
                }
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    if (mode == size-1) { mode = 0; }
                    else { mode++; }
                }
            }
            return mode;
        }
        static void Main(string[] args)
        {
            Console.Title = "Space game";
            while (true)
            {
                switch (mainmenu())
                {
                    case 0:
                        bestscore = (int)Math.Max(startgame(), bestscore);
                        break;
                    case 1:
                        bestscore = 0;
                        break;
                    case 2:
                        return;
                }
                
            }
        }
    }
}
