namespace Numbers
{
    internal class Program
    {
        //challenge 1: lưu trữ 1 danh sách số nguyên cho trước
        //             sau đó in ra: các số dương
        //                           các số âm
        //                           toàn bộ
        //                           các số chia hết cho 5
        //                           ...
        //static void Main(string[] args)
        //{
        //    //in tất cả
        //    Console.WriteLine("print all");
        //    PrintListOnDemand(n => true);   //đưa ai tao cũng true hết

        //    //in số dương
        //    Console.WriteLine("print > 0");
        //    PrintListOnDemand(n => n > 0);   //đưa ai tao cũng true hết
        //}

        static void Main(string[] args)
        {
            PlayWithBuiltInOnDemandMethodV2();
        }

        static void PlayWithBuiltInOnDemandMethodV2()
        {
            List<int> arr = new List<int>() { -10, -100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };

            Console.WriteLine("> 0 using query ");
            var result = from x in arr //với mọi giá trị x thuộc tập arr
                         where x > 0    //xem x nào > o
                         select x;      //thì lấy x đó mà trả về để dùng tiếp
            //câu querry giống như sql dùng để truy vấn data trong ram - LINQ theo style syntax
            //runtime chạy thì convert về lambda như đã viết, .where(x => x > 0) ============> METHOD SYNTAX
            foreach (var x in result)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("Divisable by 2");
             result = from x in arr //với mọi giá trị x thuộc tập arr
                         where x % 2 == 0    //xem x nào chẵn hay không 
                         select x;
            foreach ( var x in result )
            {
                Console.WriteLine(x);
            }
        }

        static void PlayWithBuiltInOnDemandMethod()
        {
            List<int> arr = new List<int>() { -10, -100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            //tui muốn in tất cả dãy số
            //arr có sẵn 1 loạt các hàm để xử lí data mà đó cất trữ thay vì ta phải tự làm. Hàm style on demand cũng cần Action, Action<>, Func<>, Predicate tùy loại hàm: in, tính tổng, count, trung bình,...

            //1. in toàn bộ 
            Console.WriteLine("all");
            arr.ForEach(x => Console.WriteLine(x));

            //2. in số âm 
            Console.WriteLine("< 0");
            arr.ForEach(x => { if (x < 0) Console.WriteLine(x); });

            //3. số dương
            //hàm demand trả về list để ta dùng tiếp thayu vì chỉ in ra thôi!!!
            Console.WriteLine("< 0 list =========");
            List<int> result = arr.Where(x => x > 0).ToList();
            result.ForEach(x => Console.WriteLine(x));
        }

        static void PrintListOnDemand(Predicate<int> f)
        {
            List<int> arr = new List<int>() { -10, -100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            foreach (int i in arr)
            {
                if (f(i))
                {
                    //2 cách: giao khoán hết ra ngoài in (Action<int>)
                    //        chủ động in, nhưng nhờ bên ngoài check giá trị Predicate<int>
                    Console.WriteLine(i);
                }
            }
        }
    }
}
