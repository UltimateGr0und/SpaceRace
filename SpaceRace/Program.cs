using System;
using System.Collections.Generic;

namespace SpaceRace
{
    class body
    {
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
        public int x_screen_size { set; get; } = 15;
        public int y_screen_size { set; get; } = 60;
        public void show()
        {
            if (posx < 0 || posx > x_screen_size || posy < 0 || posy > y_screen_size)
                return;
            Console.SetCursorPosition((int)posY + y_screen_size, (int)posX + x_screen_size);
            Console.WriteLine('o');
        }
        public void move(double timing)
        {
            posx += (vecx/timing);
            posy += (vecy/timing);
        }
        public void cnahgeDir(body obj, double SpaceConst)
        {
            vecx -=  obj.mass * SpaceConst / (obj.posX - posx);
            vecY -= obj.mass * SpaceConst / (obj.posY - posy);
        }
        public body(double x, double y)
        {
            posx = x;
            posy = y;
            vecx = rnd.Next(-10, 10);
            vecy = rnd.Next(-10, 10);
            mass = 1;
        }
    }

    class star : body
    {
        public new void show()
        {
            Console.SetCursorPosition((int)posY + 60, (int)posX + 15);
            Console.WriteLine('@');
        }
        public star(double pos_x, double pos_y) : base(pos_x, pos_y)
        {
            Mass = 1000;
        }
    }
    class ship { }
    class StarSystem
    {
        star Star;
        List<body> ast;
        Random rnd = new Random();

        public void move(double SpaceConst,double timing)
        {
            for (int i = 0; i < timing; i++)
            {
                foreach (var item in ast)
                {
                    item.move(timing);
                    item.show();
                }
                Star.show();

                System.Threading.Thread.Sleep(1000 / (int)timing);
                //Console.Clear();
            }

            foreach (var item in ast)
            {
                item.cnahgeDir(Star, SpaceConst);
                foreach (var _item in ast)
                {
                    item.cnahgeDir(_item, SpaceConst);
                }
            }

        }

        public StarSystem()
        {
            Star = new star(0, 0); 
            ast = new List<body>();
            for (int i = 0; i < 5; i++)
            {
                ast.Add(new body(rnd.Next(-10, 10), rnd.Next(-10, 10)));
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            const double SpaceConst = 0.01;
            const double timing = 100;
            StarSystem Star = new StarSystem();
            while (true)
            {
                Star.move(SpaceConst, timing);
            }
            
        }
    }
}
